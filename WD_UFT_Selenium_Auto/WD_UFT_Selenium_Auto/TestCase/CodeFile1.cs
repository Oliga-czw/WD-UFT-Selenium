using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using HP.LFT.SDK;


using WD_UFT_Selenium_Auto.Product.WD;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using static WD_UFT_Selenium_Auto.Product.WD.ClassMainWindow;
using WD_UFT_Selenium_Auto.Library.UFTLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HP.LFT.SDK.Java;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(9999)]
        [Title("XXX")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Created)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]


        [TestMethod]
        public void VSTS_9999()
        {
            string order = "test1";
            string material = "X0125";
            string method = "Net weigh";
            string barcode = "X0125001";

            Application.LaunchWDAndLogin();
            WD_Fuction.SelectOrderandMaterial(order, material);
            //dispense
            //WD_Fuction.FinishDiapense(method, barcode);
            WD.ExitApplication();

        }



        //SdkConfiguration config = new SdkConfiguration();
        //SDK.Init(config);
        public static void LaunchWDApp()
        {

            Process process = new Process();
            process.StartInfo.FileName = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Aspen Manufacturing Execution\Aspen Weigh and Dispense Execution.lnk";
            process.StartInfo.RedirectStandardError = false;
            process.Start();
            SdkConfiguration config = new SdkConfiguration();
            SDK.Init(config);
            Thread.Sleep(10000);


        }
        
        public static void ExitApplication()
        {
            
        var aspen = Process.GetProcessesByName("javaw");
            WindowDescription des = new WindowDescription();
            des.Title = "Aspen Weigh and Dispense Execution";
            Keyboard.KeyDown(Keyboard.Keys.Alt);
            Keyboard.PressKey(Keyboard.Keys.F4);

            try { aspen[0].WaitForExit(); }
            catch { Base_Assert.Fail("Failed to close wd."); }
        }
    }
}

