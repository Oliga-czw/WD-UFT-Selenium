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
        [TestCaseID(106691)]
        [Title("V8.8.6_CQ00775250:Single signature dialog should show full user name on web")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_106691()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string scale = "simulator";
            string desciption = "Sartorius1";
            string xml = "10 aspen wd signautres_106691 bulk load.xml";

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
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Signatures-Setting.PNG");
            LogStep(@"2. change equipment");
            Web_Fuction.gotoTab(WDWebTab.equipment);
            Web_Fuction.edit_scale(scale);
            Web.Equipment_Page.simultor_description.Clear();
            Web.Equipment_Page.simultor_description.SendKeys(desciption);
            Web.Equipment_Page.simultor_name.Click();
            Web.Equipment_Page.Apply.Click();
            Thread.Sleep(2000);
            LogStep(@"3. check signatures");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Signature1.PNG");
            var inputs = Web.Equipment_Page.FindElements("//input[@class='Input_TextBox_Style']");
            Base_Assert.IsFalse(inputs[0].Enabled, "disable username");
            string username = inputs[0].GetAttribute("value");
            Base_Assert.AreEqual(FulluserNameWeb.qaone1,username,"full username");
            //input reason
            Web.Equipment_Page.FindElement("//textarea[@class='DialogTextArea']").SendKeys("signature test");
            inputs[1].SendKeys(PassWord.qaone1);
            Web.Equipment_Page.FindElement("//button[text()='OK']").Click();
            Thread.Sleep(2000);
            LogStep(@"4. check the data");
            Web_Fuction.edit_scale(scale);
            string des = Web.Equipment_Page.simultor_description.GetAttribute("value");
            Base_Assert.AreEqual(desciption, des, "changed data");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Signature2.PNG");
            driver.Close();
        }
    }
}
