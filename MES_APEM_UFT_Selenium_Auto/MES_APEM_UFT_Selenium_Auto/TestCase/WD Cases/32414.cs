using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {

        [TestCaseID(32414)]
        [Title("WD:Signature export/Import")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_32414()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;

            LogStep(@"1. Open WD web and login");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            LogStep(@"2. Go and admin and signature");
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.Signatures.Click();
            Thread.Sleep(3000);
            LogStep(@"3.update the signature and click 'Apply' button.");
            Web.Signature_Page.Reset_Cancel_Weighing_signatures[3].FindElement(By.TagName("input")).Click();
            Web.Signature_Page.NewSource_Signatures[3].FindElement(By.TagName("input")).Click();
            Thread.Sleep(3000);
            Web.Signature_Page.Apply.Click();
            Thread.Sleep(3000);
            Web.Signature_Page.Apply_OK.Click();
            Thread.Sleep(5000);
            LogStep(@"4. Export the signature xml");
            string signautres_file = "10 aspen wd signautres_32414 bulk load.xml";
            WD_Fuction.Bulkload_Export(signautres_file);
            LogStep(@"5.restore the signature data and apply");
            Web.Signature_Page.Reset_Cancel_Weighing_signatures[1].FindElement(By.TagName("input")).Click();
            Web.Signature_Page.NewSource_Signatures[1].FindElement(By.TagName("input")).Click();
            Thread.Sleep(3000);
            Web.Signature_Page.Apply.Click();
            Thread.Sleep(3000);
            Web.Signature_Page.Apply_OK.Click();
            LogStep(@"6.import the signature xml");
            WD_Fuction.Bulkload(signautres_file);
            driver.Refresh();
            Thread.Sleep(5000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "ImportSignature.PNG");
            Assert.IsNotNull(Web.Signature_Page.Reset_Cancel_Weighing_signatures[3].FindElement(By.TagName("input")).GetAttribute("checked"));
            Assert.IsNotNull(Web.Signature_Page.NewSource_Signatures[3].FindElement(By.TagName("input")).GetAttribute("checked"));
           

            
           
            
           
            
            
            
            driver.Close();
        }

    }
}