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
        [TestCaseID(106694)]
        [Title("V8.8.6_CQ00775250: Deviation dialog should show full user name_Single signature on WD client")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Created)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_106694()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID+"-";
            string xml = "14 aspen wd deviation_106694 bulk load.xml";
            string order = "test1";
            string material = WDMaterial.X0125;
            string method = WDMethod.Net;
            string barcode = "X0125001";
            string tare = "10";
            string net = "300";

            LogStep(@"1. import deviation xml");
            WD_Fuction.Bulkload(xml);
            WD_Fuction.WDSign();
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.Deviations.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "insufficient quantity deviation-Setting.PNG");
            //active order
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            LogStep(@"2. open wd client and do booth clean");
            Application.LaunchWDAndLogin();
            WD_Fuction.SelectOrderandMaterial(order, material);
            WD_Fuction.SelectMehod(method, barcode);
            Thread.Sleep(3000);
            #region Finish Dispense
            //zeor
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            //tare
            WD.SimulatorWindow.weight.SetText(tare);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            //weight
            WD.SimulatorWindow.weight.SetText(net);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            #endregion
            Thread.Sleep(2000);
            LogStep(@"3. check deviation");
            //username show full user name
            WD.mainWindow.GetSnapshot(Resultpath + "insufficient quantity deviation.PNG");
            string username = WD.mainWindow.Dialog.UserID.Text;
            Base_Assert.AreEqual(FulluserNameWD.qaone1, username, "check username show full username");
            Base_Assert.IsFalse(WD.mainWindow.Dialog.UserID.IsEnabled,"username editable");
            WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone1);
            WD.mainWindow.Dialog.OK.Click();
            //check Finish Dispense
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            Base_Assert.IsTrue(WD.mainWindow.Material_SelectionInternalFrame.IsExist() || WD.mainWindow.MaterialInternalFrame.IsExist(), "Finish Dispense");
            driver.Close();
            WD_Fuction.Close();
        }
    }
}
