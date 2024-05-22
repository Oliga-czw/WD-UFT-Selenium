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
using Keys = OpenQA.Selenium.Keys;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class Mobile_TestCase
    {
        [TestCaseID(629827)]
        [Title("UC558903_Check the smart footer can work as expected")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.Critical)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(1200000)]

        [TestMethod]
        public void VSTS_629827()
        {



            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string order1 = "Case629827-1";
            string order2 = "Case629827-2";
            string RPL = "RPL629827";
            string Configpath = Base_Directory.ConfigDir + "flags.m2r_cfg";
            string ConfigKey = "EXECUTION_LINE_FOOTER = LineFooter";

            LogStep(@"1. import templete");
            Application.LaunchMocAndLogin();
            //check bpl exit
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPL).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("CASE629827.zip");
            }
            LogStep(@"2. create ORDER");
            MOC_Fuction.PlanFromRPL(RPL, order1);
            MOC_Fuction.PlanFromRPL(RPL, order2);
            APEM.ExitApplication();
            LogStep(@"3. APEM Mobile check footer");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            Mobile_Fuction.login();
            Thread.Sleep(5000);
            Mobile.OrderProcess_Page.OrderSearch.SendKeys(order1);
            Thread.Sleep(5000);
            //go to tracking page,execute the phase
            Mobile.OrderProcess_Page.GotoTracking.Click();
            driver.Wait(1000);
            Mobile.OrderTracking_Page.ExecutionButton.Click();
            Thread.Sleep(8000);
            //check footer
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "footer1.PNG");
            //click cancel
            Mobile.OrderExecution_Page.CancelButton.Click();
            Thread.Sleep(2000);
            Mobile.OrderExecution_Page.ConfirmYesButton.Click();
            Thread.Sleep(4000);
            Base_Assert.IsTrue(driver.GetUrl().Contains("tracking"), "tracking page");
            //click ok,footer dialog
            Mobile.OrderTracking_Page.ExecutionButton.Click();
            Thread.Sleep(2000);
            Mobile.OrderExecution_Page.FooterOKButton.Click();
            Thread.Sleep(2000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "footer2.PNG");
            //input user and password
            Mobile.OrderExecution_Page.MainField0.SendKeys(UserName.qaone1);
            Mobile.OrderExecution_Page.Password.SendKeys(PassWord.qaone1);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "footer3.PNG");
            //finish phase
            Mobile.OrderExecution_Page.Password.SendKeys(Keys.Enter);
            Thread.Sleep(8000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "footer4.PNG");
            Base_Assert.IsTrue(driver.GetUrl().Contains("tracking"), "tracking page");
            driver.Close();
            LogStep(@"4. Edit footer key and check no footer");
            Base_Function.DeleteConfigKey(Configpath, ConfigKey);
            //codify all and restart tomcat
            Base_Test.LaunchApp(Base_Directory.Codify_all);
            Base_Test.KillProcess("tomcat10");
            Thread.Sleep(30000);
            Base_Function.ResartServices(ServiceName.Tomcat);
            Thread.Sleep(240000);
            try
            {
                Selenium_Driver driver2 = new Selenium_Driver(Browser.chrome);
                Mobile_Fuction.gotoApemMobile(driver2);
                Mobile_Fuction.login();
                Thread.Sleep(5000);
                Mobile.OrderProcess_Page.OrderSearch.SendKeys(order2);
                Thread.Sleep(5000);
                //go to tracking page,execute the phase
                Mobile.OrderProcess_Page.GotoTracking.Click();
                Thread.Sleep(2000);
                Mobile.OrderTracking_Page.ExecutionButton.Click();
                Thread.Sleep(10000);
                //check no footer
                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "footer5.PNG");
                Mobile.OrderExecution_Page.OKButton.Click();
                Thread.Sleep(8000);
                Base_Assert.IsTrue(driver2.GetUrl().Contains("tracking"), "tracking page");
                driver2.Close();
            }
            finally
            {
                Base_Function.AddConfigKey(Configpath, ConfigKey);
                //codify all and restart tomcat
                Base_Test.LaunchApp(Base_Directory.Codify_all);
                Base_Test.KillProcess("tomcat10");
                Thread.Sleep(30000);
                Base_Function.ResartServices(ServiceName.Tomcat);
                Thread.Sleep(80000);
            }
        }
    }
}