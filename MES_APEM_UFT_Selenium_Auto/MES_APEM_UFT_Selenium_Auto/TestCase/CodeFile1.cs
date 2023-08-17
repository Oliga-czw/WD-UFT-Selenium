using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using HP.LFT.SDK;


using MES_APEM_UFT_Selenium_Auto.Product.WD;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using static MES_APEM_UFT_Selenium_Auto.Product.WD.ClassMainWindow;
using MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HP.LFT.SDK.Java;
using System.Drawing;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
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
            Application.LaunchWDAndLogin();
            //string order = "test1";
            //string material = "X0125";
            //string method = "Net weigh";
            //string barcode = "X0125001";

            //Application.LaunchWDAndLogin();
            //WD_Fuction.SelectOrderandMaterial(order, material);
            ////dispense
            ////WD_Fuction.FinishDiapense(method, barcode);
            //WD.ExitApplication();


            //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            //    SdkConfiguration config = new SdkConfiguration();
            //    SDK.Init(config);

            //    Mouse.Click(new Point(1164, 633),MouseButton.Left);
        }


        //[System.Runtime.InteropServices.DllImport("user32")]
        //private static extern int mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        ////移动鼠标 
        //const int MOUSEEVENTF_MOVE = 0x0001;
        ////模拟鼠标左键按下 
        //const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        ////模拟鼠标左键抬起 
        //const int MOUSEEVENTF_LEFTUP = 0x0004;
        ////模拟鼠标右键按下 
        //const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        ////模拟鼠标右键抬起 
        //const int MOUSEEVENTF_RIGHTUP = 0x0010;
        ////模拟鼠标中键按下 
        //const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        ////模拟鼠标中键抬起 
        //const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        ////标示是否采用绝对坐标 
        //const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        ////模拟鼠标滚轮滚动操作，必须配合dwData参数
        //const int MOUSEEVENTF_WHEEL = 0x0800;
        ////SdkConfiguration config = new SdkConfiguration();
        ////SDK.Init(config);
        ///
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

