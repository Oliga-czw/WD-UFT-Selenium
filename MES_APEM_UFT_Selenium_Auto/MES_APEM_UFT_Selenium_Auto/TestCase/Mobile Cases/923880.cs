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

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class Mobile_TestCase
    {
        [TestCaseID(923880)]
        [Title("Inspired by customer defect 913770 -- Sorting Criteria is not memorised in APEM Mobile")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_923880()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string RPLName = "FOR_STATUS";
            string Ordername = "ORDER923880";
            try
            {

                Application.LaunchMocAndLogin();
                APEM.MocmainWindow.RPLDesign.ClickSignle();
                Thread.Sleep(2000);
                MOC_Fuction.PlanFromRPL(RPLName, Ordername);
                Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
                Mobile_Fuction.gotoApemMobile(driver);
                driver.Wait();
                Mobile_Fuction.login();
                driver.Wait();
                Mobile.OrderProcess_Page.OrderHeader.Click();
                var sort_order = Mobile.OrderProcess_Page.OrderHeader.GetAttribute("aria-sort");
                Console.WriteLine(sort_order);
                Mobile.OrderProcess_Page.OrderSearch.SendKeys(Ordername);
                Thread.Sleep(1000);
                Mobile.OrderProcess_Page.GotoTracking.Click();
                Thread.Sleep(1000);
                Mobile.OrderTracking_Page.ExecutionButton.Click();
                Thread.Sleep(10000);
                Mobile.Main_Page.ProcessOrder.Click();
                Thread.Sleep(2000);
                var sort2 = Mobile.OrderProcess_Page.OrderHeader.GetAttribute("aria-sort");
                Console.WriteLine(sort2);
                Base_Assert.AreEqual(sort_order, sort2);
                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "order_sort_memorised.PNG");
                Mobile.Main_Page.Setting.Click();
                Thread.Sleep(5000);
                Mobile.Setting_Page.DarkMode.Click();
                Mobile.Main_Page.ProcessOrder.Click();
                Thread.Sleep(2000);
                var sort3 = Mobile.OrderProcess_Page.OrderHeader.GetAttribute("aria-sort");
                Console.WriteLine(sort3);
                Base_Assert.AreEqual(sort_order, sort3);
                Mobile.Main_Page.Setting.Click();
                Thread.Sleep(5000);
                Mobile.Setting_Page.Consolidated.Click();
                Thread.Sleep(2000);
                Mobile.Main_Page.Consolidated.Click();
                Thread.Sleep(2000);
                Mobile.Consolidated_Page.OrderHeader.Click();
                var sort_consolidate = Mobile.Consolidated_Page.OrderHeader.GetAttribute("aria-sort");
                Console.WriteLine(sort_consolidate);

                Mobile.Consolidated_Page.ExecutionButton.Click();
                Thread.Sleep(5000);
                Mobile.Main_Page.Consolidated.Click();
                Thread.Sleep(2000);
                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "consolidateOrder_sort_memorised.PNG");
                var sort_consolidate1 = Mobile.Consolidated_Page.OrderHeader.GetAttribute("aria-sort");
                Console.WriteLine(sort_consolidate1);
                Base_Assert.AreEqual(sort_consolidate, sort_consolidate1);
                Mobile.Main_Page.Setting.Click();
                Thread.Sleep(5000);
                Mobile.Setting_Page.DarkMode.Click();
                Mobile.Main_Page.Consolidated.Click();
                Thread.Sleep(2000);
                var sort_consolidate2 = Mobile.Consolidated_Page.OrderHeader.GetAttribute("aria-sort");
                Console.WriteLine(sort_consolidate2);
                Base_Assert.AreEqual(sort_consolidate, sort_consolidate2);
                Mobile.Main_Page.Setting.Click();
                Thread.Sleep(3000);
                Mobile.Main_Page.Consolidated.Click();
                Thread.Sleep(2000);
            }
            finally
            {
                Mobile.Main_Page.Setting.Click();
                Mobile.Setting_Page.turnOff_mode(1);
                Mobile.Setting_Page.turnOff_mode(2);
            }
        }
    }
}