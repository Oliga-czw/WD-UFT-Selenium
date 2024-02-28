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
        [TestCaseID(782513)]
        [Title("UC730341 - BP auto start on order list page/ order tracking page/consolidate page/setting page")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.Critical)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]
        //wait for defect1297888 edit 
        //[TestMethod]
        public void VSTS_782513()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string OrderName = "Order782513";
            string RPLName = "AUTOSTART";

            Application.LaunchMocAndLogin();
            LogStep(@"1. import bpl");//import bpl
            //check bpl exit
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row("AUTOSTART").Existing)
            {
                MOC_TemplatesFunction.Importtemplates("AUTOSTART.zip");
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
            Mobile.OrderProcess_Page.ExecutionButton.Click();
            //go to tracking page and execute-finish first phase
            Mobile.OrderTracking_Page.ExecutionButton.Click();
            Thread.Sleep(5000);
            Mobile.OrderExecution_Page.OKButton.Click();
            Thread.Sleep(15000);
            LogStep(@"4. check phases auto start");
            //second phase auto start
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "2nd phase auto start.PNG");
            Base_Assert.IsTrue(driver.GetUrl().Contains("execution"), "phases auto start");
            //Order list
            Mobile.Main_Page.ProcessOrder.Click();
            Thread.Sleep(15000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Order list-3rd phase auto start.PNG");
            Base_Assert.IsTrue(driver.GetUrl().Contains("execution"), "phases auto start");
            //tracking
            Mobile.Main_Page.Tracking.Click();
            Thread.Sleep(15000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "tracking-4th phase auto start.PNG");
            Base_Assert.IsTrue(driver.GetUrl().Contains("execution"), "phases auto start");
            //Setting
            Mobile.Main_Page.Setting.Click();
            Thread.Sleep(15000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Setting-5th phase auto start.PNG");
            Base_Assert.IsTrue(driver.GetUrl().Contains("execution"), "phases auto start");
            //consolidate 
            try 
            { 
                Mobile.Main_Page.Setting.Click();
                Mobile.Setting_Page.turnOn_mode(2);
                Mobile.Main_Page.Consolidated.Click();
                //click first phase and cancel,phase autostart after cancel
                Mobile.Consolidated_Page.QueueButton.Click();
                int count = Mobile.Consolidated_Page.QueueIcon.Count();
                Mobile.Consolidated_Page.QueueIcon.getElement(0).Click();
                Thread.Sleep(5000);
                Mobile.OrderExecution_Page.CancelButton.Click();
                Thread.Sleep(5000);
                Mobile.OrderExecution_Page.ConfirmYesButton.Click();
                Thread.Sleep(15000);
                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "consolidate -cancel phase auto start.PNG");
                Base_Assert.IsTrue(driver.GetUrl().Contains("execution"), "phases auto start");
                //finish the order
                Mobile.OrderExecution_Page.OKButton.Click();
                Thread.Sleep(15000);
                //finish order in queue
                for (int i =1;i<count;i++) {
                    if (Mobile.Consolidated_Page.QueueButton.isEnable())
                    {
                        Mobile.Consolidated_Page.QueueButton.Click();
                        Mobile.Consolidated_Page.QueueIcon.getElement(0).Click();
                        Thread.Sleep(5000);
                        Mobile.OrderExecution_Page.OKButton.Click();
                        Thread.Sleep(15000);
                    }
                }

            }
            finally
            {
                //off to consolidate 
                Mobile.Main_Page.Setting.Click();
                Mobile.Setting_Page.turnOff_mode(2);
            }
            driver.Close();
        }

    }
}