using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System;
using HP.LFT.SDK;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using System.IO;
using MES_APEM_UFT_Selenium_Auto.Product.SQLplus;
using System.Linq;
using MES_APEM_UFT_Selenium_Auto.Product.APRM;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using System.Collections.Generic;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(819205)]
        [Title("Inspired by customer defect- CONFIRM_SCREEN() could close phase on APEM Mobile")]
        [TestCategory(ProductArea.Integration_APEM)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1200000)]

        [TestMethod]
        public void VSTS_819205()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string OrderName = "ORDER819205";
            string RPLName = "RPL819205";
            string POname = "PO819205005";
            LogStep(@"1. config APRM admin and config enviroment");
            //APRM
            GML_Function.GMLAPRMConfig();
            //Environment
            string oldfile = Base_Directory.BatchConfig;
            string newFile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\BatchConfig.ini";
            string oldText = "MachineName";
            string newText = Environment.MachineName;
            Base_Function.ReplaceTextInNewFile(oldfile, newFile, oldText, newText);
            GML_Function.ConfigEnviroment(newFile);
            LogStep(@"2. import RPL");
            Application.LaunchMocAndLogin();
            //check bpl exit
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("TEMP819205.zip");
            }
            LogStep(@"3. Create data order from RPL and Execute ");
            APEM.MocmainWindow.Orders.ClickSignle();
            Thread.Sleep(2000);
            MOC_Fuction.CheckRowSelection();
            MOC_Fuction.PlanFromRPL(RPLName,OrderName,true,POname);
            //Execute the Order
            APEM.MocmainWindow.OrderTracking.ClickSignle();
            MOC_Fuction.CheckRowSelection();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingInternalFrame.CodeEditor.SetText(OrderName);
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingInternalFrame.Filterbutton.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingInternalFrame.OrderTable.Row("Active", "Status").DoubleClick();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.UnitProcedureUiObject.Click();
            Thread.Sleep(4000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.OperationUiObject.Click();
            Thread.Sleep(4000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.PhaseUiObject.Click();
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.PhaseUiObject._UFT_UiObject.Click(HP.LFT.SDK.MouseButton.Right);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.ExecuteButton.Select();
            Thread.Sleep(8000);
            APEM.PhaseExecWindow.ExecutionInternalFrame.OK_Button.Click();
            Thread.Sleep(7000);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.PhaseUiObject2.Click();
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.PhaseUiObject2._UFT_UiObject.Click(HP.LFT.SDK.MouseButton.Right);
            APEM.MocmainWindow.OrderTrackingPFCInternalFrame.ExecuteButton.Select();
            Thread.Sleep(60000);
            APEM.MocmainWindow.GetSnapshot(Resultpath + "Web_display_1_AutoFinished.PNG");
            Base_Assert.AreEqual(APEM.MocmainWindow.OrderTrackingPFCInternalFrame.Phase2_ExecutionStatus.Text, "Finished");





        }

    }
}