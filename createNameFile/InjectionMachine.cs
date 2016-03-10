using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Threading;
using System.Net.NetworkInformation;
using System.Data.SqlClient;
using System.Data;
namespace createNameFile
{
    
    class InjectionMachine
    {
        public string machineID;
        public string sampleTime;
        public string timestamp;
        public object clamp;
        public object injection;
        public object ejector;
        public object core;
        public object charge;
        public object carriage;
     
        public virtual void getDataFromController(ConnectionOption p)
        {
        }
    }
    //class testClass
    //{
    //    public string[] aPO_INJE = new string[2];
    //    public string[,] DeadBand =new string[10,2]; 
    //}
    class InjectionMachineWithGefran : InjectionMachine
    {
        public InjectionMachineWithGefran()
        {

            clamp = new ClampDataFromGefran();
            injection = new InjectionDataFromGefran();
            ejector = new EjectorDataFromGefran();
            core = new CoreDataFromGefran();
            charge = new ChargeDataFromGefran();
            carriage = new CarriageDataFromGefran();
        }
        public override void getDataFromController(ConnectionOption p)
        {

            string variableValue = "", variableName = "";

            DateTime beforDT = System.DateTime.Now;
            //json
            JsonSerializer jsS = new JsonSerializer();
            StringWriter sw = new StringWriter();
            jsS.Serialize(new JsonTextWriter(sw), this);

            JObject jo = (JObject)JsonConvert.DeserializeObject(sw.ToString());

       //     Console.WriteLine(jsS.DateFormatString);
            foreach (var item in jo)
            {
                if (item.Value.ToString().IndexOf(":") == -1) ;
                //  jo[item.Key] = "1";
                else
                {
                    foreach (var itemValue in (JObject)item.Value)
                    {
                        variableName += itemValue.Key + ";";
                        //  jo[item.Key][itemValue.Key] = variableValue;
                    }
                }
            }
            

            const int varArrayLength = 10;
  

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


            // p.telnet = new Telnet(p.IP, 23, 50);
          
            //if (p.telnet.Connect() == false)
            //{
            //    // // Console.WriteLine("连接失败");
            //    MessageBox.Show("连接失败");
            //    variableValue = null;
            //    variableName = null;
            //    //   p.telnetClose();
            //    return;
            //}
            //bool boolTMP= p.telnet.IsTelnetConnected();
            ////等待指定字符返回后才执行下一命令
            //p.telnet.WaitFor("login:");
            //p.telnet.Send(p.loginName);
            //p.telnet.WaitFor("password:");
            //p.telnet.Send(p.loginPassword);
            //p.telnet.WaitFor(">");
            variableValue = null;
            for (int i = 0; i < varNameList.Count(); i++)
            {
                p.telnet.Send(varNameList[i]);
                p.telnet.WaitFor(">");
                String[] varValueTmp = p.telnet.WorkingData.Split(new char[] { '\r' });

                for (int j = 0; j < varValueTmp.Count(); j++)
                {
                    if (varValueTmp[j].Split('=').Count() > 2)
                        variableValue += varValueTmp[j].Split('=')[2] + ";";
                }
            }
           
            string[] varValueArr = variableValue.Split(';');
     
            if (varValueArr.Count() < 169)
                return;
            int iTmp = 0;
            foreach (var item in jo)
            {
                if (item.Value.ToString().IndexOf(":") != -1) 
                    foreach (var itemValue in (JObject)item.Value)
                    {                  
                        jo[item.Key][itemValue.Key] = varValueArr[iTmp];
                        iTmp++;
                    }
            }
                        DateTime afterDT = System.DateTime.Now;
            TimeSpan ts = afterDT.Subtract(beforDT);
            Console.WriteLine("DateTime总共花费{0}ms.", ts.TotalMilliseconds);
            jo["machineID"] = p.machineID;
            jo["sampleTime"] = ts.TotalMilliseconds;
            jo["timestamp"] = System.DateTime.Now.ToString();

            if (p.connTotalTime == null)
                p.connTotalTime = "0";
            p.connTotalTime =(int.Parse(p.connTotalTime) + 3).ToString();
            jo["connTotalTime"] = p.connTotalTime;

            Console.WriteLine("时间戳{0}ms.", System.DateTime.Now.ToString());

            string SQLCONNECT = @"server=JS-DIANQI\SQLEXPRESS;database=mySQL;uid=sa;pwd=1234";
            SqlConnection conn = new SqlConnection(SQLCONNECT);
            conn.Open();
             SqlCommand sqlcmd = new SqlCommand("", conn);
             sqlcmd.CommandText = "if exists ( select machineID from injectionMachine where machineID = '"+p.machineID+"') "+
                 "begin update injectionMachine set machineData='"+jo.ToString()+"' end "+
                 "else begin insert injectionMachine(machineID,machineData)values('"+p.machineID+"','"+jo.ToString()+"') end";
             //sqlcmd.Parameters.Add("@machineID", SqlDbType.Char, 24).Value = p.machineID;
             //sqlcmd.Parameters.Add("@machineData", SqlDbType.NText).Value = jo.ToString();

                
            //sqlcmd.CommandText = "INSERT injectionMachine(machineID,machineData)VALUES('";
            //sqlcmd.CommandText += p.machineID + "','" + jo.ToString()+"')";
            sqlcmd.ExecuteNonQuery();  
            conn.Close();



           



        }
    }
    class ClampDataFromGefran
    {
        public string []aPO_MOLD = new string[2];
        public string[] sPO_MC01 = new string[2];
        public string[] sPO_MC02 = new string[2];
        public string[] sPO_MC03 = new string[2];
        public string[] sPO_MC04 = new string[2];
        public string[] sSP_MC01 = new string[2];
        public string[] sSP_MC02 = new string[2];
        public string[] sSP_MC03 = new string[2];
        public string[] sSP_MC04 = new string[2];
        public string[] sSP_MC05 = new string[2];
        public string[] sPR_MC01 = new string[2];
        public string[] sPR_MC02 = new string[2];
        public string[] sPR_MC03 = new string[2];
        public string[] sPR_MC04 = new string[2];
        public string[] sPR_MC05 = new string[2];
        public string[] sSP_MCSL = new string[2];
        public string[] sPR_MCSL = new string[2];
        public string[] sTM_MCSA = new string[2];
        public string[] sTM_MCLOS = new string[2];
        public string[] sPO_MOPN = new string[2];
        public string[] sPO_MO04 = new string[2];
        public string[] sPO_MO03 = new string[2];
        public string[] sPO_MO02 = new string[2];
        public string[] sPO_MO01 = new string[2];
        public string[] sSP_MO05 = new string[2];
        public string[] sSP_MO04 = new string[2];
        public string[] sSP_MO03 = new string[2];
        public string[] sSP_MO02 = new string[2];
        public string[] sSP_MO01 = new string[2];
        public string[] sPR_MO05 = new string[2];
        public string[] sPR_MO04 = new string[2];
        public string[] sPR_MO03 = new string[2];
        public string[] sPR_MO02 = new string[2];
        public string[] sPR_MO01 = new string[2];
        public string[] sSP_MOLS = new string[2];
        public string[] sPR_MOLP = new string[2];
        public string[] sTM_MOPEN = new string[2];
    }
    class InjectionDataFromGefran
    {
        public string[] aPO_INJE = new string[2];
        public string[] sPO_IN10 = new string[2];
        public string[] sPO_IN09 = new string[2];
        public string[] sPO_IN08 = new string[2];
        public string[] sPO_IN07 = new string[2];
        public string[] sPO_IN06 = new string[2];
        public string[] sPO_IN05 = new string[2];
        public string[] sPO_IN04 = new string[2];
        public string[] sPO_IN03 = new string[2];
        public string[] sPO_IN02 = new string[2];
        public string[] sPO_IN01 = new string[2];
        public string[] sSP_IN10 = new string[2];
        public string[] sSP_IN09 = new string[2];
        public string[] sSP_IN08 = new string[2];
        public string[] sSP_IN07 = new string[2];
        public string[] sSP_IN06 = new string[2];
        public string[] sSP_IN05 = new string[2];
        public string[] sSP_IN04 = new string[2];
        public string[] sSP_IN03 = new string[2];
        public string[] sSP_IN02 = new string[2];
        public string[] sSP_IN01 = new string[2];
        public string[] sPR_IN10 = new string[2];
        public string[] sPR_IN09 = new string[2];
        public string[] sPR_IN08 = new string[2];
        public string[] sPR_IN07 = new string[2];
        public string[] sPR_IN06 = new string[2];
        public string[] sPR_IN05 = new string[2];
        public string[] sPR_IN04 = new string[2];
        public string[] sPR_IN03 = new string[2];
        public string[] sPR_IN02 = new string[2];
        public string[] sPR_IN01 = new string[2];
        public string[] sSP_IN00 = new string[2];
        public string[] sPR_IN00 = new string[2];
        public string[] sTM_INJE = new string[2];
        public string[] sPO_HOLD = new string[2];
        public string[] sSP_HOLD = new string[2];
        public string[] InjSpeed2 = new string[2];
        public string[] InjPress = new string[2];
        public string[] vCQ_CUSC = new string[2];

    }
    class EjectorDataFromGefran {
        public string[] aPO_EJEC = new string[2];
        public string[] sPO_EJBK = new string[2];
        public string[] sPO_EJB1 = new string[2];
        public string[] sSP_EJB2 = new string[2];
        public string[] sSP_EJB1 = new string[2];
        public string[] sPR_EJB2 = new string[2];
        public string[] sPR_EJB1 = new string[2];
        public string[] sPO_EJF1 = new string[2];
        public string[] sPO_EJFW = new string[2];
        public string[] sSP_EJF1 = new string[2];
        public string[] sSP_EJF2 = new string[2];
        public string[] sPR_EJF1 = new string[2];
        public string[] sPR_EJF2 = new string[2];
        public string[] sSP_EJMS = new string[2];
        public string[] sPR_EJMS = new string[2];
        public string[] sCU_EJSH = new string[2];
       
    }
    class CoreDataFromGefran
    {
        public string[] sPO_CCF1 = new string[2];
        public string[] sPO_CIF1 = new string[2];
        public string[] sTM_DCF1 = new string[2];
        public string[] sTM_TCF1 = new string[2];
        public string[] sSP_COF1 = new string[2];
        public string[] sPR_COF1 = new string[2];
        public string[] sCU_USC1 = new string[2];
        public string[] sPO_CCB1 = new string[2];
        public string[] sPO_CIB1 = new string[2];
        public string[] sTM_DCB1 = new string[2];
        public string[] sTM_TCB1 = new string[2];
        public string[] sSP_COB1 = new string[2];
        public string[] sPR_COB1 = new string[2];

