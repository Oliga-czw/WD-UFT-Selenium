using HP.LFT.SDK.StdWin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Product.WD;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestMethod]
        public void Test_AFW()
        {
            //Application.LaunchWDAndLogin();
            //WD.mainWindow.HomeInternalFrame.OrderDispensing.Click();

            //Application.LaunchAFW();

            //string role = AFWRole.Admin;
            //string accout = UserName.qaone2;
            //AFW_Fuction.removeRole(role, accout);

            //AFW_Fuction.closeAFW();

            //get current date
            DateTime t = DateTime.Now;
            Console.WriteLine(t);

            string time = "4/25/23, 8:06:45 AM";
            DateTime t2 = Convert.ToDateTime(time);
           
            Console.WriteLine(t2);
            Console.WriteLine(Math.Abs(t2.Subtract(t).TotalSeconds));
            //service
            //string serviceName = "Tomcat9";
            //int timeoutMilliseconds = 200000;
            //ServiceController service = new ServiceController(serviceName);
            //try
            //{
            //    int millisec1 = 0;
            //    TimeSpan timeout;
            //    if (service.Status == ServiceControllerStatus.Running)
            //    {
            //        millisec1 = Environment.TickCount;
            //        timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
            //        service.Stop();
            //        service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            //        Console.WriteLine("stop success");
            //    }
            //    // count the rest of the timeout
            //    int millisec2 = Environment.TickCount;
            //    timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));
            //    service.Start();
            //    service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            //    Console.WriteLine("start success");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
        }
        
    }
}
