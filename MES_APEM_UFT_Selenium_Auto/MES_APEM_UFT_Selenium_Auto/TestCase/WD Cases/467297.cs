using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(467297)]
        [Title("Deviation -- use 2 scale with different resolution weigh( BOM item weight tolerance 1%)")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_467297()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID+"-";
            string xml = "14 aspen wd deviation_467297 bulk load.xml";

            string order = "test1";
            string material = WDMaterial.X0125;
            string method = WDMethod.Net;
            string barcode = "X0125001";
            string scale1 = "simulator";
            string scale2 = "simulator001";

            LogStep(@"1. import deviation xml");
            WD_Fuction.Bulkload(xml);
            LogStep(@"2. edit scale resolution");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.equipment);
            Thread.Sleep(1000);
            Web_Fuction.edit_scale(scale1);
            Web.Equipment_Page.simultor_Resolution.Clear();
            Web.Equipment_Page.simultor_Resolution.SendKeys("1");
            Web.Equipment_Page.simultor_name.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Scale1.PNG");
            Web.Equipment_Page.Apply.Click();
            Thread.Sleep(2000);
            Web_Fuction.edit_scale(scale2);
            Web.Equipment_Page.simultor_Resolution.Clear();
            Web.Equipment_Page.simultor_Resolution.SendKeys("10");
            Web.Equipment_Page.simultor_name.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Scale2.PNG");
            Web.Equipment_Page.Apply.Click();
            Thread.Sleep(2000);
            LogStep(@"3. Active order");
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            driver.Close();
            LogStep(@"4. check resolution");
            Application.LaunchWDAndLogin();
            WD_Fuction.SelectOrderandMaterial(order, material);
            WD_Fuction.SelectMehod(method, barcode);
            WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(scale1);
            Thread.Sleep(1000);
            WD.mainWindow.GetSnapshot(Resultpath + "Scale1 no insufficient resolution deviation.PNG");
            Thread.Sleep(1000);
            WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(scale2);
            Thread.Sleep(1000);
            WD.mainWindow.GetSnapshot(Resultpath + "Scale2 insufficient resolution deviation.PNG");
            WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone1);
            WD.mainWindow.Dialog.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.cancel.Click();
            Thread.Sleep(3000);
            WD_Fuction.Close();

        }
    }
}