        public string[] sPO_CCF2 = new string[2];
        public string[] sPO_CIF2 = new string[2];
        public string[] sTM_DCF2 = new string[2];
        public string[] sTM_TCF2 = new string[2];
        public string[] sSP_COF2 = new string[2];
        public string[] sPR_COF2 = new string[2];
        public string[] sCU_USC2 = new string[2];
        public string[] sPO_CCB2 = new string[2];
        public string[] sPO_CIB2 = new string[2];
        public string[] sTM_DCB2 = new string[2];
        public string[] sTM_TCB2 = new string[2];
        public string[] sSP_COB2 = new string[2];
        public string[] sPR_COB2 = new string[2];
    }
    class ChargeDataFromGefran
    {
        public string[] aPO_INJE = new string[2];
        public string[] sPO_CH01 = new string[2];
        public string[] sPO_CH02 = new string[2];
        public string[] sPO_CH03 = new string[2];
        public string[] sPO_CH04 = new string[2];
        public string[] sPO_CHST = new string[2];
        public string[] sSP_CH01 = new string[2];
        public string[] sSP_CH02 = new string[2];
        public string[] sSP_CH03 = new string[2];
        public string[] sSP_CH04 = new string[2];
        public string[] sSP_CH05 = new string[2];
        public string[] sPR_CH01 = new string[2];
        public string[] sPR_CH02 = new string[2];
        public string[] sPR_CH03 = new string[2];
        public string[] sPR_CH04 = new string[2];
        public string[] sPR_CH05 = new string[2];
        public string[] sPR_BP01 = new string[2];
        public string[] sPR_BP02 = new string[2];
        public string[] sPR_BP03 = new string[2];
        public string[] sPR_BP04 = new string[2];
        public string[] sPR_BP05 = new string[2];
        public string[] sSP_CH00 = new string[2];
        public string[] sPR_CH00 = new string[2];
        public string[] sPR_BP00 = new string[2];
        public string[] sTM_COOL = new string[2];
     //   public string sTM_CHT1;
        public string[] sPO_SBBC = new string[2];
        public string[] sSP_SB01 = new string[2];
        public string[] sPR_SB01 = new string[2];
        public string[] sPO_SBAC = new string[2];
        public string[] sSP_SB02 = new string[2];
        public string[] sPR_SB02 = new string[2];


    }
    class CarriageDataFromGefran
    {
        public string[] aPO_CARR = new string[2];
        public string[] sPO_CAFW = new string[2];
        public string[] sPO_CAF1 = new string[2];
        public string[] sSP_CAF2 = new string[2];
        public string[] sSP_CAF1 = new string[2];
        public string[] sPR_CAF2 = new string[2];
        public string[] sPR_CAF1 = new string[2];
        public string[] sPO_CAB1 = new string[2];
        public string[] sPO_CABK = new string[2];
        public string[] sSP_CAB1 = new string[2];
        public string[] sSP_CAB2 = new string[2];
        public string[] sPR_CAB1 = new string[2];
        public string[] sPR_CAB2 = new string[2];
        public string[] sSP_CAMS = new string[2];
        public string[] sPR_CAMS = new string[2];
        public string[] sSW_CABK = new string[2];
        public string[] sTM_CABK = new string[2];
        public string[] sTM_MCAFW = new string[2];
        public string[] sTM_MCABW = new string[2];
    }

