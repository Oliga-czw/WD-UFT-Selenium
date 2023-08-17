
using System;
using System.Diagnostics;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;

namespace MES_APEM_UFT_Selenium_Auto.Product.WD
{
    class MOC_Fuction
    {

       
        public static void AuditClose()
        {

            APEM.MOCAuditWindow.Close();
            APEM.CloseDialog.YesButton.Click();

        }

        public static void MocClose()
        {

            APEM.MocmainWindow.Close();
            APEM.CloseDialog.YesButton.Click();

        }
    }

    
}
