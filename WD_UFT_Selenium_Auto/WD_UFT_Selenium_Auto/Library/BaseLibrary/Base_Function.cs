using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.ServiceProcess;
using System.Text.RegularExpressions;

namespace WD_UFT_Selenium_Auto.Library.BaseLibrary
{
    public class Base_Function
    {
        public static void ResartServices(string serviceName)
        {
            //service
            //string serviceName = "Tomcat9";
            int timeoutMilliseconds = 200000;
            ServiceController service = new ServiceController(serviceName);
            int millisec1 = 0;
            TimeSpan timeout;
            if (service.Status == ServiceControllerStatus.Running)
            {
                millisec1 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            }
            // count the rest of the timeout
            int millisec2 = Environment.TickCount;
            timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));
            service.Start();
            service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            
        }

        public static void AddConfigKey(string path,string Key)
        {
            //add key
            FileStream fs = new FileStream(path, FileMode.Append);//
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(Key);//"NET_REMOVAL_REQUIRE_TARGET_TARE = 0"
            sw.Close();
            Base_logger.Info("Add config key successfully.");

        }

        public static void DeleteConfigKey(string path,string Key)
        {
            //NET_REMOVAL_REQUIRE_TARGET_TARE = 0
            //string KeyName = Key.Split('=')[0];

            //delete key
            string all = File.ReadAllText(path);
            //string pattern = $"{KeyName} = " + @"\d{1}";
            string pattern = Key;
            all = Regex.Replace(all, pattern, "");//@"NET_REMOVAL_REQUIRE_TARGET_TARE = \d{1}"
            //Console.WriteLine(all);
            File.WriteAllText(path, all);
            Base_logger.Info("Delete config key successfully.");

        }
    }        
}

