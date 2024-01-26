using System;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Text;
using OpenQA.Selenium;
using System.Linq;
using System.IO;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {

        [TestCaseID(41380)]
        [Title("folder directory for xml download - intergration ERP")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_41380()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string DownloadPath = @"C:\41380Download";
            string DownloadPathBefore = @"C:\ProgramData\AspenTech\AeBRS\WDDownload";
            LogStep(@"1. click the Administration tab-> Integration");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.Integration.Click();
            Thread.Sleep(3000);         
            LogStep(@"2. edit the 'folder directory for xml download'");
            Web.Administration_Page.folder_for_Download.Clear();
            Web.Administration_Page.folder_for_Download.SendKeys(DownloadPath);
            Web.Administration_Page.log_on_required_chx.Click();
            Web.Administration_Page.log_on_required_chx.Click();
            Thread.Sleep(3000);
            Web_Fuction.administration_Apply("Configuration successfully saved");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Config download path.PNG");
            try
            {
                LogStep(@"3. check pending folder under the path");
                Base_Assert.IsTrue(Directory.Exists(DownloadPath + @"\Pending"), "Pending folder exit");
                LogStep(@"4. download the xml");
                //delete the Material/Inventory/order from database
                WD_Fuction.CleanOrdersData();
                WD_Fuction.CleanMaterialData();
                WD_Fuction.CleanInventoryData();

                string sourceName = Base_Directory.ProjectDir + "Data\\Input\\BulkLoad\\07 aspen wd orders bulk load.xml";
                string directoryPath = "C:\\ProgramData\\AspenTech\\AeBRS\\WDDownload\\Pending\\Orders";
                //download error xml
                Base_File.CopyFile(sourceName, directoryPath, false);
                Thread.Sleep(10000);
                Selenium_Driver driver1 = new Selenium_Driver(Browser.chrome);
                Web_Fuction.gotoWDWeb(driver1);
                driver1.Wait();
                Web_Fuction.login();
                driver1.Wait();
                Web_Fuction.gotoTab(WDWebTab.order);
                Thread.Sleep(10000);
                Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "All_orders_Active.PNG");
                int orderCount = driver1.FindElements("//table[@class='Order_Table_body_Style_Collapse']/tbody/tr").Count;
                // Console.WriteLine(orderCount.ToString());
                for (int i = 2; i <= orderCount; i++)
                {
                    var XPath = "//table[@class='Order_Table_body_Style_Collapse']/tbody/tr[" + i.ToString() + "]/td[7]";
                    var orderStatus = driver1.FindElement(XPath).Text;
                    Console.WriteLine(orderStatus);
                    Base_Assert.AreEqual(orderStatus, "Active");
                }
            }
            finally
            {
                //restore
                Web_Fuction.gotoTab(WDWebTab.admin);
                Web.Administration_Page.Integration.Click();
                Thread.Sleep(3000);
                Web.Administration_Page.folder_for_Download.Clear();
                Web.Administration_Page.folder_for_Download.SendKeys(DownloadPathBefore);
                Web.Administration_Page.log_on_required_chx.Click();
                Web.Administration_Page.log_on_required_chx.Click();
                Thread.Sleep(3000);
                Web_Fuction.administration_Apply("Configuration successfully saved");
            }

        }
    }
}