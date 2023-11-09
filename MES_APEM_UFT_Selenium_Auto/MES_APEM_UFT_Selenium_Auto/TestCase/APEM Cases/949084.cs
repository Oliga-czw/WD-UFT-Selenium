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
<<<<<<< Updated upstream
            APEM.MocmainWindow.RPLDesignInternalFrame.AddRPL_Button.ClickSignle();
            Thread.Sleep(4000);
            APEM.MocmainWindow.RPLManagementInternalFrame.RPLName.SendKeys("testRpl01");
            APEM.MocmainWindow.RPLManagementInternalFrame.RPLDescription.SendKeys("for testhahhah");
            APEM.MocmainWindow.RPLManagementInternalFrame.ConfirmChanges_Button.ClickSignle();
            if (APEM.MocmainWindow.AddReasonDialog.IsExist(4))
            {
                APEM.MocmainWindow.AddReasonDialog.Reason.SendKeys("for UFT test");
                APEM.MocmainWindow.AddReasonDialog.OK.Click();
            }
            Thread.Sleep(4000);
            APEM.MocmainWindow.RPLManagementInternalFrame.RPLTabControl.Select("Basic Phase Libraries");
            Thread.Sleep(3000);
            APEM.MocmainWindow.RPLManagementInternalFrame.SelectBPL_Button.ClickSignle();
            Thread.Sleep(5000);
            APEM.MocmainWindow.AvailableBPLDialog.AvailableBPLList.SelectItems("AAA_BPL (Version 1)");
            APEM.MocmainWindow.AvailableBPLDialog.OK.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.RPLManagementInternalFrame.RPLTabControl.Select("RPL Data");
            Thread.Sleep(3000);
            APEM.MocmainWindow.RPLManagementInternalFrame.LoadDesigner_Button.ClickSignle();
            Thread.Sleep(3000);
            //Import the attached RPL
            MOC_Fuction.ImportRPLDesign("RPL_DERMS_PACK_01_02.CHK");
=======
            APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row("2BPLS").Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.RPLDesignInternalFrame.LoadDesigner_Button.ClickSignle();
>>>>>>> Stashed changes
            Thread.Sleep(5000);
            //add up and Tansition
            //up
            APEM.PFCEditorWindow.UnitProcedure._UFT_CheckBox.Click();
            Thread.Sleep(8000);
            Base_Function.MouseClick(APEM.PFCEditorWindow.PFCDesignAppInternalFrame.FirstLink.AbsoluteLocation);
            Thread.Sleep(3000);
            //Tansition
            APEM.PFCEditorWindow.Transition._UFT_CheckBox.Click();
            Thread.Sleep(8000);
            Base_Function.MouseClick(APEM.PFCEditorWindow.PFCDesignAppInternalFrame.StartLink.AbsoluteLocation);
            Thread.Sleep(3000);
            APEM.PFCEditorWindow.SaveButton.ClickSignle();
            if (APEM.AuditReasonDialog.IsExist())
            {
                APEM.AuditReasonDialog.Reason.SendKeys("for test");
                APEM.AuditReasonDialog.OK.Click();
            }
            Thread.Sleep(5000);
            APEM.PFCEditorWindow.GetSnapshot(Resultpath + "UnitProcedureSaved.PNG");
<<<<<<< Updated upstream
            Base_Assert.IsTrue(APEM.DesignSavedDialog.IsExist());
=======
            Assert.IsTrue(APEM.DesignSavedDialog.IsExist());
>>>>>>> Stashed changes
            APEM.DesignSavedDialog.OKButton.Click();
            //OP
            APEM.PFCEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject0.DoubleClick();
            Thread.Sleep(4000);
            //add op and script
            //op
            APEM.PFCEditorWindow.Operation._UFT_CheckBox.Click();
            Thread.Sleep(8000);
            Base_Function.MouseClick(APEM.PFCEditorWindow.PFCDesignAppInternalFrame.StartLink.AbsoluteLocation);
            Thread.Sleep(3000);
            //script
            APEM.PFCEditorWindow.TabbedPaneControl.Select(1);
            Thread.Sleep(2000);
            APEM.PFCEditorWindow.First_Phase.Click();
            Thread.Sleep(8000);
            Base_Function.MouseClick(APEM.PFCEditorWindow.PFCDesignAppInternalFrame.StartLink.AbsoluteLocation);
            Thread.Sleep(3000);
            APEM.PFCEditorWindow.SaveButton.ClickSignle();
            if (APEM.AuditReasonDialog.IsExist())
            {
                APEM.AuditReasonDialog.OK.Click();
            }
            Thread.Sleep(5000);
            APEM.PFCEditorWindow.GetSnapshot(Resultpath + "OperationSaved.PNG");
<<<<<<< Updated upstream
            Base_Assert.IsTrue(APEM.DesignSavedDialog.IsExist());
            APEM.DesignSavedDialog.OKButton.Click();
            Thread.Sleep(2000);
            APEM.PFCEditorWindow.DesignMenu.Save.Select();
            Base_Assert.IsTrue(APEM.DesignSavedDialog.IsExist());
=======
            Assert.IsTrue(APEM.DesignSavedDialog.IsExist());
            APEM.DesignSavedDialog.OKButton.Click();
            Thread.Sleep(2000);
            APEM.PFCEditorWindow.DesignMenu.Save.Select();
            Thread.Sleep(4000);
            Assert.IsTrue(APEM.DesignSavedDialog.IsExist());
>>>>>>> Stashed changes
            APEM.DesignSavedDialog.OKButton.Click();
            Thread.Sleep(2000);
            //phase
            APEM.PFCEditorWindow.PFCDesignAppInternalFrame.OperationUiObject1.DoubleClick();
            Thread.Sleep(4000);
            //add parallel/serial
            APEM.PFCEditorWindow.Parallel._UFT_CheckBox.Click();
            Thread.Sleep(5000);
            Base_Function.MouseClick(APEM.PFCEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            Thread.Sleep(3000);
            APEM.PFCEditorWindow.SaveButton.ClickSignle();
            if (APEM.AuditReasonDialog.IsExist())
            {
                APEM.AuditReasonDialog.OK.Click();
            }
            Thread.Sleep(5000);
            APEM.PFCEditorWindow.GetSnapshot(Resultpath + "PhaseSaved.PNG");
<<<<<<< Updated upstream
            Base_Assert.IsTrue(APEM.DesignSavedDialog.IsExist());
            APEM.DesignSavedDialog.OKButton.Click();
            Thread.Sleep(2000);
            APEM.PFCEditorWindow.DesignMenu.Save.Select();
            Base_Assert.IsTrue(APEM.DesignSavedDialog.IsExist());
=======
            Assert.IsTrue(APEM.DesignSavedDialog.IsExist());
            APEM.DesignSavedDialog.OKButton.Click();
            Thread.Sleep(2000);
            APEM.PFCEditorWindow.DesignMenu.Save.Select();
            Thread.Sleep(4000);
            Assert.IsTrue(APEM.DesignSavedDialog.IsExist());
>>>>>>> Stashed changes
            APEM.DesignSavedDialog.OKButton.Click();
            Thread.Sleep(2000);


        }

    }
}