using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System;
using HP.LFT.SDK;
using System.Windows.Forms;
using System.Drawing;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(876986)]
        [Title("822656_The screenshot can preview from MOC and APEM mobile if finish the execution phase on APEM mobile if set ' MOBILE_SCREENSHOT_ON = 1' ")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_876986()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            Library.BaseLibrary.Application.LaunchMocAndLogin();
            Thread.Sleep(5000);
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            MOC_Fuction.AddRPL_OpenDesign("RPL876986", "AAA_BPL (Version 1)");
            APEM.DesignEditorWindow.UnitProcedure._UFT_CheckBox.Click();
            Thread.Sleep(8000);
            Base_Function.MouseClick(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            Thread.Sleep(3000);
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject.DoubleClick();
            Thread.Sleep(8000);
            APEM.DesignEditorWindow.Operation._UFT_CheckBox.Click();
            Thread.Sleep(8000);
            Base_Function.MouseClick(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            Thread.Sleep(3000);
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.OperationUiObject.DoubleClick();
            Thread.Sleep(5000);
            APEM.DesignEditorWindow.TabbedPaneControl.Select(2);
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.First_Phase.Click();
            Thread.Sleep(8000);
            Base_Function.MouseClick(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            Thread.Sleep(3000);
            APEM.DesignEditorWindow.SaveButton.ClickSignle();
            if (APEM.AuditReasonDialog.IsExist())
            {
                APEM.AuditReasonDialog.Reason.SendKeys("for test");
                APEM.AuditReasonDialog.OK.Click();
            }
            Thread.Sleep(5000);
            Assert.IsTrue(APEM.DesignSavedDialog.IsExist());
            APEM.DesignSavedDialog.OKButton.Click();
            MOC_Fuction.DesignEditorClose();
            Thread.Sleep(2000);
            APEM.MocmainWindow.RPLManagementInternalFrame._UFT_InterFrame.Close();
            MOC_Fuction.VerifyRPL("RPL876986");
            MOC_Fuction.CertifyRPL("RPL876986");
            MOC_Fuction.PlanFromRPL("RPL876986", "ORDER876986");
            APEM.MocmainWindow.OrderTracking.ClickSignle();
            MOC_Fuction.CheckRowSelection();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingInternalFrame.StatusFilterButton.ClickSignle();
            APEM.MocmainWindow.RowsToViewDialog.ViewAll.Click();
            APEM.MocmainWindow.RowsToViewDialog.OK.Click();
            APEM.MocmainWindow.OrderTrackingInternalFrame.CodeEditor.SetText("ORDER876986");
            APEM.MocmainWindow.OrderTrackingInternalFrame.Filterbutton.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingInternalFrame.OrderTable.Row("Active").Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingInternalFrame.OrderTable.Row("Active").DoubleClick();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.UnitProcedureUiObject.Click();
            Thread.Sleep(4000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.OperationUiObject.Click();
            Thread.Sleep(4000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.PhaseUiObject.Click();
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.PhaseUiObject._UFT_UiObject.Click(HP.LFT.SDK.MouseButton.Right);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.ExecutionScreenshots.Select();
            Thread.Sleep(3000);
            Base_Assert.AreEqual(APEM.MocmainWindow.ScreenshotDialog.Lable.Text, "Screenshots not available for this phase");
            APEM.MocmainWindow.ScreenshotDialog.GetSnapshot(Resultpath + "NoScreenshot.PNG");
            APEM.MocmainWindow.ScreenshotDialog.OKButton.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame._UFT_InterFrame.Close();
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            driver.Wait();
            Thread.Sleep(5000);
            Mobile_Fuction.login();
            driver.Wait();
            Thread.Sleep(20000);
            Mobile.OrderProcess_Page.OrderSearch.SendKeys("ORDER876986");
            Thread.Sleep(5000);
            Mobile.OrderProcess_Page.GotoTracking.Click();
            Thread.Sleep(5000);
            Mobile.OrderTracking_Page.ExecutionButton.Click();
            Thread.Sleep(10000);
            //finish the order
            Mobile.OrderExecution_Page.OKButton.Click();
            Thread.Sleep(5000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Finished.PNG");
            Thread.Sleep(3000);
            APEM.MocmainWindow.OrderTracking.ClickSignle();
            MOC_Fuction.CheckRowSelection();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingInternalFrame.RefreshButton.ClickSignle();
            Thread.Sleep(4000);
            APEM.MocmainWindow.OrderTrackingInternalFrame.OrderTable.Row("Finished").Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingInternalFrame.OrderTable.Row("Finished").DoubleClick();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.UnitProcedureUiObject.Click();
            Thread.Sleep(4000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.OperationUiObject.Click();
            Thread.Sleep(4000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.PhaseUiObject.Click();
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.PhaseUiObject._UFT_UiObject.Click(HP.LFT.SDK.MouseButton.Right);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.ExecutionScreenshots.Select();
            Thread.Sleep(3000);
            APEM.MocmainWindow.PrintReportDialog.Print.ClickSignle();
            Thread.Sleep(8000);
            Base_Assert.IsTrue(APEM.PrintDialog.IsExist());
            APEM.PrintDialog.Close();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.PhaseUiObject.Click();
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.PhaseUiObject._UFT_UiObject.Click(HP.LFT.SDK.MouseButton.Right);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.ExecutionScreenshots.Select();
            Thread.Sleep(3000);
            APEM.MocmainWindow.PrintReportDialog.Preview.ClickSignle();
            Thread.Sleep(4000);
            var addressAndSearchUsingBingEdit = APEM.MocmainWindow.BrowserURL;
            var url = addressAndSearchUsingBingEdit.LegacyIAccessiblePattern.Value;
            Console.WriteLine(url);
            Selenium_Driver driver1 = new Selenium_Driver(Browser.chrome);
            driver1.Navigate(url);
            driver1.Maxsize();
            Thread.Sleep(5000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Preview.PNG");
            var ExecuteAPP = Mobile.PrintReport_Page.ExecuteAPP._Selenium_WebElement.Text;
            Console.WriteLine(ExecuteAPP);
            Base_Assert.IsTrue(ExecuteAPP.Contains("APEM Mobile"));
            Base_Test.KillProcess("iexplore");
            MOC_Fuction.PlanFromRPL("RPL876986", "ORDER876986");
            APEM.MocmainWindow.OrderTracking.ClickSignle();
            MOC_Fuction.CheckRowSelection();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingInternalFrame.StatusFilterButton.ClickSignle();
            APEM.MocmainWindow.RowsToViewDialog.ViewAll.Click();
            APEM.MocmainWindow.RowsToViewDialog.OK.Click();
            APEM.MocmainWindow.OrderTrackingInternalFrame.CodeEditor.SetText("ORDER876986");
            APEM.MocmainWindow.OrderTrackingInternalFrame.Filterbutton.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingInternalFrame.OrderTable.Row("Active").Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingInternalFrame.OrderTable.Row("Active").DoubleClick();
            Thread.Sleep(5000);
             APEM.MocmainWindow.OrderTrackingPFCInternalFrame.UnitProcedureUiObject.Click();
            Thread.Sleep(4000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.OperationUiObject.Click();
            Thread.Sleep(4000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.PhaseUiObject.Click();
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.PhaseUiObject._UFT_UiObject.Click(HP.LFT.SDK.MouseButton.Right);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.ExecuteButton.Select();
            Thread.Sleep(8000);
            APEM.PhaseExecWindow.ExecutionInternalFrame.Cancel_Button.ClickSignle();
            Thread.Sleep(2000);
            APEM.PhaseExecWindow.ConfirmationInternalFrame.YesButton.Click();
            Thread.Sleep(6000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame._UFT_InterFrame.Close();
            Selenium_Driver driver2 = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver2);
            driver2.Wait();
            Thread.Sleep(5000);
            Mobile_Fuction.login();
            driver2.Wait();
            Thread.Sleep(20000);
            Mobile.OrderProcess_Page.OrderSearch.SendKeys("ORDER876986");
            Thread.Sleep(5000);
            Mobile.OrderProcess_Page.GotoTracking.Click();
            Thread.Sleep(5000);
            Mobile.OrderTracking_Page.ExecutionButton.Click();
            Thread.Sleep(10000);
            //cancel again on mobile
            Mobile.OrderExecution_Page.CancelButton.Click();
            Thread.Sleep(2000);
            Mobile.OrderExecution_Page.ConfirmYesButton.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.OrderTrackingInternalFrame.RefreshButton.ClickSignle();
            Thread.Sleep(3000);
            APEM.MocmainWindow.OrderTrackingInternalFrame.OrderTable.Row("Active").Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingInternalFrame.OrderTable.Row("Active").DoubleClick();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.UnitProcedureUiObject.Click();
            Thread.Sleep(4000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.OperationUiObject.Click();
            Thread.Sleep(4000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.PhaseUiObject.Click();
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.PhaseUiObject._UFT_UiObject.Click(HP.LFT.SDK.MouseButton.Right);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.ExecutionScreenshots.Select();
            Thread.Sleep(3000);
            Base_Assert.IsTrue(APEM.MocmainWindow.PhaseExecutionsDialog.IsExist());
            APEM.MocmainWindow.PhaseExecutionsDialog.GetSnapshot(Resultpath + "executed_multiple_times.PNG");
            Thread.Sleep(4000);
            APEM.MocmainWindow.PhaseExecutionsDialog.DataTable.Row("1").Click();
            APEM.MocmainWindow.PhaseExecutionsDialog.OK.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.PrintReportDialog.Preview.ClickSignle();
            Thread.Sleep(4000);
            var addressAndSearchUsingBingEdit2 = APEM.MocmainWindow.BrowserURL;
            var url2 = addressAndSearchUsingBingEdit2.LegacyIAccessiblePattern.Value;
            Console.WriteLine(url2);
            Selenium_Driver driver3= new Selenium_Driver(Browser.chrome);
            driver3.Navigate(url2);
            driver3.Maxsize();
            Thread.Sleep(5000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Moc_cancel_Preview.PNG");
            var ExecuteAPP2 = Mobile.PrintReport_Page.ExecuteAPP._Selenium_WebElement.Text;
            Console.WriteLine(ExecuteAPP2);
            Base_Assert.IsTrue(ExecuteAPP2.Contains("MOC"));
            Base_Test.KillProcess("iexplore");
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame._UFT_InterFrame.Close();
        }

    }
}