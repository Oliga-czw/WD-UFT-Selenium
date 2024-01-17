using HP.LFT.SDK;
using HP.LFT.SDK.WinForms;
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

namespace MES_APEM_UFT_Selenium_Auto.Product.APRM
{
    public  class APRM
    {


        #region APRMAdmin windows

        public static APRMAdmin_Window APRMAdminWindow => new APRMAdmin_Window("//Window[@WindowTitleRegExp = 'Aspen Production Record Manager Administrator .*'] ");

        


        #endregion

        #region Batch Window
        public static BatchMainWindow BatchMainWindow => new BatchMainWindow("//Window[@Text = 'Aspen Production Record Manager Batch Detail Display.*']");



        #endregion
        #region Wizard Dialog
        public static WizardData_Dialog DataDialog => new WizardData_Dialog("//Dialog[@Text = 'Data Link Properties']");

        public static STD_Dialog DataLinkCheckDialog => new WizardData_Dialog("//Dialog[@Text = 'Microsoft Data Link']");

        #endregion


        #region Wizard_Methods

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


