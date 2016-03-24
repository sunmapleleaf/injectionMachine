using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using WindowsFormsApplication1.app;

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
        //机器状态
        private Dictionary<string, MachineStatusRecord> machineStatusRecord = new Dictionary<string, MachineStatusRecord>();
        private class MachineStatusRecord
        {
            public string machineStatus;
            public string lastMachineStatus;
        };
        public IMCapture()
        {
            InitializeComponent();
           

        }

         ~IMCapture()
        {

        }
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
            if (sampleInterval < 5000)
                sampleInterval = 5000;
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
         //   HXOpc = new OpcHelper();
            
           // HXOpc.getValueFormHX();

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

          //  string machineData1 = "";
          //  (new InjectionMachine()).convertedFromHX((JObject)(HXOpc.itemsJO["hongxun002"]), ref machineData1);
            if (Interlocked.Exchange(ref timeThreadRunFlag, 1) == 0)
            {
                DateTime cycleStartTime = System.DateTime.Now;
                DateTime cycleEndTime;
                TimeSpan ts;
                lock (locker)
                {
                    ConnectionOption.checkConnections(listConn);
                    foreach (ConnectionOption item in listConn)
                    {
                        string machineData = "";

                        if (!lastQualityDataCountDic.ContainsKey(item.machineID))
                        {
                            lastQualityDataCountDic.Add(item.machineID, -1);
                        }
                        if (!machineStatusRecord.ContainsKey(item.machineID))
                        {


                            //找出mgdb中编号为item.machineID的状态数据
                            IMongoQuery iq = Query.Matches("machineID", item.machineID);
                            //IEnumerable<MachineStatus> ieMs = MachineStatus.Search(iq);
                            IMongoUpdate iu = MongoDB.Driver.Builders.Update.Set("machineID", item.machineID);
                            MachineStatus machineStatusTop = MachineStatus.FindAndModify(iq ,iu, "startTime", true);
                            if (machineStatusTop != null)
                            {
                                string machineLastStatus = machineStatusTop.lastStatus;
                                //记录时间
                                if (machineLastStatus == "softwareOff")
                                {
                                    iu = MongoDB.Driver.Builders.Update.Set("endTime", System.DateTime.Now.ToString());
                                    MachineStatus.FindAndModify(iq, iu, "startTime", true);
                                }
                                else
                                {
                                    iu = MongoDB.Driver.Builders.Update.Set("endTime", machineStatusTop.startTime);
                                    MachineStatus.FindAndRemove(iq, "startTime", true);
                                    MachineStatus ms = new MachineStatus(item.machineID, "softwareOff", machineStatusTop.startTime, System.DateTime.Now.ToString());
                                    ms.Insert();

                                }
                            }
                            else {
                                MachineStatus.Remove(iq);
                            }
                           
                            machineStatusRecord.Add(item.machineID, new MachineStatusRecord() { machineStatus = "softwareOn",lastMachineStatus="softwareOn"});
                        }
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
                            machineData = "";
                            if (item.controllerType == "gefranVedo")
                            {
                                string qualityData = "";
                                string qualityDataCount = jo["overView"]["TOTPR"][1].ToString();
                                (new InjectionMachine()).convertedFromGefranVedo(jo, ref machineData);
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
                                (new InjectionMachine()).convertedFromGefranPerforma(jo, ref machineData);
                                //InjectionMachine.getQualityDataFromGefranPerforma(jo, ref qualityData);
                                if (Int32.Parse(qualityDataCount) != lastQualityDataCountDic[item.machineID])
                                {
                                    lastQualityDataCountDic[item.machineID] = Int32.Parse(qualityDataCount);
                                    InjectionMachine.getQualityDataFromGefranPerforma(jo, ref qualityData);
                                    (new IMDataBase()).writeQualityDataToDB(IMDataBase.connStr, item.machineID, qualityDataCount, qualityData);
                                }

                            }
                            (new IMDataBase()).writeDataBase(IMDataBase.connStr, item.machineID, machineData);
                            SetValue(item.machineID + "数据写入成功");
                        }
                        //如果是KEBA
                        else if (item.controllerType.IndexOf("keba") != -1 && item.connStatus == "1")
                        {
                            string kebaData = "";
                            machineData = "";
                            try
                            {
                                StreamReader sr = new StreamReader(@"c:\data\" + item.controllerType + @"\" + item.machineID + ".txt");
                                if (sr != null)
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
                                        
                                        macWithKeba.convertedFromKeba(jo, ref machineData);
                                        //kebaData = JsonConvert.SerializeObject(new InjectionMachineWithGefran());
                                        string qualityData = "";
                                        string qualityDataCount = jo["system.sv_iShotCounterAct"][1].ToString();
                                        if (lastQualityDataCountDic[item.machineID] == -1)
                                            lastQualityDataCountDic[item.machineID] = Int32.Parse(qualityDataCount);
                                        else if (Int32.Parse(qualityDataCount) != lastQualityDataCountDic[item.machineID])
                                        {
                                            lastQualityDataCountDic[item.machineID] = Int32.Parse(qualityDataCount);
                                            InjectionMachine.getQualityDataFromKeba(jo, ref qualityData);
                                            (new IMDataBase()).writeQualityDataToDB(IMDataBase.connStr, item.machineID, qualityDataCount, qualityData);
                                        }

                                        (new IMDataBase()).writeDataBase(IMDataBase.connStr, item.machineID, machineData);
                                        SetValue(item.machineID + "数据写入成功");

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
                        //如果是宏讯
                        else if (item.controllerType.IndexOf("hongxun") != -1 && item.connStatus == "1")
                        {
                            //machineData = "";
                            //(new InjectionMachine()).convertedFromHX((JObject)(HXOpc.itemsJO[item.machineID]), ref machineData);
                            //string qualityData = "";
                            //string qualityDataCount = HXOpc.itemsJO[item.machineID]["Basic"]["tmShotCount"].ToString();
                            //if (lastQualityDataCountDic[item.machineID] == -1)
                            //    lastQualityDataCountDic[item.machineID] = Int32.Parse(qualityDataCount);
                            //else if (Int32.Parse(qualityDataCount) != lastQualityDataCountDic[item.machineID])
                            //{
                            //    lastQualityDataCountDic[item.machineID] = Int32.Parse(qualityDataCount);
                            //    InjectionMachine.getQualityDataFromHX((JObject)(HXOpc.itemsJO[item.machineID]), ref qualityData);
                            //    (new IMDataBase()).writeQualityDataToDB(IMDataBase.connStr, item.machineID, qualityDataCount, qualityData);
                            //}

                            //(new IMDataBase()).writeDataBase(IMDataBase.connStr, item.machineID, machineData);
                            //SetValue(item.machineID + "数据写入成功");

                        }
                        try
                        {
                            //记录机器运行状态
                            //如果是第一次
                            if (machineStatusRecord[item.machineID].machineStatus == "softwareOn")
                            {
                                machineStatusRecord[item.machineID].machineStatus = getMachineStatus(machineData, item);
                                machineStatusRecord[item.machineID].lastMachineStatus = machineStatusRecord[item.machineID].machineStatus;
                                MachineStatus ms = new MachineStatus(item.machineID, machineStatusRecord[item.machineID].machineStatus, System.DateTime.Now.ToString(), "");
                                ms.Insert();

                            }
                            //不是第一次
                            else
                            {
                                
                                machineStatusRecord[item.machineID].machineStatus = getMachineStatus(machineData, item);
                                if (machineStatusRecord[item.machineID].lastMachineStatus != machineStatusRecord[item.machineID].machineStatus)
                                {
                                    IMongoQuery iq = Query.Matches("machineID", item.machineID);
                                    //IMongoUpdate iu = MongoDB.Driver.Builders.Update.Set("machineID", item.machineID);
                                    //MachineStatus machineStatusTop = MachineStatus.FindAndModify(iq, iu, "startTime", true); 
                                    //IEnumerable<MachineStatus> ieMs = MachineStatus.Search(iq);
                                    //MachineStatus machineStatusTop = ieMs.Count() > 0 ? ieMs.ToList()[0] : null;
                                    //if (machineStatusTop != null)
                                    
                                    {
                                        IMongoUpdate iu = MongoDB.Driver.Builders.Update.Set("endTime", System.DateTime.Now.ToString());
                                        MachineStatus.FindAndModify(iq,iu,"startTime",true);
                                        MachineStatus ms = new MachineStatus(item.machineID, machineStatusRecord[item.machineID].machineStatus, System.DateTime.Now.ToString(), "");
                                        ms.Insert();
                                        machineStatusRecord[item.machineID].lastMachineStatus = machineStatusRecord[item.machineID].machineStatus;


                                    }


                                }


                            }

                        }
                        catch { 
                        
                        
                        }




                    }//foreach end
                }
                Console.WriteLine("fast");

                cycleEndTime = System.DateTime.Now;
                ts = cycleEndTime.Subtract(cycleStartTime);
                Console.WriteLine("循环耗时{0}ms.", ts.TotalMilliseconds);

                Interlocked.Exchange(ref timeThreadRunFlag, 0);

            }

            

        }
        /// <summary>
        /// 窗体控件的线程安全调用
        /// </summary>
        /// <param name=""></param>   
        /// <returns></returns>
        private string getMachineStatus(string machineData, ConnectionOption item)
        {
            string status = "";
            JObject jo = (JObject)JsonConvert.DeserializeObject(machineData);
            if(item.connStatus != "1")
            {
                status = "machineOff";
            }
            else if (jo!=null&&jo["overView"]!=null&&jo["overView"]["alarm"]!=null&&jo["overView"]["alarm"][1].ToString()!=""&&Int32.Parse(jo["overView"]["alarm"][1].ToString()) > 0)
                status = "alarm";
            else
                status = "working";
            return status;
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
            if (this.listBox1.InvokeRequired)
            {
                SetValueCallback d = new SetValueCallback(SetValue);
                this.Invoke(d, new object[] { str });
            }
            else
            {
                this.listBox1.Items.Add(str);
                if(listBox1.Items.Count>10)
                     listBox1.Items.RemoveAt(0);
                listBox1.SetSelected(listBox1.Items.Count - 1, true);
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }





        private void button1_Click(object sender, EventArgs e)
        {
            
            
            // 添加一条数据  
            MachineStatus ms = new MachineStatus("keba001","softwareOff","2016-1-05",System.DateTime.Now.ToString());

           
            // 删除一条数据  
            //IMongoQuery iq = new QueryDocument("name", "TestNameA");
            //Users.Remove(iq);  

            //修改一条数据  
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("machineID", "keba001");
            IMongoQuery iq = new QueryDocument(dic);
            IMongoUpdate iu = MongoDB.Driver.Builders.Update.Set("machineID", "keba001");
         //   MachineStatus.Update(iq, iu, UpdateFlags.Multi);  

            //获取数据列表  
            //IMongoQuery iq = new QueryDocument("name", "TestNameA");  
            // iq = Query.And(Query.GTE("age", 101), Query.Matches("name", "/^Test/"));//>40  
            iq = Query.And(Query.Matches("machineID", "/^keba/"), Query.EQ("runningFlag", true));
                                                                   
             MachineStatus bd = MachineStatus.Search(iq).ToList()[0];
            //排序，获取第一个
              string connectionString = "mongodb://112.124.23.181";
               string databaseName = "IMDB"; 
             MongoServer server = MongoServer.Create(connectionString);
             //获取databaseName对应的数据库，不存在则自动创建  
             MongoDatabase mongoDatabase = server.GetDatabase(databaseName);

             MongoCollection<BsonDocument> collection = mongoDatabase.GetCollection<BsonDocument>("machineStatus");

            
                var query = Query.And(Query.Matches("machineID", "/^keba/"),Query.GT("endTime",(System.DateTime.Now.AddMinutes(-2).ToString())));
                var sortBy = SortBy.Descending("endTime");
                var result = collection.FindAndModify(
                    query,
                    sortBy,
                    iu,
                    true,
                    true
                );
                var chosenJob = result.ModifiedDocument;



        }

        private void IMCapture_Load(object sender, EventArgs e)
        {

        }

        private void IMCapture_FormClosed(object sender, FormClosedEventArgs e)
        {
            getDataTimer.Enabled = false;
            slowTimer.Enabled = false;

            foreach (ConnectionOption item in listConn)
            {
                //IMongoQuery iq = Query.And(Query.Matches("machineID", item.machineID), Query.EQ("runningFlag", true));
                //IEnumerable<MachineStatus> ieMs = MachineStatus.Search(iq);
                //IMongoUpdate iu = MongoDB.Driver.Builders.Update.Set("endTime", System.DateTime.Now.ToString()).Set("runningFlag", false);
                //MachineStatus.Update(iq, iu, UpdateFlags.Multi);
                //MachineStatus ms = new MachineStatus(item.machineID, "softwareOff", System.DateTime.Now.ToString(), "");
                //ms.Insert();


            }
        }










    }
}

