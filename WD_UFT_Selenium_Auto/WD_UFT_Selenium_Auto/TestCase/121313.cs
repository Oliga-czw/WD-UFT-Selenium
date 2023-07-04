using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

using System.Threading;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;
using WD_UFT_Selenium_Auto.Product.WD;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(121313)]
        [Title("V8.8.6_CQ00775250:Deviation dialog should show full user name_Single signature on WD web")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Created)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_121313()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID+"-";
            string xml = "14 aspen wd deviation_121313 bulk load.xml";
            string order = "test1";
            string material = WDMaterial.X0125;
            string method = WDMethod.Net;
            string barcode = "X0125001";
            string tare = "10";
            string net = "454.4";

            LogStep(@"1. import deviation xml");
            WD_Fuction.Bulkload(xml);
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.Deviations.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Material redispense deviation-Setting.PNG");
            //active order
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            LogStep(@"2. open wd client and finish dispense");
            Application.LaunchWDAndLogin();
            WD_Fuction.SelectOrderandMaterial(order, material);
            WD_Fuction.SelectMehod(method, barcode);
            WD_Fuction.FinishNetDiapense(tare, net);
            WD_Fuction.Close();
            Thread.Sleep(3000);
            LogStep(@"3. go to web and redispense");
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(6000);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(6000);
            Web_Fuction.edit_order(order);
            Web.Order_Page.Redispense.Click();
            //select all material to redispense
            driver.FindElements("//td[@class='dialogMiddleCenter']//table[@class='Order_Table_body_Style_Collapse']//input")[0].Click();
            driver.FindElement("//button[text()='Redispense Material']").Click();
            Thread.Sleep(2000);
            //redispense deviation shows
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "redispense deviation.PNG");
            var inputs = Web.Equipment_Page.FindElements("//input[@class='Input_TextBox_Style']");
            Base_Assert.IsFalse(inputs[0].Enabled, "disable username");
            string username = inputs[0].GetAttribute("value");
            //deviation show full username
            Base_Assert.AreEqual(FulluserNameWeb.qaone1, username, "full username");
            //input reason
            Web.Equipment_Page.FindElement("//textarea[@class='DialogTextArea']").SendKeys("redispense deviation test");
            inputs[1].SendKeys(PassWord.qaone1);
            Web.Equipment_Page.FindElement("//button[text()='OK']").Click();
            Thread.Sleep(2000);

            driver.Close();
            
        }
    }
}
