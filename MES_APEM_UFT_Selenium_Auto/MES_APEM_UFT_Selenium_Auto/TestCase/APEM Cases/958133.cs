using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System;
using HP.LFT.SDK;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;

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
            //import rpl
            //APEM.MocmainWindow.RPLDesign.ClickSignle();
            //if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row("PREPARERPL").Existing)
            //{
            //    MOC_TemplatesFunction.Importtemplates("CASE958133.zip");
            //}
            MOC_TemplatesFunction.Importtemplates("CASE958133.zip");
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            Thread.Sleep(5000);
            APEM.MocmainWindow.RPLDesignInternalFrame.SearchEditor.SetText("PREPARERPL");
            APEM.MocmainWindow.RPLDesignInternalFrame.Filterbutton.Click();
            APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row("Compilable").Click();
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
            MOC_Fuction.VerifyRPL("PREPARERPL");
            MOC_Fuction.CertifyRPL("PREPARERPL");
            Thread.Sleep(3000);
            MOC_Fuction.PlanFromRPL("PREPARERPL", "ORDER958133");
            APEM.MocmainWindow.WorkstationBP.ClickSignle();
            MOC_Fuction.CheckRowSelection();
            Thread.Sleep(3000);
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText("ORDER958133");
            APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(10000);
            APEM.PhaseExecWindow.ExecutionInternalFrame.OK_Button.Click();
            Thread.Sleep(10000);
            var status = APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable._UFT_Table.GetCell(0, "Status").Value;
            Assert.AreEqual(status, "Finished");

        }

    }
}