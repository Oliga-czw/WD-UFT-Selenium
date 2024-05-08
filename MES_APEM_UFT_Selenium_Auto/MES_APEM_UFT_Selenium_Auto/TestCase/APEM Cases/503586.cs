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
using System.Data;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(503586)]
        [Title("Inspired from defect 497119 -- SQL_UPDATE function")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_503586()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";

            string BPL = "BPL503586";


            Application.LaunchMocAndLogin();
          
            LogStep(@"1. import BP and Execute the BP");
            //check bpl exit
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPL).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("BPL503586.zip");
            }
            //Execute BP
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            APEM.MocmainWindow.BPLListInternalFrame.Refresh_Button.ClickSignle();
            APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPL).Click();
            APEM.MocmainWindow.BPLListInternalFrame.LoadDesigner_Button.ClickSignle();
            APEM.DesignEditorWindow.ExecuteButton.ClickSignle();
            //add reason
            if (APEM.AuditReasonDialog.IsExist())
            {
                APEM.AuditReasonDialog.Reason.SendKeys("Execute");
                APEM.AuditReasonDialog.OK.Click();
            }
            //click sql update
            APEM.DesignEditorWindow.ExecuteMainInternalFrame.SQL_UPDATEButton.ClickSignle();
            Thread.Sleep(1000);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "Error message.PNG");
            Base_Assert.AreEqual("Database error: Invalid object name 'S_Data'.", APEM.DesignEditorWindow.ErrorInterFrame.message.AttachedText,"Error message");
            APEM.DesignEditorWindow.ErrorInterFrame.OKButton.ClickSignle();

            APEM.DesignEditorWindow.ExecuteMainInternalFrame.OKButton.Click();
            APEM.ExecutionFinishedDialog.OKButton.Click();
            APEM.DesignEditorWindow.Close();
            APEM.CloseDialog.YesButton.Click();
            Thread.Sleep(3000);
            APEM.ExitApplication();
            LogStep(@"2. check result in DB");
            SqlHelper helper = new SqlHelper();
            string SQL = $"DBCC OPENTRAN";
            DataSet ds = new DataSet();
            helper.ExecuteSQLQuery(SQL, ref ds);
            Base_Assert.IsTrue(ds.Tables.Count==0,"No Transactions");



        }

    }
}