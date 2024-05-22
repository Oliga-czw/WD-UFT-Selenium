using System;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Collections;
using HP.LFT.SDK.Java;
using OpenQA.Selenium;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(32115)]
        [Title("V8.8.3_CQ00613342:Sequence number in B2MML enforce weighing sequence")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Low)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_32115()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test6";
            string xml1 = "07 aspen wd orders bulk loadSequence.xml";

            LogStep(@"1. import order xml when order is plan");
            //import Sequence
            WD_Fuction.Bulkload(xml1);

            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            //get order
            Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath("//input[@class='Tab_Manu_bar_Margin Tab_Menu_Bar_Search_Box']")).Clear();
            Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath("//input[@class='Tab_Manu_bar_Margin Tab_Menu_Bar_Search_Box']")).SendKeys(order);
            Thread.Sleep(1000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Sequence.PNG");
            Base_Assert.IsTrue(Web.Order_Page.OrderTableRows.getElement(1).FindElements(By.TagName("td"))[14].Text == "Yes", "Sequence");
            Web_Fuction.active_order(order);
            //add booth
            Web_Fuction.edit_order(order);
            Web.Order_Page.AllMaterialCheckBox.Click();
            Web.Order_Page.AssigntoBooth.Click();
            Web.Order_Page.OK.Click();
            Thread.Sleep(2000);
            Web.Order_Page.Order_Apply.Click();
            Thread.Sleep(2000);
            driver.Close();
            LogStep(@"2. Execute order");
            //start order
            Application.LaunchWDAndLogin();
            //select sequence3 X0125
            WD_Fuction.SelectOrderandMaterial(order, WDMaterial.X0125);
            Thread.Sleep(2000);
            //error message
            WD.mainWindow.GetSnapshot(Resultpath + "sequence message1.PNG");
            Base_Assert.AreEqual("Sequence 1, 2 material should be completed before dispensing this material.", WD.MessageDialog.Lable.AttachedText, "sequence message");
            WD.MessageDialog.OKButton.Click();
            Thread.Sleep(2000);
            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            //select sequence2 1072
            WD_Fuction.SelectOrderandMaterial(order, WDMaterial.x1072);
            Thread.Sleep(2000);
            //error message
            WD.mainWindow.GetSnapshot(Resultpath + "sequence message2.PNG");
            Base_Assert.AreEqual("Sequence 1 material should be completed before dispensing this material.", WD.MessageDialog.Lable.AttachedText, "sequence message");
            WD.MessageDialog.OKButton.Click();
            Thread.Sleep(2000);
            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            //finish M801890
            WD_Fuction.SelectOrderandMaterial(order, WDMaterial.M801890);
            WD_Fuction.SelectMehod(WDMethod.Net, "M801890001");
            WD_Fuction.FinishNetDiapense("15", "215");
            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            //select sequence3 X0125
            WD_Fuction.SelectOrderandMaterial(order, WDMaterial.X0125);
            Thread.Sleep(2000);
            //error message
            WD.mainWindow.GetSnapshot(Resultpath + "sequence message3.PNG");
            Base_Assert.AreEqual("Sequence 2 material should be completed before dispensing this material.", WD.MessageDialog.Lable.AttachedText, "sequence message");
            WD.MessageDialog.OKButton.Click();
            Thread.Sleep(2000);
            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            //finish 1072
            WD_Fuction.SelectOrderandMaterial(order, WDMaterial.x1072);
            WD_Fuction.SelectMehod(WDMethod.Net, "1072003");
            WD_Fuction.FinishNetDiapense("15", "215");
            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            //finish x0125
            WD_Fuction.SelectOrderandMaterial(order, WDMaterial.X0125);
            WD_Fuction.SelectMehod(WDMethod.Net, "X0125001");
            WD_Fuction.FinishNetDiapense("15", "459");

            WD_Fuction.Close();
        }

       
    }
}