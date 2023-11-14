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
            Library.BaseLibrary.Application.LaunchMocAndLogin();
            Thread.Sleep(5000);
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            MOC_Fuction.AddRPL_OpenDesign("TESTRPL01", "AAA_BPL (Version 1)");
            //UP
            APEM.DesignEditorWindow.UnitProcedure._UFT_CheckBox.Click();
            Thread.Sleep(8000);
            Base_Function.MouseClick(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            Thread.Sleep(3000);
            MOC_Fuction.AssertDesignWindow();
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "UnitProcedure.PNG");
            //copy
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject.Click();
            APEM.DesignEditorWindow.CopyButton.ClickSignle();
            if (APEM.LoseCopiedDialog.IsExist())
            {
                APEM.LoseCopiedDialog.YesButton.Click();
            }
            Thread.Sleep(5000);
            //paste

            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.FirstLink.Click();
            APEM.DesignEditorWindow.PasteButton.ClickSignle();
            if (APEM.DesignEditorWindow.PasteRenamedDialog.IsExist())
            {
                APEM.DesignEditorWindow.PasteRenamedDialog.Close();
            }
            MOC_Fuction.AssertDesignWindow();
            //drag to other location
            var UnitProcedure1 = APEM.DesignEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject1;
            UnitProcedure1.Click();
            Mouse.DragAndDrop(UnitProcedure1.AbsoluteLocation, APEM.DesignEditorWindow.PFCDesignAppInternalFrame.StartLink.AbsoluteLocation, MouseButton.Left);
            MOC_Fuction.AssertDesignWindow();
            //cut & paste
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject1.Click();
            APEM.DesignEditorWindow.CutButton.ClickSignle();
            Thread.Sleep(5000);
            APEM.CutElementDialog.YesButton.Click();
            MOC_Fuction.AssertDesignWindow();
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.FirstLink.Click();
            APEM.DesignEditorWindow.PasteButton.ClickSignle();
            if (APEM.DesignEditorWindow.PasteRenamedDialog.IsExist())
            {
                APEM.DesignEditorWindow.PasteRenamedDialog.Close();
            }
            MOC_Fuction.AssertDesignWindow();
            //delete
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject1.Click();
            Thread.Sleep(2000);
            SendKeys.SendWait("{DELETE}");
            Thread.Sleep(5000);
            MOC_Fuction.AssertDesignWindow();
            //APEM.DesignEditorWindow.Parallel._UFT_CheckBox.Click();
            //Thread.Sleep(5000);
            //Base_Function.MouseClick(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.StartLink.AbsoluteLocation);
            //Thread.Sleep(3000);
            
            //OP
            Thread.Sleep(3000);
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject.DoubleClick();
            Thread.Sleep(8000);
            APEM.DesignEditorWindow.Operation._UFT_CheckBox.Click();
            Thread.Sleep(8000);
            Base_Function.MouseClick(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            Thread.Sleep(3000);
            MOC_Fuction.AssertDesignWindow();
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "Operation.PNG");
            //copy
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.OperationUiObject.Click();
            APEM.DesignEditorWindow.CopyButton.ClickSignle();
            if (APEM.LoseCopiedDialog.IsExist())
            {
                APEM.LoseCopiedDialog.YesButton.Click();
            }
            Thread.Sleep(5000);
            //paste

            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.FirstLink.Click();
            APEM.DesignEditorWindow.PasteButton.ClickSignle();
            if (APEM.DesignEditorWindow.PasteRenamedDialog.IsExist())
            {
                APEM.DesignEditorWindow.PasteRenamedDialog.Close();
            }
            MOC_Fuction.AssertDesignWindow();
            //drag to other location
            var Operation1 = APEM.DesignEditorWindow.PFCDesignAppInternalFrame.OperationUiObject1;
            Operation1.Click();
            Mouse.DragAndDrop(Operation1.AbsoluteLocation, APEM.DesignEditorWindow.PFCDesignAppInternalFrame.FirstLink.AbsoluteLocation, MouseButton.Left);
            MOC_Fuction.AssertDesignWindow();
            //cut & paste
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.OperationUiObject1.Click();
            APEM.DesignEditorWindow.CutButton.ClickSignle();
            Thread.Sleep(5000);
            APEM.CutElementDialog.YesButton.Click();
            MOC_Fuction.AssertDesignWindow();
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.FirstLink.Click();
            APEM.DesignEditorWindow.PasteButton.ClickSignle();
            if (APEM.DesignEditorWindow.PasteRenamedDialog.IsExist())
            {
                APEM.DesignEditorWindow.PasteRenamedDialog.Close();
            }
            MOC_Fuction.AssertDesignWindow();
            //delete
            Thread.Sleep(5000);
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.OperationUiObject1.Click();
            SendKeys.SendWait("{DELETE}");
            Thread.Sleep(5000);
            MOC_Fuction.AssertDesignWindow();
            //Phase
            //APEM.DesignEditorWindow.Operation._UFT_CheckBox.Click();
            //Thread.Sleep(8000);
            //Base_Function.MouseClick(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            Thread.Sleep(3000);
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.OperationUiObject.DoubleClick();
            Thread.Sleep(5000);
            APEM.DesignEditorWindow.TabbedPaneControl.Select(2);
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.First_Phase.Click();
            Thread.Sleep(8000);
            Base_Function.MouseClick(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            Thread.Sleep(3000);
            MOC_Fuction.AssertDesignWindow();
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "Phase.PNG");
            //copy
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.PhaseUiObject.Click();
            APEM.DesignEditorWindow.CopyButton.ClickSignle();
            if (APEM.LoseCopiedDialog.IsExist())
            {
                APEM.LoseCopiedDialog.YesButton.Click();
            }
            Thread.Sleep(5000);
            //paste

            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.FirstLink.Click();
            APEM.DesignEditorWindow.PasteButton.ClickSignle();
            if (APEM.DesignEditorWindow.PasteRenamedDialog.IsExist())
            {
                APEM.DesignEditorWindow.PasteRenamedDialog.Close();
            }
            MOC_Fuction.AssertDesignWindow();
            //drag to other location
            var phaphase1 = APEM.DesignEditorWindow.PFCDesignAppInternalFrame.PhaseUiObject1;
            phaphase1.Click();
            Mouse.DragAndDrop(phaphase1.AbsoluteLocation, APEM.DesignEditorWindow.PFCDesignAppInternalFrame.FirstLink.AbsoluteLocation, MouseButton.Left);
            MOC_Fuction.AssertDesignWindow();
            //cut & paste
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.PhaseUiObject1.Click();
            APEM.DesignEditorWindow.CutButton.ClickSignle();
            Thread.Sleep(5000);
            APEM.CutElementDialog.YesButton.Click();
            MOC_Fuction.AssertDesignWindow();
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.FirstLink.Click();
            APEM.DesignEditorWindow.PasteButton.ClickSignle();
            if (APEM.DesignEditorWindow.PasteRenamedDialog.IsExist())
            {
                APEM.DesignEditorWindow.PasteRenamedDialog.Close();
            }
            MOC_Fuction.AssertDesignWindow();
            //delete
            Thread.Sleep(5000);
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.PhaseUiObject1.Click();
            SendKeys.SendWait("{DELETE}");
            
            Thread.Sleep(5000);
            MOC_Fuction.AssertDesignWindow();
            //Import the attached RPL and check its design structure at three levels in the PFC editor
            APEM.DesignEditorWindow.BackButton.ClickSignle();
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.BackButton.ClickSignle();
            Thread.Sleep(2000);
            MOC_Fuction.ImportCHKDesign("RPL_DERMS_PACK_01_02.CHK");
            MOC_Fuction.AssertDesignWindow();
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject1.DoubleClick();
            Thread.Sleep(4000);
            MOC_Fuction.AssertDesignWindow();
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.OperationUiObject1.DoubleClick();
            Thread.Sleep(4000);
            MOC_Fuction.AssertDesignWindow();
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame._UFT_InterFrame.Resize(325, 790);
            Thread.Sleep(4000);
            MOC_Fuction.AssertDesignWindow();
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "Resize.PNG");
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame._UFT_InterFrame.Resize(1104, 852);
            Thread.Sleep(4000);
            MOC_Fuction.AssertDesignWindow();

        }

    }
}