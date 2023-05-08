using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

using System.Threading;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;
using WD_UFT_Selenium_Auto.Product.WD;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(728667)]
        [Title("Inspired from customer defect 631704 - when deviation (Scale limit not suitable for this weigh task) is not created and allowed to proceed if cancel is clicked")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Created)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_728667()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID+"-";
            string xml = "14 aspen wd deviation_728667 bulk load.xml";
            string xml2 = "02 aspen wd scales_728667 bulk load.xml";
            string order = "test1";
            string material = WDMaterial.X0125;
            string method = WDMethod.Net;
            string barcode = "X0125001";
            string simulator = "simulator";

            LogStep(@"1. import deviation and scale xml");
            WD_Fuction.Bulkload(xml);
            WD_Fuction.Bulkload_Overwrite(xml2);
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.Deviations.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "outside scale limits deviation-Setting.PNG");
            //active order
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            LogStep(@"2. open wd client and do dispense");
            Application.LaunchWDAndLogin();
            WD_Fuction.SelectOrderandMaterial(order, material);
            WD_Fuction.SelectMehod(method, barcode);
            Thread.Sleep(2000);
            //Weight outside scale limits error message
            string lable = WD.MessageDialog.Lable.Text;
            Base_Assert.AreEqual("simulator  :  Weight outside scale limits.", lable, "error message");
            WD.MessageDialog.OKButton.Click();
            LogStep(@"3. check outside scale limits deviation");
            WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(simulator);
            Thread.Sleep(2000);
            WD.mainWindow.GetSnapshot(Resultpath + "outside scale limits deviation.PNG");
            WD.mainWindow.Dialog.Cancel.Click();
            Thread.Sleep(2000);
            //weighing process shouldn't be allowed to continue.
            WD.mainWindow.GetSnapshot(Resultpath + "can not to continue.PNG");
            bool enable = WD.mainWindow.ScaleWeightInternalFrame.zero.IsEnabled;
            Base_Assert.IsFalse(enable, "can not to continue");
            //exit dispense
            WD.mainWindow.ScaleWeightInternalFrame.reset.ClickSignle();
            Thread.Sleep(5000);
            WD.MessageDialog.OKButton.Click();
            WD.mainWindow.ScaleWeightInternalFrame.cancel.Click();
            driver.Close();
            WD_Fuction.Close();
        }
    }
}
