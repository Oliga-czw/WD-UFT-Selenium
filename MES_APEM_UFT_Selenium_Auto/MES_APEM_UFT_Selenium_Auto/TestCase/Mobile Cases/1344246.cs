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
        [TestCaseID(1344246)]
        [Title("UC1340154_Users have 'Production Execution web user' permission in AFW only login APEM mobile client")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1200000)]

        [TestMethod]
        public void VSTS_1344246()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            ////MOC
            //without permission
            Base_Test.LaunchApp(Base_Directory.MOCDir);
            SdkConfiguration config = new SdkConfiguration();
            SDK.Init(config);
            APEM.MocmainWindow.LogonInternalFrame.userNameEditor.SetText(UserName.qaone3);
            APEM.MocmainWindow.LogonInternalFrame.passwordEditor.SetSecure(PassWord.qaone3);
            APEM.MocmainWindow.LogonInternalFrame.loginbutton.ClickSignle();
            Thread.Sleep(2000);
            APEM.MocmainWindow.GetSnapshot(Resultpath + "MOC_login_error.PNG");
            Base_Assert.AreEqual(APEM.ErrorDialog.Lable.Text, "You do not have permission to login. Please contact your system administrator to access.");
            APEM.ErrorDialog.OKButton.Click();
            APEM.ExitApplication();
            ////ApemMobile
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver); 
            //login successfully
            Mobile_Fuction.login(UserName.qaone3, PassWord.qaone3);
            Thread.Sleep(5000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Mobile_login_successfully.PNG");
            Base_Assert.IsTrue(driver.GetUrl().Contains("order"));
            driver.Close();

        }
    }
}