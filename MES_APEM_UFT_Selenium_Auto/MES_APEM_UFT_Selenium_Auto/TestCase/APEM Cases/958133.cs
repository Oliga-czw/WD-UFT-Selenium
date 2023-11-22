using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System;
using HP.LFT.SDK;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(958133)]
        [Title("UC822669_MOC_PFC editor:  Copy and paste transition/script across the levels")]
        [TestCategory(ProductArea.RecipeManagement)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_958133()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            Application.LaunchMocAndLogin();
            Thread.Sleep(5000);
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            Thread.Sleep(5000);
            APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row("PREPARERPL").Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.RPLDesignInternalFrame.LoadDesigner_Button.ClickSignle();
            Thread.Sleep(5000);
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ScriptUiObject.Click();
            //copy
            APEM.DesignEditorWindow.CopyButton.ClickSignle();
            if (APEM.LoseCopiedDialog.IsExist())
            {
                APEM.LoseCopiedDialog.YesButton.Click();
            }
            Thread.Sleep(5000);
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject.DoubleClick();
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.FirstLink.Click();
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.PasteButton.ClickSignle();
            if (APEM.DesignEditorWindow.PasteRenamedDialog.IsExist())
            {
                APEM.DesignEditorWindow.PasteRenamedDialog.Close();
            }
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "PasteOnOperation.PNG");
            Thread.Sleep(5000);
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.OperationUiObject.DoubleClick();
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.StartLink.Click();
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.PasteButton.ClickSignle();
            if (APEM.DesignEditorWindow.PasteRenamedDialog.IsExist())
            {
                APEM.DesignEditorWindow.PasteRenamedDialog.Close();
            }
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "PasteOnPhase.PNG");
            APEM.DesignEditorWindow.BackButton.ClickSignle();
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.TransitionUiObject.Click();
            //copy
            APEM.DesignEditorWindow.CopyButton.ClickSignle();
            if (APEM.LoseCopiedDialog.IsExist())
            {
                APEM.LoseCopiedDialog.YesButton.Click();
            }
            Thread.Sleep(5000);
            APEM.DesignEditorWindow.BackButton.ClickSignle();
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.FirstLink.Click();
            APEM.DesignEditorWindow.PasteButton.ClickSignle();
            if (APEM.DesignEditorWindow.PasteRenamedDialog.IsExist())
            {
                APEM.DesignEditorWindow.PasteRenamedDialog.Close();
            }
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "PasteOnUP.PNG");
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject.DoubleClick();
            Thread.Sleep(5000);
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.OperationUiObject.DoubleClick();
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.StartLink.Click();
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.PasteButton.ClickSignle();
            if (APEM.DesignEditorWindow.PasteRenamedDialog.IsExist())
            {
                APEM.DesignEditorWindow.PasteRenamedDialog.Close();
            }
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "PasteOnPhase(2).PNG");
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.SaveButton.ClickSignle();
            if (APEM.AuditReasonDialog.IsExist())
            {
                APEM.AuditReasonDialog.Reason.SendKeys("for test");
                APEM.AuditReasonDialog.OK.Click();
            }
            Thread.Sleep(5000);
            APEM.DesignSavedDialog.OKButton.Click();
            Thread.Sleep(2000);
            MOC_Fuction.DesignEditorClose();
            Thread.Sleep(5000);
            APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row("PREPARERPL").Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.RPLDesignInternalFrame.VerifyButton.ClickSignle();
            Thread.Sleep(3000);
            APEM.VerifyDialog.YesButton.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.OrderPlanDialog.CodeEditor.SendKeys("ORDER958133");
            APEM.MocmainWindow.OrderPlanDialog.DescriptionEditor.SendKeys("test for 958133");
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
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText("ORDER958133");
            APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(10000);
            APEM.PhaseExecWindow.ExecutionInternalFrame.OK_Button.Click();
            var status = APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable._UFT_Table.GetCell(0, "Status").Value;
            Assert.AreEqual(status, "Finished");

        }

    }
}