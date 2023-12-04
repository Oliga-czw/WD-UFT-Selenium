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
        [TestCaseID(863166)]
        [Title("UC822680 _ PFC editor _ Add components from the Component palette")]
        [TestCategory(ProductArea.RecipeManagement)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_863166()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            Library.BaseLibrary.Application.LaunchMocAndLogin();
            Thread.Sleep(5000);
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            MOC_Fuction.AddRPL_OpenDesign("RPL863166", "AAA_BPL (Version 1)");
            APEM.DesignEditorWindow.UnitProcedure._UFT_CheckBox.Click();
            Thread.Sleep(8000);
            Point adress = new Point(200, 400);
            Mouse.Move(adress);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "UnitProcedure.PNG");
            Thread.Sleep(5000);
            Mouse.Move(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "UPHighlight.PNG");
            Thread.Sleep(5000);
            Base_Function.MouseClick(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "UPAdded.PNG");
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject.DoubleClick();
            Thread.Sleep(4000);
            APEM.DesignEditorWindow.Operation._UFT_CheckBox.Click();
            Thread.Sleep(8000);
            Mouse.Move(adress);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "Operation.PNG");
            Thread.Sleep(5000);
            Mouse.Move(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "OPHighlight");
            Thread.Sleep(5000);
            Base_Function.MouseClick(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            Thread.Sleep(3000);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "OPAdded.PNG");


            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.OperationUiObject.DoubleClick();
            Thread.Sleep(4000);
            APEM.DesignEditorWindow.TabbedPaneControl.Select(2);
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.First_Phase.Click();
            Thread.Sleep(8000);
            Mouse.Move(adress);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "Phase.PNG");
            Thread.Sleep(5000);
            Mouse.Move(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "PHHighlight.PNG");
            Thread.Sleep(5000);
            Base_Function.MouseClick(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            Thread.Sleep(3000);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "PHAdded.PNG");

        }

    }
}