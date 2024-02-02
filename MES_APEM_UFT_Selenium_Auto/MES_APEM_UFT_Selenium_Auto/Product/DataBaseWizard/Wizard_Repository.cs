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
using MES_APEM_UFT_Selenium_Auto.Product.DataBaseWizard;
using static MES_APEM_UFT_Selenium_Auto.Product.WD.ClassMainWindow;

namespace MES_APEM_UFT_Selenium_Auto.Product.DataBaseWizard
{
    public  class Wizard
    {
        #region Wizard windows



        //public static MenuWindow menuWindow => new MenuWindow("//JavaWindow[@Title = 'Aspen Weigh and Dispense Execution']");

        private static IWindow mainWindow = Desktop.Describe<IWindow>(new WindowDescription
        {
            WindowTitleRegExp = @"Aspen Database Wizard",
            ObjectName = @"frmWizard",
            Text = @"Aspen Database Wizard"
        });



        public static Wizard_Window WizardWindow => new Wizard_Window(mainWindow);



        #endregion

        #region Wizard Dialog
        public static WizardData_Dialog DataLinkPropertiesDialog => new WizardData_Dialog("//Dialog[@Text = 'Data Link Properties']");

        public static STD_Dialog DataLinkCheckDialog => new WizardData_Dialog("//Dialog[@Text = 'Microsoft Data Link']");

        public static STD_Dialog ADSAExitDialog => new STD_Dialog(mainWindow, "//Dialog[@Text = 'Aspen Database Wizard']");
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


