using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System;
using HP.LFT.SDK;
using System.Windows.Forms;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class GML_TestCase
    {
        [TestCaseID(31324)]
        [Title("GML-Functionality RPL GML_UGM (Covered APEM Moc integration with IP.21/MMDM/APRM)")]
        [TestCategory(ProductArea.RecipeManagement)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_31324()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            Library.BaseLibrary.Application.LaunchMocAndLogin();
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            Thread.Sleep(5000);
            APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row("GML_UGM").Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.RPLDesignInternalFrame.VerifyButton.ClickSignle();
            Thread.Sleep(3000);
            APEM.VerifyDialog.YesButton.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.OrderPlanDialog.CodeEditor.SendKeys("GML_TEST");
            APEM.MocmainWindow.OrderPlanDialog.DescriptionEditor.SendKeys("test for GML_UGM");
            APEM.MocmainWindow.OrderPlanDialog.POEditor.SendKeys("PO");
            APEM.MocmainWindow.OrderPlanDialog.POStepEditor.SendKeys("POStep");
            APEM.MocmainWindow.OrderPlanDialog.ArticleEditor.SendKeys("Article");
            APEM.MocmainWindow.OrderPlanDialog.BatchEditor.SendKeys("Batch");
            APEM.MocmainWindow.OrderPlanDialog.QuantityEditor.SendKeys("123.65");
            APEM.MocmainWindow.OrderPlanDialog.Quantity_unitEditor.SendKeys("kg");
            APEM.MocmainWindow.OrderPlanDialog.DateEditor.SendKeys("12/12/22, 3:23:00 AM");
            APEM.MocmainWindow.OrderPlanDialog.END_DateEditor.SendKeys("5/6/26, 10:23:34 PM");
            APEM.MocmainWindow.OrderPlanDialog.WorkcenterList.Select("ProcessCellLine2");
            Thread.Sleep(3000);
            APEM.MocmainWindow.OrderPlanDialog.Auto_ActivateCheckBox.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.OrderPlanDialog.OK.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.AddReasonDialog.Reason.SendKeys("for test");
            APEM.MocmainWindow.AddReasonDialog.OK.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.WorkstationBP.ClickSignle();
            Thread.Sleep(3000);
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText("GML_TEST");
            APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
            //Introduction & Equipment Selection
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(10000);
            //Verify click View SOP button would open dialog with items in Document table definition.
            APEM.PhaseExecWindow.ExecutionInternalFrame.ViewSOP_Button.ClickSignle();
            Thread.Sleep(5000);
            Assert.IsTrue(APEM.PhaseExecWindow.PopUpInternalFrame.SelectSOP._UFT_Table.IsEnabled);
            APEM.PhaseExecWindow.GetSnapshot(Resultpath + "ViewSop(1).PNG");
            APEM.PhaseExecWindow.PopUpInternalFrame.Cancel_Button.ClickSignle();
            Thread.Sleep(2000);
            //Verify click Deviation button would invoke screen for inputing deviation.
            APEM.PhaseExecWindow.ExecutionInternalFrame.Deviation_Button.Click();
            Thread.Sleep(6000);
            Assert.IsTrue(APEM.PhaseExecWindow.ExecutionInternalFrame.DeviationTypeList._UFT_IList.IsEnabled);
            APEM.PhaseExecWindow.GetSnapshot(Resultpath + "Deviation.PNG");
            APEM.PhaseExecWindow.ExecutionInternalFrame.Cancel_Button.ClickSignle();
            Thread.Sleep(2000);
            APEM.PhaseExecWindow.ConfirmationInternalFrame.YesButton.Click();
            Thread.Sleep(6000);
            //Verify click Print button would open Print dialog.
            APEM.PhaseExecWindow.ExecutionInternalFrame.Print_Button.Click();
            Thread.Sleep(6000);
            Assert.IsTrue(APEM.PrintDialog._STD_Dialog.IsEnabled);
            APEM.PhaseExecWindow.GetSnapshot(Resultpath + "Print.PNG");
            APEM.PrintDialog.Cancel.Click();
            Thread.Sleep(3000);
            //Verify after click OK button, the phase is executed to Finished.
            APEM.PhaseExecWindow.ExecutionInternalFrame.OK_Button.Click();
            Thread.Sleep(10000);
            //Verify the phase is executed automatically after above INTRODUCTION finished.
            var status1 = APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable._UFT_Table.GetCell(0, "Status").Value;
            Assert.AreEqual(status1, "Finished");
            var status2 = APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable._UFT_Table.GetCell(1, "Status").Value;
            Assert.AreEqual(status2, "Finished");
            Thread.Sleep(3000);
            //Start Up Checklist
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(10000);
            //Verify View SOP button work OK
            APEM.PhaseExecWindow.ExecutionInternalFrame.ViewSOP_Button.Click();
            Thread.Sleep(5000);
            Assert.IsTrue(APEM.PhaseExecWindow.PopUpInternalFrame.SelectSOP._UFT_Table.IsEnabled);
            APEM.PhaseExecWindow.GetSnapshot(Resultpath + "ViewSop(2).PNG");
            APEM.PhaseExecWindow.PopUpInternalFrame.Cancel_Button.ClickSignle();
            Thread.Sleep(2000);
            //Verify Check list part with contents as parameter iCheckList designed.
            APEM.PhaseExecWindow.GetSnapshot(Resultpath + "Checklist.PNG");
            APEM.PhaseExecWindow.ExecutionInternalFrame.SelectBox.GetCell(0, "Select").SetValue("1");
            APEM.PhaseExecWindow.ExecutionInternalFrame.SelectBox.GetCell(1, "Select").SetValue("1");
            APEM.PhaseExecWindow.ExecutionInternalFrame.SelectBox.GetCell(2, "Select").SetValue("1");
            Thread.Sleep(3000);
            APEM.PhaseExecWindow.ExecutionInternalFrame.UserIDEditor.SendKeys("qae\\qaone1");
            APEM.PhaseExecWindow.ExecutionInternalFrame.PasswordEditor.SetText("Aspen111");
            SendKeys.SendWait("{Enter}");
            Thread.Sleep(10000);
            //Start Up Introduction
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(10000);
            APEM.PhaseExecWindow.GetSnapshot(Resultpath + "StartUp_Introduction.PNG");
            APEM.PhaseExecWindow.ExecutionInternalFrame.UserIDEditor.SendKeys("qae\\qaone1");
            APEM.PhaseExecWindow.ExecutionInternalFrame.PasswordEditor.SetText("Aspen111");
            SendKeys.SendWait("{Enter}");
            Thread.Sleep(10000);
            var status3 = APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable._UFT_Table.GetCell(0, "Status").Value;
            Assert.AreEqual(status3, "Finished");
            var status4 = APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable._UFT_Table.GetCell(1, "Status").Value;
            Assert.AreEqual(status4, "Finished");
            //open order tracking
            APEM.MocmainWindow.OrderTracking.ClickSignle();
            Thread.Sleep(3000);
            APEM.MocmainWindow.OrderTrackingInternalFrame.CodeEditor.SetText("GML_TEST");
            APEM.MocmainWindow.OrderTrackingInternalFrame.Filterbutton.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingInternalFrame.OrderTable._UFT_Table.ActivateRow(0);
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.UnitProcedureUiObject.DoubleClick();
            Thread.Sleep(4000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.OperationUiObject1.DoubleClick();
            Thread.Sleep(4000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.Script1.Click();
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.Script1.Click(HP.LFT.SDK.MouseButton.Right);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.ByPassCondition.Select();
            Thread.Sleep(3000);
            MOC_Fuction.AddReason();
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.Script2.Click();
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.Script2.Click(MouseButton.Right);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.ByPassCondition.Select();
            Thread.Sleep(3000);
            MOC_Fuction.AddReason();
            Thread.Sleep(3000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame._UFT_InterFrame.Close();
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(10000);
            //APEM.PhaseExecWindow.GetSnapshot(Resultpath + "FILLING_MEASUREMENT1.PNG");
            APEM.PhaseExecWindow.ExecutionInternalFrame.PHActualEditor.SendKeys("8.3");
            APEM.PhaseExecWindow.ExecutionInternalFrame.TempActualEditor.SendKeys("55.00");
            APEM.PhaseExecWindow.ExecutionInternalFrame.UserIDEditor.SendKeys("qae\\qaone1");
            APEM.PhaseExecWindow.ExecutionInternalFrame.PasswordEditor.SetText("Aspen111");
            SendKeys.SendWait("{Enter}");
            Thread.Sleep(10000);
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(10000);
            //APEM.PhaseExecWindow.GetSnapshot(Resultpath + "FILLING_MEASUREMENT2.PNG");
            APEM.PhaseExecWindow.ExecutionInternalFrame.PHActualEditor.SendKeys("8.3");
            APEM.PhaseExecWindow.ExecutionInternalFrame.TempActualEditor.SendKeys("64.00");
            APEM.PhaseExecWindow.ExecutionInternalFrame.UserIDEditor.SendKeys("qae\\qaone1");
            APEM.PhaseExecWindow.ExecutionInternalFrame.PasswordEditor.SetText("Aspen111");
            SendKeys.SendWait("{Enter}");
            Thread.Sleep(10000);
            APEM.PhaseExecWindow.ExecutionInternalFrame.UserIDEditor.SendKeys("qae\\qaone2");
            APEM.PhaseExecWindow.ExecutionInternalFrame.PasswordEditor.SetText("Aspen111");
            SendKeys.SendWait("{Enter}");
            Thread.Sleep(10000);
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(10000);
            //APEM.PhaseExecWindow.GetSnapshot(Resultpath + "MIXING_MEASUREMENT.PNG");
            APEM.PhaseExecWindow.ExecutionInternalFrame.PHActualEditor.SendKeys("8.3");
            APEM.PhaseExecWindow.ExecutionInternalFrame.TempActualEditor.SendKeys("64.00");
            APEM.PhaseExecWindow.ExecutionInternalFrame.UserIDEditor.SendKeys("qae\\qaone1");
            APEM.PhaseExecWindow.ExecutionInternalFrame.PasswordEditor.SetText("Aspen111");
            SendKeys.SendWait("{Enter}");
            Thread.Sleep(10000);
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(10000);
            APEM.PhaseExecWindow.GetSnapshot(Resultpath + "MIXING_SAMPLING(1).PNG");
            APEM.PhaseExecWindow.ExecutionInternalFrame.UserIDEditor.SendKeys("qae\\qaone1");
            APEM.PhaseExecWindow.ExecutionInternalFrame.PasswordEditor.SetText("Aspen111");
            SendKeys.SendWait("{Enter}");
            Thread.Sleep(4000);
            APEM.PhaseExecWindow.ConfirmationInternalFrame.YesButton.Click();
            APEM.PhaseExecWindow.ExecutionInternalFrame.ProductionStoppedList.SelectItems("No");
            APEM.PhaseExecWindow.ExecutionInternalFrame.ProductionResponseEditor.SendKeys("yyy");
            APEM.PhaseExecWindow.GetSnapshot(Resultpath + "MIXING_SAMPLING(2).PNG");
            APEM.PhaseExecWindow.ExecutionInternalFrame.UserIDEditor.SendKeys("qae\\qaone1");
            APEM.PhaseExecWindow.ExecutionInternalFrame.PasswordEditor.SetText("Aspen111");
            SendKeys.SendWait("{Enter}");
            Thread.Sleep(10000);
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(10000);
            APEM.PhaseExecWindow.GetSnapshot(Resultpath + "PRODUCT_VALIDATION.PNG");
            APEM.PhaseExecWindow.ExecutionInternalFrame.SelectBox.GetCell(0, "Select").SetValue("1");
            APEM.PhaseExecWindow.ExecutionInternalFrame.SelectBox.GetCell(1, "Select").SetValue("1");
            APEM.PhaseExecWindow.ExecutionInternalFrame.SelectBox.GetCell(2, "Select").SetValue("1");
            Thread.Sleep(3000);
            APEM.PhaseExecWindow.ExecutionInternalFrame.UserIDEditor.SendKeys("qae\\qaone1");
            APEM.PhaseExecWindow.ExecutionInternalFrame.PasswordEditor.SetText("Aspen111");
            SendKeys.SendWait("{Enter}");
            Thread.Sleep(10000);
        }

    }
}