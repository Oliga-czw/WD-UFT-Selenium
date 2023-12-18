using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;

namespace MES_APEM_UFT_Selenium_Auto.Product.ApemMobile
{

    class Mobile
    {
        public static Selenium_Driver driver => new Selenium_Driver(Browser.chrome);
        public static Mobile_Page Mobile_Page => new Mobile_Page(Selenium_Driver._Selenium_Driver);
        public static Login_Page Login_Page => new Login_Page(Selenium_Driver._Selenium_Driver);
        public static Main_Page Main_Page => new Main_Page(Selenium_Driver._Selenium_Driver);
        public static EventLog_Page EventLog_Page => new EventLog_Page(Selenium_Driver._Selenium_Driver);
        public static OrderProcess_Page OrderProcess_Page => new OrderProcess_Page(Selenium_Driver._Selenium_Driver);
        public static Consolidated_Page Consolidated_Page => new Consolidated_Page(Selenium_Driver._Selenium_Driver);
        public static PrintReport_Page PrintReport_Page => new PrintReport_Page(Selenium_Driver._Selenium_Driver);
        public static OrderTracking_Page OrderTracking_Page => new OrderTracking_Page(Selenium_Driver._Selenium_Driver);
        public static OrderExecution_Page OrderExecution_Page => new OrderExecution_Page(Selenium_Driver._Selenium_Driver);
        public static Setting_Page Setting_Page => new Setting_Page(Selenium_Driver._Selenium_Driver);
    }
}
