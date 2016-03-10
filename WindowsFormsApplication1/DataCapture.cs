using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ICSharpCode.SharpZipLib.GZip;
namespace WindowsFormsApplication1
{
    class DataCapture
    {
        public void getFilesFromGefran(string name,string password,string sourcePath, string targetPath,string targetFileName)
        {
            FtpHelper ftpTmp = new FtpHelper();


            string[] strFileName = ftpTmp.GetFilePath(name, password, sourcePath);
            strFileName = strFileName.OrderBy(s => int.Parse(System.Text.RegularExpressions.Regex.Replace(s, @"[^\d]+", "0"))).ToArray();

            ftpTmp.Download(name, password, sourcePath, targetPath, strFileName[strFileName.Length - 2], targetFileName);




            Stream gzStream = new GZipInputStream(File.OpenRead(targetPath + "\\"+targetFileName));


            FileStream fs = File.Create(targetPath +"\\"+ targetFileName.Remove(targetFileName.LastIndexOf('.'))+".txt");
            int size = 2048;
            byte[] writeData = new byte[size];//指定缓冲区的大小     
            while (true)
            {
                size = gzStream.Read(writeData, 0, size);//读入一个压缩块     
                if (size > 0)
                {
                    fs.Write(writeData, 0, size);//写入解压文件代表的文件流     
                }
                else
                {
                    fs.Close();
                    break;//若读到压缩文件尾，则结束     
                }
            }
            gzStream.Close();

        }
    }
}
