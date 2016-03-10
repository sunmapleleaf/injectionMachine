﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.ComponentModel;

using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.Application;
using System.Windows.Forms;
using System.Reflection;
namespace test
{
    class excelHelper
    {
        //加载Excel   
        public static void writeDataToExcel(string filePath, string data)
        {
            //创建Application对象
            Excel.Application xApp = new Excel.Application();

            //xApp.Visible = true;
            //得到WorkBook对象, 可以用两种方式之一: 下面的是打开已有的文件
            Excel.Workbook xBook = xApp.Workbooks._Open(@"D:\4.xls",
            Missing.Value, Missing.Value, Missing.Value, Missing.Value
            , Missing.Value, Missing.Value, Missing.Value, Missing.Value
            , Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            //xBook=xApp.Workbooks.Add(Missing.Value);//新建文件的代码
            //指定要操作的Sheet，两种方式：

            Excel.Worksheet xSheet = (Excel.Worksheet)xBook.Sheets[1];
            //Excel.Worksheet xSheet=(Excel.Worksheet)xApp.ActiveSheet;

            ////读取数据，通过Range对象
            //Excel.Range rng1 = xSheet.get_Range("A1", Type.Missing);
            //Console.WriteLine(rng1.Value2);

            ////读取，通过Range对象，但使用不同的接口得到Range
            ////Excel.Range rng2 = (Excel.Range)xSheet.Cells[3, 1];
            //Console.WriteLine(rng2.Value2);
            //写入数据
            StreamReader sr = new StreamReader("D:\\1.txt");
            string strTmp = "";
            int iCounter=3;
            List<Excel.Range> listRange = new List<Excel.Range>();
            strTmp = sr.ReadLine();
            while ((strTmp = sr.ReadLine()) != null)
            {
                iCounter++;
                string[] tmp = strTmp.Split(';');
                Excel.Range rng3 = xSheet.get_Range("C" + iCounter.ToString(), Missing.Value);
                rng3.Value2 = tmp[0];
                Excel.Range rng4 = xSheet.get_Range("D" + iCounter.ToString(), Missing.Value);
                rng4.Value2 = tmp[1];
                Excel.Range rng5 = xSheet.get_Range("E" + iCounter.ToString(), Missing.Value);
                rng5.Value2 = tmp[2];
                Excel.Range rng6 = xSheet.get_Range("F" + iCounter.ToString(), Missing.Value);
                rng6.Value2 = tmp[3];
                listRange.Add(rng3);
                listRange.Add(rng4);
                listRange.Add(rng5);
                listRange.Add(rng6);
                Console.WriteLine(strTmp);
            }

           // rng3.Interior.ColorIndex = 6; //设置Range的背景色

            //保存方式一：保存WorkBook
            xBook.SaveAs(@"D:\4.xls",
            Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
            Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value,
            Missing.Value, Missing.Value);

            ////保存方式二：保存WorkSheet
            //xSheet.SaveAs(@"D:\CData2.xls",
            //Missing.Value, Missing.Value, Missing.Value, Missing.Value,
            //Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            //保存方式三
             //xBook.Save();

            xSheet = null;
            xBook = null;
            xApp.Quit(); //这一句是非常重要的，否则Excel对象不能从内存中退出
            xApp = null;
        }
      
    }
}
