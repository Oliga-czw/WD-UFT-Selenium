using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(29613)]
        [Title("Order kitting :reprint last label")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_29613()
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
            LogStep(@"1. Open Wd client and login");
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD.mainWindow.HomeInternalFrame.OrderDispensing.Click();
            Thread.Sleep(2000);
            WD.mainWindow.DispensingInternalFrame.orderTable.SelectRows(0);
            WD.mainWindow.DispensingInternalFrame.next.Click();
            Thread.Sleep(2000);
            WD.mainWindow.MaterialInternalFrame.materialTable.SelectRows(0);
            WD.mainWindow.MaterialInternalFrame.next.Click();
            if (WD.mainWindow.BoothCleanInternalFrame.IsEnabled)
            {
                WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
            }
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("X0125001");
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            WD.SimulatorWindow.weight.SetText("444");

            WD.SimulatorWindow.OK.Click();

            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }

            WD.mainWindow.MaterialInternalFrame.materialTable.SelectRows(0);
            WD.mainWindow.MaterialInternalFrame.next.Click();
            if (WD.mainWindow.BoothCleanInternalFrame.IsEnabled)
            {
                WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
                
            }
            WD.mainWindow.HandingInternalFrame.AcknowledgeButton.ClickSignle();
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("M801890001");
            Thread.Sleep(2000);
            if (WD.ConfirmationDialog._UFT_Dialog.IsEnabled)
            {
                WD.ConfirmationDialog.YesButton.Click();
            }
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            WD.SimulatorWindow.weight.SetText("180");

            WD.SimulatorWindow.OK.Click();

            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }


            WD.mainWindow.MaterialInternalFrame.materialTable.SelectRows(0);
            WD.mainWindow.MaterialInternalFrame.next.Click();
            if (WD.mainWindow.BoothCleanInternalFrame.IsEnabled)
            {
                WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
                
            }
            WD.mainWindow.HandingInternalFrame.AcknowledgeButton.ClickSignle();
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("1072003");
            if (WD.ConfirmationDialog._UFT_Dialog.IsEnabled)
            {
                WD.ConfirmationDialog.YesButton.Click();
            }
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            WD.SimulatorWindow.weight.SetText("180");

            WD.SimulatorWindow.OK.Click();

            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            ////order Kitting
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            WD.mainWindow.HomeInternalFrame.OrderKitting.Click();    
            WD.mainWindow.SelectAnOrderToKittingFrame.orderTable.SelectRows(0);
            WD.mainWindow.SelectAnOrderToKittingFrame.StartKitButton.Click();
            //ArrayList KitBarcodeList = new ArrayList();
            Thread.Sleep(4000);
            string test01 = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.GetCell(0, "Container").Value.ToString();
            string test02 = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.GetCell(1, "Container").Value.ToString();
            string test03 = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.GetCell(2, "Container").Value.ToString();
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys("test1");
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys(test01);
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys(test02);
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys(test03);
            WD.mainWindow.SelectAnOrderToKittingFrame.accept.ClickSignle();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            Thread.Sleep(5000);
            WD.mainWindow.SelectAnOrderToKittingFrame.printButton.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            WD.MessageDialog.OKButton.Click();
            Thread.Sleep(2000);
            Web_Fuction.gotoTab(WDWebTab.report);
            driver.FindElement("//div[text()='Label Reprint']").Click();
            Thread.Sleep(2000);
            driver.FindElement("//button[text()='Generate Report']").Click();
            Thread.Sleep(5000);
            int labelReport = driver.FindElements("//*[@class='Report_Paper_Border_Shading']/tbody/tr[4]/td/table/tbody/tr").Count;
            ArrayList firstReportData1 = new ArrayList();
            foreach (var data in driver.FindElements("//*[@class='Report_Paper_Border_Shading']/tbody/tr[4]/td/table/tbody/tr[2]/td"))
            {
                firstReportData1.Add(data.Text);
            }
            firstReportData1.RemoveAt(0);
            LogStep(@"2.Without select any order and click 'Start Kitting' button.");
            WD_Fuction.Close();
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD.mainWindow.HomeInternalFrame.OrderKitting.Click();
            Thread.Sleep(5000);
            WD.mainWindow.SelectAnOrderToKittingFrame.StartKitButton.Click();
            Base_Assert.AreEqual(WD.MessageDialog.Lable.AttachedText, "Please select an order to kit.");
            WD.mainWindow.GetSnapshot(Resultpath + "no_order_startkitting.PNG");
            WD.MessageDialog.OKButton.Click();
            Thread.Sleep(2000);
            WD.MessageDialog.OKButton.Click();
   
            LogStep(@"3.click reprint last label.");
            WD.mainWindow.SelectAnOrderToKittingFrame.printButton.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            WD.MessageDialog.OKButton.Click();
            Thread.Sleep(2000);
            Web_Fuction.gotoTab(WDWebTab.report);
            //web report
            driver.FindElement("//div[text()='Label Reprint']").Click();
            Web_Fuction.gotoTab(WDWebTab.report);
            Thread.Sleep(2000);
            driver.FindElement("//button[text()='Generate Report']").Click();
            Thread.Sleep(5000);
            ArrayList firstReportData2 = new ArrayList();
            foreach (var data in driver.FindElements("//*[@class='Report_Paper_Border_Shading']/tbody/tr[4]/td/table/tbody/tr[2]/td"))
            {
                firstReportData2.Add(data.Text);
            }
            firstReportData2.RemoveAt(0);
            Base_Assert.AreEqual(driver.FindElements("//*[@class='Report_Paper_Border_Shading']/tbody/tr[4]/td/table/tbody/tr").Count, labelReport + 1);
            for (int a = 0; a < firstReportData1.Count; a++)
            {
                Base_Assert.AreEqual(firstReportData1[a],firstReportData2[a]);
            }
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "report.PNG");

        }


    }
}