using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;//MD5加密需引入的命名空间
using System.Data.SqlClient;//数据库操作需引入的命名空间
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace WindowsFormsApplication1
{


    class IMDataBase
    {
         static public string connStr = @"server=112.124.23.181;database=mySQL;uid=sa;pwd=1234";
      //  static public string connStr = @"server=JS-DIANQI\SQLEXPRESS;database=mySQL;uid=sa;pwd=1234";

       /// <summary>
       /// 将数据写入数据库
       /// </summary>
       /// <param name="connStr">连数据库字串</param>  
       /// <param name="machineID">机器ID</param>
       /// <param name="machineID">数据</param> 
       /// <returns></returns>
        public void writeDataBase(string connStr,string machineID,string data)
        {
            //string SQLCONNECT = @"server=JS-DIANQI\SQLEXPRESS;database=mySQL;uid=sa;pwd=1234";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand sqlcmd = new SqlCommand("", conn);
            sqlcmd.CommandText = "if exists ( select machineID from injectionMachine where machineID = '" + machineID + "') " +
                "begin update injectionMachine set machineData='" + data + "' where machineID='"+machineID+"' end " +
                "else begin insert injectionMachine(machineID,machineData)values('" + machineID + "','" + data + "') end";
            sqlcmd.ExecuteNonQuery();
            conn.Close();
        }
        /// <summary>
        /// 将质量管理数据写入数据库
        /// </summary>
        /// <param name="connStr">连数据库字串</param>  
        /// <param name="machineID">机器ID</param>
        /// <param name="number">编号</param>
        /// <param name="data">数据</param> 
        /// <returns></returns>
        public void writeQualityDataToDB(string connStr, string machineID,string number, string data)
        {
            //string SQLCONNECT = @"server=JS-DIANQI\SQLEXPRESS;database=mySQL;uid=sa;pwd=1234";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand sqlcmd = new SqlCommand("", conn);
            sqlcmd.CommandText = "insert qualityManage(machineID,number,qualityData) values('"+machineID+"','"+number+"','"+data+"')";
            sqlcmd.ExecuteNonQuery();
            conn.Close();
        }
        static int  sampleT = 3;

        /// <summary>
        /// 将控制器配置数据写入数据库
        /// </summary>
        /// <param name="connOption">所有配置信息</param>  
        /// <returns></returns>
        public void writeConnToDataBase(ConnectionOption connOption)
        {
            string SQLCONNECT = IMDataBase.connStr;
            SqlConnection conn = new SqlConnection(SQLCONNECT);
            conn.Open();
            JObject jo = (JObject)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(connOption));
            string SQLCOMMAND = " ";
            SqlCommand sqlcmd = new SqlCommand(SQLCOMMAND, conn);
            if (jo["conntotalTime"] == null)
                jo["conntotalTime"] = sampleT.ToString();
            else
                jo["conntotalTime"] = (int.Parse(jo["conntotalTime"].ToString()) + sampleT).ToString();
            //   sqlcmd.CommandText = "INSERT connectionOption(machineID,connDetail,connConfirm,isDisplay)VALUES('";
            sqlcmd.CommandText = "update connectionOption SET connDetail='" + jo.ToString() + "',conntotalTime='" + jo["conntotalTime"] + "'where machineID = '" + connOption.machineID + "'";
            int sqlcmdResult = 0;
            try{
                sqlcmdResult=sqlcmd.ExecuteNonQuery();
                conn.Close();
            }
            catch{
                
                conn.Close();
            }
            

        }
        /// <summary>
        /// 从数据库读取所有控制器配置信息
        /// </summary>
        /// <param name="listConn">所有配置信息</param>  
        /// <returns></returns>
        static public void readAllConnections(ref List<ConnectionOption> listConn)
        {
            try
            {

                listConn.Clear();
                string SQLCONNECT = IMDataBase.connStr;
                SqlConnection conn = new SqlConnection(SQLCONNECT);
                conn.Open();

                string SQLCOMMAND = "select connDetail from connectionOption";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(SQLCOMMAND, conn);
                da.Fill(ds);

                listConn.Clear();
                foreach (DataRow tmp in ds.Tables[0].Rows)
                {
                    listConn.Add(JsonConvert.DeserializeObject<ConnectionOption>(tmp[0].ToString()));
                }
                conn.Close();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("数据库读取链接失败！检查网络！");
            
            }


        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="strPwd"></param>  
        /// <returns></returns>
        public string GetMD5(string strPwd)
        {
            string pwd = "";
            //实例化一个md5对象
            MD5 md5 = MD5.Create();
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(strPwd));
            //翻转生成的MD5码        
            s.Reverse();
            //通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            //只取MD5码的一部分，这样恶意访问者无法知道取的是哪几位
            for (int i = 3; i < s.Length - 1; i++)
            {
                //将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                //进一步对生成的MD5码做一些改造
                pwd = pwd + (s[i] < 198 ? s[i] + 28 : s[i]).ToString("X");
            }
            return pwd;
        }

    }
}
