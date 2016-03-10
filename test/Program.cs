using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.NetworkInformation;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.Application;
using System.Windows.Forms;
using System.Reflection;
using DotNet4.Utilities;
namespace test
{
    class InjectionMachine
    {
        
        public string machineID{get;set;}
        public Clamp clamp;
        public Injection injection;
        public InjectionMachine()
        {
            clamp = new Clamp();
            injection = new Injection();
        }
        
        
    }
    class Clamp
    {

        public string sPO_MC01;
        public string sPO_MC02;
    }
    class Injection
    {
        public string sIJ_MC01;
        public string sIJ_MC02;
    }
    class Program
    {
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
            Thread.Sleep(50);
        }
       static void _myPing_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            if (e.Reply.Status == IPStatus.Success)
            {
                ipList.Add(e.Reply.Address.ToString());
            }
        }
       //static string GetWebContent(string url)
       //{
       //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
       //    HttpWebResponse respone = (HttpWebResponse)request.GetResponse();
       //    Stream stream = respone.GetResponseStream();

       //    Encoding encoding = Encoding.Default;
       //    if (ddlEncoding.SelectedItem != null && ddlEncoding.SelectedItem.ToString() != "" && this.ddlEncoding.SelectedItem.ToString() != "Default")
       //    {
       //        encoding = Encoding.GetEncoding(ddlEncoding.SelectedItem.ToString());
       //    }

       //    StreamReader streamReader = new StreamReader(stream, encoding);
       //    return streamReader.ReadToEnd();
       //}  
       //static string polishing(int i)
       //{
       //    i.ToString("x5");
       //}
       static string uuid = "";

       //存储提交后缀

       static string lastName = "";

       static string lastValue = "";

       static string cookies = "";

        static void Main(string[] args)
        {

            //string strUrl = "http://passport.jd.com/new/login.aspx?returnUrl=http%3A%2F%2Fvip.jd.com%2F";

            //// WebRequest request = WebRequest.Create(strUrl);
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);
            //HttpWebResponse respone = (HttpWebResponse)request.GetResponse();
            //Stream stream = respone.GetResponseStream();
            ////WebResponse response = request.GetResponse();
            ////  StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
            //StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.GetEncoding("UTF-8"));

            //string strMsg = reader.ReadToEnd();




            HttpItem item = new HttpItem();

            HttpHelper helper = new HttpHelper();

            HttpResult result = new HttpResult();



            item.Method = "GET";

            item.URL = "http://passport.jd.com/new/login.aspx?returnUrl=http%3A%2F%2Fvip.jd.com%2F";

            result = helper.GetHtml(item);
            
            cookies = result.Cookie;

            string htmltext = result.Html;

            var ary = Regex.Matches(htmltext, @"(?is)<input(?=[^>]*?name=[""'](?<name>[^""'\s]+)[""'])(?=[^>]*?value=[""'](?<value>[^""'\s]+)[""'])[^>]+>").OfType<Match>().Select(t => new { name = t.Groups["name"].Value, value = t.Groups["value"].Value }).ToArray();

            uuid = ary.ToList()[0].value;

            lastName = ary.ToList()[1].name;

            lastValue = ary.ToList()[1].value;

            loginJD();
  





     


        }
        static void loginJD()
        {
            HttpItem item = new HttpItem();

            HttpHelper helper = new HttpHelper();

            HttpResult result = new HttpResult();

 

            item.URL = "http://passport.jd.com/uc/loginService?uuid=" + uuid + "&ReturnUrl=http%3A%2F%2Fvip.jd.com%2F&r=0.3457543252407181";

            item.Method = "POST";

            item.Allowautoredirect = true;

            item.ContentType = "application/x-www-form-urlencoded";

            item.Postdata = "uuid=" + uuid + "&loginname=" + "18868879376" + "&loginpwd=" + "yaoyuxi123456" + "&machineNet=&machineCpu=&machineDisk=&authcode=&"+lastName+"="+lastValue+"";

            item.Header.Add("x-requested-with", "XMLHttpRequest");

            item.Header.Add("Accept-Encoding", "gzip, deflate");

            item.Referer = "http://passport.jd.com/new/login.aspx?ReturnUrl=http%3A%2F%2Fvip.jd.com%2F";

            item.Accept = "*/*";

            item.Encoding = Encoding.UTF8;

            item.Cookie = cookies;

            result = helper.GetHtml(item);

            cookies ="__jda=95931165.290243407.1371634814.1371634814.1371634814.1; __jdb=95931165.1.290243407|1.1371634814; __jdc=95931165; __jdv=95931165|direct|-|none|-;" + result.Cookie;

            cookies = cookies.Replace("HttpOnly,", null);

            Console.WriteLine("登陆成功了！\n"+ result.Html);
            

 

        
        }
        static void downloadMarathonResualtsFromHM()
        {
            string strMsg = string.Empty;
            int j = 5;
            for (int i = 8203; i < 15000; i++)
            {
                string strUrl = "http://www.hzim.org/p/hzim/match/queryresult.jsf?qr=" + i.ToString("d5") + "&matchyear=2015";

                // WebRequest request = WebRequest.Create(strUrl);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);
                HttpWebResponse respone = (HttpWebResponse)request.GetResponse();
                Stream stream = respone.GetResponseStream();
                //WebResponse response = request.GetResponse();
                //  StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
                StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.GetEncoding("UTF-8"));

                strMsg = reader.ReadToEnd();
                while (true)
                {
                    string subStr = Regex.Match(strMsg, @"&#[^;]+;").ToString();
                    if (subStr == "") break;
                    string unUnicodeSTR = System.Text.Encoding.Unicode.GetString(System.Text.Encoding.Default.GetBytes(subStr));

                    string source = Regex.Match(subStr, @"[\d]+").ToString();
                    string dest = ((char)int.Parse(int.Parse(source).ToString("x4"), System.Globalization.NumberStyles.HexNumber)).ToString();
                    strMsg = strMsg.Replace(subStr, dest);

                }
                if (strMsg.IndexOf("姓名") == -1)
                {
                    continue;
                }
                string name = Regex.Match(strMsg.Substring(strMsg.IndexOf("姓名")), "(?<=>)[^>]+(?=</div)").ToString();
                string[] r = Regex.Match(strMsg.Substring(strMsg.IndexOf("净成绩")), "(?<=>)[^>]+(?=</div)").ToString().Split('\n');
                name = name.Replace("\t", "");
                name = name.Replace("\n", "").Trim();
                r[1] = r[1].Replace("\t", "");
                r[2] = r[2].Replace("\t", "").Trim();
                string data = i.ToString("d5") + ";" + name + ";" + r[1] + ";" + r[2];
                j++;
                StreamWriter sw = File.AppendText("D:\\1.txt");

                sw.WriteLine(data);
                Console.WriteLine(data);
                sw.Close();

                //   excelHelper.writeDataToExcel("", data, j.ToString());
                // string strReg = Regex.Match(subStr, regex).ToString();
                reader.Close();
                reader.Dispose();
                //response.Close();


            }





            excelHelper.writeDataToExcel("", "");
        }

        static string FromUnicodeString( string str)
        {
            //最直接的方法Regex.Unescape(str);
            StringBuilder strResult = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {
                string[] strlist = str.Replace("\\", "").Split('u');
                try
                {
                    for (int i = 1; i < strlist.Length; i++)
                    {
                        int charCode = Convert.ToInt32(strlist[i], 16);
                        strResult.Append((char)charCode);
                    }
                }
                catch (FormatException ex)
                {
                    return Regex.Unescape(str);
                }
            }
            return strResult.ToString();
        }




        private System.Timers.Timer getDataTimer;
        private int x = 0;
        private int timeNum = 0;
        private object locker = new object();
        public void test()
        {

            getDataTimer = new System.Timers.Timer(1000);
            getDataTimer.Elapsed += new System.Timers.ElapsedEventHandler(writeLine);
            
            getDataTimer.AutoReset = true;
            getDataTimer.Enabled = true;

        }
        
        public void writeLine(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock(locker){
                timeNum++;
                Console.WriteLine("线程号{0}",timeNum.ToString());

            }
            if(Interlocked.Exchange(ref x,1)==0){
                DateTime first = DateTime.Now;

           //     Console.WriteLine("1\n");
                Thread.Sleep(3000);
           //     Console.WriteLine("2\n");

                DateTime second = DateTime.Now;
                TimeSpan ts = second.Subtract(first);
                Console.WriteLine(getDataTimer.Interval);
                Console.WriteLine("线程耗时{0}", ((int)ts.TotalMilliseconds).ToString());
                if (ts.TotalMilliseconds > getDataTimer.Interval + 1000)
                    getDataTimer.Stop();
                
                Console.WriteLine(getDataTimer.Interval);

                Interlocked.Exchange(ref x, 0);
            }

        }
 
    }
}
