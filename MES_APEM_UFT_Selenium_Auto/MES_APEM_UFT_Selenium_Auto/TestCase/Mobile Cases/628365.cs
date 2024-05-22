using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System;
using OpenQA.Selenium;
using HP.LFT.SDK;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Application = MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary.Application;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class Mobile_TestCase
    {
        [TestCaseID(628365)]
        [Title("UC558903_APEM mobile-Check the queue shows correctly on execution page")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_628365()
        {

            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string order = "Case628365";
            string RPL = "RPL628365";

            LogStep(@"1. import templete");
            Application.LaunchMocAndLogin();
            //check bpl exit
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPL).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("CASE628365.zip");
            }
            LogStep(@"2. create ORDER");
            MOC_Fuction.PlanFromRPL(RPL, order);
            APEM.ExitApplication();
            LogStep(@"3. APEM Mobile check queue");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            Mobile_Fuction.login();
            Thread.Sleep(5000);
            Mobile.OrderProcess_Page.OrderSearch.SendKeys(order);
            Thread.Sleep(5000);
            //go to tracking page,execute the phase
            Mobile.OrderProcess_Page.GotoTracking.Click();
            driver.Wait(1000);
            Mobile.OrderTracking_Page.ExecutionButton.Click();
            Thread.Sleep(2000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "user1_queue1.PNG");
            Match match1 = Regex.Match(Mobile.OrderExecution_Page.QueueText.Text(), @"\((\d+)\)");
            string number1 = match1.Groups[1].Value;
            Base_Assert.AreEqual("0",number1);
            //go to PFC
            Mobile.Main_Page.Tracking.Click();
            Thread.Sleep(2000);
            Mobile.OrderTracking_Page.PFCButton.Click();
            Thread.Sleep(2000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "user1_queue2.PNG");
            Match match2 = Regex.Match(Mobile.OrderExecution_Page.QueueText.Text(), @"\((\d+)\)");
            string number2 = match2.Groups[1].Value;
            Base_Assert.AreEqual("1", number2);

            //login user2
            Selenium_Driver edge = new Selenium_Driver(Browser.edge);
            Mobile_Fuction.gotoApemMobile(edge);
            Mobile_Fuction.login(UserName.qaone2, PassWord.qaone2);
            Mobile.OrderProcess_Page.OrderSearch.SendKeys(order);
            Thread.Sleep(5000);
            //go to tracking page,execute the phase
            Mobile.OrderProcess_Page.GotoTracking.Click();
            Thread.Sleep(5000);
            Mobile.OrderTracking_Page.ReadyPhase.Click();
            Thread.Sleep(5000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "user2_queue1.PNG");
            Match match3 = Regex.Match(Mobile.OrderExecution_Page.QueueText.Text(), @"\((\d+)\)");
            string number3 = match3.Groups[1].Value;
            Base_Assert.AreEqual("0", number3);
            //execute another phase
            Mobile.Main_Page.Tracking.Click();
            Thread.Sleep(2000);
            Mobile.OrderTracking_Page.ReadyPhase.Click();
            Thread.Sleep(5000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "user2_queue2.PNG");
            Match match4 = Regex.Match(Mobile.OrderExecution_Page.QueueText.Text(), @"\((\d+)\)");
            string number4 = match4.Groups[1].Value;
            Base_Assert.AreEqual("1", number4);
            //go to PFC
            Mobile.Main_Page.Tracking.Click();
            Thread.Sleep(2000);
            Mobile.OrderTracking_Page.PFCButton.Click();
            Thread.Sleep(2000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "user2_queue3.PNG");
            Match match5 = Regex.Match(Mobile.OrderExecution_Page.QueueText.Text(), @"\((\d+)\)");
            string number5 = match5.Groups[1].Value;
            Base_Assert.AreEqual("2", number5);
            //Finish order
            Mobile.OrderTracking_Page.QueueButton.Click();
            Thread.Sleep(2000);
            string phase = Mobile.OrderTracking_Page.PhaseName.Text();
            Mobile.OrderTracking_Page.QueueExecut.Click();
            Thread.Sleep(2000);
            //check go to executing
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "user2_to_Executing page.PNG");
            Base_Assert.IsTrue(edge.GetUrl().Contains("execution"), "execution page");
            Base_Assert.AreEqual(phase, Mobile.OrderExecution_Page.PhaseTitle.Text(), "Same phase");
            Mobile.OrderExecution_Page.OKButton.Click();
            Thread.Sleep(5000);

            Mobile.OrderTracking_Page.QueueButton.Click();
            Thread.Sleep(2000);
            Mobile.OrderTracking_Page.QueueExecut.Click();
            Thread.Sleep(2000);
            Mobile.OrderExecution_Page.OKButton.Click();
            Thread.Sleep(5000);
            //Finish order
            edge.SwitchToChrome();
            Mobile.OrderTracking_Page.QueueButton.Click();
            Thread.Sleep(2000);
            Mobile.OrderTracking_Page.QueueExecut.Click();
            Thread.Sleep(2000);
            Mobile.OrderExecution_Page.OKButton.Click();
            Thread.Sleep(5000);
            driver.Close();
        }
    }
}