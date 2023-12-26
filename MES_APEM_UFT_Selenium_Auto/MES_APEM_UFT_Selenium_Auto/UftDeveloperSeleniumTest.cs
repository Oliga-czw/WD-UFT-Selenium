using HP.LFT.SDK;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System.Windows.Forms;
using System;
using HP.LFT.SDK.UIAPro;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;
using OpenQA.Selenium;
using System.Drawing;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using System.IO;

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
            SdkConfiguration config = new SdkConfiguration();
            SDK.Init(config);
            Thread.Sleep(3000);
            //Application.LaunchMocAndLogin();
            //MOC_TemplatesFunction.Importtemplates("TEMP912584.zip");

            //APEM.MocmainWindow.Config_moudle.Click();
            //Thread.Sleep(5000);
            //APEM.MOCConfigWindow.Export.ClickSignle();
            //Thread.Sleep(2000);
            //APEM.MOCConfigWindow.ConfigExportDialog.HomeButton.ClickSignle();
            //APEM.MOCConfigWindow.ConfigExportDialog.FileName.SetText("EN914582");
            //APEM.MOCConfigWindow.ConfigExportDialog.ExportToFileButton.ClickSignle();
            string filePath = "C:\\Users\\qaone1\\Desktop\\EN914582.ini";
            //string newData = "#Executable BPs in Mobile\r\nWEB_EXECUTABLE_1 = BPL912584_1.CREATE\r\nWEB_EXECUTABLE_2 = BPL912584_2.BP_NONWEB\r\nWEB_EXECUTABLE_3 = BPL912584_3.BP_NOCERTIFY\r\n";
            //using (StreamWriter sw = new StreamWriter(filePath, true))
            //{
            //    sw.WriteLine(newData);  
            //}
            //Console.WriteLine("数据已成功写入.ini文件末尾。");
            //APEM.MOCConfigWindow.Import_ReplaceMerge.ClickSignle();
            //APEM.MOCConfigWindow.ConfigImportDialog.FileName.SendKeys(filePath);
            //MOC_Fuction.ConfigClose();
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            Mobile_Fuction.login();
            Thread.Sleep(5000);
            Mobile.Main_Page.BPList.Click();
            Thread.Sleep(3000);
            //Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "ExecutableBPs.PNG");
            var BPLName = Mobile.BPList_Page.BPListTableRows[0].FindElements(By.TagName("td"))[1].Text;
            var BP_Name = Mobile.BPList_Page.BPListTableRows[0].FindElements(By.TagName("td"))[2].Text;
            var Version = Mobile.BPList_Page.BPListTableRows[0].FindElements(By.TagName("td"))[3].Text;
            var Description = Mobile.BPList_Page.BPListTableRows[0].FindElements(By.TagName("td"))[4].Text;
            //Base_Assert.AreEqual(BPLName, "BPL912584_1");
            //Base_Assert.AreEqual(BP_Name, "CREATE");
            //Base_Assert.AreEqual(Version, "2");
            //Base_Assert.AreEqual(Description, "test");
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOn_mode(1);
            Mobile.Main_Page.BPList.Click();
            Thread.Sleep(3000);
            //Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Drak_ExecutableBPs.PNG");
            var BPLName1 = Mobile.BPList_Page.BPListTableRows[0].FindElements(By.TagName("td"))[1].Text;
            var BP_Name1 = Mobile.BPList_Page.BPListTableRows[0].FindElements(By.TagName("td"))[2].Text;
            var Version1 = Mobile.BPList_Page.BPListTableRows[0].FindElements(By.TagName("td"))[3].Text;
            var Description1 = Mobile.BPList_Page.BPListTableRows[0].FindElements(By.TagName("td"))[4].Text;
            //Base_Assert.AreEqual(BPLName1, "BPL912584_1");
            //Base_Assert.AreEqual(BP_Name1, "CREATE");
            //Base_Assert.AreEqual(Version1, "2");
            //Base_Assert.AreEqual(Description1, "test");
            Mobile.Setting_Page.turnOff_mode(1);
        }





    }

}
