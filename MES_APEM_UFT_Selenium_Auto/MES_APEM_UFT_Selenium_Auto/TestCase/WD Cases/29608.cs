using System;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Text;
using OpenQA.Selenium;
using System.Linq;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {

        [TestCaseID(29608)]
        [Title("check 'Automatically active released orders' -ERP download")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_29608()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            //System.IO.File.WriteAllText("C:/Users/qaone1/Desktop/eee.txt", Resultpath, Encoding.Default);
            LogStep(@"1. click the Administration tab-> Integration");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.Integration.Click();
            Thread.Sleep(3000);         
            LogStep(@"2. select the 'folder directory for xml download'");
            Web.Administration_Page.folder_for_Download.Clear();
            Web.Administration_Page.folder_for_Download.SendKeys("C:\\ProgramData\\AspenTech\\AeBRS\\WDDownload");
            Thread.Sleep(3000);
            LogStep(@"3. check the 'Automatically active released orders'");
            if (Web.Administration_Page.Automatically_checkbox.GetAttribute("checked") is null)
            {
                Web.Administration_Page.Automatically_checkbox.Click();
                LogStep(@"4. click Apply");
                Web_Fuction.administration_Apply("Configuration successfully saved");
            }
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Automatically_checkbox.PNG");
            driver.Close();
            LogStep(@"5. download the orders");
            //delete the orders from database
            WD_Fuction.CleanOrdersData();
            string sourceName = Base_Directory.ProjectDir + "Data\\Input\\BulkLoad\\07 aspen wd orders bulk load.xml";
            string directoryPath = "C:\\ProgramData\\AspenTech\\AeBRS\\WDDownload\\Pending\\Orders";
            Base_File.CopyFile(sourceName, directoryPath,false);
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
    }
}