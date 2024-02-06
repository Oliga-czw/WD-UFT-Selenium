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
        [TestCaseID(949087)]
        [Title("UC822683_MOC_PFC editor_ Verify and Compile items are under the new added Build menu at the Operation and Phase level")]
        [TestCategory(ProductArea.RecipeManagement)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_949087()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            Application.LaunchMocAndLogin();
            Thread.Sleep(5000);
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            Thread.Sleep(5000);
            APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row("HASDHSV").Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.RPLDesignInternalFrame.LoadDesigner_Button.ClickSignle();
            Thread.Sleep(5000);
            //up
            //click verify
            APEM.DesignEditorWindow.Build.Verify.Select();
            Thread.Sleep(5000);
            APEM.AuditReasonDialog.Reason.SendKeys("for test");
            APEM.AuditReasonDialog.OK.Click();
            Thread.Sleep(4000);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "UnitProcedureVerify.PNG");
            Base_Assert.AreEqual(APEM.DesignVerificationDialog._UFT_Dialog.IsEnabled, true);
            APEM.DesignVerificationDialog.OKButton.Click();
            ////click compile
            APEM.DesignEditorWindow.Build.Compile.Select();
            Thread.Sleep(5000);
            APEM.AuditReasonDialog.OK.Click();
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "UnitProcedureCompile.PNG");
            Base_Assert.IsTrue(APEM.DesignCompilationDialog._UFT_Dialog.IsEnabled);
            APEM.DesignCompilationDialog.OKButton.Click();
            //OP
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject1.DoubleClick();
            Thread.Sleep(4000);
            //click verify
            APEM.DesignEditorWindow.Build.Verify.Select();
            Thread.Sleep(5000);
            APEM.AuditReasonDialog.OK.Click();
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "OperationVerify.PNG");
            Base_Assert.IsTrue(APEM.DesignVerificationDialog._UFT_Dialog.IsEnabled);
            APEM.DesignVerificationDialog.OKButton.Click();
            //click compile
            APEM.DesignEditorWindow.Build.Compile.Select();
            Thread.Sleep(5000);
            APEM.AuditReasonDialog.OK.Click();
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "OperationCompile.PNG");
            Base_Assert.IsTrue(APEM.DesignCompilationDialog._UFT_Dialog.IsEnabled);
            APEM.DesignCompilationDialog.OKButton.Click();
            //phase
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.OperationUiObject1.DoubleClick();
            Thread.Sleep(4000);
            //click verify
            APEM.DesignEditorWindow.Build.Verify.Select();
            Thread.Sleep(5000);
            APEM.AuditReasonDialog.OK.Click();
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "PhaseVerify.PNG");
            Base_Assert.IsTrue(APEM.DesignVerificationDialog._UFT_Dialog.IsEnabled);
            APEM.DesignVerificationDialog.OKButton.Click();
            //click compile
            APEM.DesignEditorWindow.Build.Compile.Select();
            Thread.Sleep(5000);
            APEM.AuditReasonDialog.OK.Click();
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "PhaseCompile.PNG");
            Base_Assert.IsTrue(APEM.DesignCompilationDialog._UFT_Dialog.IsEnabled);
            APEM.DesignCompilationDialog.OKButton.Click();


        }

    }
}