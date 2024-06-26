using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using System.IO;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(818450)]
        [Title("Inspired from customer defect 782254 - W&D - Kitting Label and report shows the barcode as DUMMY")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]
        [Defect("1365979")]
        [TestMethod]
        public void VSTS_818450()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test1";
            string material = WDMaterial.X0125;
            string method = WDMethod.Net;
            string barcode = "X0125001";
            string tare = "10";
            string net = "444.4";
            string xml1 = "13 aspen wd user exits bulk load.xml";
            string xml2 = "13 aspen wd user exits_818450_1 bulk load.xml";
            string xml3 = "13 aspen wd user exits_818450_2 bulk load.xml";

            
            LogStep(@"1. Open WD web and login");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            LogStep(@"2. Active order");
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            LogStep(@"3. Change user exit");
            try
            {
                WD_Fuction.Bulkload(xml3);
                Web_Fuction.gotoTab(WDWebTab.admin);
                Web.Administration_Page.UserExits.Click();
                Web.Administration_Page.UID.Click();
                Web.Administration_Page.Pallets.Click();
                Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Pallets Setting.PNG");
                LogStep(@"4. Open Wd client and finish an order");
                Application.LaunchWDAndLogin();
                //X0125
                WD_Fuction.SelectOrderandMaterial(order, material);
                WD_Fuction.SelectMehod(method, barcode);
                WD_Fuction.FinishNetDiapense(tare,net);
                //M801890
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
                //1072
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
            
                Thread.Sleep(5000);
                LogStep(@"5. Check order kitting");
                WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
                WD.mainWindow.HomeInternalFrame.OrderKitting.Click();
                WD.mainWindow.SelectAnOrderToKittingFrame.orderTable.SelectRows(0);
                WD.mainWindow.SelectAnOrderToKittingFrame.StartKitButton.Click();
                Thread.Sleep(4000);
                //error shows
                WD.mainWindow.GetSnapshot(Resultpath + "error message.PNG");
                Base_Assert.AreEqual("Pallet UID generation error. Please check user exit - UID Generation - Pallets.", WD.MessageDialog.Lable.Text,"Error Text");
                WD.MessageDialog.OKButton.Click();
                LogStep(@"6. Change user exit");
                WD_Fuction.Bulkload(xml2);
                Thread.Sleep(10000);
                Web_Fuction.gotoTab(WDWebTab.admin);
                Web.Administration_Page.KittingScan.Click();
                Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Kitting Scan Setting.PNG");
            
                LogStep(@"7. Check order kitting");
                WD.mainWindow.SelectAnOrderToKittingFrame.orderTable.SelectRows(0);
                WD.mainWindow.SelectAnOrderToKittingFrame.StartKitButton.Click();
                Thread.Sleep(4000);
                //check pallet
                WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys(",test1");
                WD.mainWindow.GetSnapshot(Resultpath + "pallet change.PNG");
                Base_Assert.AreEqual("test1", WD.mainWindow.SelectAnOrderToKittingFrame.Pallet.AttachedText, "pallet change");
                //finish kitting
                int count = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.Rowscount();
                for (int i = 0; i < count; i++)
                {
                    string test01 = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.GetCell(i, "Container").Value.ToString();
                    WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys(test01);
                }
                WD.mainWindow.SelectAnOrderToKittingFrame.accept.ClickSignle();
                if (WD.ErrorDialog.IsExist())
                {
                    WD.ErrorDialog.OKButton.Click();
                }
                WD_Fuction.Close();
                LogStep(@"8. Check order report and reprint label");
                Web_Fuction.gotoTab(WDWebTab.order);
                Thread.Sleep(2000);
                Web.Order_Page.SearchInput.SendKeys("test1");
                Thread.Sleep(5000);
                Web.Order_Page.orderCheckbox.Click();
                Thread.Sleep(2000);
                //reprint label
                Web.Order_Page.ReprintLable.Click();
                Thread.Sleep(2000);
                Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Reprint label Pallets.PNG");
                var row = Web.Order_Page.Labeltablerows;
                for(int i = 1; i < row.Count(); i++)
                {
                    Base_Assert.IsTrue(row.getElement(i).FindElements(By.TagName("td"))[1].Text!=null,"pallet shows");
                }
                Web.Order_Page.ReprintLableClose.Click();
                //order report
                Web.Order_Page.PrintReport.Click();
                Thread.Sleep(10000);
                Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "order report Pallets.PNG");
                var urlll = Selenium_Driver._Selenium_Driver.FindElement(By.XPath("//iframe[@class='gwt-Frame']")).GetAttribute("src");
                string[] parts = urlll.Split('/');
                string ReportFileName = parts[parts.Length - 1];
                string ReportText = Web_Fuction.OrderPrint(ReportFileName);
                Base_Assert.IsTrue(ReportText.Contains(order+"0"),"order report pallets");
                driver.Close();
            }
            finally
            {
                WD_Fuction.Bulkload(xml1);
            }
            //restore user exit
            
        } 

    }
}
