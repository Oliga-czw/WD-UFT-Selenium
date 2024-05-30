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
        [TestCaseID(918007)]
        [Title("Inspired from custom defect 867449 and 1236707_batch_record_write() function should create new subbatch and read the data successfully")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1200000)]

        [TestMethod]
        public void VSTS_918007()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string PO_value = "case918007";
            string Ordername1 = "ORDER918007";
            string RPLname1 = "RPL918007";
            string Ordername2 = "ORDER918007_V1";
            string RPLname2 = "RPL918007_V1";
            //config APRM and enviroment
            GML_Function.GML_mMDMConfig();
            APRM_Fuction.BatchImportAprmAdmin();
            Application.LaunchBatchDetailDisplay();
            Batch_Fuction.setOptionData("Batch");
            APRM.BatchMainWindow.Close();
            Thread.Sleep(30000);
            string oldfile = Base_Directory.GMLConfig;
            string newFile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GMLConfig.ini";
            string oldText = "MachineName";
            string newText = Environment.MachineName;
            Base_Function.ReplaceTextInNewFile(oldfile, newFile, oldText, newText);
            GML_Function.ConfigEnviroment(newFile);
            Application.LaunchMocAndLogin();
            //check bpl exit
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row("BPL918007").Existing)
            {
                MOC_TemplatesFunction.Importtemplates("TEMP918007.zip");
            }
            MOC_Fuction.PlanFromRPL(RPLname1, Ordername1, true, PO_value);
            APEM.MocmainWindow.WorkstationBP.ClickSignle();
            MOC_Fuction.CheckRowSelection();
            Thread.Sleep(2000);
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText(Ordername1);
            APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(4000);
            APEM.PhaseExecWindow.ExecutionInternalFrame.WrinteBR_Button.Click();
            Thread.Sleep(2000);
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
            APRM.BatchMainWindow.TreeView.GetNode("Batch;UNITPROCEDURE5 [1]").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;UNITPROCEDURE5 [1];OPERATION11 [1]").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;UNITPROCEDURE5 [1];OPERATION11 [1];PHASE17 [1]").Expand();
            APRM.BatchMainWindow.TreeView.Select("Batch;UNITPROCEDURE5 [1];OPERATION11 [1];PHASE17 [1];TEST [1]");
            //wait for loading
            Thread.Sleep(5000);
            APRM.BatchMainWindow.GetSnapshot(Resultpath + "APRM Batch detail(1).PNG");
            var shown_items = APRM.BatchMainWindow.ListView._STD_ListView.GetVisibleText();
            //Console.WriteLine(shown_items);
            Assert.IsTrue(shown_items.Contains("test value 2"));
            Assert.IsTrue(shown_items.Contains("test value 3"));
            APRM.BatchMainWindow.Close();
            APEM.PhaseExecWindow.ExecutionInternalFrame.Field1_Editor.SendKeys("test value one");
            APEM.PhaseExecWindow.ExecutionInternalFrame.WrinteBR_Button.Click();
            Thread.Sleep(2000);
            BatchQueryTool.BatchQueryToolWindow.SetActive();
            Keyboard.PressKey(Keyboard.Keys.F9);
            Thread.Sleep(8000);
            BatchQueryTool.BatchQueryToolWindow.ListView._STD_ListView.ActivateItem(PO_value);
            //wait for loading
            Thread.Sleep(15000);
            //Check Begin_Source_Gross and End_Source_Gross fields should not show in APRM
            APRM.BatchMainWindow.TreeView.GetNode("Batch").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;UNITPROCEDURE5 [1]").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;UNITPROCEDURE5 [1];OPERATION11 [1]").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;UNITPROCEDURE5 [1];OPERATION11 [1];PHASE17 [1]").Expand();
            APRM.BatchMainWindow.TreeView.Select("Batch;UNITPROCEDURE5 [1];OPERATION11 [1];PHASE17 [1];TEST [2]");
            //wait for loading
            Thread.Sleep(5000);
            APRM.BatchMainWindow.GetSnapshot(Resultpath + "APRM Batch detail(2).PNG");
            var shown_items2 = APRM.BatchMainWindow.ListView._STD_ListView.GetVisibleText();
            //Console.WriteLine(shown_items2);
            Assert.IsTrue(shown_items2.Contains("test value one"));
            Assert.IsTrue(shown_items2.Contains("test value 2"));
            Assert.IsTrue(shown_items2.Contains("test value 3"));
            APRM.BatchMainWindow.Close();
            APEM.PhaseExecWindow.ExecutionInternalFrame.WrinteBR_Button.Click();
            Thread.Sleep(2000);
            BatchQueryTool.BatchQueryToolWindow.SetActive();
            Keyboard.PressKey(Keyboard.Keys.F9);
            Thread.Sleep(8000);
            BatchQueryTool.BatchQueryToolWindow.ListView._STD_ListView.ActivateItem(PO_value);
            //wait for loading
            Thread.Sleep(15000);
            APRM.BatchMainWindow.TreeView.GetNode("Batch").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;UNITPROCEDURE5 [1]").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;UNITPROCEDURE5 [1];OPERATION11 [1]").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;UNITPROCEDURE5 [1];OPERATION11 [1];PHASE17 [1]").Expand();
            APRM.BatchMainWindow.TreeView.Select("Batch;UNITPROCEDURE5 [1];OPERATION11 [1];PHASE17 [1];TEST [3]");
            //wait for loading
            Thread.Sleep(5000);
            APRM.BatchMainWindow.GetSnapshot(Resultpath + "APRM Batch detail(3).PNG");
            var shown_items3 = APRM.BatchMainWindow.ListView._STD_ListView.GetVisibleText();
            //Console.WriteLine(shown_items3);
            Assert.IsTrue(shown_items3.Contains("test value one"));
            Assert.IsTrue(shown_items3.Contains("test value 2"));
            Assert.IsTrue(shown_items3.Contains("test value 3"));
            APRM.BatchMainWindow.Close();
            BatchQueryTool.BatchQueryToolWindow.Close();
            BatchQueryTool.BatchQueryToolWindow.Save_Dialog.NO.Click();
            APEM.PhaseExecWindow.ExecutionInternalFrame.Cancel_Button.ClickSignle();
            Thread.Sleep(1000);
            APEM.PhaseExecWindow.ConfirmationInternalFrame.YesButton.Click();
            Thread.Sleep(2000);
            MOC_Fuction.PlanFromRPL(RPLname2, Ordername2);
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            driver.Wait();
            Mobile_Fuction.login();
            driver.Wait();
            Mobile.OrderProcess_Page.OrderSearch.SendKeys(Ordername2);
            Thread.Sleep(1000);
            Mobile.OrderProcess_Page.GotoTracking.Click();
            Thread.Sleep(1000);
            Mobile.OrderTracking_Page.ExecutionButton.Click();
            Thread.Sleep(5000);
            Mobile.OrderExecution_Page.WritedatainBR_button.Click();
            Thread.Sleep(2000);
            Mobile.OrderExecution_Page.ReadData_button.Click();
            Thread.Sleep(2000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "PathNotNull.PNG");
            Assert.IsNotNull(Mobile.OrderExecution_Page.Path_table._Selenium_WebElement.GetProperty("value"));
            Mobile.OrderExecution_Page.CancelButton.Click();
        }

    }
}