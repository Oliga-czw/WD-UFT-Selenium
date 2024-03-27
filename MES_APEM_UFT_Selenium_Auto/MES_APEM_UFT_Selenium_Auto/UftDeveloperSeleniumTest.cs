using HP.LFT.SDK;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System.Windows.Forms;
using System;
using HP.LFT.SDK.StdWin;
using System.Diagnostics;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;
using OpenQA.Selenium;
using System.Drawing;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using System.IO;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using System.Collections.Generic;
using MES_APEM_UFT_Selenium_Auto.Product.DataBaseWizard;
using OpenQA.Selenium.Interactions;
using Spire.Pdf;
using System.Text;
using Spire.Pdf.Texts;
using MES_APEM_UFT_Selenium_Auto.Product.APRM;
using MES_APEM_UFT_Selenium_Auto.Product.SQLplus;
using Application = MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary.Application;
using System.Linq;
using Keys = OpenQA.Selenium.Keys;

namespace MES_APEM_UFT_Selenium_Auto
{
    [TestClass]
    public class UftDeveloperSeleniumTest
    {

        [TestInitialize]
        public void TestInitialize()
        {
            Base_logger.GenerateLogFile("TestMethod1");
        }

        [TestMethod]
        public void TestMethod1()
        {
            SdkConfiguration config = new SdkConfiguration();
            SDK.Init(config);
            string RPLname = "WEB_TEST";
            string Ordername = "ORDER2222";
            Library.BaseLibrary.Application.LaunchMocAndLogin();
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLname).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("TEMP771207.zip");
            }
            MOC_Fuction.PlanFromRPL(RPLname, Ordername);
            APEM.MocmainWindow.WorkstationBP.ClickSignle();
            MOC_Fuction.CheckRowSelection();
            Thread.Sleep(3000);
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText(Ordername);
            APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(15000);
            //APEM.PhaseExecWindow.GetSnapshot(Resultpath + "Executing_MOC.PNG");
            var time1 = APEM.PhaseExecWindow.ExecutionInternalFrame.current_time.AttachedText;
            Thread.Sleep(5000);
            var time2 = APEM.PhaseExecWindow.ExecutionInternalFrame.current_time.AttachedText;
            DateTime current_time1 = DateTime.Parse(time1);
            DateTime current_time2 = DateTime.Parse(time2);
            TimeSpan timeDifference = current_time2.Subtract(current_time1);
            int seconds = (int)timeDifference.TotalSeconds;
            Console.WriteLine(seconds);
            //Base_Assert.AreEqual(seconds, 5);
            APEM.PhaseExecWindow.ExecutionInternalFrame.Cancel_Button.ClickSignle();
            Thread.Sleep(1000);
            APEM.PhaseExecWindow.ConfirmationInternalFrame.YesButton.Click();
            Thread.Sleep(2000);
            MOC_Fuction.MocClose();
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            driver.Wait();
            Mobile_Fuction.login();
            driver.Wait();
            Mobile.OrderProcess_Page.OrderSearch.SendKeys(Ordername);
            Thread.Sleep(1000);
            Mobile.OrderProcess_Page.GotoTracking.Click();
            Thread.Sleep(1000);
            Mobile.OrderTracking_Page.ExecutionButton.Click();
            Thread.Sleep(10000);
            //Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "MobileExecute.PNG");
            Thread.Sleep(5000);
            var time_mobile1 = Mobile.OrderExecution_Page.time_label.GetAttribute("innerText");
            Thread.Sleep(5000);
            var time_mobile2 = Mobile.OrderExecution_Page.time_label.Text();
            DateTime current_Mobiletime1 = DateTime.Parse(time_mobile1);
            DateTime current_Mobiletime2 = DateTime.Parse(time_mobile2);
            TimeSpan MobiletimeDifference = current_Mobiletime2.Subtract(current_Mobiletime1);
            int Mobile_seconds = (int)MobiletimeDifference.TotalSeconds;
            Console.WriteLine(Mobile_seconds);
            //Base_Assert.AreEqual(Mobile_seconds, 5);
            Mobile.OrderExecution_Page.CancelButton.Click();
            Thread.Sleep(2000);
            Mobile.OrderExecution_Page.ConfirmYesButton.Click();
            Thread.Sleep(4000);
            driver.Close();

        }
    }

}
