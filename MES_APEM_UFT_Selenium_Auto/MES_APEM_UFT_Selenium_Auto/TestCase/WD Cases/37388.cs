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

        [TestCaseID(37388)]
        [Title("V8.8.4_<Dispensation with insufficient quantity> deviation to test dispensing quantity out of range")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_37388()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test1";
            string material = WDMaterial.X0125;
            string method = WDMethod.Net;
            string barcode = "X0125001";
            string tare = "10";
            string net1 = "30";
            string net2 = "454.4";
            string material2 = WDMaterial.M801890;
            string method2 = WDMethod.Netremoval;
            string barcode2 = "M801890002";
            string source_start = "500";
            string source_left1 = "400";
            string source_left2 = "300";

            string xml = "14 aspen wd deviation_37388 bulk load.xml";
            

            LogStep(@"1. Not check prevent deviation");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            //deviation
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.Deviations.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Dispensation with insufficient quantity not check prevent.PNG");
            //active order
            Web_Fuction.gotoTab(WDWebTab.order);
            driver.Wait();
            Web_Fuction.active_order(order);
            driver.Wait();
            LogStep(@"2. Dispense order");
            //Order Dispense
            Application.LaunchWDAndLogin();
            WD_Fuction.SelectOrderandMaterial(order, material);
            WD_Fuction.SelectMehod(method, barcode);
            //zero
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            //tare
            WD.SimulatorWindow.weight.SetText(tare);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            Thread.Sleep(2000);
            //weight1
            WD.SimulatorWindow.weight.SetText(net1);
            WD.SimulatorWindow.OK.Click();
            Thread.Sleep(1000);
            //check accept button enable
            Base_Assert.IsTrue(WD.mainWindow.ScaleWeightInternalFrame.accept.IsEnabled, "Not check prevent1");
            WD.mainWindow.GetSnapshot(Resultpath + "Not check prevent1.PNG");
            //weight2
            WD.SimulatorWindow.weight.SetText(net2);
            WD.SimulatorWindow.OK.Click();
            Thread.Sleep(1000);
            //check accept button enable
            Base_Assert.IsTrue(WD.mainWindow.ScaleWeightInternalFrame.accept.IsEnabled, "Not check prevent2");
            WD.mainWindow.GetSnapshot(Resultpath + "Not check prevent2.PNG");
            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            Thread.Sleep(1000);
            LogStep(@"3. check prevent deviation");
            WD_Fuction.Bulkload(xml);
            //deviation
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.Deviations.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Dispensation with insufficient quantity check prevent.PNG");
            driver.Close();
            LogStep(@"4. Dispense order");
            WD.mainWindow.MaterialInternalFrame.materialTable.Row(material2).Click();
            WD.mainWindow.MaterialInternalFrame.next.Click();
            if (WD.mainWindow.BoothCleanInternalFrame.IsExist())
            {
                WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
            }
            if (WD.mainWindow.HandleInformationInterFrame.IsExist())
            {
                WD.mainWindow.HandleInformationInterFrame.Acknowledge.ClickSignle();
            }
            WD_Fuction.SelectMehod(method2, barcode2);
            //zero
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            //tare
            WD.mainWindow.ScaleWeightInternalFrame.tare_editor.SetText(tare, true);
            //start weight
            WD.SimulatorWindow.weight.SetText(source_start);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            //input remove weight1
            WD.SimulatorWindow.weight.SetText(source_left1);
            WD.SimulatorWindow.OK.Click();
            //check accept button disable
            Base_Assert.IsFalse(WD.mainWindow.ScaleWeightInternalFrame.accept.IsEnabled, "check prevent1");
            WD.mainWindow.GetSnapshot(Resultpath + "check prevent1.PNG");
            //input remove weight2
            WD.SimulatorWindow.weight.SetText(source_left2);
            WD.SimulatorWindow.OK.Click();
            //check accept button enable
            Base_Assert.IsTrue(WD.mainWindow.ScaleWeightInternalFrame.accept.IsEnabled, "check prevent2");
            WD.mainWindow.GetSnapshot(Resultpath + "check prevent2.PNG");
            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            Thread.Sleep(1000);
            WD_Fuction.Close();
        }


    }
}