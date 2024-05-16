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

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(431802)]
        [Title("Inspired by custom defect_TC For Defect 421725--RPL with editing BPL which has script component  can compile.")]
        [TestCategory(ProductArea.RecipeManagement)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1500000)]

        [TestMethod]
        public void VSTS_431802()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            Thread.Sleep(2000);
            string BPLname = "BPL431802";
            string RPLname = "RPL431802";
            Library.BaseLibrary.Application.LaunchMocAndLogin();
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLname).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("TEMP431802.zip");
            }

            APEM.MocmainWindow.BPLDesign.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLname, "Name").Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLListInternalFrame.EditBPL_Button.Click();
            Thread.Sleep(1000);
            APEM.BPLEditDialog.YesButton.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLDataInternalFrame.ConfirmChanges_Button.Click();
            MOC_Fuction.AddReason();
            Thread.Sleep(4000);
            APEM.MocmainWindow.RPLDesign.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLname, "Name").Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.RPLDesignInternalFrame.LoadDesigner_Button.Click();
            Thread.Sleep(3000);
            APEM.DesignEditorWindow.Build.Compile.Select();
            Thread.Sleep(5000);
            APEM.AuditReasonDialog.OK.Click();
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "CompileWell.PNG");
            Base_Assert.IsTrue(APEM.DesignCompilationDialog._UFT_Dialog.IsEnabled);
            APEM.DesignCompilationDialog.OKButton.Click();
            MOC_Fuction.DesignEditorClose();



        }

    }
}