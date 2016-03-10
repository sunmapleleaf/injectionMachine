using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace createNameFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string  variableName = "";

            DateTime beforDT = System.DateTime.Now;
            //json
            JsonSerializer jsS = new JsonSerializer();
            StringWriter sw = new StringWriter();
            jsS.Serialize(new JsonTextWriter(sw), new InjectionMachineWithGefranPerfoma());

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
                        if (jo[item.Key][itemValue.Key][0].Count() >= 2)
                        {
                            for (int arrJ = 0; arrJ < jo[item.Key][itemValue.Key].Count(); arrJ++)
                            {
                                variableName += itemValue.Key + "[" + arrJ.ToString() + "]" + ";";

                            }
                        }
                        else
                          variableName += itemValue.Key + ";";
                        //  jo[item.Key][itemValue.Key] = variableValue;
                    }
                }
            }
            







            //StreamReader file = new StreamReader(textBox1.Text);
            //string variableName = file.ReadToEnd();
            //file.Close();
            string[] strTmp = variableName.Split(';');
            int i = 0,j=0;
            foreach (string strItem in strTmp)
            {
                if(strItem !="")
                {
                    Label lable1 = new Label();
                    lable1.Name = "la" + strItem;
                    lable1.Text = strItem;
                    lable1.Size = new System.Drawing.Size(55, 21);

                    TextBox textBoxTmp = new TextBox();
                    textBoxTmp.Name = strItem;
                    textBoxTmp.Size = new System.Drawing.Size(80, 21);
                    textBoxTmp.TabIndex = 1;

                    StreamReader file2 = new StreamReader(textBox2.Text);
                    string[] strTmpA = file2.ReadToEnd().Split(';');
                    file2.Close();
                    foreach (string strItem2 in strTmpA)
                    {
                        if (strItem2.IndexOf(':') != -1 && strItem2.Split(':')[0] == textBoxTmp.Name)
                        {
                            textBoxTmp.Text = strItem2.Split(':')[1];
                        }

                    }

                    if (i % 4 == 0)
                    {
                        j += 30;
                    }
                    textBoxTmp.Location = new System.Drawing.Point(270 + (i % 4) * 270, 65 + j);
                    this.Controls.Add(textBoxTmp);

                    lable1.Location = new System.Drawing.Point(100 + (i % 4) * 270, 65 + j);
                    lable1.Width = 200;
                    this.Controls.Add(lable1);
                    i++;
                }
               

            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            string strVarName = "";
            foreach (Control item in Controls)
            {
                if (item.GetType().ToString() == "System.Windows.Forms.TextBox"&&(item.Text.IndexOf('\\')==-1))
                {
                    strVarName += item.Name + ":" + item.Text + ";";
                }
            }

            File.WriteAllText(textBox2.Text,strVarName);
        }
    }
}
