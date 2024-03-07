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
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(1006618)]
        [Title("Inspired from custom defect 968829_column validation code lost when deleting a column of a table component")]
        [TestCategory(ProductArea.MOC)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_1006618()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string BPLName = "BPL1006618";

            Application.LaunchMocAndLogin();
            LogStep(@"1. import BPL");
            //check BPL exit
            APEM.MocmainWindow.BPLDesign.Click();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLName).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("BPL1006618.zip");
            }
            LogStep(@"2. Execute BP");
            APEM.MocmainWindow.BPLDesign.Click();
            APEM.MocmainWindow.BPLListInternalFrame.Refresh_Button.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLName).Click();
            APEM.MocmainWindow.BPLListInternalFrame.LoadDesigner_Button.ClickSignle();
            LogStep(@"3. Check the columns validations");
            APEM.DesignEditorWindow.Window0.DoubleClick();
            APEM.DesignEditorWindow.Table.Click();
            APEM.DesignEditorWindow.Modify.Click();
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "columns data.PNG");
            List<string> D1 = new List<string>();
            List<string> D2 = new List<string>();
            //check Validation
            for (int i = 0; i < 4; i++)
            {
                APEM.DesignEditorWindow.ColumnEditorDialog.Table.GetCell(i, "Validation").Click();
                for (int j = 0; j < 4; j++)
                {
                    Base_Assert.IsFalse(APEM.DesignEditorWindow.ActionsEditorDialog.Table.GetCell(j, "Action").Value.ToString().Equals(""), "validation exists.");
                    //get column D data
                    if (i == 3)
                    {
                        APEM.DesignEditorWindow.GetSnapshot(Resultpath + "Column D Validation before.PNG");
                        D1.Add(APEM.DesignEditorWindow.ActionsEditorDialog.Table.GetCell(j, "Action").Value.ToString());
                    }
                }
                APEM.DesignEditorWindow.ActionsEditorDialog.Close();
                APEM.SaveChangesDialog.NoButton.Click();

            }
            //delete column B and check column D
            APEM.DesignEditorWindow.ColumnEditorDialog.Table.SelectRows(1);
            APEM.DesignEditorWindow.ColumnEditorDialog.Cut.Click();
            APEM.DesignEditorWindow.ColumnEditorDialog.Accept.Click();

            APEM.DesignEditorWindow.Modify.Click();
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "Delete column B.PNG");
            APEM.DesignEditorWindow.ColumnEditorDialog.Table.GetCell(2, "Validation").Click();
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "Column D Validation After.PNG");
            for (int j = 0; j < 4; j++)
            {
                Base_Assert.IsFalse(APEM.DesignEditorWindow.ActionsEditorDialog.Table.GetCell(j, "Action").Value.ToString().Equals(""), "validation exists.");
                //get column D data
                D2.Add(APEM.DesignEditorWindow.ActionsEditorDialog.Table.GetCell(j, "Action").Value.ToString());
            }
            APEM.DesignEditorWindow.ActionsEditorDialog.Close();
            APEM.SaveChangesDialog.NoButton.Click();
            //close column editor/Design Editor
            APEM.DesignEditorWindow.ColumnEditorDialog.Close();
            APEM.SaveChangesDialog.NoButton.Click();
            APEM.DesignEditorWindow.Close();
            APEM.CloseDialog.YesButton.Click();
            APEM.ChangesDesignDialog.NoButton.Click();
            Base_Assert.IsTrue(D1.SequenceEqual(D2), "The last column validation SAME.");

            APEM.ExitApplication();

            

        }

    }
}