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
        [TestCaseID(949084)]
        [Title("UC822683_MOC_PFC editor_ Add save item at Operation and Phase level")]
        [TestCategory(ProductArea.RecipeManagement)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_949084()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            Application.LaunchMocAndLogin();
            Thread.Sleep(5000);
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row("2BPLS").Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.RPLDesignInternalFrame.LoadDesigner_Button.ClickSignle();
            Thread.Sleep(5000);
            MOC_Fuction.ImportCHKDesign("RPL_DERMS_PACK_01_02.CHK");
            //add up and Tansition
            //up
            APEM.DesignEditorWindow.UnitProcedure._UFT_CheckBox.Click();
            Thread.Sleep(8000);
            Base_Function.MouseClick(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.FirstLink.AbsoluteLocation);
            Thread.Sleep(3000);
            //Tansition
            APEM.DesignEditorWindow.Transition._UFT_CheckBox.Click();
            Thread.Sleep(8000);
            Base_Function.MouseClick(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.StartLink.AbsoluteLocation);
            Thread.Sleep(3000);
            APEM.DesignEditorWindow.SaveButton.ClickSignle();
            if (APEM.AuditReasonDialog.IsExist())
            {
                APEM.AuditReasonDialog.Reason.SendKeys("for test");
                APEM.AuditReasonDialog.OK.Click();
            }
            Thread.Sleep(5000);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "UnitProcedureSaved.PNG");
            Assert.IsTrue(APEM.DesignSavedDialog.IsExist());
            APEM.DesignSavedDialog.OKButton.Click();
            //OP
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject0.DoubleClick();
            Thread.Sleep(4000);
            //add op and script
            //op
            APEM.DesignEditorWindow.Operation._UFT_CheckBox.Click();
            Thread.Sleep(8000);
            Base_Function.MouseClick(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.StartLink.AbsoluteLocation);
            Thread.Sleep(3000);
            //script
            APEM.DesignEditorWindow.TabbedPaneControl.Select(1);
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.First_Phase.Click();
            Thread.Sleep(8000);
            Base_Function.MouseClick(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.StartLink.AbsoluteLocation);
            Thread.Sleep(3000);
            APEM.DesignEditorWindow.SaveButton.ClickSignle();
            if (APEM.AuditReasonDialog.IsExist())
            {
                APEM.AuditReasonDialog.OK.Click();
            }
            Thread.Sleep(5000);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "OperationSaved.PNG");
            Assert.IsTrue(APEM.DesignSavedDialog.IsExist());
            APEM.DesignSavedDialog.OKButton.Click();
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.DesignMenu.Save.Select();
            Thread.Sleep(4000);
            Assert.IsTrue(APEM.DesignSavedDialog.IsExist());
            APEM.DesignSavedDialog.OKButton.Click();
            Thread.Sleep(2000);
            //phase
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.OperationUiObject1.DoubleClick();
            Thread.Sleep(4000);
            //add parallel/serial
            APEM.DesignEditorWindow.Parallel._UFT_CheckBox.Click();
            Thread.Sleep(5000);
            Base_Function.MouseClick(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            Thread.Sleep(3000);
            APEM.DesignEditorWindow.SaveButton.ClickSignle();
            if (APEM.AuditReasonDialog.IsExist())
            {
                APEM.AuditReasonDialog.OK.Click();
            }
            Thread.Sleep(5000);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "PhaseSaved.PNG");
            Assert.IsTrue(APEM.DesignSavedDialog.IsExist());
            APEM.DesignSavedDialog.OKButton.Click();
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.DesignMenu.Save.Select();
            Thread.Sleep(4000);
            Assert.IsTrue(APEM.DesignSavedDialog.IsExist());
            APEM.DesignSavedDialog.OKButton.Click();
            Thread.Sleep(2000);


        }

    }
}