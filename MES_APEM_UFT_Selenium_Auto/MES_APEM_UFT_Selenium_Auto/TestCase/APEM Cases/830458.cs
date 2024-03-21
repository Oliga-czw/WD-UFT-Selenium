using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System;
using HP.LFT.SDK;
using System.Windows.Forms;
using System.Drawing;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(830458)]
        [Title("UC822687 _ PFC editor _ Check instructions of components when design RPL")]
        [TestCategory(ProductArea.RecipeManagement)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_830458()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            Library.BaseLibrary.Application.LaunchMocAndLogin();
            Thread.Sleep(5000);
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            MOC_Fuction.AddRPL_OpenDesign("RPL830458", "AAA_BPL (Version 1)");
            //do not click any components
            Base_Assert.IsTrue(APEM.DesignEditorWindow.components_instruction.AttachedText.Contains("Click the button to show the description."));
            //click up
            APEM.DesignEditorWindow.UnitProcedure._UFT_CheckBox.Click();
            Base_Assert.IsTrue(APEM.DesignEditorWindow.components_instruction.AttachedText.Contains("The UP is selected, drag to the desired position or click on the desired position to create an UP."));
            Thread.Sleep(8000);
            Point adress = new Point(200, 400);
            Mouse.Move(adress);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "UnitProcedure.PNG");
            Thread.Sleep(5000);
            Mouse.Move(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            Thread.Sleep(5000);
            Base_Function.MouseClick(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            //click Parallel
            APEM.DesignEditorWindow.Parallel._UFT_CheckBox.Click();
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "Parallel.PNG");
            Base_Assert.IsTrue(APEM.DesignEditorWindow.components_instruction.AttachedText.Contains("The parallel is selected, drag to the desired position or click on the desired position to create a parallel."));
            //click Serial
            APEM.DesignEditorWindow.Serial._UFT_CheckBox.Click();
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "Serial.PNG");
            Base_Assert.IsTrue(APEM.DesignEditorWindow.components_instruction.AttachedText.Contains("The serial is selected, drag to the desired position or click on the desired position to create a serial."));
            //click Transition
            APEM.DesignEditorWindow.Transition._UFT_CheckBox.Click();
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "Transition.PNG");
            Base_Assert.IsTrue(APEM.DesignEditorWindow.components_instruction.AttachedText.Contains("The transition is selected, drag to the desired position or click on the desired position to create a transition."));
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject.DoubleClick();
            Thread.Sleep(4000);
            //click Operation
            APEM.DesignEditorWindow.Operation._UFT_CheckBox.Click();
            Base_Assert.IsTrue(APEM.DesignEditorWindow.components_instruction.AttachedText.Contains("The operation is selected, drag to the desired position or click on the desired position to create an operation."));
            Thread.Sleep(8000);
            Mouse.Move(adress);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "Operation.PNG");
            Thread.Sleep(5000);
            Mouse.Move(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            Thread.Sleep(5000);
            Base_Function.MouseClick(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            Thread.Sleep(3000);
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.OperationUiObject.DoubleClick();
            Thread.Sleep(4000);
            APEM.DesignEditorWindow.TabbedPaneControl.Select(2);
            Thread.Sleep(2000);
            //click Phase
            APEM.DesignEditorWindow.First_Phase.Click();
            Base_Assert.IsTrue(APEM.DesignEditorWindow.components_instruction.AttachedText.Contains("The phase is selected, drag to on the desired position or click on the desired position to create a phase."));
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "Phase.PNG");
            Thread.Sleep(2000);
            //click Scripts
            APEM.DesignEditorWindow.TabbedPaneControl.Select(1);
            Thread.Sleep(2000);
            //click Phase
            APEM.DesignEditorWindow.Script.Click();
            Base_Assert.IsTrue(APEM.DesignEditorWindow.components_instruction.AttachedText.Contains("The script is selected, drag to on the desired position or click on the desired position to create a script."));
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "Scripts.PNG");
            Thread.Sleep(2000);

        }

    }
}