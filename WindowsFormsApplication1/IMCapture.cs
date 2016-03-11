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
        //获得数据线程
        private System.Timers.Timer getDataTimer;
        //慢速线程（<获取数据线程）
        private System.Timers.Timer slowTimer;
        //宏讯OPC接口对象
        private OpcHelper HXOpc;
        //数据库中所有控制器配置信息
        static List<ConnectionOption> listConn = new List<ConnectionOption>();
        //原子操作
        private int timeThreadRunFlag = 0;
        //线程数据锁
        static object locker = new object();
        //字典对象,存放每台机器上一次质量管理数据编号,如果改变则存入数据库
        private Dictionary<string, int> lastQualityDataCountDic = new Dictionary<string, int>();
        public IMCapture()
        {
            InitializeComponent();
           

        }

        /// <summary>
        ///telnet方式获得gefran数据
        /// 
        /// </summary>
        /// <param name="name"></param>   
        /// <returns></returns>

        //private void getDatafromGefranByFile(String fileName, out String variableValue,  String variableName)
        //{
        //    const int varArrayLength = 10;
        //    if (fileName != "")
        //    {
        //        StreamReader file = new StreamReader(fileName);
        //        variableName = file.ReadToEnd();
        //        file.Close();
        //    }
  
        //    String[] varNameTmp = variableName.Split(';');
        //    List<String> varNameList = new List<string>();

        //    for (int i = 0, j = 0; i < varNameTmp.Count(); i++)
        //    {
        //        if (i % varArrayLength == 0)
        //        {
        //            varNameList.Add(varNameTmp[i]);
        //            j++;
        //        }
        //        else
        //            varNameList[j - 1] += ";" + varNameTmp[i];
        //    }

            
        //    Telnet p = new Telnet("192.168.8.211", 23, 50);
            
        //    if (p.Connect() == false)
        //    {
        //        // // Console.WriteLine("连接失败");
        //        MessageBox.Show("连接失败");
        //        variableValue = null;
        //        variableName = null;
        //        //   p.telnetClose();
        //        return;
        //    }

        //    //等待指定字符返回后才执行下一命令
      
        //    p.WaitFor("login:");
        //    p.Send("telnet");
        //    p.WaitFor("password:");
        //    p.Send("gefranseven");
        //    DateTime beforDT = System.DateTime.Now;
        //    p.WaitFor(">");
        //    DateTime afterDT = System.DateTime.Now;
        //    TimeSpan ts = afterDT.Subtract(beforDT);
        //    Console.WriteLine("getData11总共花费{0}ms.", ts.TotalMilliseconds);
        //    beforDT = System.DateTime.Now;

        //     variableValue = null;
        //    for (int i = 0; i < varNameList.Count(); i++)
        //    {
        //        p.Send(varNameList[i]);
        //        p.WaitFor(">");
        //        String[] varValueTmp = p.WorkingData.Split(new char[] { '\r' });

        //        for (int j = 0; j < varValueTmp.Count(); j++)
        //        {
        //            if (varValueTmp[j].Split('=').Count() > 2)
        //                variableValue += varValueTmp[j].Split('=')[2] + ";";
        //        }
        //       // variableValue = variableValue.Trim(';');
        //    }
        //    afterDT = System.DateTime.Now;
        //    ts = afterDT.Subtract(beforDT);
        //    Console.WriteLine("getData33总共花费{0}ms.", ts.TotalMilliseconds);
          
        //}

        /// <summary>
        ///开始采样,开启数据抓取线程和慢速线程.
        /// 
        /// </summary>
        /// <param name=""></param>   
        /// <returns></returns>
        private void sample_click(object sender, EventArgs e)
        {
            lock (locker)
            {
                IMDataBase.readAllConnections(ref listConn);
            }

            /*  开启数据抓取线程 */
            int sampleInterval = Int32.Parse(textBox1.Text);
           // if (sampleInterval < 3000)
                //sampleInterval = 3000;
            getDataTimer = new System.Timers.Timer(sampleInterval);
            getDataTimer.Elapsed += new System.Timers.ElapsedEventHandler(theout);            
            getDataTimer.AutoReset = true;
            getDataTimer.Enabled = true;

            /*  开启慢速线程 */
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

        /// <summary>
        /// 慢速线程执行函数.
        /// 1.获取GEFRAN数据文件
        /// </summary>
        /// <param name=""></param>   
        /// <returns></returns>
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

        /// <summary>
        /// 数据抓取线程
        /// 1.根据控制器类型执行不同的数据获取代码
        /// 2.将代码归一化到InjectionMachine类中
        /// 3.将InjectionMachine类JSON化并存入数据库
        /// </summary>
        /// <param name=""></param>   
        /// <returns></returns>

        private void theout(object sender,System.Timers.ElapsedEventArgs e)
        {
            string machineData1 = "";
            (new InjectionMachine()).convertedFromHX((JObject)(HXOpc.itemsJO["hongxun001"]), ref machineData1);

          //  SetValue(HXOpc.itemsValue[27][1]);
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
                        //如果是GERAN
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
                        //如果是KEBA
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
                        else if (item.controllerType.IndexOf("hongxun") != -1 && item.connStatus == "1")
                        {
                            string machineData = "";
                            (new InjectionMachine()).convertedFromHX((JObject)(HXOpc.itemsJO[item.machineID]), ref machineData);
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
        /// <summary>
        /// 窗体控件的线程安全调用
        /// </summary>
        /// <param name=""></param>   
        /// <returns></returns>
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
        private void stopSample_Click(object sender, EventArgs e)
        {
            getDataTimer.Enabled = false;
            slowTimer.Enabled = false;
        }

        static public void readAllConn()
        {
            lock (locker)
            {
                IMDataBase.readAllConnections(ref listConn);
            }

        }
        private void option_Click(object sender, EventArgs e)
        {
            Option connectionOption = new Option();
            connectionOption.Show();
        }

        private void viewHXData_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }

        private void IMCapture_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(HXOpc!=null)
                 HXOpc.OpcClosing();

        }











    }
}

