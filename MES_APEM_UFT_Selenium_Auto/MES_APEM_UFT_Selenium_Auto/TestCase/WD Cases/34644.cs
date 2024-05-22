using System;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Collections;
using HP.LFT.SDK.Java;
using OpenQA.Selenium;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(34644)]
        [Title("V8.8.3_CQ00613342:No sequence number in B2MML do not enforce weighing sequence")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_34644()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test6";
            string xml1 = "07 aspen wd orders bulk load No Sequence.xml";

            LogStep(@"1. import order xml when order is plan");
            //import Sequence
            WD_Fuction.Bulkload(xml1);

            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            //get order
            Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath("//input[@class='Tab_Manu_bar_Margin Tab_Menu_Bar_Search_Box']")).Clear();
            Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath("//input[@class='Tab_Manu_bar_Margin Tab_Menu_Bar_Search_Box']")).SendKeys(order);
            Thread.Sleep(1000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Sequence.PNG");
            Base_Assert.IsTrue(Web.Order_Page.OrderTableRows.getElement(1).FindElements(By.TagName("td"))[14].Text == "No", "Sequence");
            Web_Fuction.active_order(order);
            
            driver.Close();
            LogStep(@"2. Execute order");
            //start order
            Application.LaunchWDAndLogin();

            //finish M801890
            WD_Fuction.SelectOrderandMaterial(order, WDMaterial.M801890);
            WD_Fuction.SelectMehod(WDMethod.Net, "M801890001");
            WD_Fuction.FinishNetDiapense("15", "215");
            WD.mainWindow.GetSnapshot(Resultpath + "Finish 2.PNG");
            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            //finish 1072
            WD_Fuction.SelectOrderandMaterial(order, WDMaterial.x1072);
            WD_Fuction.SelectMehod(WDMethod.Net, "1072003");
            WD_Fuction.FinishNetDiapense("15", "215");
            WD.mainWindow.GetSnapshot(Resultpath + "Finish 3.PNG");
            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            //finish x0125
            WD_Fuction.SelectOrderandMaterial(order, WDMaterial.X0125);
            WD_Fuction.SelectMehod(WDMethod.Net, "X0125001");
            WD_Fuction.FinishNetDiapense("15", "459");
            WD.mainWindow.GetSnapshot(Resultpath + "Finish 1.PNG");
            WD_Fuction.Close();
        }

       
    }
}