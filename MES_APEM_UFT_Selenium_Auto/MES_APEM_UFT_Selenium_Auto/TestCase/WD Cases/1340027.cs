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
        [TestCaseID(1340027)]
        [Title("UC1332305_'Invalid Net...' error message shows when input Non-integer for UOM as EA")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_1340027()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID+"-";

            string order = "testEA";
            string material = WDMaterial.X0125;
            string barcode = "X012501";
            string scale = "simulator";

            string net = "44.00";

            LogStep(@"1. active order");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            //edit scale
            Web_Fuction.gotoTab(WDWebTab.equipment);
            Web_Fuction.edit_scale(scale);
            Web.Equipment_Page.simultor_status.select_option("Disconnected");
            Web.Equipment_Page.Apply.Click();
            Thread.Sleep(2000);
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
            if (WD.ConfirmationDialog.IsExist())
            {
                WD.ConfirmationDialog.YesButton.Click();
            }
            //dispense with string: a, or a value: 270.0
            WD.mainWindow.ScaleWeightInternalFrame.net_editor.SetText("net", true);
            WD.mainWindow.GetSnapshot(Resultpath + "string error.PNG");
            string message = "Please input a positive integer.";
            Base_Assert.AreEqual(message, WD.MessageDialog.Lable.Text,"string error message");
            WD.MessageDialog.OKButton.Click();
            //value
            WD.mainWindow.ScaleWeightInternalFrame.net_editor.SetText(net, true);
            WD.mainWindow.GetSnapshot(Resultpath + "value error.PNG");
            Base_Assert.AreEqual(message, WD.MessageDialog.Lable.Text, "value error message");
            WD.MessageDialog.OKButton.Click();

            WD.mainWindow.ScaleWeightInternalFrame.cancel.Click();
            Thread.Sleep(5000);
            WD_Fuction.Close();





        }
    }
}
