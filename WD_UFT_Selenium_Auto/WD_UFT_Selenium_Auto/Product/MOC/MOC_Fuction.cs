
using System;
using System.Diagnostics;
using System.Threading;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;

namespace WD_UFT_Selenium_Auto.Product.WD
{
    class MOC_Fuction
    {

       
        public static void AuditClose()
        {

            MOC.MOCAuditWindow.Close();
            MOC.CloseDialog.YesButton.Click();

        }

        public static void MocClose()
        {

            MOC.MocmainWindow.Close();
            MOC.CloseDialog.YesButton.Click();

        }
    }

    
}
