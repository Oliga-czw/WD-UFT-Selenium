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
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
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
            Web.Administration_Page.Automatically_checkbox.Click();
            Web.Administration_Page.Automatically_checkbox.Click();
            Web_Fuction.administration_Apply("Configuration successfully saved");
            Thread.Sleep(10000);
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

                string errorName1 = Base_Directory.ProjectDir + @"Data\Input\BulkLoad\07 aspen wd orders bulk load error.xml";
                string errorName2 = Base_Directory.ProjectDir + @"Data\Input\BulkLoad\05 aspen wd inventory bulk load errror.xml";
                string errorName3 = Base_Directory.ProjectDir + @"Data\Input\BulkLoad\04 aspen wd material bulk load error.xml";
                string sucessName1 = Base_Directory.ProjectDir + @"Data\Input\BulkLoad\07 aspen wd orders bulk load.xml";
                string sucessName2 = Base_Directory.ProjectDir + @"Data\Input\BulkLoad\05 aspen wd inventory bulk load.xml";
                string sucessName3 = Base_Directory.ProjectDir + @"Data\Input\BulkLoad\04 aspen wd material bulk load.xml";
                string directoryPath1 = DownloadPath+@"\Pending\Orders";
                string directoryPath2 = DownloadPath + @"\Pending\Inventory";
                string directoryPath3 = DownloadPath + @"\Pending\Material";
                //download error xml
                Base_File.CopyFile(errorName1, directoryPath1, false);
                Base_File.CopyFile(errorName2, directoryPath2, false);
                Base_File.CopyFile(errorName3, directoryPath3, false);
                Thread.Sleep(30000);
                //check folder
                //pending
                Base_Assert.IsFalse(File.Exists(directoryPath2 + @"\05 aspen wd inventory bulk load errror.xml"), "pending inventory not exit");
                Base_Assert.IsFalse(File.Exists(directoryPath1 + @"\07 aspen wd orders bulk load error.xml"), "pending orders not exit");
                Base_Assert.IsFalse(File.Exists(directoryPath3 + @"\04 aspen wd material bulk load error.xml"), "pending material not exit");
                //reject
                Base_Assert.IsTrue(File.Exists(DownloadPath + @"\Rejected\Inventory\05 aspen wd inventory bulk load errror.xml"), "Reject inventory exit");
                Base_Assert.IsTrue(File.Exists(DownloadPath + @"\Rejected\Orders\07 aspen wd orders bulk load error.xml"), "Reject orders exit");
                Base_Assert.IsTrue(File.Exists(DownloadPath + @"\Rejected\Material\04 aspen wd material bulk load error.xml"), "Reject material exit");

                //download success xml
                Base_File.CopyFile(sucessName1, directoryPath1, false);
                Base_File.CopyFile(sucessName2, directoryPath2, false);
                Base_File.CopyFile(sucessName3, directoryPath3, false);
                Thread.Sleep(30000);
                //check folder
                //pending
                Base_Assert.IsFalse(File.Exists(directoryPath2 + @"\05 aspen wd inventory bulk load.xml"), "pending inventory not exit");
                Base_Assert.IsFalse(File.Exists(directoryPath1 + @"\07 aspen wd orders bulk load.xml"), "pending orders not exit");
                Base_Assert.IsFalse(File.Exists(directoryPath3 + @"\04 aspen wd material bulk load.xml"), "pending material not exit");
                //Processed
                Base_Assert.IsTrue(File.Exists(DownloadPath + @"\Processed\Inventory\05 aspen wd inventory bulk load.xml"), "Processed inventory exit");
                Base_Assert.IsTrue(File.Exists(DownloadPath + @"\Processed\Orders\07 aspen wd orders bulk load.xml"), "Processed orders exit");
                Base_Assert.IsTrue(File.Exists(DownloadPath + @"\Processed\Material\04 aspen wd material bulk load.xml"), "Processed material exit");

                //check data in web
                //order
                Web_Fuction.gotoTab(WDWebTab.order);
                Thread.Sleep(10000);
                Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "order Downloaded sucess.PNG");
                Base_Assert.IsTrue(driver.FindElements("//table[@class='Order_Table_body_Style_Collapse']/tbody/tr").Count > 0, "order Downloaded sucess");
                //inventory
                Web_Fuction.gotoTab(WDWebTab.inventory);
                Thread.Sleep(10000);
                Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Inventory Downloaded sucess.PNG");
                Base_Assert.IsTrue(driver.FindElements("//div[text()='1072']").Count > 0, "Inventory Downloaded sucess");
                //material
                Web_Fuction.gotoTab(WDWebTab.material);
                Thread.Sleep(10000);
                Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Material Downloaded sucess.PNG");
                Base_Assert.IsTrue(driver.FindElements("//td[text()='X0125']").Count > 0, "Material Downloaded sucess");

            }
            finally
            {
                //restore
                Web_Fuction.gotoTab(WDWebTab.admin);
                Web.Administration_Page.Integration.Click();
                Thread.Sleep(3000);
                Web.Administration_Page.folder_for_Download.Clear();
                Web.Administration_Page.folder_for_Download.SendKeys(DownloadPathBefore);
                Web.Administration_Page.Automatically_checkbox.Click();
                Web.Administration_Page.Automatically_checkbox.Click();
                Thread.Sleep(3000);
                Web_Fuction.administration_Apply("Configuration successfully saved");
            }

        }
    }
}