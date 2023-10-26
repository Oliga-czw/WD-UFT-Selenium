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
        [TestCaseID(948794)]
        [Title("UC822683_MOC_PFC editor_ The design structure displays on the center of the design window")]
        [TestCategory(ProductArea.RecipeManagement)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_948794()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            Application.LaunchMocAndLogin();
            Thread.Sleep(5000);
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            APEM.MocmainWindow.RPLDesignInternalFrame.AddRPL_Button.ClickSignle();
            Thread.Sleep(4000);
            APEM.MocmainWindow.RPLManagementInternalFrame.RPLName.SendKeys("testRpl");
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
            // UP
            APEM.PFCEditorWindow.UnitProcedure._UFT_CheckBox.Click();
            Thread.Sleep(8000);
            Mouse.Click(APEM.PFCEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            Thread.Sleep(3000);
            MOC_Fuction.AssertDesignWindow();
            APEM.PFCEditorWindow.GetSnapshot(Resultpath + "UnitProcedure.PNG");
            //copy
            APEM.PFCEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject.Click();
            APEM.PFCEditorWindow.CopyButton.ClickSignle();
            if (APEM.LoseCopiedDialog.IsExist())
            {
                APEM.LoseCopiedDialog.YesButton.Click();
            }
            Thread.Sleep(5000);
            //paste

            APEM.PFCEditorWindow.PFCDesignAppInternalFrame.FirstLink.Click();
            APEM.PFCEditorWindow.PasteButton.ClickSignle();
            if (APEM.PFCEditorWindow.PasteRenamedDialog.IsExist(3000))
            {
                APEM.PFCEditorWindow.PasteRenamedDialog.Close();
            }
            MOC_Fuction.AssertDesignWindow();
            //drag to other location
            var UnitProcedure1 = APEM.PFCEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject1;
            UnitProcedure1.Click();
            Mouse.DragAndDrop(UnitProcedure1.AbsoluteLocation, APEM.PFCEditorWindow.PFCDesignAppInternalFrame.FirstLink.AbsoluteLocation, MouseButton.Left);
            MOC_Fuction.AssertDesignWindow();
            //cut
            APEM.PFCEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject1.Click();
            APEM.PFCEditorWindow.CutButton.ClickSignle();
            Thread.Sleep(5000);
            APEM.CutElementDialog.YesButton.Click();
            MOC_Fuction.AssertDesignWindow();
            //delete
            APEM.PFCEditorWindow.PFCDesignAppInternalFrame.FirstLink.Click();
            APEM.PFCEditorWindow.PasteButton.ClickSignle();
            if (APEM.PFCEditorWindow.PasteRenamedDialog.IsExist())
            {
                APEM.PFCEditorWindow.PasteRenamedDialog.Close();
            }
            APEM.PFCEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject1.Click();
            APEM.PFCEditorWindow._UFT_Window.SendKeys(HP.LFT.SDK.Keys.Delete);
            Thread.Sleep(5000);
            MOC_Fuction.AssertDesignWindow();
            ////APEM.PFCEditorWindow.Parallel._UFT_CheckBox.Click();
            ////Thread.Sleep(5000);
            ////Mouse.Click(APEM.PFCEditorWindow.PFCDesignAppInternalFrame.FirstLink.AbsoluteLocation);
            ////Thread.Sleep(3000);
            ////APEM.PFCEditorWindow.UnitProcedure._UFT_CheckBox.Click();
            ////Thread.Sleep(5000);
            ////Mouse.Click(APEM.PFCEditorWindow.PFCDesignAppInternalFrame.LinkUiObject.AbsoluteLocation);
            ////Thread.Sleep(3000);
            ////APEM.PFCEditorWindow.UnitProcedure._UFT_CheckBox.Click();
            ////Thread.Sleep(5000);
            ////Mouse.Click(APEM.PFCEditorWindow.PFCDesignAppInternalFrame.ParallelDivergentUiObject._UFT_UiObject.AbsoluteLocation);
            ////Thread.Sleep(3000);
            ////MOC_Fuction.AssertDesignWindow();
            //OP
            APEM.PFCEditorWindow.UnitProcedure._UFT_CheckBox.Click();
            Thread.Sleep(5000);
            Mouse.Click(APEM.PFCEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            Thread.Sleep(3000);
            APEM.PFCEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject.DoubleClick();
            Thread.Sleep(8000);
            APEM.PFCEditorWindow.Operation._UFT_CheckBox.Click();
            Thread.Sleep(8000);
            Mouse.Click(APEM.PFCEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            Thread.Sleep(3000);
            MOC_Fuction.AssertDesignWindow();
            APEM.PFCEditorWindow.GetSnapshot(Resultpath + "Operation.PNG");
            //copy
            APEM.PFCEditorWindow.PFCDesignAppInternalFrame.OperationUiObject.Click();
            APEM.PFCEditorWindow.CopyButton.ClickSignle();
            if (APEM.LoseCopiedDialog.IsExist())
            {
                APEM.LoseCopiedDialog.YesButton.Click();
            }
            Thread.Sleep(5000);
            //paste

            APEM.PFCEditorWindow.PFCDesignAppInternalFrame.FirstLink.Click();
            APEM.PFCEditorWindow.PasteButton.ClickSignle();
            if (APEM.PFCEditorWindow.PasteRenamedDialog.IsExist())
            {
                APEM.PFCEditorWindow.PasteRenamedDialog.Close();
            }
            MOC_Fuction.AssertDesignWindow();
            //drag to other location
            var Operation1 = APEM.PFCEditorWindow.PFCDesignAppInternalFrame.OperationUiObject1;
            Operation1.Click();
            Mouse.DragAndDrop(Operation1.AbsoluteLocation, APEM.PFCEditorWindow.PFCDesignAppInternalFrame.FirstLink.AbsoluteLocation, MouseButton.Left);
            MOC_Fuction.AssertDesignWindow();
            //cut
            APEM.PFCEditorWindow.PFCDesignAppInternalFrame.OperationUiObject1.Click();
            APEM.PFCEditorWindow.CutButton.ClickSignle();
            Thread.Sleep(5000);
            APEM.CutElementDialog.YesButton.Click();
            MOC_Fuction.AssertDesignWindow();
            //delete
            Thread.Sleep(5000);
            APEM.PFCEditorWindow.PFCDesignAppInternalFrame.OperationUiObject.Click();
            APEM.PFCEditorWindow._UFT_Window.SendKeys(HP.LFT.SDK.Keys.Delete);
            Thread.Sleep(5000);
            MOC_Fuction.AssertDesignWindow();
            //Phase
            APEM.PFCEditorWindow.Operation._UFT_CheckBox.Click();
            Thread.Sleep(8000);
            Mouse.Click(APEM.PFCEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            Thread.Sleep(3000);
            APEM.PFCEditorWindow.PFCDesignAppInternalFrame.OperationUiObject.DoubleClick();
            Thread.Sleep(5000);
            APEM.PFCEditorWindow.TabbedPaneControl.Select(2);
            Thread.Sleep(2000);
            APEM.PFCEditorWindow.First_Phase.Click();
            Thread.Sleep(8000);
            Mouse.Click(APEM.PFCEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            Thread.Sleep(3000);
            MOC_Fuction.AssertDesignWindow();
            APEM.PFCEditorWindow.GetSnapshot(Resultpath + "Phase.PNG");
            //copy
            APEM.PFCEditorWindow.PFCDesignAppInternalFrame.PhaseUiObject.Click();
            APEM.PFCEditorWindow.CopyButton.ClickSignle();
            if (APEM.LoseCopiedDialog.IsExist(3000))
            {
                APEM.LoseCopiedDialog.YesButton.Click();
            }
            Thread.Sleep(5000);
            //paste

            APEM.PFCEditorWindow.PFCDesignAppInternalFrame.FirstLink.Click();
            APEM.PFCEditorWindow.PasteButton.ClickSignle();
            if (APEM.PFCEditorWindow.PasteRenamedDialog.IsExist(3000))
            {
                APEM.PFCEditorWindow.PasteRenamedDialog.Close();
            }
            MOC_Fuction.AssertDesignWindow();
            //drag to other location
            var phaphase1 = APEM.PFCEditorWindow.PFCDesignAppInternalFrame.PhaseUiObject1;
            phaphase1.Click();
            Mouse.DragAndDrop(phaphase1.AbsoluteLocation, APEM.PFCEditorWindow.PFCDesignAppInternalFrame.FirstLink.AbsoluteLocation, MouseButton.Left);
            MOC_Fuction.AssertDesignWindow();
            //cut
            APEM.PFCEditorWindow.PFCDesignAppInternalFrame.PhaseUiObject1.Click();
            APEM.PFCEditorWindow.CutButton.ClickSignle();
            Thread.Sleep(5000);
            APEM.CutElementDialog.YesButton.Click();
            MOC_Fuction.AssertDesignWindow();
            //delete
            Thread.Sleep(5000);
            APEM.PFCEditorWindow.PFCDesignAppInternalFrame.PhaseUiObject.Click();
            APEM.PFCEditorWindow._UFT_Window.SendKeys(HP.LFT.SDK.Keys.Delete);
            Thread.Sleep(5000);
            MOC_Fuction.AssertDesignWindow();
            //Import the attached RPL and check its design structure at three levels in the PFC editor
            APEM.PFCEditorWindow.BackButton.ClickSignle();
            Thread.Sleep(2000);
            APEM.PFCEditorWindow.BackButton.ClickSignle();
            Thread.Sleep(2000);
            MOC_Fuction.ImportRPLDesign("RPL_DERMS_PACK_01_02.CHK");
            MOC_Fuction.AssertDesignWindow();
            APEM.PFCEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject1.DoubleClick();
            Thread.Sleep(4000);
            MOC_Fuction.AssertDesignWindow();
            APEM.PFCEditorWindow.PFCDesignAppInternalFrame.OperationUiObject1.DoubleClick();
            Thread.Sleep(4000);
            MOC_Fuction.AssertDesignWindow();
            APEM.PFCEditorWindow.PFCDesignAppInternalFrame._UFT_InterFrame.Resize(325, 790);
            Thread.Sleep(4000);
            MOC_Fuction.AssertDesignWindow();
            APEM.PFCEditorWindow.GetSnapshot(Resultpath + "Resize.PNG");
            APEM.PFCEditorWindow.PFCDesignAppInternalFrame._UFT_InterFrame.Resize(1104, 852);
            Thread.Sleep(4000);
            MOC_Fuction.AssertDesignWindow();

        }

    }
}