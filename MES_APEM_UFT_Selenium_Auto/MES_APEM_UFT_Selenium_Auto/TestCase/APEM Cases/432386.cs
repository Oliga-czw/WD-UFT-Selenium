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
using System.Collections.Generic;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(432386)]
        [Title("Inspired by customer defect 308651-- Web or Manage Enabling of BPs can be selected.")]
        [TestCategory(ProductArea.MOC)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        //[TestMethod]
        public void VSTS_432386()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string BPLname = "BPL432386";
            Library.BaseLibrary.Application.LaunchMocAndLogin();
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLname).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("TEMP432386.zip");
            }
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLname).DoubleClick();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLDataInternalFrame.TabbedPaneControl.Select("Basic Phases");
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLDataInternalFrame.ManageTable._UFT_Table.ActivateRow(0);
            Thread.Sleep(3000);
            APEM.MocmainWindow.BPLDataInternalFrame.ManageTable.GetCell(0, "Web").Click();
            APEM.MocmainWindow.BPLDataInternalFrame.ManageTable.GetCell(0, "Manage").Click();
            Thread.Sleep(1000);
            APEM.MocmainWindow.BPLDataInternalFrame.ConfirmChanges_Button.Click();
            MOC_Fuction.AddReason();
            Thread.Sleep(4000);
            APEM.MocmainWindow.BPLDataInternalFrame.ManageTable.GetCell(1, "Web").DoubleClick();
            Thread.Sleep(3000);
            APEM.MocmainWindow.BPLDataInternalFrame.ManageTable.GetCell(1, "Web").Click();
            APEM.MocmainWindow.BPLDataInternalFrame.ManageTable.GetCell(1, "Manage").Click();
            Thread.Sleep(1000);
            APEM.MocmainWindow.BPLDataInternalFrame.ConfirmChanges_Button.Click();
            MOC_Fuction.AddReason();
            Thread.Sleep(4000);
            APEM.MocmainWindow.GetSnapshot(Resultpath + "worksWell.PNG");
            SqlHelper helper = new SqlHelper();
            string SQL = $"select WEB_ENABLED,CAN_MANAGE from EBR_SUBPROC_SUBOPERATION where ID_SUBPROC =(select ID_SUBPROCEDURE from EBR_SUBPROCEDURE where TAG = 'BPL432386');";
            List<List<string>> Source = helper.Execute(SQL);
            Base_Assert.AreEqual(Source[0][0], "1");
            Base_Assert.AreEqual(Source[0][0], "1");
            Base_Assert.AreEqual(Source[1][0], "1");
            Base_Assert.AreEqual(Source[1][0], "1");
            Base_Assert.AreEqual(Source[2][0], "0");
            Base_Assert.AreEqual(Source[2][0], "0");

        }

    }
}