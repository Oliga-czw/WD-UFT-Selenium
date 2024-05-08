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
using MES_APEM_UFT_Selenium_Auto.Product.SQLplus;
using MES_APEM_UFT_Selenium_Auto.Product.APRM;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(539370)]
        [Title("Inspired by customer defect 516907 - run environment and run script with BATCH_RECORD_WRITE")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        //[TestMethod]
        public void VSTS_539370()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string RPLname = "FOR_STATUS";
            string Ordername1 = "Test001";
            string BPLname = "BPL520174";
            GML_Function.GMLAPRMConfig();
            Library.BaseLibrary.Application.LaunchMocAndLogin();
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLname).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("TEMP539370.zip");
            }
            MOC_Fuction.PlanFromRPL(RPLname, Ordername1, false);
            APEM.MocmainWindow.BPLDesign.Click();
            APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLname, "Name").Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLListInternalFrame.LoadDesigner_Button.Click();
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.Execute.Run_Environment.Select();
            Thread.Sleep(4000);
            APEM.DesignEditorWindow.RunEnvironmentInternalFrame.SelectOrder.SelectItems(Ordername);
            APEM.DesignEditorWindow.RunEnvironmentInternalFrame.OKButton.Click();
            Thread.Sleep(3000);
            APEM.DesignEditorWindow.MessageInterFrame.OKButton.Click();
            APEM.ExecutionFinishedDialog.OKButton.Click();
            APEM.DesignEditorWindow.Close();
            //Open Batch query tool
            Application.LaunchBatchQueryTool();
            Thread.Sleep(3000);
            //open new query
            BatchQueryTool.NewQuery();
            //open batch detail display
            BatchQueryTool.BatchQueryToolWindow.ListView._STD_ListView.ActivateItem(PO_value);
            //wait for loading
            Thread.Sleep(15000);
            APRM.BatchMainWindow.TreeView.GetNode("Batch").Expand();
        }

    }
}