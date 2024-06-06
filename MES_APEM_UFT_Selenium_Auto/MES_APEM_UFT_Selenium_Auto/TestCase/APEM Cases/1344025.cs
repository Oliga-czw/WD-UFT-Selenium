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
using MES_APEM_UFT_Selenium_Auto.Product.WD;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(1344025)]
        [Title("UC1334643_Audit Module: Check the login records")]
        [TestCategory(ProductArea.MOC)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1200000)]

        [TestMethod]
        public void VSTS_1344025()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            var no_permission_name = Environment.MachineName + "\\administrator";
            string message_Invalid = @"Invalid username or password. Please re-enter domain\username and password.";
            string message_Permission = @"You do not have permission to login. Please contact your system administrator to access.";
            string messageWD_Invalid = @"Username or password is incorrect.";
            string messageWD_Permission = @"User has insufficient permission.";

            ////MOC
            //without permission
            Base_Test.LaunchApp(Base_Directory.MOCDir);
            SdkConfiguration config = new SdkConfiguration();
            SDK.Init(config);
            APEM.MocmainWindow.LogonInternalFrame.userNameEditor.SetText(UserName.qaone3);
            APEM.MocmainWindow.LogonInternalFrame.passwordEditor.SetSecure(PassWord.qaone3);
            APEM.MocmainWindow.LogonInternalFrame.loginbutton.ClickSignle();
            Thread.Sleep(2000);
            APEM.ErrorDialog.OKButton.Click();
            //Invalid password
            APEM.MocmainWindow.LogonInternalFrame.userNameEditor.SetText("qae\\huhuu");
            APEM.MocmainWindow.LogonInternalFrame.passwordEditor.SetSecure("huhuu");
            APEM.MocmainWindow.LogonInternalFrame.loginbutton.Click();
            Thread.Sleep(2000);
            APEM.ErrorDialog.OKButton.Click();
            APEM.MocmainWindow.LogonInternalFrame.userNameEditor.SetText(UserName.qaone1);
            APEM.MocmainWindow.LogonInternalFrame.passwordEditor.SetSecure(PassWord.qaone1);
            APEM.MocmainWindow.LogonInternalFrame.loginbutton.ClickSignle();
            Thread.Sleep(2000);
            ////ApemMobile
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            //without permission
            Mobile_Fuction.login(no_permission_name, PassWord.admin);
            Mobile.Login_Page.username.Clear();
            //Invalid password
            Mobile_Fuction.login("qae\\qaone4", "Aspenhhhhh");
            Mobile.Login_Page.username.Clear();
            //login successfully
            Mobile_Fuction.login(UserName.qaone1, PassWord.qaone1);
            driver.Close();
            ////W&D  web
            Selenium_Driver driver2 = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            //without permission
            Web.Login_Page.username.SendKeys(UserName.qaone3);
            Web.Login_Page.password.SendKeys(PassWord.qaone3);
            Web.Login_Page.login.Click();
            Thread.Sleep(3000);
            Base_Assert.AreEqual(Web.Main_Page.Tabs._Selenium_WebElements.Count, 1);
            Web.Main_Page.Logoff.Click();
            Thread.Sleep(3000);
            //Invalid password
            Web.Login_Page.username.SendKeys("qae\\qaone4");
            Web.Login_Page.password.SendKeys(PassWord.qaone3);
            Web.Login_Page.login.Click();
            Thread.Sleep(3000);
            //login successfully
            Web.Login_Page.username.SendKeys(UserName.qaone1);
            Web.Login_Page.password.SendKeys(PassWord.qaone1);
            Web.Login_Page.login.Click();
            Thread.Sleep(3000);
            driver2.Close();
            ////W&D  client
            //without permission
            Base_Test.LaunchApp(Base_Directory.WDDir);
            Base_Test.Login(UserName.qaone3, PassWord.qaone3);
            WD.MessageDialog.OKButton.Click();
            //Invalid password
            Base_Test.Login("qae\\qaone4", "Aspenhhhhh");
            WD.MessageDialog.OKButton.Click();
            Base_Test.Login(UserName.qaone1, PassWord.qaone1);
            //check Audit Module
            APEM.MocmainWindow.Audit_moudle.Click();
            Thread.Sleep(2000);
            APEM.MOCAuditWindow.Users_Failures.ClickSignle();
            Thread.Sleep(2000);
            APEM.MOCAuditWindow.LoginFailureInterFrame.MaximizeButton.Click();
            APEM.MOCAuditWindow.GetSnapshot(Resultpath + "Audit result.PNG");
            MOC_Fuction.AuditAssert("WDWorkstation", messageWD_Invalid, 1);
            MOC_Fuction.AuditAssert("WDWorkstation", messageWD_Permission, 2);
            MOC_Fuction.AuditAssert("WDServer", message_Invalid, 3);
            MOC_Fuction.AuditAssert("ApemMobileServer", message_Invalid, 4);
            MOC_Fuction.AuditAssert("MOC", message_Invalid, 5);
            MOC_Fuction.AuditAssert("MOC", message_Permission, 6);






        }

    }
}
