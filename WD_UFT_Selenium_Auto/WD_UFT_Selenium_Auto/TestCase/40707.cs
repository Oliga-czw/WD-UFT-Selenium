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
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;
using WD_UFT_Selenium_Auto.Product.WD;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {

        [TestCaseID(40707)]
        [Title("Administration Signature:check the sinagure and comment setting for execution system")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_40707()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test1";
            string xml = "10 aspen wd signautres_40707 bulk load.xml";
            LogStep(@"1. import Signature xml");
            WD_Fuction.Bulkload(xml);
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.Signatures.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "sinagure_and_comment setting.PNG");
            driver.FindElement("//div[text()='Permissions']").Click();
            Thread.Sleep(5000);
            driver.FindElement("//select/option[@value='Production Execution Manager']").Click();
            if (driver.FindElement("//label[text()='Allow access']/..").GetAttribute("checked") is null) 
            {
                driver.FindElement("//label[text()='Allow access']/..").Click();
                driver.FindElement("//button[text()='Apply']").Click();
                Thread.Sleep(2000);
                driver.FindElement("//button[text()='OK']").Click();           
            }
            
            LogStep(@"2. Open WD client and booth clean");
            Application.LaunchWDAndLogin();
            Thread.Sleep(3000);
            WD.mainWindow.HomeInternalFrame.BoothCleaning.Click();
            WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
            Thread.Sleep(2000);
            Base_Assert.IsTrue(WD.mainWindow.HomeInternalFrame.IsEnabled);
            LogStep(@"3. Open scale check");
            WD.mainWindow.HomeInternalFrame.ScaleChecking.Click();
            Thread.Sleep(2000);
            WD.mainWindow.ScaleCheckInternalFrame.Standardization_type.SelectRows(0);
            WD.mainWindow.ScaleCheckInternalFrame.startcheck.Click();
            if (WD.mainWindow.ScaleCheckInternalFrame.startcheck.IsEnabled) 
            { 
                WD.mainWindow.ScaleCheckInternalFrame.startcheck.ClickSignle(); 
            }
            Thread.Sleep(2000);
            
            WD.mainWindow.CheckWeightInternalFrame.zero.Click();
            WD.SimulatorWindow.weight.SetText("100");
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.CheckWeightInternalFrame.readScale.ClickSignle();
            WD.mainWindow.CheckWeightInternalFrame.accept.Click();
            Base_Assert.IsTrue(WD.mainWindow.Dialog._UFT_Dialog.IsEnabled);
            WD.mainWindow.GetSnapshot(Resultpath + "Scale_Check_Signature.PNG");
            WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone1);
            
            WD.mainWindow.Dialog.OK.Click();
            Thread.Sleep(2000);
            WD.mainWindow.ScaleCheckInternalFrame.homeButton.Click();
            Thread.Sleep(2000);
            LogStep(@"4. Open Order Dispensing");
            WD.mainWindow.HomeInternalFrame.OrderDispensing.Click();
            WD.mainWindow.DispensingInternalFrame.orderTable.SelectRows(0);
            WD.mainWindow.DispensingInternalFrame.next.Click();
            Thread.Sleep(2000);
            WD.mainWindow.MaterialInternalFrame.materialTable.SelectRows(0);
            WD.mainWindow.MaterialInternalFrame.next.Click();
            Thread.Sleep(2000);
            //if (WD.mainWindow.BoothCleanInternalFrame.IsEnabled is true)
            //{
            //    WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
            //    Thread.Sleep(2000);
            //}
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("X0125001");
            Thread.Sleep(5000);
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            WD.mainWindow.ScaleWeightInternalFrame.comment.ClickSignle();
            Thread.Sleep(2000);
            WD.mainWindow.CommentInternalFrame.commentEditor.SendKeys("For Test");
            WD.mainWindow.CommentInternalFrame.OKButton.Click();
            Thread.Sleep(2000);
            WD.mainWindow.GetSnapshot(Resultpath + "Comment_Signature.PNG");
            WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone1);
            WD.mainWindow.Dialog.OK.Click();
            Thread.Sleep(3000);
            WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems("simulator");
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            WD.SimulatorWindow.weight.SetText("400");
            WD.SimulatorWindow.OK.Click();
            Thread.Sleep(3000);
            WD.mainWindow.ScaleWeightInternalFrame.NewSource.Click();
            Thread.Sleep(2000);
            Base_Assert.IsTrue(WD.mainWindow.Dialog._UFT_Dialog.IsEnabled);
            WD.mainWindow.GetSnapshot(Resultpath + "NEW_Source_Signature.PNG");
            WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone1);
            //WD.mainWindow.Dialog.Comment.SetText("Scale Check Standardization Signature test");
            WD.mainWindow.Dialog.OK.Click();
            Thread.Sleep(2000);
            WD.mainWindow.ScaleWeightInternalFrame.reset.Click();
            Base_Assert.IsTrue(WD.mainWindow.Dialog._UFT_Dialog.IsEnabled);
            WD.mainWindow.GetSnapshot(Resultpath + "cancel_reset_Signature.PNG");
            WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone1);
            //WD.mainWindow.Dialog.Comment.SetText("Scale Check Standardization Signature test");
            WD.mainWindow.Dialog.OK.Click();
            Thread.Sleep(2000);
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("X0125001");
            Thread.Sleep(5000);
            WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems("simulator");
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            WD.SimulatorWindow.weight.SetText("444");
            WD.SimulatorWindow.OK.Click();
            Thread.Sleep(3000);
            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            Thread.Sleep(2000);
            Base_Assert.IsTrue(WD.mainWindow.Dialog._UFT_Dialog.IsEnabled);
            WD.mainWindow.GetSnapshot(Resultpath + "Dispensing_Signature.PNG");
            WD.mainWindow.Dialog.UserID.SetText(UserName.qaone1);
            WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone1);
            //WD.mainWindow.Dialog.Comment.SetText("Scale Check Standardization Signature test");
            WD.mainWindow.Dialog.OK.Click();
            Thread.Sleep(2000);
            if (WD.ConfirmationDialog.IsExist()) 
            {
                WD.ConfirmationDialog.YesButton.Click();
            }
            Thread.Sleep(2000);
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            Thread.Sleep(2000);
            WD.mainWindow.MaterialInternalFrame.materialTable.SelectRows(0);
            WD.mainWindow.MaterialInternalFrame.next.Click();
            if (WD.mainWindow.BoothCleanInternalFrame.IsEnabled)
            {
                WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();

            }
            WD.mainWindow.HandingInternalFrame.AcknowledgeButton.ClickSignle();
            Thread.Sleep(4000);
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("M801890001");
            Thread.Sleep(4000);
            if (WD.ConfirmationDialog._UFT_Dialog.IsEnabled)
            {
                WD.ConfirmationDialog.YesButton.Click();
            }
            Thread.Sleep(5000);
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            WD.SimulatorWindow.weight.SetText("180");

            WD.SimulatorWindow.OK.Click();

            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            Thread.Sleep(4000);
            WD.mainWindow.Dialog.UserID.SetText(UserName.qaone1);
            WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone1);
            WD.mainWindow.Dialog.OK.Click();
            Thread.Sleep(2000);
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            Thread.Sleep(4000);
            WD.mainWindow.MaterialInternalFrame.materialTable.SelectRows(0);
            WD.mainWindow.MaterialInternalFrame.next.Click();
            if (WD.mainWindow.BoothCleanInternalFrame.IsEnabled)
            {
                WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();

            }
            WD.mainWindow.HandingInternalFrame.AcknowledgeButton.ClickSignle();
            Thread.Sleep(4000);
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("1072003");
            if (WD.ConfirmationDialog._UFT_Dialog.IsEnabled)
            {
                WD.ConfirmationDialog.YesButton.Click();
            }
            Thread.Sleep(4000);
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            WD.SimulatorWindow.weight.SetText("180");

            WD.SimulatorWindow.OK.Click();

            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            Thread.Sleep(4000);
            WD.mainWindow.Dialog.UserID.SetText(UserName.qaone1);
            WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone1);
            WD.mainWindow.Dialog.OK.Click();
            Thread.Sleep(2000);
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            ////order Kitting
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            WD.mainWindow.HomeInternalFrame.OrderKitting.Click();
            WD.mainWindow.SelectAnOrderToKittingFrame.orderTable.SelectRows(0);
            WD.mainWindow.SelectAnOrderToKittingFrame.StartKitButton.Click();
            Thread.Sleep(4000);
            string test01 = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.GetCell(0, "Container").Value.ToString();
            string test02 = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.GetCell(1, "Container").Value.ToString();
            string test03 = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.GetCell(2, "Container").Value.ToString();
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys("test1");
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys(test01);
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys("test2");
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys(test02);
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys("test3");
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys(test03);
            WD.mainWindow.SelectAnOrderToKittingFrame.accept.ClickSignle();
            Base_Assert.IsTrue(WD.mainWindow.Dialog._UFT_Dialog.IsEnabled);
            WD.mainWindow.GetSnapshot(Resultpath + "OrderKitting_Operator_Signature.PNG");
            WD.mainWindow.Dialog.UserID.SetText(UserName.qaone1);
            WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone1);
            WD.mainWindow.Dialog.Comment.SetText("For Test");
            WD.mainWindow.Dialog.OK.Click();
            Thread.Sleep(2000);
            WD.mainWindow.GetSnapshot(Resultpath + "OrderKitting_Manager_Signature.PNG");
            WD.mainWindow.Dialog.UserID.SetText(UserName.qaone3);
            WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone3);
            WD.mainWindow.Dialog.Comment.SetText("For Test");
            WD.mainWindow.Dialog.OK.Click();
            Thread.Sleep(2000);
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }

        }

    }
}