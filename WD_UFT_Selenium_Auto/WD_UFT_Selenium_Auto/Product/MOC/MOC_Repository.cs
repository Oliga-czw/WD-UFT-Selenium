﻿using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.UFTLibrary;
using WD_UFT_Selenium_Auto.Product.WD;
using static WD_UFT_Selenium_Auto.Product.WD.ClassMainWindow;

namespace WD_UFT_Selenium_Auto.Product.WD
{
    public  class MOC
    {
        #region MOC windows

        public static MOCMainWindow MocmainWindow => new MOCMainWindow("//JavaWindow[@ObjectName = 'MOC']");
        public static MOCAuditWindow MOCAuditWindow => new MOCAuditWindow("//JavaWindow[@ObjectName = 'Audit']");

        //public static MenuWindow menuWindow => new MenuWindow("//JavaWindow[@Title = 'Aspen Weigh and Dispense Execution']");

        public static IWindow window = UFT_Xpath.GetDesktopWindow<IWindow>("//JavaWindow[@ObjectName = 'MOC']");
        
 

        #endregion

        #region WD Dialog
        public static UFT_Dialog CloseDialog => new UFT_Dialog("//Dialog[@Title = 'Close the Application']");
        public static UFT_Dialog ErrorDialog => new UFT_Dialog("//Dialog[@Title = 'Error']");
        public static UFT_Dialog MessageDialog => new UFT_Dialog("//Dialog[@Title = 'Message']");

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
            MocmainWindow.SetActive();
            Keyboard.KeyDown(Keyboard.Keys.Alt);
            Keyboard.PressKey(Keyboard.Keys.F4);
            if (CloseDialog.IsExist())
            {
                CloseDialog.YesButton.Click();
            }

            try { aspen[0].WaitForExit(10000); }
            catch { Base_Assert.Fail("Failed to close moc."); }
        }
        #endregion

    }
}


