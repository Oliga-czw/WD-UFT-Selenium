using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(357570)]
        [Title("V10.0.1_298520:Inventory can not download from WDDownload folder if set \"Return_No\" for \"Inventory download\" on \"User exits\"")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_357570()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID+"-";
            string xml = "13 aspen wd user exits_357570 bulk load.xml";
            string xml2 = "13 aspen wd user exits bulk load.xml";
            string xml3 = "05 aspen wd inventory_357570 bulk load.xml";

            LogStep(@"1. import user exit xml");
            WD_Fuction.Bulkload(xml);
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.UserExits.Click();
            Web.Administration_Page.ERPDownload.Click();
            Web.Administration_Page.ERPInventoryDownload.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "User Exit Inventory Download-Setting1.PNG");
            LogStep(@"2. clean inventory");
            WD_Fuction.CleanInventoryData();
            LogStep(@"3. put Inventory Download in folder");
            Base_File.CopyFile(Path.Combine(Base_Directory.BulkLoadDir,xml3), Path.Combine(Base_Directory.InventoryDownloadDir, xml3));
            //wait for download
            Thread.Sleep(10000);
            LogStep(@"4. check Inventory is empty in web");
            Web_Fuction.gotoTab(WDWebTab.inventory);
            string js = "return document.evaluate(\"//div[text()='1072']\", document).iterateNext()";
            Base_Assert.IsTrue(driver.execute_script_return(js) == null, "Inventory not Downloaded");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Inventory not Downloaded .PNG");
            LogStep(@"5. import user exit xml2");
            WD_Fuction.Bulkload(xml2);
            Web_Fuction.gotoTab(WDWebTab.admin);
            //one session not need
            //Web.Administration_Page.UserExits.Click();
            //Web.Administration_Page.ERPDownload.Click();
            //Web.Administration_Page.ERPInventoryDownload.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "User Exit Inventory Download-Setting2.PNG");
            LogStep(@"6. put Inventory Download in folder again");
            Base_File.CopyFile(Path.Combine(Base_Directory.BulkLoadDir, xml3), Path.Combine(Base_Directory.InventoryDownloadDir, xml3));
            //wait for download
            Thread.Sleep(10000);
            LogStep(@"7. check Inventory is Downloaded in web");
            Web_Fuction.gotoTab(WDWebTab.inventory);
            Base_Assert.IsTrue(driver.FindElements("//div[text()='1072']").Count == 2, "Inventory Downloaded");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Inventory Downloaded .PNG");
            driver.Close();
        }
    }
}
