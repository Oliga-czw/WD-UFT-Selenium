using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System;
using OpenQA.Selenium;
using HP.LFT.SDK;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Application = MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary.Application;
using System.Linq;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class Mobile_TestCase
    {
        [TestCaseID(824732)]
        [Title("UC730129_the order phase table status auto refresh when status changes in MOC")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_824732()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            Application.LaunchMocAndLogin();
            MOC_Fuction.VerifyRPL("FOR_STATUS");
            MOC_Fuction.CertifyRPL("FOR_STATUS");
            Thread.Sleep(3000);
            MOC_Fuction.PlanFromRPL("FOR_STATUS", "ORDRE824732", false);
            APEM.MocmainWindow.OrderListInternalFrame.OrderTabControl.Select("Phases");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            driver.Wait();
            Mobile_Fuction.login();
            driver.Wait();
            Thread.Sleep(5000);
            Mobile.OrderProcess_Page.OrderSearch.SendKeys("ORDRE824732");
            Thread.Sleep(2000);
            Mobile.OrderProcess_Page.GotoTracking.Click();
            Thread.Sleep(3000);
            int no = 0;
            int i = 0;
        
            foreach (IWebElement head in Mobile.OrderTracking_Page.OrderPhaseTableHeads)
            {
                if (head.Text == "Status")
                {
                    no = i;
                }
                i++;
            }
            foreach (IWebElement Phase in Mobile.OrderTracking_Page.OrderPhaseTableRows)
            {
                var Status = Phase.FindElements(By.TagName("td"))[no].Text;
                Console.WriteLine(Status);
                Assert.IsTrue(Status.Contains("Not ready"));
            }
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Planned.PNG");
            APEM.MocmainWindow.OrderListInternalFrame.OrderTabControl.Select("Orders");
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Row("ORDRE824732").Click();
            APEM.MocmainWindow.OrderListInternalFrame.Activate_Button.ClickSignle();
            Thread.Sleep(2000);
            APEM.ActivateDialog.YesButton.Click();
            MOC_Fuction.AddReason();
            foreach (IWebElement Phase in Mobile.OrderTracking_Page.OrderPhaseTableRows)
            {
                var Status = Phase.FindElements(By.TagName("td"))[no].Text;
                Console.WriteLine(Status);
                Assert.IsTrue((Status.Contains("Ready")) || (Status.Contains("Not ready")));
            }
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Active.PNG");
            LogStep(@"Execute in moc");
            APEM.MocmainWindow.WorkstationBP.ClickSignle();
            MOC_Fuction.CheckRowSelection();
            Thread.Sleep(3000);
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText("ORDRE824732");
            APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
            //Excution
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("PHASE55", "Name").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(6000);
            foreach (IWebElement Name in Mobile.OrderTracking_Page.OrderPhaseNames)
            {
                if (Name.Text == "PHASE55")
                {
                    var status1 = Name.FindElement(By.XPath("../../..")).FindElements(By.TagName("td"))[no].Text;
                    Console.WriteLine(status1);
                    Assert.IsTrue(status1.Contains("Executing"));
                }
            }
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Executing.PNG");
            LogStep(@"Click cancel button when executing in moc");
            APEM.PhaseExecWindow.ExecutionInternalFrame.Cancel_Button.ClickSignle();
            Thread.Sleep(1000);
            APEM.PhaseExecWindow.ConfirmationInternalFrame.YesButton.Click();
            Thread.Sleep(2000);
            foreach (IWebElement Name in Mobile.OrderTracking_Page.OrderPhaseNames)
            {
                if (Name.Text == "PHASE55")
                {
                    var status2 = Name.FindElement(By.XPath("../../..")).FindElements(By.TagName("td"))[no].Text;
                    Console.WriteLine(status2);
                    Assert.IsTrue(status2.Contains("Ready"));
                }
            }
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "cancel.PNG");
            LogStep(@"Interrupted in moc");
            APEM.MocmainWindow.OrderListInternalFrame.OrderTabControl.Select("Phases");
            Thread.Sleep(2000);
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("PHASE55", "Name").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(5000);
            APEM.PhaseExecWindow.StopPhaseButton.ClickSignle();
            Thread.Sleep(3000);
            SendKeys.SendWait("{Enter}");
            Thread.Sleep(1000);
            APEM.PhaseExecWindow.UserConfirmationInternalFrame.PassWord.SendKeys(PassWord.qaone1);
            APEM.PhaseExecWindow.UserConfirmationInternalFrame.Comment.SendKeys("for test");
            APEM.PhaseExecWindow.UserConfirmationInternalFrame.OKButton.Click();
            Thread.Sleep(2000);
            foreach (IWebElement Name in Mobile.OrderTracking_Page.OrderPhaseNames)
            {
                if (Name.Text == "PHASE55")
                {
                    var status3 = Name.FindElement(By.XPath("../../..")).FindElements(By.TagName("td"))[no].Text;
                    Console.WriteLine(status3);
                    Assert.IsTrue(status3.Contains("Interrupted"));
                }
            }
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Interrupted.PNG");
            LogStep(@"click cancel button which on toolbar in moc");
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("PHASE55", "Name").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(5000);
            APEM.PhaseExecWindow.CancelPhaseButton.ClickSignle();
            Thread.Sleep(3000);
            SendKeys.SendWait("{Enter}");
            Thread.Sleep(1000);
            APEM.PhaseExecWindow.UserConfirmationInternalFrame.PassWord.SendKeys(PassWord.qaone1);
            APEM.PhaseExecWindow.UserConfirmationInternalFrame.Comment.SendKeys("for test");
            APEM.PhaseExecWindow.UserConfirmationInternalFrame.OKButton.Click();
            Thread.Sleep(2000);

            foreach (IWebElement Name in Mobile.OrderTracking_Page.OrderPhaseNames)
            {
                if (Name.Text == "PHASE55")
                {
                    var status3 = Name.FindElement(By.XPath("../../..")).FindElements(By.TagName("td"))[no].Text;
                    Console.WriteLine(status3);
                    Assert.IsTrue(status3.Contains("Cancelled"));
                }
            }
            LogStep(@"Reactive the phase in moc");
            APEM.MocmainWindow.OrderListInternalFrame.OrderTabControl.Select("Phases");
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderListInternalFrame.Refresh_Button.ClickSignle();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Row("PHASE55").Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderListInternalFrame.Reactivate_Button.ClickSignle();
            Thread.Sleep(2000);
            APEM.ReactivateDialog.YesButton.Click();
            Thread.Sleep(2000);
            APEM.ReactivateDialog.YesButton.Click();
            Thread.Sleep(2000);
            MOC_Fuction.AddReason();
            Thread.Sleep(6000);
            APEM.MocmainWindow.OrderListInternalFrame.Refresh_Button.ClickSignle();
            Thread.Sleep(2000);
            foreach (IWebElement Name in Mobile.OrderTracking_Page.OrderPhaseNames)
            {
                if (Name.Text == "PHASE55")
                {
                    var status3 = Name.FindElement(By.XPath("../../..")).FindElements(By.TagName("td"))[no].Text;
                    Console.WriteLine(status3);
                    Assert.IsTrue(status3.Contains("Interrupted"));
                }
            }
            LogStep(@"Finished the phase in moc");
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("PHASE55", "Name").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(5000);
            APEM.PhaseExecWindow.ExecutionInternalFrame.OK_Button.ClickSignle();
            Thread.Sleep(1000);
            foreach (IWebElement Name in Mobile.OrderTracking_Page.OrderPhaseNames)
            {
                if (Name.Text == "PHASE55")
                {
                    var status3 = Name.FindElement(By.XPath("../../..")).FindElements(By.TagName("td"))[no].Text;
                    Console.WriteLine(status3);
                    Assert.IsTrue(status3.Contains("Finished"));
                }
            }
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Finished.PNG");
            LogStep(@"canceled the phase in order list");
            APEM.MocmainWindow.OrderListInternalFrame.OrderTabControl.Select("Phases");
            APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Row("PHASE67").Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderListInternalFrame.CancelBP_Button.ClickSignle();
            Thread.Sleep(2000);
            APEM.CancelBPDialog.YesButton.Click();
            Thread.Sleep(2000);
            MOC_Fuction.AddReason();
            Thread.Sleep(2000);
            foreach (IWebElement Name in Mobile.OrderTracking_Page.OrderPhaseNames)
            {
                if (Name.Text == "PHASE67")
                {
                    var status3 = Name.FindElement(By.XPath("../../..")).FindElements(By.TagName("td"))[no].Text;
                    Console.WriteLine(status3);
                    Assert.IsTrue(status3.Contains("Cancelled"));
                }
            }
            LogStep(@"Disabled the phase in order list");
            APEM.MocmainWindow.OrderListInternalFrame.OrderTabControl.Select("Phases");
            APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Row("PHASE67").Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderListInternalFrame.DisableBP_Button.ClickSignle();
            Thread.Sleep(2000);
            APEM.DisableBPDialog.YesButton.Click();
            Thread.Sleep(2000);
            MOC_Fuction.AddReason();
            Thread.Sleep(2000);
            foreach (IWebElement Name in Mobile.OrderTracking_Page.OrderPhaseNames)
            {
                if (Name.Text == "PHASE67")
                {
                    var status3 = Name.FindElement(By.XPath("../../..")).FindElements(By.TagName("td"))[no].Text;
                    Console.WriteLine(status3);
                    Assert.IsTrue(status3.Contains("Disabled"));
                }
            }
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Disabled.PNG");
            LogStep(@"canceled the order in order list");
            APEM.MocmainWindow.OrderListInternalFrame.OrderTabControl.Select("Orders");
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Row("ORDRE824732").Click();
            APEM.MocmainWindow.OrderListInternalFrame.Cancel_Button.ClickSignle();
            Thread.Sleep(2000);
            APEM.MocmainWindow.CancelOrderDialog.YesButton.Click();
            MOC_Fuction.AddReason();
            Thread.Sleep(4000);
            foreach (IWebElement Phase in Mobile.OrderTracking_Page.OrderPhaseTableRows)
            {
                var Status = Phase.FindElements(By.TagName("td"))[no].Text;
                Console.WriteLine(Status);
                Assert.IsTrue(Status.Contains("Cancel"));
            }
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "OrderCanceled.PNG");
            driver.Close();
        }
    }
}