    class InjectionMachineWithGefranPerfoma : InjectionMachine
    {
        public InjectionMachineWithGefranPerfoma()
        {
              clamp = new ClampDataFromGefranPerfoma();
              injection = new InjectionDataFromGefranPerfoma();
              ejector = new EjectorDataFromGefranPerfoma();
              core = new CoreDataFromGefranPerfoma();
              charge = new ChargeDataFromGefranPerfoma();
              carriage = new CarriageDataFromGefranPerfoma();
        }
    }
    class ClampDataFromGefranPerfoma
    {
        public string[] OP_MOULD_DATA_M__END_POS=new string[2];
        public string[,]  CL_MOULD_DATA_M__POS_DS =  new string[5,2];
        public string[,] CL_MOULD_DATA_M__SPD_DS = new string[6, 2];
        public string[,] CL_MOULD_DATA_M__PRE_DS = new string[6,2];
        public string[] T_MOLD_SAFE=new string[2];
        public string[,] OP_MOULD_DATA_M__POS_DS = new string[5, 2];
        public string[,] OP_MOULD_DATA_M__SPD_DS = new string[6, 2];
        public string[,] OP_MOULD_DATA_M__PRE_DS = new string[5, 2];
    }
    class InjectionDataFromGefranPerfoma
    {
        public string[] INJEC_DELAY = new string[2];
        public string[,] INJEC_DATA_M__POS_DS = new string[8, 2];
        public string[,] INJEC_DATA_M__SPD_DS = new string[8, 2];
        public string[,] INJEC_DATA_M__PRE_DS  = new string[8, 2];
        public string[] TA_RIEMP = new string[2];

