using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(106692)]
        [Title("V8.8.6_CQ00775250:Double signature dialog should not show full user name")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_106692()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string xml = "10 aspen wd signautres_106692 bulk load.xml";

            LogStep(@"1. import Signatures xml");
            WD_Fuction.Bulkload(xml);
            WD_Fuction.WDSign();
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.Signatures.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "clean booth Signatures-Setting.PNG");
            LogStep(@"2. open wd client and do booth clean");
            Application.LaunchWDAndLogin();
            WD.mainWindow.HomeInternalFrame.BoothCleaning.Click();
            WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
            LogStep(@"3. check signatures");
            //signature1 not show full user name
            WD.mainWindow.GetSnapshot(Resultpath + "clean booth signature1.PNG");
            string signature1 = WD.mainWindow.Dialog.UserID.Text;
            Base_Assert.AreEqual("", signature1, "check signature1 not show full username");
            WD.mainWindow.Dialog.UserID.SetText(UserName.qaone1);
            WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone1);
            WD.mainWindow.Dialog.OK.Click();
            //signature2 not show full user name
            WD.mainWindow.GetSnapshot(Resultpath + "clean booth signature2.PNG");
            string signature2 = WD.mainWindow.Dialog.UserID.Text;
            Base_Assert.AreEqual("", signature2, "check signature2 not show full username");
            WD.mainWindow.Dialog.UserID.SetText(UserName.qaone2);
            WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone2);
            WD.mainWindow.Dialog.OK.Click();
            //check Finish Clean
            Base_Assert.IsTrue(WD.mainWindow.HomeInternalFrame.IsExist(), "Finish Clean");
            WD_Fuction.Close();
            driver.Close();

        }
    }
}
