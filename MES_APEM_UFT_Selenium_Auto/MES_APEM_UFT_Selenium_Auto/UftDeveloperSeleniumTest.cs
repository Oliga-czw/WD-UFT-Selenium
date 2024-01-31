using HP.LFT.SDK;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System.Windows.Forms;
using System;
using HP.LFT.SDK.StdWin;
using System.Diagnostics;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;
using OpenQA.Selenium;
using System.Drawing;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using System.IO;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using System.Collections.Generic;
using MES_APEM_UFT_Selenium_Auto.Product.DataBaseWizard;
using OpenQA.Selenium.Interactions;
using Spire.Pdf;
using System.Text;
using Spire.Pdf.Texts;
using MES_APEM_UFT_Selenium_Auto.Product.APRM;
using MES_APEM_UFT_Selenium_Auto.Product.SQLplus;
using Application = MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary.Application;

namespace MES_APEM_UFT_Selenium_Auto
{
    [TestClass]
    public class UftDeveloperSeleniumTest
    {

        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void TestMethod1()
        {
            //SdkConfiguration config = new SdkConfiguration();
            //SDK.Init(config);
            //Thread.Sleep(3000);
            string DownloadPath = @"C:\41380Download";
            string DownloadPathBefore = @"C:\ProgramData\AspenTech\AeBRS\WDDownload";

            string errorName1 = Base_Directory.ProjectDir + @"Data\Input\BulkLoad\07 aspen wd orders bulk load error.xml";
            string errorName2 = Base_Directory.ProjectDir + @"Data\Input\BulkLoad\05 aspen wd inventory bulk load errror.xml";
            string errorName3 = Base_Directory.ProjectDir + @"Data\Input\BulkLoad\04 aspen wd material bulk load error.xml";
            string sucessName1 = Base_Directory.ProjectDir + @"Data\Input\BulkLoad\07 aspen wd orders bulk load.xml";
            string sucessName2 = Base_Directory.ProjectDir + @"Data\Input\BulkLoad\05 aspen wd inventory bulk load.xml";
            string sucessName3 = Base_Directory.ProjectDir + @"Data\Input\BulkLoad\04 aspen wd material bulk load.xml";
            string directoryPath1 = DownloadPath + @"\Pending\Orders";
            string directoryPath2 = DownloadPath + @"\Pending\Inventory";
            string directoryPath3 = DownloadPath + @"\Pending\Material";
            Base_File.CopyFile(sucessName1, directoryPath1, false);
            Base_File.CopyFile(sucessName2, directoryPath2, false);
            Base_File.CopyFile(sucessName3, directoryPath3, false);
            Console.WriteLine(File.Exists(directoryPath2 + @"\05 aspen wd inventory bulk load.xml"));
            Console.WriteLine(File.Exists(directoryPath1 + @"\07 aspen wd orders bulk load.xml"));
            Console.WriteLine(File.Exists(directoryPath3 + @"\04 aspen wd material bulk load.xml"));

            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Thread.Sleep(10000);
            Assert.IsTrue(driver.FindElements("//table[@class='Order_Table_body_Style_Collapse']/tbody/tr").Count > 0, "order Downloaded sucess");
            //inventory
            Web_Fuction.gotoTab(WDWebTab.inventory);
            Thread.Sleep(10000);
            Assert.IsTrue(driver.FindElements("//div[text()='1072']").Count > 0, "Inventory Downloaded sucess");
            //material
            Web_Fuction.gotoTab(WDWebTab.material);
            Thread.Sleep(10000);
            Assert.IsTrue(driver.FindElements("//td[text()='X0125']").Count > 0, "Material Downloaded sucess");
            //Console.WriteLine(File.Exists(DownloadPath + @"\Rejected\Inventory\05 aspen wd inventory bulk load errror.xml"));
            //Console.WriteLine(File.Exists(DownloadPath + @"\Rejected\Orders\07 aspen wd orders bulk load error.xml"));
            //Console.WriteLine(File.Exists(DownloadPath + @"\Rejected\Material\04 aspen wd material bulk load error.xml"));
        }





    }

}
