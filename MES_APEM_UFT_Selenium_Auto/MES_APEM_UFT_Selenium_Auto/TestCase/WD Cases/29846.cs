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
        [TestCaseID(29846)]
        [Title("W&D: If an existing order is not activated, update the existing order with the same order in the download message")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_29846()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test1";
            

            string xml1 = "07 aspen wd orders bulk load.xml";
            string xml2 = "07 aspen wd orders bulk load test1 quantity.xml";
            string xml3 = "07 aspen wd orders bulk load test1 remove.xml";
            string xml4 = "07 aspen wd orders bulk load test1 add.xml";
            
            LogStep(@"1. import order xml when order is plan");
            
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.edit_order(order);
            Thread.Sleep(3000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "initial data.PNG");
            Base_Assert.IsTrue(Web.Order_Page.EditTableRows.getElement(1).FindElements(By.TagName("td"))[6].Text == "400.000", "initial data");//Quantity
            //import quantity
            WD_Fuction.Bulkload(xml2);
            Thread.Sleep(5000);
            Web_Fuction.refresh_order();
            Thread.Sleep(5000);
            Web_Fuction.edit_order(order);
            Thread.Sleep(3000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "change quantity plan.PNG");
            Base_Assert.IsTrue(Web.Order_Page.EditTableRows.getElement(1).FindElements(By.TagName("td"))[6].Text == "800.000", "change quantity");
            //import remove
            WD_Fuction.Bulkload(xml3);
            Thread.Sleep(5000);
            Web_Fuction.refresh_order();
            Thread.Sleep(5000);
            Web_Fuction.edit_order(order);
            Thread.Sleep(3000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "remove plan.PNG");
            Base_Assert.IsTrue(Web.Order_Page.EditTableRows.Count()==3, "remove");
            // import add
            WD_Fuction.Bulkload(xml4);
            Thread.Sleep(5000);
            Web_Fuction.refresh_order();
            Thread.Sleep(5000);
            Web_Fuction.edit_order(order);
            Thread.Sleep(3000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "add plan.PNG");
            Base_Assert.IsTrue(Web.Order_Page.EditTableRows.Count() == 4, "add");
            LogStep(@"2. import order xml when order is active");
            //restore data
            WD_Fuction.Bulkload(xml1);
            //active order
            Web_Fuction.refresh_order();
            Thread.Sleep(5000);
            Web_Fuction.active_order(order);
            //import quantity
            WD_Fuction.Bulkload(xml2);
            Thread.Sleep(5000);
            Web_Fuction.refresh_order();
            Thread.Sleep(5000);
            Web_Fuction.edit_order(order);
            Thread.Sleep(3000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "change quantity active.PNG");
            Base_Assert.IsTrue(Web.Order_Page.EditTableRows.getElement(1).FindElements(By.TagName("td"))[6].Text == "400.000", "change quantity");
            //import remove
            WD_Fuction.Bulkload(xml3);
            Thread.Sleep(5000);
            Web_Fuction.refresh_order();
            Thread.Sleep(5000);
            Web_Fuction.edit_order(order);
            Thread.Sleep(3000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "remove active.PNG");
            Base_Assert.IsTrue(Web.Order_Page.EditTableRows.Count() == 4, "remove");
            // import add
            WD_Fuction.Bulkload(xml4);
            Thread.Sleep(5000);
            Web_Fuction.refresh_order();
            Thread.Sleep(5000);
            Web_Fuction.edit_order(order);
            Thread.Sleep(3000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "add active.PNG");
            Base_Assert.IsTrue(Web.Order_Page.EditTableRows.Count() == 4, "add");
            LogStep(@"3. import order xml when order is started");
            //start order
            Application.LaunchWDAndLogin();
            WD_Fuction.SelectOrderandMaterial(order, WDMaterial.X0125);
            WD_Fuction.SelectMehod(WDMethod.Net, "X0125001");
            WD_Fuction.FinishNetDiapense("15", "459");
            //import quantity
            WD_Fuction.Bulkload(xml2);
            Thread.Sleep(5000);
            Web_Fuction.refresh_order();
            Thread.Sleep(5000);
            Web_Fuction.edit_order(order);
            Thread.Sleep(3000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "change quantity started.PNG");
            Base_Assert.IsTrue(Web.Order_Page.EditTableRows.getElement(1).FindElements(By.TagName("td"))[6].Text == "400.000", "change quantity");
            //import remove
            WD_Fuction.Bulkload(xml3);
            Thread.Sleep(5000);
            Web_Fuction.refresh_order();
            Thread.Sleep(5000);
            Web_Fuction.edit_order(order);
            Thread.Sleep(3000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "remove started.PNG");
            Base_Assert.IsTrue(Web.Order_Page.EditTableRows.Count() == 4, "remove");
            // import add
            WD_Fuction.Bulkload(xml4);
            Thread.Sleep(5000);
            Web_Fuction.refresh_order();
            Thread.Sleep(5000);
            Web_Fuction.edit_order(order);
            Thread.Sleep(3000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "add started.PNG");
            Base_Assert.IsTrue(Web.Order_Page.EditTableRows.Count() == 4, "add");


            driver.Close();
            WD_Fuction.Close();
        }

       
    }
}