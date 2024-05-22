using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System;
using HP.LFT.SDK;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(213862)]
        [Title("CQ00298254 - successful to  track an order in which there is one phase in execution status")]
        [TestCategory(ProductArea.RecipeManagement)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_213862()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string ordername = "ORDER213862";
            string RPLname = "RPL213862";
            Library.BaseLibrary.Application.LaunchMocAndLogin();
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLname).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("TEMP213862.zip");
            }
            MOC_Fuction.PlanFromRPL(RPLname, ordername);
            MOC_Fuction.CheckRowSelection();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTracking.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingInternalFrame.CodeEditor.SetText(ordername);
            APEM.MocmainWindow.OrderTrackingInternalFrame.Filterbutton.Click();
            Thread.Sleep(2000);
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingInternalFrame.OrderTable._UFT_Table.ActivateRow(0);
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.UnitProcedureUiObject.DoubleClick();
            Thread.Sleep(4000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.OperationUiObject1.DoubleClick();
            Thread.Sleep(4000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.PhaseUiObject.Click();
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.PhaseUiObject._UFT_UiObject.Click(HP.LFT.SDK.MouseButton.Right);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.ExecuteButton.Select();
            Thread.Sleep(8000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame._UFT_InterFrame.Close();
            APEM.MocmainWindow.OrderTrackingInternalFrame._UFT_InterFrame.Close();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTracking.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingInternalFrame.CodeEditor.SetText(ordername);
            APEM.MocmainWindow.OrderTrackingInternalFrame.Filterbutton.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingInternalFrame.OrderTable._UFT_Table.ActivateRow(0);
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.UnitProcedureUiObject.DoubleClick();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.OperationUiObject1.DoubleClick();
            Thread.Sleep(1000);
            APEM.MocmainWindow.GetSnapshot(Resultpath + "successful_to_track.PNG");
            Base_Assert.AreEqual(APEM.MocmainWindow.OrderTrackingPFCInternalFrame.ExecutionStatus.Text, "Executing");
        }

    }
}