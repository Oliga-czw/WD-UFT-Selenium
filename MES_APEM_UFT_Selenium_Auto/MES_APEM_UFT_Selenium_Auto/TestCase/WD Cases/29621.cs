using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {

        [TestCaseID(29621)]
        [Title("WD Licensing: WD should denied access when no license server configured.")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_29621()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID+"-";
            string LicenseServer = "shslmtest";
            

            LogStep(@"1. Remove license in SLM");
            Application.LaunchSLM();
            SLM.SLMmainWindow.SLMConfigurationWizard.Click();
            //remove
            SLM.SLMConfigWindow.RemoveServer.Click();
            SLM.SLMConfigWindow.Apply.Click();
            //wait for applying
            Thread.Sleep(5000);
            LogStep(@"2. Open WD web and login");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            //wait for loading
            Thread.Sleep(5000);
            //check error
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "web no license.PNG");
            string web_error = "Unable to acquire SLM_RN_PME_WghDsp_Web";
            string message1 = driver.FindElement("//div[@class='gwt-Label License_Block_Msg']").Text;
            Base_Assert.AreEqual(web_error, message1,"web license error check");
            driver.Close();
            LogStep(@"3. Open Wd client");
            Base_Test.LaunchApp(Base_Directory.WDDir);
            //wait for loading
            Thread.Sleep(5000);
            //check error
            WD.mainWindow.GetSnapshot(Resultpath + "client no license.PNG");
            string message2 = WD.LicenseMessageDialog.LicenseLable.Text;
            string client_error = "Unable to acquire SLM_RN_PME_WghDsp";
            Base_Assert.AreEqual(client_error, message2, "client license error check");
            WD.LicenseMessageDialog.OKButton.Click();
            LogStep(@"4. restone data--add license server");
            //add
            SLM.SLMConfigWindow.ServerEdit.SendKeys(LicenseServer);
            SLM.SLMConfigWindow.AddServer.Click();
            //wait for adding
            Thread.Sleep(30000);
            SLM.SLMConfigWindow.Apply.Click();
            //wait for applying
            Thread.Sleep(30000);
            SLM.SLMConfigWindow.Close();
            Thread.Sleep(2000);
            SLM.SLMmainWindow.Close();

        }

    }
}