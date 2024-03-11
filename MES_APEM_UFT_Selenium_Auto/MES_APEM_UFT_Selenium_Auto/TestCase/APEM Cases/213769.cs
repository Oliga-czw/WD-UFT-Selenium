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
        [Timeout(600000)]

        //defect 1324485
        //[TestMethod]
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
                MOC_TemplatesFunction.Importtemplates("CASE213769.zip");

            }
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
            Base_Assert.IsFalse(APEM.MocmainWindow.BPLDataInternalFrame.CertifyButton.IsEnabled);
            APEM.MocmainWindow.GetSnapshot(Resultpath + "subdoc_certifyDiabled.PNG");
            APEM.MocmainWindow.BPLDataInternalFrame._UFT_InterFrame.Close();
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName).DoubleClick();
            APEM.MocmainWindow.RPLManagementInternalFrame.RPLTabControl.Select("Documents");
            Base_Assert.IsFalse(APEM.MocmainWindow.RPLManagementInternalFrame.VerifyButton.IsEnabled);
            Base_Assert.IsFalse(APEM.MocmainWindow.RPLManagementInternalFrame.CertifyButton.IsEnabled);
            APEM.MocmainWindow.GetSnapshot(Resultpath + "doc_verify_certifyDiabled.PNG");
            MOC_Fuction.VerifyBPL(BPLName);
            MOC_Fuction.CertifyBPL(BPLName);
            MOC_Fuction.VerifyRPL(RPLName);
            MOC_Fuction.CertifyRPL(RPLName);
            APEM.MocmainWindow.RPLDesignInternalFrame._UFT_InterFrame.Close();
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
            APEM.MocmainWindow.RPLManagementInternalFrame.VerifyButton.ClickSignle();
            APEM.MocmainWindow.ConfirmDialog.PasswordEditor.SendKeys(PassWord.qaone1);
            APEM.MocmainWindow.ConfirmDialog.Reason.SendKeys("Test");
            APEM.MocmainWindow.ConfirmDialog.OK.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.RPLManagementInternalFrame.CertifyButton.ClickSignle();
            APEM.MocmainWindow.ConfirmDialog.PasswordEditor.SendKeys(PassWord.qaone1);
            APEM.MocmainWindow.ConfirmDialog.Reason.SendKeys("Test");
            APEM.MocmainWindow.ConfirmDialog.OK.Click();
            Thread.Sleep(3000);

        }

    }
}