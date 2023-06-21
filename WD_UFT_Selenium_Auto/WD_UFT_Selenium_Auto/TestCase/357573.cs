using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;
using WD_UFT_Selenium_Auto.Product.WD;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(357573)]
        [Title("V10.0.1_298520:Order can not download from WDDownload folder if set \"Return_No\" for \"Order download\" on \"User exits\"")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Created)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_357573()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID+"-";
            string xml = "13 aspen wd user exits_357573 bulk load.xml";
            string xml2 = "13 aspen wd user exits bulk load.xml";
            string xml3 = "07 aspen wd orders bulk load.xml";

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
            Web.Administration_Page.ERPOrderDownload.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "User Exit Order Download-Setting1.PNG");
            LogStep(@"2. clean Order");
            WD_Fuction.CleanOrdersData();
            LogStep(@"3. put Order Download in folder");
            Base_File.CopyFile(Path.Combine(Base_Directory.BulkLoadDir,xml3), Path.Combine(Base_Directory.OrdersDownloadDir, xml3));
            //wait for download
            Thread.Sleep(10000);
            LogStep(@"4. check Order is empty in web");
            Web_Fuction.gotoTab(WDWebTab.order);
            string js = "return document.evaluate(\"//table[@class='Order_Table_body_Style_Collapse']/tbody/tr[@class]\", document).iterateNext()";
            Base_Assert.IsTrue(driver.execute_script_return(js) == null, "Inventory not Downloaded");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Order not Downloaded .PNG");
            LogStep(@"5. import user exit xml2");
            WD_Fuction.Bulkload(xml2);
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.UserExits.Click();
            Web.Administration_Page.ERPDownload.Click();
            Web.Administration_Page.ERPOrderDownload.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "User Exit Order Download-Setting2.PNG");
            LogStep(@"6. put Order Download in folder again");
            Base_File.CopyFile(Path.Combine(Base_Directory.BulkLoadDir, xml3), Path.Combine(Base_Directory.OrdersDownloadDir, xml3));
            //wait for download
            Thread.Sleep(10000);
            LogStep(@"7. check Order is Downloaded in web");
            Web_Fuction.gotoTab(WDWebTab.order);
            Base_Assert.IsTrue(driver.FindElements("//table[@class='Order_Table_body_Style_Collapse']/tbody/tr[@class]").Count == 5, "Order Downloaded");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Order Downloaded .PNG");
            driver.Close();
        }
    }
}
