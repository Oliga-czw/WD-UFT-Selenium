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

namespace MES_APEM_UFT_Selenium_Auto.Product.WD
{
    public  class mMDM
    {
        #region mMDM windows



        //public static MenuWindow menuWindow => new MenuWindow("//JavaWindow[@Title = 'Aspen Weigh and Dispense Execution']");

        private static IWindow mMDMWizard_Window = Desktop.Describe<IWindow>(new WindowDescription
        {
            WindowTitleRegExp = @"Aspen mMDM Administrator Wizard"
        });

        public static mMDMWizard_Window WizardWindow => new mMDMWizard_Window(mMDMWizard_Window);

        private static IWindow mMDM_Window = Desktop.Describe<IWindow>(new WindowDescription
        {
            Text = As.RegExp("Aspen mMDM Administrator.*")
        });

        public static mMDM_Window mMDMWindow => new mMDM_Window(mMDM_Window);
        

        #endregion

        #region mMDM Dialog
        public static Success_Dialog SuccessDialog => new Success_Dialog(mMDMWizard_Window, "//Dialog[@Text = 'Database Connection']");

        #endregion


        #region mMDM Methods

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


