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

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class Mobile_TestCase
    {
        [TestCaseID(916366)]
        [Title("WAIT_MESSAGE works in APEM Mobile")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.Critical)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]
        [TestMethod]
        public void VSTS_916366()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string OrderName = "Order916366";
            string RPLName = "RPL916366";

            Application.LaunchMocAndLogin();
            LogStep(@"1. import bpl");//import bpl
            //check bpl exit
            APEM.MocmainWindow.BPLDesign.Click();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row("BPL916366").Existing)
            {
                MOC_TemplatesFunction.Importtemplates("TEMP916366.zip");
            }
            LogStep(@"2. create orders");
            MOC_Fuction.PlanFromRPL(RPLName, OrderName);
            APEM.ExitApplication();
            LogStep(@"3. login mobile");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            Mobile_Fuction.login();
            LogStep(@"4. Select order to execute");
            Mobile.OrderProcess_Page.OrderSearch.SendKeys(OrderName);
            driver.Wait(5000);
            //go to tracking page,execute the phase
            Mobile.OrderProcess_Page.GotoTracking.Click();
            driver.Wait(1000);
            Mobile.OrderTracking_Page.ExecutionButton.Click();
            do
            {
                Thread.Sleep(1000);
            } while (driver.is_element_exist(Mobile.OrderExecution_Page.Wait_message) == false);
            DateTime currentTime_appears = DateTime.Now;
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Message.PNG");
            do
            {
                Thread.Sleep(1000);
            } while (driver.is_element_exist(Mobile.OrderExecution_Page.CancelButton) == false);
            DateTime currentTime1_disappears = DateTime.Now;
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "ExecutionMain.PNG");
            TimeSpan timeDifference = currentTime1_disappears.Subtract(currentTime_appears);
            int seconds = (int)timeDifference.TotalSeconds;
            Console.WriteLine(seconds);
            Base_Assert.AreEqual(seconds,5);
            Mobile.OrderExecution_Page.CancelButton.Click();
            Thread.Sleep(2000);
            Mobile.OrderExecution_Page.ConfirmYesButton.Click();

            driver.Close();
        }

    }
}