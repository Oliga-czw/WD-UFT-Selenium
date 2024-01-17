using HP.LFT.SDK;
using HP.LFT.SDK.Java;
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
    public  class WD
    {
        #region WD windows

        public static ClassMainWindow mainWindow => new ClassMainWindow("//JavaWindow[@Title = 'Aspen Weigh and Dispense Execution']");
        public static IWindow window = UFT_Xpath.GetDesktopWindow<IWindow>("//JavaWindow[@Title = 'Aspen Weigh and Dispense Execution']");
        
        public static SimulatorWindow SimulatorWindow => new SimulatorWindow("//Window[@Title = 'simulator']");
        public static SimulatorWindow SimulatorWindow001 => new SimulatorWindow("//Window[@Title = 'simulator001']");

        #endregion

        #region WD Dialog
        public static UFT_Dialog CloseDialog => new UFT_Dialog("//Dialog[@Title = 'Close the Application']");
        public static UFT_Dialog ErrorDialog => new UFT_Dialog("//Dialog[@Title = 'Error']");
        public static UFT_Dialog MessageDialog => new UFT_Dialog("//Dialog[@Title = 'Message']");
        public static UFT_Dialog DeviationDialog => new UFT_Dialog(window, "//Dialog[@Title = 'Create Deviation']");
        public static UFT_Dialog ConfirmationDialog => new UFT_Dialog(window, "//Dialog[@Title = 'Confirmation']");
        public static UFT_Dialog reopenMessageDialog => new UFT_Dialog("//Dialog[@Title = 'Module Already Running']");

        public static License_Dialog LicenseMessageDialog => new License_Dialog("//Dialog[@Title = 'Licensing problems']");
        #endregion
        #region WD_Controls
        //EX button
        #endregion

        #region WD_Methods
        //EX select order...
        //public static void ExitApplication(bool isSave = false, bool isSaveAsApw = false, string filePath = null)
        //{
        //    var aspen = Process.GetProcessesByName("javaw");
        //    MainWindow.SetActive();
        //    window.Click();
        //    window.SendKeys(FunctionKeys.F4, KeyModifier.Alt);
        //    if (DialogWindow.IsExist())
        //    {
        //        if (isSave)
        //        {
        //            DialogWindow.YesButton.Click();

        //            //if (filePath != null)
        //            //{
        //            //    MainWindow.SaveAsDialog.FileNameEditField.SetText(filePath);
        //            //    MainWindow.SaveAsDialog.SaveButton.Click();
        //            //}

        //            if (isSaveAsApw)
        //                DialogWindow.YesButton.Click();
        //            else
        //                DialogWindow.NoButton.Click();


        //        }
        //        else
        //            DialogWindow.NoButton.Click();
        //    }

        //    try { aspen[0].WaitForExit(); }
        //    catch { Base_Assert.Fail("Failed to close aspen plus."); }
        //}
        public static void ExitApplication()
        {
            var aspen = Process.GetProcessesByName("javaw");
            mainWindow.SetActive();
            Keyboard.KeyDown(Keyboard.Keys.Alt);
            Keyboard.PressKey(Keyboard.Keys.F4);
            if (CloseDialog.IsExist())
            {
                CloseDialog.YesButton.Click();
            }

            try { aspen[0].WaitForExit(10000); }
            catch { Base_Assert.Fail("Failed to close wd."); }
        }
        #endregion

        #region AFW Dialog
        public static Login_Dialog AFWloginDialog => new Login_Dialog("//Dialog[@Text = 'AFW Security Manager' and @Index = '0']");

        public static STD_Dialog AFWSecuredDialog => new STD_Dialog("//Dialog[@Text = 'AFW Security Manager' and @Index = '0']");

        public static STD_Dialog AFWCloseDialog => new STD_Dialog("//Dialog[@Text = 'Microsoft Management Console']");

        public static Property_Dialog AFWPropertyDialog => new Property_Dialog("//Dialog[@Index = '0']");

        public static SelectUser_Dialog AFWSelectUserDialog => new SelectUser_Dialog("//Dialog[@Text = 'Select Users or Groups']");
        #endregion
        #region AFW Window
        public static AFWMainWindow AFWMainWindow => new AFWMainWindow("//Window[@WindowTitleRegExp = 'AFW Security Manager']");



        #endregion
       
    }
}


