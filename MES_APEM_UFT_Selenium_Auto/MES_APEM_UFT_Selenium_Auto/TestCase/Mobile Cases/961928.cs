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
        [TestCaseID(961928)]
        [Title("SET_CURRENT_USER function work correctly in APEM mobile.")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_961928()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string OrderName = "Order961928";
            string RPLName = "RPL961928";

            Application.LaunchMocAndLogin();
            LogStep(@"1. import rpl");
            //check rpl exit
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row("RPL961928").Existing)
            {
                MOC_TemplatesFunction.Importtemplates("CASE961928.zip");
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
            //check current user before click API
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "before click API.PNG");
            Base_Assert.AreEqual(UserName.qaone1, Mobile.OrderExecution_Page.Labels.getElement(0).Text,"First user");
            Mobile.OrderExecution_Page.SET_CURRENT_USER_Button.Click();
            Thread.Sleep(5000);
            // check current user after click API
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "after click API.PNG");
            Base_Assert.AreEqual(UserName.qaone2, Mobile.OrderExecution_Page.Labels.getElement(0).Text, "Second user");
            Base_Assert.AreEqual("Yes", Mobile.OrderExecution_Page.Labels.getElement(1).Text, "Second user");
            Mobile.OrderExecution_Page.OKButton.Click();
            Thread.Sleep(15000);
            driver.Close();
        }

    }
}