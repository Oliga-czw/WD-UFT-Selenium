using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.ServiceProcess;

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

    }        
}

