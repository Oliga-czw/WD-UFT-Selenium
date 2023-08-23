
using System;
using System.Diagnostics;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;

namespace MES_APEM_UFT_Selenium_Auto.Product.APEM
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
        //public static void Importtemplates(string filename)
        //{
        //    APEM.MocmainWindow.Templates_moudle.Click();
        //    Thread.Sleep(6000);
        //    APEM.MOCTemplatesWindow.Templates.Import.Select();
        //    Thread.Sleep(3000);
        //    APEM.MOCTemplatesWindow.

        //}
    }

    
}
