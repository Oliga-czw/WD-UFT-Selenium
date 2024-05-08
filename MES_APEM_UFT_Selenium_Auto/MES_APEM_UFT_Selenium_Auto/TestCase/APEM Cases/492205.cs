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
        [TestCaseID(492205)]
        [Title("Can copy/paste RPL documents")]
        [TestCategory(ProductArea.MOC)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_492205()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string RPLName = "RPL492205";
            string DocumentName = "TESTDOCUMENT";
            string DocumentName1 = DocumentName + "_copy";
            Application.LaunchMocAndLogin();
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("TEMP492205.zip");
            }
            APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName).DoubleClick();
            APEM.MocmainWindow.RPLManagementInternalFrame.RPLTabControl.Select("Documents");
            APEM.MocmainWindow.RPLManagementInternalFrame.ListTable.Row(DocumentName, "Name").Click();
            APEM.MocmainWindow.RPLManagementInternalFrame.Copy_Button.Click();
            APEM.MocmainWindow.RPLManagementInternalFrame.Paste_Button.Click();
            APEM.MocmainWindow.DocumentFrame.DocName.SetText(DocumentName1);
            Thread.Sleep(2000);
            APEM.MocmainWindow.DocumentFrame.ConfirmChanges_Button.Click();
            Thread.Sleep(2000);
            MOC_Fuction.AddReason();
            APEM.MocmainWindow.DocumentFrame._UFT_InterFrame.Close();
            APEM.MocmainWindow.GetSnapshot(Resultpath + "copy_pasteDoc.PNG");
            Base_Assert.IsTrue(APEM.MocmainWindow.RPLManagementInternalFrame.ListTable.Row(DocumentName, "Name").Existing);
        }

    }
}