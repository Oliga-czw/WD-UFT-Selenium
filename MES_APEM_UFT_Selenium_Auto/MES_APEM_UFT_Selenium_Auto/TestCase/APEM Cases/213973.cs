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
using System.IO;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(213973)]
        [Title("Full BPL Export- Multiple BPL selection-Export/merge-Ignore non-recognized files")]
        [TestCategory(ProductArea.RecipeManagement)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_213973()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string BPLname1 = "B213973_1";
            string BPLname2 = "B213973_2";
            var Unrecognizedfile = Base_File.DesktopPath + BPLname1 + "\\file.scss";
            Library.BaseLibrary.Application.LaunchMocAndLogin();
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            Thread.Sleep(3000);
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLname1, "Name").Existing)
            {
                MOC_TemplatesFunction.Importtemplates("TEMP213973.zip");
            }

            APEM.MocmainWindow.BPLDesign.Click();
            APEM.MocmainWindow.BPLListInternalFrame.NameSearch_Editor.SetText("B213973");
            APEM.MocmainWindow.BPLListInternalFrame.Filter_Button.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table._UFT_Table.SelectRowsRange(0, 1);
            APEM.MocmainWindow.BPLListInternalFrame.FullExport_Import_Button.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLExport_ImportDialog.FolderEditor.SetText(Base_File.DesktopPath);
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLExport_ImportDialog.ExportAllButton.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLExport_ImportDialog.OK.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.GetSnapshot(Resultpath + "ExportAll.PNG");
            if (APEM.MocmainWindow.LogWindow_Dialog.IsExist())
            {
                APEM.MocmainWindow.LogWindow_Dialog.Close();
            }
            Base_Assert.IsTrue(Directory.Exists(Base_File.DesktopPath + BPLname1));
            Base_Assert.IsTrue(Directory.Exists(Base_File.DesktopPath + BPLname2));
            string sourceName = Base_Directory.ProjectDir + "Data\\Input\\TestFile\\file.scss";
            string directoryPath = Base_File.DesktopPath + BPLname1;
            Base_File.CopyFile(sourceName, directoryPath, false);
            Thread.Sleep(10000);
            APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLname1, "Name").DoubleClick();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLDataInternalFrame.TabbedPaneControl.Select("Components");
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLDataInternalFrame.Delete_Button.Click();
            MOC_Fuction.AddReason();
            APEM.DeleteRowDialog.YesButton.Click();
            APEM.MocmainWindow.BPLDataInternalFrame._UFT_InterFrame.Close();
            APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLname2, "Name").DoubleClick();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLDataInternalFrame.TabbedPaneControl.Select("Local Basic Phases");
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLDataInternalFrame.Delete_Button.Click();
            MOC_Fuction.AddReason();
            APEM.DeleteRowDialog.YesButton.Click();
            APEM.MocmainWindow.BPLDataInternalFrame._UFT_InterFrame.Close();
            APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table._UFT_Table.SelectRowsRange(0, 1);
            APEM.MocmainWindow.BPLListInternalFrame.FullExport_Import_Button.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLExport_ImportDialog.Export_MergeButton.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLExport_ImportDialog.Ignore_non_recognized_files.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLExport_ImportDialog.OK.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.GetSnapshot(Resultpath + "Export_Merge(1).PNG");
            if (APEM.MocmainWindow.LogWindow_Dialog.IsExist())
            {
                foreach (var listdata in APEM.MocmainWindow.LogWindow_Dialog.Datalist.Items)
                {
                    Base_Assert.IsFalse(listdata.Text.Contains("local/TESTLOCAL"));
                    Base_Assert.IsFalse(listdata.Text.Contains("component/TESTHHH"));
                    Base_Assert.IsFalse(listdata.Text.Contains(Unrecognizedfile));
                }
                APEM.MocmainWindow.LogWindow_Dialog.Close();
            }
            APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table._UFT_Table.SelectRowsRange(0, 1);
            APEM.MocmainWindow.BPLListInternalFrame.FullExport_Import_Button.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLExport_ImportDialog.Export_MergeButton.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLExport_ImportDialog.OK.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.GetSnapshot(Resultpath + "Export_Merge(2).PNG");
            if (APEM.MocmainWindow.LogWindow_Dialog.IsExist())
            {
                foreach (var listdata in APEM.MocmainWindow.LogWindow_Dialog.Datalist.Items)
                {
                    if (listdata.Text.Contains("Unrecognized file " + Unrecognizedfile + " in export folder"))
                    {
                        break;
                    }
                }
                APEM.MocmainWindow.LogWindow_Dialog.Close();
            }

        }



    }


}