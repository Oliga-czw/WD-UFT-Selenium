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
using System.IO;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(213864)]
        [Title("CQ00310391 - successful to create Template  when RPL docs included")]
        [TestCategory(ProductArea.MOC)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1200000)]

        [TestMethod]
        public void VSTS_213864()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string RPLname = "RPL213864";
            string RPLName1Copy = "RPL213864_2";
            string TempName = "TEST213864";
            Library.BaseLibrary.Application.LaunchMocAndLogin();
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLname).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("TEMP213864.zip");
            }
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName1Copy).Existing)
            {
                APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLname, "Name").Click();
                APEM.MocmainWindow.RPLDesignInternalFrame.Copy_Button.ClickSignle();
                APEM.MocmainWindow.RPLDesignInternalFrame.Paste_Button.ClickSignle();
                APEM.MocmainWindow.RPLManagementInternalFrame.RPLName.SetText(RPLName1Copy);
                APEM.MocmainWindow.RPLManagementInternalFrame.ConfirmChanges_Button.ClickSignle();
                MOC_Fuction.AddReason();
            }

            APEM.MocmainWindow.Templates_moudle.Click();
            APEM.MOCTemplatesWindow.Templates.Export.Select();
            Thread.Sleep(4000);
            APEM.MOCTemplatesWindow.RPL.Click();
            Thread.Sleep(2000);
            APEM.MOCTemplatesWindow.RPLListInterFrame.SearchName.SetText(RPLName1Copy);
            APEM.MOCTemplatesWindow.RPLListInterFrame.LocalFilter.Click();
            Thread.Sleep(1000);
            APEM.MOCTemplatesWindow.RPLListInterFrame.SelectAll.ClickSignle();
            Thread.Sleep(1000);
            APEM.MOCTemplatesWindow.RPLListInterFrame.SearchName.SetText("");
            APEM.MOCTemplatesWindow.RPLListInterFrame.LocalFilter.Click();
            Thread.Sleep(1000);
            APEM.MOCTemplatesWindow.Check_Template.ClickSignle();
            if (APEM.MOCTemplatesWindow.LogWindow.IsExist(10))
            {
                APEM.MOCTemplatesWindow.LogWindow.Close();
            }
            APEM.MOCTemplatesWindow.Execute_Template.ClickSignle();
            APEM.MOCTemplatesWindow.TemplateExport.TemplateName.SetText(TempName);
            APEM.MOCTemplatesWindow.TemplateExport.Description.SetText("for test");
            APEM.MOCTemplatesWindow.TemplateExport.Version.SetText("1");
            APEM.MOCTemplatesWindow.TemplateExport.FileButton.Click();
            Thread.Sleep(2000);
            APEM.MOCTemplatesWindow.FileExport.HomeButton.Click();
            Thread.Sleep(1000);
            APEM.MOCTemplatesWindow.FileExport.FileExportButton.Click();
            Thread.Sleep(2000);
            if (APEM.ConfirmFileReplaceDialog.IsExist())
            {
                APEM.ConfirmFileReplaceDialog.YesButton.Click();
                Thread.Sleep(2000);
            }
            APEM.MOCTemplatesWindow.TemplateExport.ConfirmChangesButton.Click();
            Thread.Sleep(2000);
            if (APEM.RepeatedTemplateDialog.IsExist())
            {
                APEM.RepeatedTemplateDialog.OK.Click();
                Thread.Sleep(2000);
                APEM.MOCTemplatesWindow.TemplateExport.Version.SetText("3");
                APEM.MOCTemplatesWindow.TemplateExport.ConfirmChangesButton.Click();
            }
            Thread.Sleep(2000);
            if (APEM.ExecuteTemplateDialog.IsExist())
            {
                APEM.ExecuteTemplateDialog.GetSnapshot(Resultpath + "ExportSuccessful.PNG");
                APEM.ExecuteTemplateDialog.OKButton.Click();
            }
            APEM.MOCTemplatesWindow.Close();
            APEM.CloseDialog.YesButton.Click();
            Thread.Sleep(2000);
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = desktopPath + "\\" + TempName + ".ZIP";
            Console.WriteLine(filePath);
            Base_Assert.IsTrue(File.Exists(filePath));
            APEM.MocmainWindow.Templates_moudle.Click();
            Thread.Sleep(3000);
            APEM.MOCTemplatesWindow.Templates.Import.Select();
            Thread.Sleep(4000);
            APEM.MOCTemplatesWindow.FileImport.HomeButton.ClickSignle();
            Thread.Sleep(4000);
            APEM.MOCTemplatesWindow.FileImport.LookInList._UFT_IList.ActivateItem(TempName + ".zip");

            if (APEM.MOCTemplatesWindow.LogWindow.IsExist(10))
            {
                APEM.MOCTemplatesWindow.LogWindow.Close();
            }
            APEM.MOCTemplatesWindow.Check_Template.ClickSignle();
            if (APEM.MOCTemplatesWindow.LogWindow.IsExist(10))
            {
                APEM.MOCTemplatesWindow.LogWindow.Close();
            }
            APEM.MOCTemplatesWindow.Execute_Template.ClickSignle();
            if (APEM.MOCTemplatesWindow.LogWindow.IsExist(360))
            {
                APEM.MOCTemplatesWindow.LogWindow.Close();
            }
            if (APEM.ExecuteTemplateDialog.IsExist())
            {
                APEM.ExecuteTemplateDialog.OKButton.Click();
            }
            APEM.MOCTemplatesWindow.Close();
            APEM.CloseDialog.YesButton.Click();
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            Thread.Sleep(3000);
            APEM.MocmainWindow.RPLDesignInternalFrame.SearchEditor.SetText(RPLName1Copy);
            APEM.MocmainWindow.RPLDesignInternalFrame.Filterbutton.Click();
            Thread.Sleep(1000);
            APEM.MocmainWindow.GetSnapshot(Resultpath + "ImportSuccessful.PNG");
            Base_Assert.IsTrue(APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Rowscount() > 1);
            APEM.MocmainWindow.RPLDesignInternalFrame.SearchEditor.SetText("");
            APEM.MocmainWindow.RPLDesignInternalFrame.Filterbutton.Click();
        }

    }
}