        public string[] HOLD_SPD = new string[2];
        public string[,] LP_PHT_DS = new string[8, 2];
        public string[,] INJEC_LP_DATA__PRE_DS = new string[8, 2];
    }
    class EjectorDataFromGefranPerfoma
    {
   
        public string[,] EJECT_FW_DATA_M__POS_DS = new string[3, 2];
        public string[,] EJECT_FW_DATA_M__SPD_DS = new string[3, 2];
        public string[,] EJECT_FW_DATA_M__PRE_DS = new string[3, 2];
        public string[] EJ_FIRSTOUTDELAY = new string[2];

        public string[,] EJECT_BW_DATA_M__POS_DS = new string[3, 2];
        public string[,] EJECT_BW_DATA_M__SPD_DS = new string[3, 2];
        public string[,] EJECT_BW_DATA_M__PRE_DS = new string[3, 2];
        public string[] EJ_FINALBKDELAY = new string[2];
    }
    class CoreDataFromGefranPerfoma
    {

        public string[,] ACT_SPEED_A_DS__N  = new string[2, 2];
        public string[,] ACT_PRESSURE_A_DS__N = new string[2, 2];
        public string[,] ACT_SPEED_B_DS__N = new string[2, 2];
        public string[,] ACT_PRESSURE_B_DS__N = new string[2, 2];
    
    }
    class ChargeDataFromGefranPerfoma
    {      
        public string[,] SCREW_DATA_M__POS_DS = new string[8, 2];
        public string[,] SCREW_DATA_M__SPD_DS = new string[8, 2];
        public string[,] SCREW_DATA_M__PRE_DS = new string[8, 2];     
    }
    class CarriageDataFromGefranPerfoma
    {
        public string[,] CFW_DATA_M__POS_DS = new string[2, 2];
        public string[,] CFW_DATA_M__SPD_DS = new string[2, 2];
        public string[,] CFW_DATA_M__PRE_DS = new string[2, 2];
        public string[,] CBK_DATA_M__POS_DS = new string[2, 2];
        public string[,] CBK_DATA_M__SPD_DS = new string[2, 2];
        public string[,] CBK_DATA_M__PRE_DS = new string[2, 2];
    }
    class ConnectionOption
    {
        public string controllerType { get; set; }
        public string machineID{get;set;}
        public string protocol { get; set; }
        public string IP { get; set; }
        public string loginName { get; set; }
        public string loginPassword { get; set; }
        public string connConfirm { get; set; }
        public string connStatus { get; set; }
        public string isDisplay { get; set; }
        public string connTotalTime { get; set; }
        public string connTimeList { get; set; }
        public Telnet telnet;
        public bool connectToController()
        {
            bool status = false;
            foreach (string item in ipList)
            {
                if(item ==IP)
                {
                    status = true;
                    break;
                }
            }

            Ping pingController = new Ping();
            PingReply reply = pingController.Send(IP, 10);
            if (reply.Status == IPStatus.Success)
            {
                status = true;
            }
            else
                status = false;

      
            //telnet = new Telnet(IP, 23, 50);

            //if (telnet.Connect() == false)
            //{
            //    status = false;
            //    MessageBox.Show("连接失败");
            //    return status;
            //}

            //if (status.ToString() != connStatus)
            //{               
            //    connTimeList += DateTime.Now.ToString();
            //}

            ////等待指定字符返回后才执行下一命令
            //telnet.WaitFor("login:");
            //telnet.Send(loginName);
            //telnet.WaitFor("password:");
            //telnet.Send(loginPassword);
            //telnet.WaitFor(">");
            if (status)
                connStatus = "1";
            else
                connStatus = "0";
            return status;
        }
        public void disconnectToController()
        {
             telnet.telnetClose();
        }
        static List<string> ipList = new List<string>();
        static public void getIP()
        {

            ////获取本地机器名 
            //string _myHostName = Dns.GetHostName();
            ////获取本机IP 
            //string _myHostIP = Dns.GetHostEntry(_myHostName).AddressList[0].ToString();
            ////截取IP网段
            // string ipDuan = _myHostIP.Remove(_myHostIP.LastIndexOf('.'));
            //枚举网段计算机
            for (int i = 1; i <= 255; i++)
            {
                Ping myPing = new Ping();
                myPing.PingCompleted += new PingCompletedEventHandler(_myPing_PingCompleted);
                string pingIP = "192.168.8." + i.ToString();
                myPing.SendAsync(pingIP, 1000, null);

            }
            Thread.Sleep(500);
        }
        static void _myPing_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            if (e.Reply.Status == IPStatus.Success)
            {
                ipList.Add(e.Reply.Address.ToString());
            }
        }

    }
       
}
