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
        [TestCaseID(1365479)]
        [Title("APEM Mobile -- For a Basic Phase Table with 2 Columns the Drop-Down List will display for Column 2.")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1200000)]

        [TestMethod]
        public void VSTS_1365479()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string RPLname = "RPL1365479";
            string ordername = "ORDER1365479";
            Application.LaunchMocAndLogin();
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLname).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("TEMP1365479.zip");
            }
            MOC_Fuction.PlanFromRPL(RPLname, ordername);
            APEM.MocmainWindow.Close();
            Selenium_Driver chrome_driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(chrome_driver);
            Mobile_Fuction.login();
            Thread.Sleep(5000);
            Mobile.OrderProcess_Page.OrderSearch.SendKeys(ordername);
            Thread.Sleep(5000);
            //go to tracking page
            Mobile.OrderProcess_Page.GotoTracking.Click();
            Thread.Sleep(1000);
            Mobile.OrderTracking_Page.ExecutionButton.Click();
            Thread.Sleep(5000);
            Mobile.OrderExecution_Page.column0_0.Click();
            Mobile.OrderExecution_Page.Value_Options[0].Click();
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "column_1.PNG");
            Base_Assert.IsTrue(Mobile.OrderExecution_Page.column0_0._Selenium_WebElement.GetProperty("value").Contains("A"));

            Mobile.OrderExecution_Page.column0_1.Click();
            Mobile.OrderExecution_Page.Value_Options[2].Click();
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "column_2.PNG");
            Base_Assert.IsTrue(Mobile.OrderExecution_Page.column0_1._Selenium_WebElement.GetProperty("value").Contains("C"));

            Mobile.OrderExecution_Page.column0_0.Click();
            Mobile.OrderExecution_Page.Value_Options[1].Click();
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "modify_column_1.PNG");
            Base_Assert.IsTrue(Mobile.OrderExecution_Page.column0_0._Selenium_WebElement.GetProperty("value").Contains("NNB"));

            Mobile.OrderExecution_Page.column1_0.Click();
            Mobile.OrderExecution_Page.Value_Options[1].Click();
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "select_more.PNG");
            Base_Assert.IsTrue(Mobile.OrderExecution_Page.column1_0._Selenium_WebElement.GetProperty("value").Contains("NNB"));
            Mobile.OrderExecution_Page.CancelButton.Click();
            Thread.Sleep(2000);
            Mobile.OrderExecution_Page.ConfirmYesButton.Click();
            Thread.Sleep(3000);


        }

    }
}
