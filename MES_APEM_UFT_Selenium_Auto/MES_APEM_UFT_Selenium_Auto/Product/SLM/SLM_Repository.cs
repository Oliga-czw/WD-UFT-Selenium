using HP.LFT.SDK;
using HP.LFT.SDK.WPF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using static MES_APEM_UFT_Selenium_Auto.Product.WD.ClassMainWindow;

namespace MES_APEM_UFT_Selenium_Auto.Product.WD
{
    public  class SLM
    {
        #region SLM windows

       

        //public static MenuWindow menuWindow => new MenuWindow("//JavaWindow[@Title = 'Aspen Weigh and Dispense Execution']");

        private static IWindow mainWindow = Desktop.Describe<IWindow>(new WindowDescription
        {
            ObjectName = @"aspenONE SLM License Manager"
        });

        private static IWindow configWindow = Desktop.Describe<IWindow>(new WindowDescription
        {
            ObjectName = @"SLM Configuration Wizard"
        });

        public static SLMMainWindow SLMmainWindow => new SLMMainWindow(mainWindow);


        public static SLMConfigWindow SLMConfigWindow => new SLMConfigWindow(configWindow);

        #endregion

        #region SLM Dialog
        //public static UFT_Dialog CloseDialog => new UFT_Dialog("//Dialog[@Title = 'Close the Application']");

        #endregion


        #region SLM_Methods

        //public static void ExitApplication()
        //{
        //    var aspen = Process.GetProcessesByName("javaw");
        //    MocmainWindow.SetActive();
        //    Keyboard.KeyDown(Keyboard.Keys.Alt);
        //    Keyboard.PressKey(Keyboard.Keys.F4);
        //    if (CloseDialog.IsExist())
        //    {
        //        CloseDialog.YesButton.Click();
        //    }

        //    try { aspen[0].WaitForExit(10000); }
        //    catch { Base_Assert.Fail("Failed to close moc."); }
        //}
        #endregion

    }
}


