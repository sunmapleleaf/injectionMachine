using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;
using System.Data.SqlClient;
using System.IO;
using ICSharpCode.SharpZipLib.GZip; //解压缩zip



using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.NetworkInformation;
using System.Xml;
namespace WindowsFormsApplication1
{

    
    public partial class IMCapture : Form
    {
        public IMCapture()
        {
            InitializeComponent();
           

        }
        private int ID;
        private bool getDataFlag = false;
        private int dataNum = 0;

      //  static ConcurrentBag<ConnectionOption> listConn = new ConcurrentBag<ConnectionOption>;
        static  List<ConnectionOption> listConn = new List<ConnectionOption>();
        private void button1_Click(object sender, EventArgs e)
        {

            ConnectionOption.getIP();
          
        }
  
        private void getDatafromGefranByFile(String fileName, out String variableValue,  String variableName)
        {
            const int varArrayLength = 10;
            if (fileName != "")
            {
                StreamReader file = new StreamReader(fileName);
                variableName = file.ReadToEnd();
                file.Close();
            }
  
            String[] varNameTmp = variableName.Split(';');
            List<String> varNameList = new List<string>();

            for (int i = 0, j = 0; i < varNameTmp.Count(); i++)
            {
                if (i % varArrayLength == 0)
                {
                    varNameList.Add(varNameTmp[i]);
                    j++;
                }
                else
                    varNameList[j - 1] += ";" + varNameTmp[i];
            }

            
            Telnet p = new Telnet("192.168.8.211", 23, 50);
            
            if (p.Connect() == false)
            {
                // // Console.WriteLine("连接失败");
                MessageBox.Show("连接失败");
                variableValue = null;
                variableName = null;
                //   p.telnetClose();
                return;
            }

            //等待指定字符返回后才执行下一命令
      
            p.WaitFor("login:");
            p.Send("telnet");
            p.WaitFor("password:");
            p.Send("gefranseven");
            DateTime beforDT = System.DateTime.Now;
            p.WaitFor(">");
            DateTime afterDT = System.DateTime.Now;
            TimeSpan ts = afterDT.Subtract(beforDT);
            Console.WriteLine("getData11总共花费{0}ms.", ts.TotalMilliseconds);
            beforDT = System.DateTime.Now;

             variableValue = null;
            for (int i = 0; i < varNameList.Count(); i++)
            {
                p.Send(varNameList[i]);
                p.WaitFor(">");
                String[] varValueTmp = p.WorkingData.Split(new char[] { '\r' });

                for (int j = 0; j < varValueTmp.Count(); j++)
                {
                    if (varValueTmp[j].Split('=').Count() > 2)
                        variableValue += varValueTmp[j].Split('=')[2] + ";";
                }
               // variableValue = variableValue.Trim(';');
            }
            afterDT = System.DateTime.Now;
            ts = afterDT.Subtract(beforDT);
            Console.WriteLine("getData33总共花费{0}ms.", ts.TotalMilliseconds);
          
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void writeToSQL_Click(object sender, EventArgs e)
        {
            
        }
        private System.Timers.Timer getDataTimer;
        private System.Timers.Timer slowTimer;
        private OpcHelper HXOpc;
        private void button1_Click_1(object sender, EventArgs e)
        {

            lock (locker)
            {
                IMDataBase.readAllConnections(ref listConn);
            }


            int sampleInterval = Int32.Parse(textBox1.Text);
           // if (sampleInterval < 3000)
                //sampleInterval = 3000;
            getDataTimer = new System.Timers.Timer(sampleInterval);
            getDataTimer.Elapsed += new System.Timers.ElapsedEventHandler(theout);            
            getDataTimer.AutoReset = true;
            getDataTimer.Enabled = true;

            int slowThreadInterval = 5000;
            slowTimer = new System.Timers.Timer(slowThreadInterval);
            slowTimer.Elapsed += new System.Timers.ElapsedEventHandler(slowThreadProcess);
            slowTimer.AutoReset = true;
            slowTimer.Enabled = true;
            Console.WriteLine("slow");

            /*  启动OPC客户端读宏讯数据 */
            HXOpc = new OpcHelper();
            
            HXOpc.getValueFormHX();

        }
        private void slowThreadProcess(object sender, System.Timers.ElapsedEventArgs e)
        {
            //lock (locker)
            {
                ConnectionOption.checkConnections(listConn);
                foreach (ConnectionOption item in listConn)
                {
                    (new IMDataBase()).writeConnToDataBase(item);
                    if (item.controllerType.IndexOf("gefran") != -1 && item.connStatus == "1")
                    {
                        new DataCapture().getFilesFromGefran(item.loginName, item.loginPassword, @"ftp://" + item.IP + @"/gefran/recipes/", Application.StartupPath + @"\data\" + item.controllerType, item.machineID + ".zip");
                    }
                }
            }
        }
        delegate void SetValueCallback(string value);

        private void SetValue(string str)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.tbText.InvokeRequired)
            {
                SetValueCallback d = new SetValueCallback(SetValue);
                this.Invoke(d, new object[] { str });
            }
            else
            {
                this.tbText.Text = str;
            }


        }
        private int timeThreadRunFlag = 0;
        //private int lastQualityDataCount = -1;
        private Dictionary<string, int> lastQualityDataCountDic = new Dictionary<string,int>();
        private void theout(object sender,System.Timers.ElapsedEventArgs e)
        {

            SetValue(HXOpc.itemsValue[27][1]);
            if(Interlocked.Exchange(ref timeThreadRunFlag,1)==0)
            {
                DateTime cycleStartTime = System.DateTime.Now;
                DateTime cycleEndTime;
                TimeSpan ts;
                lock (locker)
                {
                    ConnectionOption.checkConnections(listConn);
                    foreach (ConnectionOption item in listConn)                    
                    {
                        if (!lastQualityDataCountDic.ContainsKey(item.machineID))
                            lastQualityDataCountDic.Add(item.machineID, -1);
                        DateTime connStartTime = System.DateTime.Now;
                        (new IMDataBase()).writeConnToDataBase(item);
                        if (item.controllerType.IndexOf("gefran") != -1 && item.connStatus == "1")
                        {

                           // new DataCapture().getFilesFromGefran(item.loginName, item.loginPassword, @"ftp://" + item.IP + @"/gefran/recipes/", Application.StartupPath + @"\data\" + item.controllerType, item.machineID + ".zip");


                            JObject jo = ObjectAnalysis.analyseGefranFile(Application.StartupPath + @"\data\" + item.controllerType, item.machineID + ".txt", 0, item.controllerType);
                            //DateTime connEndTime = System.DateTime.Now;
                            //ts = connEndTime.Subtract(connStartTime);
                            //jo["sampleTime"] = ts.TotalMilliseconds.ToString().Remove(ts.TotalMilliseconds.ToString().IndexOf('.') + 2);
                            //Console.WriteLine("{0}耗时{1}ms.", item.machineID, ts.TotalMilliseconds);
                            string data="";
                            if (item.controllerType == "gefranVedo")
                            {
                                string qualityData = "";
                                string qualityDataCount = jo["overView"]["TOTPR"][1].ToString();
                                (new InjectionMachine()).convertedFromGefranVedo(jo, ref data);
                                InjectionMachine.getQualityDataFromGefranVedo(jo, ref qualityData);
                                if (Int32.Parse(qualityDataCount) != lastQualityDataCountDic[item.machineID])
                                {
                                    lastQualityDataCountDic[item.machineID] = Int32.Parse(qualityDataCount);
                                    InjectionMachine.getQualityDataFromGefranVedo(jo, ref qualityData);
                                    (new IMDataBase()).writeQualityDataToDB(IMDataBase.connStr, item.machineID, qualityDataCount, qualityData);
                                }
                            }
                            else
                            {
                                string qualityData = "";
                                string qualityDataCount = jo["overView"]["CURRENT_TOTAL_PRODUCTION"][1].ToString();
                                (new InjectionMachine()).convertedFromGefranPerforma(jo, ref data);
                                //InjectionMachine.getQualityDataFromGefranPerforma(jo, ref qualityData);
                                if (Int32.Parse(qualityDataCount) != lastQualityDataCountDic[item.machineID])
                                {
                                    lastQualityDataCountDic[item.machineID] = Int32.Parse(qualityDataCount);
                                    InjectionMachine.getQualityDataFromGefranPerforma(jo, ref qualityData);
                                    (new IMDataBase()).writeQualityDataToDB(IMDataBase.connStr, item.machineID, qualityDataCount, qualityData);
                                }
                           
                            }
                            (new IMDataBase()).writeDataBase(IMDataBase.connStr, item.machineID, data);
                        }
                        else if(item.controllerType.IndexOf("keba")!=-1&&item.connStatus == "1")
                        {
                            string kebaData = "";
                            try {
                                 StreamReader sr = new StreamReader(@"c:\data\" + item.controllerType + @"\" + item.machineID + ".txt");
                                 if (sr!=null)
                                 {
                                     kebaData = sr.ReadToEnd();
                                     sr.Close();
                                  //   kebaData = kebaData.Replace("Mold1", "clamp");
                                  //   kebaData = kebaData.Replace("Ejector1", "ejector");
                                  //   kebaData = kebaData.Replace("Injection1", "injection");
                                     //kebaData.Replace("Carrage", "carrage");
                                   //  kebaData = kebaData.Replace("Core1", "core");
                                     kebaData = kebaData.Replace("Unknown", "\"Unknown\"");                                 
                                     kebaData = kebaData.Replace("---", "\"---\"");
                                     Console.Write(item.machineID);
                                     var joTmp = JsonConvert.DeserializeObject(kebaData);
                                     if (joTmp != null)
                                     {
                                         JObject jo = (JObject)joTmp;
                                    // DateTime connEndTime = System.DateTime.Now;
                                    // ts = connEndTime.Subtract(connStartTime);
                                     jo.Add("sampleTime", "");
                                     jo.Add("timestamp", System.DateTime.Now.ToString());
                                     jo.Add("machineID", item.machineID);
                                    // jo["sampleTime"] = ts.TotalMilliseconds.ToString().Remove(ts.TotalMilliseconds.ToString().IndexOf('.') + 2);
                                     //jo["timestamp"] = System.DateTime.Now.ToString();
                                     InjectionMachine macWithKeba = new InjectionMachine();
                                    string machineData = "";
                                    macWithKeba.convertedFromKeba(jo,ref machineData);
                                     //kebaData = JsonConvert.SerializeObject(new InjectionMachineWithGefran());
                                    string qualityData="";
                                    string qualityDataCount = jo["system.sv_iShotCounterAct"][1].ToString();
                                    if (lastQualityDataCountDic[item.machineID] ==-1)
                                        lastQualityDataCountDic[item.machineID] = Int32.Parse(qualityDataCount);
                                    else if (Int32.Parse(qualityDataCount) != lastQualityDataCountDic[item.machineID])
                                    {
                                        lastQualityDataCountDic[item.machineID] = Int32.Parse(qualityDataCount);
                                        InjectionMachine.getQualityDataFromKeba(jo, ref qualityData);
                                        (new IMDataBase()).writeQualityDataToDB(IMDataBase.connStr,item.machineID, qualityDataCount, qualityData);
                                    }

                                    (new IMDataBase()).writeDataBase(IMDataBase.connStr, item.machineID, machineData);
                                     }
                                    
                                 }

                               


                            }
                            catch (ArgumentException k)
                            {
                                Console.WriteLine("me");
                                Console.WriteLine(k);
                            }
                            catch (Exception k)
                            {
                                Console.WriteLine("others");
                            }
                           // DateTime connEndTime = System.DateTime.Now;
                           // ts = connEndTime.Subtract(connStartTime);
                            //Console.WriteLine(ts);
                        }

                    }
                }
                Console.WriteLine("fast");

                cycleEndTime = System.DateTime.Now;
                ts = cycleEndTime.Subtract(cycleStartTime);
                Console.WriteLine("循环耗时{0}ms.", ts.TotalMilliseconds);

                Interlocked.Exchange(ref timeThreadRunFlag,0);

            }

            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            getDataTimer.Enabled=false;
            //slowTimer.Enabled = false;
        }
        static object locker = new object();
        static public void readAllConn()
        {
            lock (locker)
            {
                IMDataBase.readAllConnections(ref listConn);
            }

        }


        private void button1_Click_2(object sender, EventArgs e)
        {
            Form2 connectionOption = new Form2();
            connectionOption.Show();
        }


        private void IMCapture_Load(object sender, EventArgs e)
        {

        }

        private void viewHXData_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }
    }
}

