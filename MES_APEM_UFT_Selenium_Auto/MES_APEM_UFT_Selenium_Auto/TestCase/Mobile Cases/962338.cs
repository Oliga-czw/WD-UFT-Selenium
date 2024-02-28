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
        [TestCaseID(962338)]
        [Title("DELETE_ORDER() function behaviour in APEM mobile")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_962338()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string OrderName = "Order962338";
            string RPLName = "RPL962338";

            Application.LaunchMocAndLogin();
            LogStep(@"1. import rpl");
            //check rpl exit
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("Case962338.zip");
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
            Thread.Sleep(5000);
            //go to tracking page,execute the phase
            Mobile.OrderProcess_Page.GotoTracking.Click();
            driver.Wait(1000);
            Mobile.OrderTracking_Page.ExecutionButton.Click();
            Thread.Sleep(10000);
            LogStep(@"5. check API function");
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "debug window shows.PNG");
            //check NUM2STR function
            var Numbers = Mobile.OrderExecution_Page.Numbers.getAll();
            foreach(var number in Numbers){
                string line = number.Text;
                var a = Math.Round(Convert.ToDouble(line.Split(' ')[0]), 3);
                var b = Convert.ToDouble(line.Split(' ')[1]);
                Base_Assert.IsTrue(a == b, "Round work well");
            }
            //close debug window
            Keyboard.PressKey(Keyboard.Keys.Escape);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "debug window close.PNG");
            driver.Close();
        }

    }
}