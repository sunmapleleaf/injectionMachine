using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Data.SqlClient;
namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        List<ConnectionOption> connList = new List<ConnectionOption>();
        public Form2()
        {
            InitializeComponent();
            initCBConns();
            
        }
        public delegate void readAllConnsEventHandler();
        public void initCBConns()
        {
            IMDataBase.readAllConnections(ref connList);
            cbConns.Items.Clear();
            foreach (var item in connList)
            {
                cbConns.Items.Add(item.machineID);
                cbConns.SelectedIndexChanged += new EventHandler(cbConnsSelectedIndexChanged);
            }
            // MessageBox.Show(cbConns.Items[0].ToString());
            cbConns.SelectedIndex = 0;
        }


        private void cbConnsSelectedIndexChanged(object sender, EventArgs e)
        {
            JsonSerializer jsS = new JsonSerializer();
            StringWriter sw = new StringWriter();
            jsS.Serialize(new JsonTextWriter(sw), connList[cbConns.SelectedIndex]);

            JObject jo = (JObject)JsonConvert.DeserializeObject(sw.ToString());
            foreach (Control item in panel1.Controls)
            {
                item.Text = jo[item.Name].ToString();

            } 
        }
        private void OK_Click(object sender, EventArgs e)
        {

            JObject jo = (JObject)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(new ConnectionOption()));

            
            foreach (Control item in panel1.Controls)
            {
                jo[item.Name] = item.Text;
 
            }      
            jo["connStatus"] = "0";
            jo["connTotalTime"] = "0";
            jo["connTimeList"] = "";
         //   string SQLCONNECT = @"server=JS-DIANQI\SQLEXPRESS;database=mySQL;uid=sa;pwd=1234";
            string SQLCONNECT = IMDataBase.connStr;
            SqlConnection conn = new SqlConnection(SQLCONNECT);
            conn.Open();

   


            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter("select connDetail from connectionOption", conn);
                da.Fill(ds);
            int inDataBase = 0;
            foreach(DataRow tmp in ds.Tables[0].Rows){
                ConnectionOption connTmp = JsonConvert.DeserializeObject<ConnectionOption>(tmp[0].ToString());
                if(machineID.Text == connTmp.machineID)
                {
                    inDataBase = 1;
                    MessageBox.Show("机器名重名");
                }
                if (IP.Text == connTmp.IP)
                {
                    inDataBase = 1;
                    MessageBox.Show("IP重名");
                }

            }
             


            if(inDataBase == 0)
            {
                string SQLCOMMAND = "INSERT connectionOption(machineID,connDetail,connConfirm,isDisplay)VALUES('";
                SqlCommand sqlcmd = new SqlCommand(SQLCOMMAND, conn);
             //   sqlcmd.CommandText = "INSERT connectionOption(machineID,connDetail,connConfirm,isDisplay)VALUES('";
                sqlcmd.CommandText += machineID.Text+"','"+jo.ToString() + "','" + connConfirm.Text +"','"+ isDisplay.Text+ "')";
                sqlcmd.ExecuteNonQuery();
                //SqlDataReader dr = sqlcmd.ExecuteReader();      
                
                readAllConnsEventHandler myDelegate = new readAllConnsEventHandler(IMCapture.readAllConn);
                myDelegate();
                MessageBox.Show("成功");
                initCBConns();
              //  this.Close();
            }
            conn.Close();
            
        }

        private void alter_Click(object sender, EventArgs e)
        {
           // JsonSerializer jsS = new JsonSerializer();
          //  StringWriter sw = new StringWriter();
           // jsS.Serialize(new JsonTextWriter(sw), new ConnectionOption());

            JObject jo = (JObject)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(new ConnectionOption()));


            foreach (Control item in panel1.Controls)
            {
                jo[item.Name] = item.Text;

            }
            jo["connStatus"] = "0";
            jo["connTotalTime"] = "0";
            jo["connTimeList"] = "";
            //   string SQLCONNECT = @"server=JS-DIANQI\SQLEXPRESS;database=mySQL;uid=sa;pwd=1234";
            string SQLCONNECT = IMDataBase.connStr;
            SqlConnection conn = new SqlConnection(SQLCONNECT);
            conn.Open();

            string SQLCOMMAND = "Update connectionOption set connDetail='"+jo.ToString()+"'where machineID='"+machineID.Text+"'";
            SqlCommand sqlcmd = new SqlCommand(SQLCOMMAND, conn);
            //   sqlcmd.CommandText = "INSERT connectionOption(machineID,connDetail,connConfirm,isDisplay)VALUES('";
            sqlcmd.ExecuteNonQuery();
            //SqlDataReader dr = sqlcmd.ExecuteReader();      
            initCBConns();

            readAllConnsEventHandler myDelegate = new readAllConnsEventHandler(IMCapture.readAllConn);
            myDelegate();

        }

        private void deleteConn_Click(object sender, EventArgs e)
        {
            string SQLCONNECT = IMDataBase.connStr;
            SqlConnection conn = new SqlConnection(SQLCONNECT);
            conn.Open();
            string SQLCOMMAND = "delete connectionOption where machineID='"+connList[cbConns.SelectedIndex].machineID+"'";
            SqlCommand sqlcmd = new SqlCommand(SQLCOMMAND, conn);
            //   sqlcmd.CommandText = "INSERT connectionOption(machineID,connDetail,connConfirm,isDisplay)VALUES('";
            sqlcmd.ExecuteNonQuery();
            //SqlDataReader dr = sqlcmd.ExecuteReader();      
            initCBConns();
            readAllConnsEventHandler myDelegate = new readAllConnsEventHandler(IMCapture.readAllConn);
            myDelegate();
        }

 
    }
}
