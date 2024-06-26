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
        [TestCaseID(1342249)]
        [Title("Inspired by customer - APEM Mobile - Checklist sel color in dark mode")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(1200000)]

        [TestMethod]
        public void VSTS_1342249()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string OrderName1 = "Order1342249";
            string RPLName = "RPL1342249";

            
            
           
            Application.LaunchMocAndLogin();
            LogStep(@"1. import rpl");
            //check bpl exit
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("CASE1342249.zip");
            }
            LogStep(@"2. create order");
            MOC_Fuction.PlanFromRPL(RPLName, OrderName1);
            APEM.ExitApplication();
            LogStep(@"3. login mobile");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            Mobile_Fuction.login();
            LogStep(@"4. check close user session function");
            //on dark mode
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOn_mode(1);
            Thread.Sleep(2000);
            try
            {
                //execute phase for dark mode
                Mobile.Main_Page.ProcessOrder.Click();
                Thread.Sleep(5000);
                Mobile.OrderProcess_Page.OrderSearch.SendKeys(OrderName1);
                Thread.Sleep(5000);
                Mobile.OrderProcess_Page.GotoTracking.Click();
                Thread.Sleep(5000);
                Mobile.OrderTracking_Page.ReadyPhase.Click();
                Thread.Sleep(5000);
                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "checkbox background.PNG");
                var checkbox = driver.FindElements("//table//td")[0].GetAttribute("style");
                string targetSubstring = "background-color: rgb(255, 255, 255);";
                bool containsTarget = checkbox.Contains(targetSubstring);
                Base_Assert.IsTrue(containsTarget, "checkbox background is white");
                Mobile.OrderExecution_Page.CancelButton.Click();
                Thread.Sleep(2000);
                Mobile.OrderExecution_Page.ConfirmYesButton.Click();
                Thread.Sleep(5000);
                
            }
            finally
            {
                //restore data
                Mobile.Main_Page.Setting.Click();
                Mobile.Setting_Page.turnOff_mode(1);
                driver.Close();
            }
            
           
        }
      
    }
}