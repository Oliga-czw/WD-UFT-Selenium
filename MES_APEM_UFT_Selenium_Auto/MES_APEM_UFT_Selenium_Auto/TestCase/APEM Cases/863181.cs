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
        [TestCaseID(863181)]
        [Title("UC822680 _ PFC editor _ Drag component on the Chart area window and click Undo drag button")]
        [TestCategory(ProductArea.RecipeManagement)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_863181()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            Library.BaseLibrary.Application.LaunchMocAndLogin();
            Thread.Sleep(5000);
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            MOC_Fuction.AddRPL_OpenDesign("RPL863181", "AAA_BPL (Version 1)");
            MOC_Fuction.ImportCHKDesign("RPL_DERMS_PACK_01_02.CHK");
            //UP
            Mouse.ButtonDown(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject1.AbsoluteLocation);
            Thread.Sleep(3000);
            Point adress = new Point(200, 400);
            Mouse.Move(adress);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "DragFollow.PNG");
            Thread.Sleep(3000);
            Mouse.Move(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.FirstLink.AbsoluteLocation);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "DragHighlight.PNG");
            Thread.Sleep(3000);
            Mouse.ButtonUp(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.FirstLink.AbsoluteLocation);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "DragDrop.PNG");
            Thread.Sleep(3000);
            //drag Parallel
            Mouse.ButtonDown(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ParallelDivergent.AbsoluteLocation);
            Thread.Sleep(3000);
            Mouse.Move(adress);
            Thread.Sleep(3000);
            Mouse.ButtonUp(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.LinkUiObject2.AbsoluteLocation);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "DragParallel.PNG");
            Thread.Sleep(3000);
            APEM.DesignEditorWindow.UndoButton.ClickSignle();
            Thread.Sleep(3000);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "Undo.PNG");

        }

    }
}