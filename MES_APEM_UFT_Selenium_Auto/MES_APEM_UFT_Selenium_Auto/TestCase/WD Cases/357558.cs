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
        [TestCaseID(357558)]
        [Title("V10.0.1_298520:Material can not download from WDDownload folder if set \"Return_No\" for \"Order download\" on \"User exits\"")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_357558()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID+"-";
            string xml = "13 aspen wd user exits_357558 bulk load.xml";
            string xml2 = "13 aspen wd user exits bulk load.xml";
            string xml3 = "04 aspen wd material_357558 bulk load.xml";
            string xml4 = "04 aspen wd material bulk load.xml";

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
            Web.Administration_Page.ERPMaterialDownload.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "User Exit Material Download-Setting1.PNG");
            LogStep(@"2. clean Material");
            WD_Fuction.CleanMaterialData();
            LogStep(@"3. put Material Download in folder");
            Base_File.CopyFile(Path.Combine(Base_Directory.BulkLoadDir,xml3), Path.Combine(Base_Directory.MaterialDownloadDir, xml3));
            //wait for download
            Thread.Sleep(10000);
            LogStep(@"4. check Material is empty in web");
            Web_Fuction.gotoTab(WDWebTab.material);
            string js = "return document.evaluate(\"//td[text()='X0125']\", document).iterateNext()";
            Base_Assert.IsTrue(driver.execute_script_return(js) == null, "Material not Downloaded");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Material not Downloaded .PNG");
            LogStep(@"5. import user exit xml2");
            WD_Fuction.Bulkload(xml2);
            Web_Fuction.gotoTab(WDWebTab.admin);
            //Web.Administration_Page.UserExits.Click();
            //Web.Administration_Page.ERPDownload.Click();
            //Web.Administration_Page.ERPOrderDownload.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "User Exit Material Download-Setting2.PNG");
            LogStep(@"6. put Material Download in folder again");
            Base_File.CopyFile(Path.Combine(Base_Directory.BulkLoadDir, xml3), Path.Combine(Base_Directory.MaterialDownloadDir, xml3));
            //wait for download
            Thread.Sleep(10000);
            LogStep(@"7. check Material is Downloaded in web");
            Web_Fuction.gotoTab(WDWebTab.material);
            Base_Assert.IsTrue(driver.FindElements("//td[text()='X0125']").Count == 2, "Material Downloaded");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Material Downloaded .PNG");

            //import all material
            WD_Fuction.Bulkload(xml4);

            driver.Close();
        }
    }
}
