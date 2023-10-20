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
        //public static Main_Page Main_Page => new Main_Page(Selenium_Driver._Selenium_Driver);
        //public static Iventory_Page Iventory_Page => new Iventory_Page(Selenium_Driver._Selenium_Driver);
        //public static Administration_Page Administration_Page => new Administration_Page(Selenium_Driver._Selenium_Driver);

        //public static Equipment_Page Equipment_Page => new Equipment_Page(Selenium_Driver._Selenium_Driver);
        //public static Order_Page Order_Page => new Order_Page(Selenium_Driver._Selenium_Driver);
        //public static Report_Page Report_Page => new Report_Page(Selenium_Driver._Selenium_Driver);
        //public static CleanRules_Page CleanRules_Page => new CleanRules_Page(Selenium_Driver._Selenium_Driver);
    }
}
