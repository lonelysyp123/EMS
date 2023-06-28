using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EMS.Storage.File
{
    public static class FileHelper
    {
        public static void SaveDataInFile(string FilePath, string Type, BatteryModelForFile[] batteries)
        {
            try
            {
                // 保存到本都csv文件
                string header = "ID,BatteryID,Voltage,Current";
                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                }
                string path = FilePath + Type + "_" + DateTime.Now.Ticks.ToString() + ".csv";
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.WriteLine(header);
                for (int i = 0; i < batteries.Length; i++)
                {
                    string body = batteries[i].ID + "," +
                        batteries[i].BatteryID + "," +
                        batteries[i].Voltage + "," +
                        batteries[i].Current;
                    sw.WriteLine(body);
                }
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据保存出错");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
