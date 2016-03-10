using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace WindowsFormsApplication1
{
    class ObjectAnalysis
    {
        /// <summary>
        ///从解压出的Gefran文件中，获得需要的变量
        /// 
        /// </summary>
        /// <param name="sourcePath">文件存放位置</param>   
        /// <param name="fileName">文件名字</param>   
        /// <param name="option">从文件获取变量时有可选值：0和1位点值，2为实际值等</param>   
        /// <param name="model">控制器类型，“gefranVedo”或“gefranPerfoma”</param>   
        /// <returns></returns>
       static public JObject analyseGefranFile(string sourcePath,string fileName,int option,string model="gefranVedo")
        {
            StreamReader file = new StreamReader(sourcePath + "\\" + fileName);
           
            string strTmpA = file.ReadToEnd();
            file.Close();
            //  string subStr = System.Text.RegularExpressions.Regex.Match(strTmpA, @"OTHER3POSSHOW[^A-Za-z]+").ToString();

            JsonSerializer jsS = new JsonSerializer();
            StringWriter sw = new StringWriter();
            StreamReader nameReader;
            string nameStr="";         
            if (model == "gefranVedo")
            {
                nameReader = new StreamReader(sourcePath + "\\" + @"gefranVedo_ch.txt");
                nameStr = nameReader.ReadToEnd();
                jsS.Serialize(new JsonTextWriter(sw), new InjectionMachineWithGefran());
                nameReader.Close();
            }
            else if(model == "gefranPerfoma")
            {
                nameReader = new StreamReader(sourcePath + "\\" + @"gefranPerfoma_ch.txt");
                nameStr = nameReader.ReadToEnd();
                jsS.Serialize(new JsonTextWriter(sw), new InjectionMachineWithGefranPerfoma());
                nameReader.Close();
            }
            else
                return null;

            JObject jo = (JObject)JsonConvert.DeserializeObject(sw.ToString());
            foreach (var item in jo)
            {
                if (item.Value.ToString().IndexOf(":") != -1)
                    foreach (var itemValue in (JObject)item.Value)
                    {
                        //在值string流中匹配当前变量的相关字段

                        string matchStr = System.Text.RegularExpressions.Regex.Match(strTmpA, itemValue.Key.Replace("__",".") + @".*=[^A-Za-z]+").ToString();
                        string nameSearch = "";
                        if (jo[item.Key][itemValue.Key][0].Count() >= 2)//当前变量是数组
                        {
                            string[] arrTmp = matchStr.Split('=')[1].Trim().Split(' ');
                            for (int j = 0; j < arrTmp.Count() && j < jo[item.Key][itemValue.Key].Count(); j++) //循环次数小于值数组长度或者对象中变量数组长度
                            {
                                nameSearch = System.Text.RegularExpressions.Regex.Match(nameStr, itemValue.Key + "\\[" + j + "\\][^;]+").ToString();
                                jo[item.Key][itemValue.Key][j][1] = arrTmp[j];
                                if (nameSearch.IndexOf(':') != -1 && nameSearch.Split(':')[1]!="")
                                    jo[item.Key][itemValue.Key][j][0] = nameSearch.Split(':')[1];
                                else
                                    jo[item.Key][itemValue.Key][j][0] = itemValue.Key+"["+j+"]";
                            }

                        }
                        else if (matchStr.Split('\n').Count() > 1 + option)//当前变量下有可选值：0和1位点值，2为实际值等
                        {
                            //  string strTmp = matchStr.Split('\n')[option];
                            nameSearch = System.Text.RegularExpressions.Regex.Match(nameStr, itemValue.Key + "[^;]+").ToString();
                            jo[item.Key][itemValue.Key][1] = matchStr.Split('\n')[option].Split('=')[1].Replace("\r", "");
                            if (nameSearch.IndexOf(':') != -1 && nameSearch.Split(':')[1] != "")
                                jo[item.Key][itemValue.Key][0] = nameSearch.Split(':')[1];
                            else
                                 jo[item.Key][itemValue.Key][0] = itemValue.Key;
                        }
                        else
                        {
                            nameSearch = System.Text.RegularExpressions.Regex.Match(nameStr, itemValue.Key + "[^;]+").ToString();
                            jo[item.Key][itemValue.Key][1] = System.Text.RegularExpressions.Regex.Match(matchStr, @"\d+").ToString();
                            if (nameSearch.IndexOf(':') != -1 && nameSearch.Split(':')[1] != "")
                                jo[item.Key][itemValue.Key][0] = nameSearch.Split(':')[1];
                            else
                                jo[item.Key][itemValue.Key][0] = itemValue.Key;
                        }
                    }
            }
            
            jo["machineID"] = fileName.Remove(fileName.LastIndexOf('.'));
            jo["timestamp"] = System.DateTime.Now.ToString();       

            return jo;
        } 
    }
}
