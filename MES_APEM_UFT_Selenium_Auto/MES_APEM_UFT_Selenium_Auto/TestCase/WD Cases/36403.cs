using System;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Collections;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(36403)]
        [Title("The weight bar zooming should be symmetric when the weight increases or decreases. (from CQ00552879)")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_36403()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test1";

            //active order 
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            LogStep(@"1. conduct a weighing");
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD.mainWindow.HomeInternalFrame.OrderDispensing.Click();
            WD.mainWindow.DispensingInternalFrame.orderTable.SelectRows(0);
            WD.mainWindow.DispensingInternalFrame.next.Click();
            Thread.Sleep(2000);
            WD.mainWindow.MaterialInternalFrame.materialTable.SelectRows(0);
            WD.mainWindow.MaterialInternalFrame.next.Click();
            Thread.Sleep(2000);
            if (WD.mainWindow.BoothCleanInternalFrame.IsEnabled is true)
            {
                WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
                Thread.Sleep(2000);
            }
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("X0125001");
            Thread.Sleep(2000);
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            WD.SimulatorWindow.weight.SetText("400");
            WD.SimulatorWindow.OK.Click();
            Thread.Sleep(3000);
            WD.mainWindow.GetSnapshot(Resultpath + "Min_and_Max1.PNG");
            var TextList1 = WD.mainWindow.ScaleWeightInternalFrame.weighBar.GetVisibleText();
            //Console.WriteLine(TextList1);
            WD.SimulatorWindow.weight.SetText("444");
            WD.SimulatorWindow.OK.Click();
            Thread.Sleep(3000);
            WD.mainWindow.GetSnapshot(Resultpath + "Min_and_Max2.PNG");
            var TextList2 = WD.mainWindow.ScaleWeightInternalFrame.weighBar.GetVisibleText();
            //Console.WriteLine(TextList2);
            WD.SimulatorWindow.weight.SetText("400");
            WD.SimulatorWindow.OK.Click();
            Thread.Sleep(3000);
            WD.mainWindow.GetSnapshot(Resultpath + "Min_and_Max3.PNG");
            var TextList3 = WD.mainWindow.ScaleWeightInternalFrame.weighBar.GetVisibleText();
            //Console.WriteLine(TextList3);
            WD.SimulatorWindow.weight.SetText("444");
            WD.SimulatorWindow.OK.Click();
            Thread.Sleep(3000);
            WD.mainWindow.GetSnapshot(Resultpath + "Min_and_Max4.PNG");
            var TextList4 = WD.mainWindow.ScaleWeightInternalFrame.weighBar.GetVisibleText();
            //Console.WriteLine(TextList4);
            Base_Assert.AreEqual(TextList1,TextList3);
            Base_Assert.AreEqual(TextList2, TextList4);
            WD.mainWindow.ScaleWeightInternalFrame.reset.Click();
            WD.mainWindow.ScaleWeightInternalFrame.cancel.ClickSignle();
            WD_Fuction.Close();
        }

    }
}