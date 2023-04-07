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
        [TestCaseID(106695)]
        [Title("V8.8.6_CQ00775250:Deviation dialog should not show full user name_Double signature")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Created)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_106695()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID+"-";
            string xml = "14 aspen wd deviation_106695 bulk load.xml";
            string xml2 = "02 aspen wd scales_106695 bulk load.xml";
            string order = "test1";
            string material = WDMaterial.X0125;
            string simulator = "simulator";
            string method = WDMethod.Net;
            string barcode = "X0125001";
            string tare = "10";
            string net = "454.4";

            LogStep(@"1. import deviation xml");
            WD_Fuction.Bulkload(xml);
            WD_Fuction.Bulkload_Overwrite(xml2);
            WD_Fuction.WDSign();
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.Deviations.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Weight outside scale limits deviation-Setting.PNG");
            //active order
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            LogStep(@"2. open wd client and do dispense");
            Application.LaunchWDAndLogin();
            WD_Fuction.SelectOrderandMaterial(order, material);
            WD_Fuction.SelectMehod(method, barcode);
            Thread.Sleep(3000);
            LogStep(@"3. check deviation");
            //Weight outside scale limits error shows
            WD.mainWindow.GetSnapshot(Resultpath + "Weight outside scale limits error.PNG");
            string message = WD.MessageDialog.Lable.Text;
            Base_Assert.AreEqual("simulator  :  Weight outside scale limits.", message,"error message");
            WD.MessageDialog.OKButton.Click();
            //select scale
            WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(simulator);
            //signature1 not show full user name
            WD.mainWindow.GetSnapshot(Resultpath + "clean booth signature1.PNG");
            string signature1 = WD.mainWindow.Dialog.UserID.Text;
            Base_Assert.AreEqual("", signature1, "check signature1 not show full username");
            WD.mainWindow.Dialog.UserID.SetText(UserName.qaone1);
            WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone1);
            WD.mainWindow.Dialog.OK.Click();
            //signature2 not show full user name
            WD.mainWindow.GetSnapshot(Resultpath + "clean booth signature2.PNG");
            string signature2 = WD.mainWindow.Dialog.UserID.Text;
            Base_Assert.AreEqual("", signature2, "check signature2 not show full username");
            WD.mainWindow.Dialog.UserID.SetText(UserName.qaone2);
            WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone2);
            WD.mainWindow.Dialog.OK.Click();
            //Finish Dispense
            WD_Fuction.FinishNetDiapense(tare,net);
            driver.Close();
            WD_Fuction.Close();
        }
    }
}
