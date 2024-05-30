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
        [TestCaseID(213769)]
        [Title("CQ00171285: Prevent (sub-)document certfication if parent RPL/BPL is not certified")]
        [TestCategory(ProductArea.MOC)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1200000)]

        
        [TestMethod]
        public void VSTS_213769()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string BPLName = "BPL213769";
            string RPLName = "RPL213769";
            Application.LaunchMocAndLogin();
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            Thread.Sleep(2000);
            //check RPL exit
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLName).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("TEMP213769.zip");

            }
            //verify BPL_Subdoc
            APEM.MocmainWindow.BPLListInternalFrame.Refresh_Button.ClickSignle();
            APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLName).DoubleClick();
            APEM.MocmainWindow.BPLDataInternalFrame.TabbedPaneControl.Select("Subdocuments");
            APEM.MocmainWindow.BPLDataInternalFrame.VerifyButton.ClickSignle();
            Thread.Sleep(2000);
            APEM.VerificationConfirmDialog.YesButton.Click();
            APEM.MocmainWindow.ConfirmDialog.PasswordEditor.SendKeys(PassWord.qaone1);
            APEM.MocmainWindow.ConfirmDialog.Reason.SendKeys("Test");
            APEM.MocmainWindow.ConfirmDialog.OK.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.GetSnapshot(Resultpath + "subdoc_certifyDiabled.PNG");
            Base_Assert.IsFalse(APEM.MocmainWindow.BPLDataInternalFrame.CertifyButton.IsEnabled);
            APEM.MocmainWindow.BPLDataInternalFrame._UFT_InterFrame.Close();
            APEM.MocmainWindow.RPLDesign.ClickSignle();

            //verify RPL_doc
            APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName).DoubleClick();
            APEM.MocmainWindow.RPLManagementInternalFrame.RPLTabControl.Select("Documents");
            APEM.MocmainWindow.RPLManagementInternalFrame.LoadDesigner_Button.Click();
            Thread.Sleep(3000);
            APEM.DesignEditorWindow.Build.Compile.Select();
            MOC_Fuction.AddReason();
            APEM.DesignCompilationDialog.OKButton.Click();
            MOC_Fuction.DesignEditorClose();
            Thread.Sleep(2000);
            APEM.MocmainWindow.RPLManagementInternalFrame.VerifyButton.ClickSignle();
            Thread.Sleep(3000);
            APEM.VerifyDialog.NoButton.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.ConfirmDialog.PasswordEditor.SendKeys(PassWord.qaone1);
            APEM.MocmainWindow.ConfirmDialog.Reason.SendKeys("Test");
            APEM.MocmainWindow.ConfirmDialog.OK.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.GetSnapshot(Resultpath + "doc_certifyDiabled.PNG");
            Base_Assert.IsFalse(APEM.MocmainWindow.RPLManagementInternalFrame.CertifyButton.IsEnabled);
            //verify and certify  BPL,RPL
            MOC_Fuction.VerifyBPL(BPLName);
            MOC_Fuction.CertifyBPL(BPLName);
            MOC_Fuction.VerifyRPL(RPLName);
            MOC_Fuction.CertifyRPL(RPLName);
            APEM.MocmainWindow.RPLDesignInternalFrame._UFT_InterFrame.Close();
            //certify BPL_Subdoc
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLName).DoubleClick();
            APEM.MocmainWindow.BPLDataInternalFrame.TabbedPaneControl.Select("Subdocuments");
            APEM.MocmainWindow.BPLDataInternalFrame.CertifyButton.ClickSignle();
            Thread.Sleep(2000);
            APEM.CertifyConfirmDialog.YesButton.Click();
            APEM.MocmainWindow.ConfirmDialog.PasswordEditor.SendKeys(PassWord.qaone1);
            APEM.MocmainWindow.ConfirmDialog.Reason.SendKeys("Test");
            APEM.MocmainWindow.ConfirmDialog.OK.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.GetSnapshot(Resultpath + "subdoc_certifysuccessfully.PNG");
            //certify RPL_Doc
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName).DoubleClick();
            APEM.MocmainWindow.RPLManagementInternalFrame.RPLTabControl.Select("Documents");
            APEM.MocmainWindow.RPLManagementInternalFrame.CertifyButton.ClickSignle();
            Thread.Sleep(3000);
            APEM.CertifyDialog.YesButton.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.ConfirmDialog.PasswordEditor.SendKeys(PassWord.qaone1);
            APEM.MocmainWindow.ConfirmDialog.Reason.SendKeys("Test");
            APEM.MocmainWindow.ConfirmDialog.OK.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.GetSnapshot(Resultpath + "Doc_certifysuccessfully.PNG");
        }

    }
}