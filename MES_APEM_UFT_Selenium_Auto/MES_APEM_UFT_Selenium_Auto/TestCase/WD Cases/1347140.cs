using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(1347140)]
        [Title("UC1332305_'Invalid Net...' error message shows when input Non-integer for UOM as EA")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_1347140()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID+"-";

            string order = "testEA";
            string material = WDMaterial.X0125;
            string barcode = "X0125001";

            LogStep(@"1. active order");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            //active order
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            Thread.Sleep(2000);
            driver.Close();
            LogStep(@"2. open wd client and dispense");
            Application.LaunchWDAndLogin();
            WD_Fuction.SelectOrderandMaterial(order, material);
            //input barcode
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys(barcode);
            Thread.Sleep(2000);
            WD.mainWindow.GetSnapshot(Resultpath + "barcode error.PNG");
            string message = "The scanned HU cannot be used because the UOM of the HU does not match the UOM of the BOM.";
            Base_Assert.AreEqual(message, WD.MessageDialog.Lable.Text,"error message");
            WD.MessageDialog.OKButton.Click();
         

            WD.mainWindow.ScaleWeightInternalFrame.cancel.Click();
            Thread.Sleep(5000);
            WD_Fuction.Close();





        }
    }
}
