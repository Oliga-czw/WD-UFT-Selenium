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
        [TestCaseID(40818)]
        [Title("Booth not clean or  scale standardization not done can not do the weighing")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Low)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
  
        public void VSTS_40818()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string xml = "02 aspen wd scales_40818 bulk load.xml";
            string xml2 = "14 aspen wd deviation_40818 bulk load.xml";
            string xml3 = "14 aspen wd deviation bulk load.xml";
            string weight1 = "100";
            string weight2 = "500";
            string order = "test1";
            string order2 = "test2";
            string material = WDMaterial.X0125;

            LogStep(@"1. import xml");
            WD_Fuction.Bulkload_Overwrite(xml);
            WD_Fuction.Bulkload(xml2);
            //active order
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            //active order
            Web_Fuction.gotoTab(WDWebTab.order);
            driver.Wait();
            Web_Fuction.active_order(order);
            Web_Fuction.active_order(order2);
            driver.Wait();
            driver.Close();
            LogStep(@"2. Open WD client and weigh");
            Application.LaunchWDAndLogin();
            //select order and material
            WD.mainWindow.HomeInternalFrame.OrderDispensing.Click();
            //need change select function
            WD.mainWindow.DispensingInternalFrame.orderTable.Row(order).Click();
            WD.mainWindow.DispensingInternalFrame.next.Click();
            WD.mainWindow.MaterialInternalFrame.materialTable.Row(material).Click();
            WD.mainWindow.MaterialInternalFrame.next.Click();
            Thread.Sleep(2000);
            LogStep(@"3. booth clean and scale standardization deviation");
            WD.mainWindow.GetSnapshot(Resultpath + "booth clean deviation.PNG");
            Base_Assert.IsTrue(WD.mainWindow.Dialog.IsExist());
            WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone1);
            WD.mainWindow.Dialog.OK.Click();
            Thread.Sleep(2000);
            WD.mainWindow.GetSnapshot(Resultpath + "scale standardization deviation.PNG");
            Base_Assert.IsTrue(WD.mainWindow.Dialog.IsExist());
            WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone1);
            WD.mainWindow.Dialog.OK.Click();
            Thread.Sleep(2000);
            //go to weigh page
            WD.mainWindow.GetSnapshot(Resultpath + "weigh page after deviatin.PNG");
            Base_Assert.IsTrue(WD.mainWindow.ScaleWeightInternalFrame.IsExist());
            WD.mainWindow.ScaleWeightInternalFrame.cancel.Click();
            Thread.Sleep(2000);
            WD_Fuction.Close();
            LogStep(@"4.change prevent booth clean and scale standardization deviation");
            WD_Fuction.Bulkload(xml3);
            LogStep(@"5. booth clean and scale standardization page");
            Application.LaunchWDAndLogin();
            //select order and material
            WD.mainWindow.HomeInternalFrame.OrderDispensing.Click();
            //need change select function
            WD.mainWindow.DispensingInternalFrame.orderTable.Row(order2).Click();
            WD.mainWindow.DispensingInternalFrame.next.Click();
            WD.mainWindow.MaterialInternalFrame.materialTable.Row(material).Click();
            WD.mainWindow.MaterialInternalFrame.next.Click();
            Thread.Sleep(2000);
            //booth clean and scale standardization page
            WD.mainWindow.GetSnapshot(Resultpath + "booth clean page.PNG");
            Base_Assert.IsTrue(WD.mainWindow.BoothCleanInternalFrame.IsExist());
            WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
            Thread.Sleep(2000);
            //scale standardization page
            WD.mainWindow.GetSnapshot(Resultpath + "scale standardization page.PNG");
            Base_Assert.IsTrue(WD.mainWindow.ScaleCheckInternalFrame.IsExist());
            WD.mainWindow.ScaleCheckInternalFrame.startcheck.Click();
            WD.mainWindow.ScaleCheckInternalFrame.startcheck.Click();
            //check weight1
            WD.mainWindow.CheckWeightInternalFrame.zero.Click();
            WD.SimulatorWindow.weight.SetText(weight1);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.CheckWeightInternalFrame.readScale.ClickSignle();
            //check weight2
            WD.mainWindow.CheckWeightInternalFrame.zero.Click();
            WD.SimulatorWindow.weight.SetText(weight2);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.CheckWeightInternalFrame.readScale.ClickSignle();
            WD.mainWindow.CheckWeightInternalFrame.accept.Click();
            //go to weigh page
            WD.mainWindow.GetSnapshot(Resultpath + "weigh page after scale check.PNG");
            Base_Assert.IsTrue(WD.mainWindow.ScaleWeightInternalFrame.IsExist());
            WD.mainWindow.ScaleWeightInternalFrame.cancel.Click();
            WD_Fuction.Close();
        }
    }
}
