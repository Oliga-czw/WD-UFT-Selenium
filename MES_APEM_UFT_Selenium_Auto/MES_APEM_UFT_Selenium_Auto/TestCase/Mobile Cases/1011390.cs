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
        [TestCaseID(1011390)]
        [Title("DELETE_ORDER() function behaviour in APEM mobile")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_1011390()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string OrderName = "Order1011390";
            string RPLName = "SIMPLE";

            string Path = Base_Directory.ConfigDir + "path.m2r_cfg";
            string ConfigKey1 = @"WEB_INACTIVITY_PERIOD = 60";
            string ConfigKey2 = @"WEB_INACTIVITY_PERIOD = 300";

            
            try
            {
                LogStep(@"1. change path config");
                //set config in flag
                Base_Function.EditConfigKey(Path, ConfigKey1);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
                //restart tomcat
                Base_Function.ResartServices(ServiceName.Tomcat);
                Application.LaunchMocAndLogin();
                Thread.Sleep(5000);
                LogStep(@"2. import templete");
                //check bpl exit
                APEM.MocmainWindow.BPLDesign.ClickSignle();
                if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(RPLName).Existing)
                {
                    MOC_TemplatesFunction.Importtemplates("SAMPLE.zip");
                }
                LogStep(@"3. create orders");
                MOC_Fuction.PlanFromRPL(RPLName, OrderName);
                APEM.ExitApplication();
                LogStep(@"4. login mobile");
                Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
                Mobile_Fuction.gotoApemMobile(driver);
                Mobile_Fuction.login();
                LogStep(@"5. Select order to execute");
                Mobile.OrderProcess_Page.OrderSearch.SendKeys(OrderName);
                Thread.Sleep(5000);
                //go to tracking page,execute the phase
                Mobile.OrderProcess_Page.GotoTracking.Click();
                driver.Wait(1000);
                Mobile.OrderTracking_Page.ExecutionButton.Click();
                LogStep(@"6. check Re-enter User dialog");
                //wait for Re-enter User dialog
                Thread.Sleep(120000);
                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Re-enter User dialog.PNG");
                Base_Assert.AreEqual("Re-Enter User", Mobile.Mobile_Page.Title.Text(), "Dialog title");
                //Re-login
                //var User = Mobile.Mobile_Page.Inputs.getElement(0);
                var Password = Mobile.Mobile_Page.Inputs.getElement(1);
                //Base_Assert.AreEqual(UserName.qaone1,User.Text,"USER");
                Password.SendKeys(PassWord.qaone1);
                Mobile.Mobile_Page.Login.Click();
                bool exist = true;
                try
                {
                    var dialog = Mobile.Mobile_Page.Dialog;
                }
                catch
                {
                    exist = false;
                }
                Base_Assert.IsFalse(exist, "Re login Success");
                //Finish order
                Mobile.OrderExecution_Page.OKButton.Click();
                Thread.Sleep(5000);
                driver.Close();
            }
            finally
            {
                LogStep(@"7.restore config key ");
                Base_Function.EditConfigKey(Path, ConfigKey2);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
                //restart tomcat
                Base_Function.ResartServices(ServiceName.Tomcat);
            }

        }
    }
}