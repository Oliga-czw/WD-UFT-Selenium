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
        [TestCaseID(958262)]
        [Title("UC822683_MOC_PFC editor_ Verify and Compile items are under the new added Build menu at the Operation and Phase level (When the design have problem)")]
        [TestCategory(ProductArea.RecipeManagement)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_958262()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            Application.LaunchMocAndLogin();
            Thread.Sleep(5000);
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            MOC_Fuction.AddRPL_OpenDesign("TESTRPL");
            MOC_Fuction.ImportRPLDesign("RPL_DERMS_PACK_01_02.CHK");
            Thread.Sleep(5000);
            APEM.PFCEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject1.DoubleClick();
            Thread.Sleep(4000);
            APEM.PFCEditorWindow.Transition._UFT_CheckBox.Click();
            Thread.Sleep(8000);
            Mouse.Click(APEM.PFCEditorWindow.PFCDesignAppInternalFrame.StartLink.AbsoluteLocation);
            Thread.Sleep(3000);
            //click verify
            APEM.PFCEditorWindow.Build.Verify.Select();
            Thread.Sleep(5000);
            Base_Assert.AreEqual(APEM.DesignVerificationWindow._UFT_Window.IsEnabled, true);
            var listVerifyMeaasge = APEM.DesignVerificationWindow.ErrorList._UFT_IList.GetVisibleText();
            Console.WriteLine(listVerifyMeaasge);
            Base_Assert.IsTrue(listVerifyMeaasge.Contains("Error: Components not found or not compiled:"));
            APEM.DesignVerificationWindow.GetSnapshot(Resultpath + "OPVerifyError.PNG");
            Thread.Sleep(3000);
            APEM.DesignVerificationWindow.Close();
            //click compile
            APEM.PFCEditorWindow.Build.Compile.Select();
            Thread.Sleep(5000);
            Base_Assert.AreEqual(APEM.DesignCompilationWindow._UFT_Window.IsEnabled, true);
            APEM.DesignCompilationWindow.GetSnapshot(Resultpath + "OPCompileError.PNG");
            Thread.Sleep(3000);
            var listCompileMeaasge = APEM.DesignCompilationWindow.ErrorList._UFT_IList.GetVisibleText();
            Console.WriteLine(listCompileMeaasge);
            Base_Assert.IsTrue(listCompileMeaasge.Contains("Error: Components not found or not compiled:"));
            APEM.DesignCompilationWindow.Close();
            ////phase
            APEM.PFCEditorWindow.PFCDesignAppInternalFrame.OperationUiObject1.DoubleClick();
            Thread.Sleep(4000);
            //click verify
            APEM.PFCEditorWindow.Build.Verify.Select();
            Thread.Sleep(5000);
            Base_Assert.AreEqual(APEM.DesignVerificationWindow._UFT_Window.IsEnabled, true);
            var listVerifyMeaasge1 = APEM.DesignVerificationWindow.ErrorList._UFT_IList.GetVisibleText();
            Console.WriteLine(listVerifyMeaasge1);
            Base_Assert.IsTrue(listVerifyMeaasge1.Contains("Error: Components not found or not compiled:"));
            APEM.DesignVerificationWindow.GetSnapshot(Resultpath + "PhaseVerifyError.PNG");
            Thread.Sleep(3000);
            APEM.DesignVerificationWindow.Close();
            //click compile
            Thread.Sleep(2000);
            APEM.PFCEditorWindow.Build.Compile.Select();
            Thread.Sleep(5000);
            Base_Assert.AreEqual(APEM.DesignCompilationWindow._UFT_Window.IsEnabled, true);
            APEM.DesignCompilationWindow.GetSnapshot(Resultpath + "PhaseCompileError.PNG");
            Thread.Sleep(3000);
            var listCompileMeaasge1 = APEM.DesignCompilationWindow.ErrorList._UFT_IList.GetVisibleText();
            Console.WriteLine(listCompileMeaasge1);
            Base_Assert.IsTrue(listCompileMeaasge1.Contains("Error: Components not found or not compiled:"));
            APEM.DesignCompilationWindow.Close();


        }

    }
}