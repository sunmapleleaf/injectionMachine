using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
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

namespace WindowsFormsApplication1
{
    class ConnectionOption
    {
        public string controllerType { get; set; }
        public string machineID { get; set; }
        public string protocol { get; set; }
        public string IP { get; set; }
        public string loginName { get; set; }
        public string loginPassword { get; set; }
        public string connConfirm { get; set; }
        public string connStatus { get; set; }
        public string isDisplay { get; set; }
        public string connTotalTime { get; set; }
        public string connTimeList { get; set; }
        public string orderNumber { get; set; }
        public string userID { get; set; }
        public Telnet telnet;
        //  public void writeConnToDataBase();
        static public void checkConnections(List<ConnectionOption> connList)
        {
            ipList.Clear();
            foreach (ConnectionOption item in connList)
            {
                Ping myPing = new Ping();
                myPing.PingCompleted += new PingCompletedEventHandler(_myPing_PingCompleted);
                myPing.SendAsync(item.IP, 1000, null);
            }
            Thread.Sleep(150);


            foreach (ConnectionOption item in connList)
            {
                foreach (string connectedIP in ipList)
                    if (connectedIP == item.IP)
                    {
                        item.connStatus = "1";
                        break;
                    }
                    else
                        item.connStatus = "0";
                if (ipList.Count == 0)
                {
                    item.connStatus = "0";
                }
            }

            //Ping pingController = new Ping();
            //PingReply reply = pingController.Send(IP, 10);
            //if (reply.Status == IPStatus.Success)
            //{
            //    status = true;
            //}
            //else
            //    status = false;


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
            //if (status)
            //    connStatus = "1";
            //else
            //    connStatus = "0";
            //return status;
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
