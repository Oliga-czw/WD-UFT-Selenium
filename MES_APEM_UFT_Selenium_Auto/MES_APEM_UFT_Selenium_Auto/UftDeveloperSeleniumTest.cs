using HP.LFT.SDK;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System.Windows.Forms;
using System;
using HP.LFT.SDK.StdWin;
using System.Diagnostics;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;
using OpenQA.Selenium;
using System.Drawing;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using System.IO;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using System.Collections.Generic;
using MES_APEM_UFT_Selenium_Auto.Product.DataBaseWizard;
using OpenQA.Selenium.Interactions;
using Spire.Pdf;
using System.Text;
using Spire.Pdf.Texts;
using MES_APEM_UFT_Selenium_Auto.Product.APRM;
using MES_APEM_UFT_Selenium_Auto.Product.SQLplus;
using Application = MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary.Application;

namespace MES_APEM_UFT_Selenium_Auto
{
    [TestClass]
    public class UftDeveloperSeleniumTest
    {

        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void TestMethod1()
        {
            SdkConfiguration config = new SdkConfiguration();
            SDK.Init(config);
            Thread.Sleep(3000);
            string BPLName = "BPLRRRRR";
            string RPLName = "RPLTEST ";
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
            APEM.MocmainWindow.BPLDataInternalFrame._UFT_InterFrame.Close();
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName).DoubleClick();
            APEM.MocmainWindow.RPLManagementInternalFrame.RPLTabControl.Select("Documents");
            Base_Assert.IsFalse(APEM.MocmainWindow.RPLManagementInternalFrame.VerifyButton.IsEnabled);
            Base_Assert.IsFalse(APEM.MocmainWindow.RPLManagementInternalFrame.CertifyButton.IsEnabled);

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
