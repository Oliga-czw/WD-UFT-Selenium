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
        [TestCaseID(43623)]
        [Title("Effective Date is mandatory for recipe elements if recipe expiration feature is enabled")]
        [TestCategory(ProductArea.RecipeManagement)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_43623()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string BPLname = "BPL43623";
            string RPLname = "RPL43623";
            string MRname = "MR43623";
            string Configpath = Base_Directory.ConfigDir + "flags.m2r_cfg";
            string ConfigKey = "EFFECTIVE_RECIPE_VERSION = 0";
            try
            {
                LogStep(@"1. Set key in config file");
                Base_Function.EditConfigKey(Configpath, ConfigKey);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
                Library.BaseLibrary.Application.LaunchMocAndLogin();
                APEM.MocmainWindow.BPLDesign.ClickSignle();
                if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLname).Existing)
                {
                    MOC_TemplatesFunction.Importtemplates("TEMP43623.zip");
                }
                //BPLverify
                APEM.MocmainWindow.BPLDesign.ClickSignle();
                APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLname).Click();
                Thread.Sleep(2000);
                APEM.MocmainWindow.BPLListInternalFrame.VerifyButton.ClickSignle();
                Thread.Sleep(3000);
                APEM.MocmainWindow.ConfirmDialog.PasswordEditor.SendKeys(PassWord.qaone1);
                APEM.MocmainWindow.ConfirmDialog.Reason.SendKeys("Test");
                APEM.MocmainWindow.ConfirmDialog.OK.Click();
                DateTime BPLverify = DateTime.Now;
                // Console.WriteLine(BPLverify);
                Thread.Sleep(3000);

                //BPLcertify
                APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLname).Click();
                Thread.Sleep(2000);
                APEM.MocmainWindow.BPLListInternalFrame.CertifyButton.Click();
                Thread.Sleep(3000);
                APEM.BPLCertifyDialog.YesButton.Click();
                Thread.Sleep(3000);
                APEM.MocmainWindow.ConfirmDialog.PasswordEditor.SendKeys(PassWord.qaone1);
                APEM.MocmainWindow.ConfirmDialog.Reason.SendKeys("Test");
                APEM.MocmainWindow.ConfirmDialog.OK.Click();
                DateTime BPLcertify = DateTime.Now;
                //Console.WriteLine(BPLcertify);
                Thread.Sleep(3000);
                APEM.MocmainWindow.BPLDesign.ClickSignle();
                APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLname).DoubleClick();
                Thread.Sleep(2000);
                APEM.MocmainWindow.GetSnapshot(Resultpath + "BPLDate.PNG");
                DateTime BPLverifydate = DateTime.Parse(APEM.MocmainWindow.BPLDataInternalFrame.verifyDate.Text);
                DateTime BPLcertifydate = DateTime.Parse(APEM.MocmainWindow.BPLDataInternalFrame.certifyDate.Text);
                TimeSpan timeDifference1 = BPLverifydate.Subtract(BPLverify);
                Base_Assert.IsTrue(timeDifference1.TotalSeconds <= 2);
                TimeSpan timeDifference2 = BPLcertifydate.Subtract(BPLcertify);
                Base_Assert.IsTrue(timeDifference2.TotalSeconds <= 2);

                //RPLverify
                APEM.MocmainWindow.RPLDesign.ClickSignle();
                Thread.Sleep(5000);
                APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLname).Click();
                Thread.Sleep(2000);
                APEM.MocmainWindow.RPLDesignInternalFrame.VerifyButton.ClickSignle();
                Thread.Sleep(3000);
                APEM.VerifyDialog.NoButton.Click();
                Thread.Sleep(3000);
                APEM.MocmainWindow.ConfirmDialog.PasswordEditor.SendKeys(PassWord.qaone1);
                APEM.MocmainWindow.ConfirmDialog.Reason.SendKeys("Test");
                APEM.MocmainWindow.ConfirmDialog.OK.Click();
                DateTime RPLverify = DateTime.Now;
                //Console.WriteLine(RPLverify);
                Thread.Sleep(3000);
                //RPLcertify
                APEM.MocmainWindow.RPLDesign.ClickSignle();
                Thread.Sleep(5000);
                APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLname).Click();
                Thread.Sleep(2000);
                APEM.MocmainWindow.RPLDesignInternalFrame.CertifyButton.ClickSignle();
                Thread.Sleep(3000);
                APEM.CertifyDialog.YesButton.Click();
                Thread.Sleep(3000);
                APEM.MocmainWindow.ConfirmDialog.PasswordEditor.SendKeys(PassWord.qaone1);
                APEM.MocmainWindow.ConfirmDialog.Reason.SendKeys("Test");
                APEM.MocmainWindow.ConfirmDialog.OK.Click();
                DateTime RPLcertify = DateTime.Now;
                //Console.WriteLine(RPLcertify);
                Thread.Sleep(3000);
                APEM.MocmainWindow.RPLDesign.ClickSignle();
                Thread.Sleep(5000);
                APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLname).DoubleClick();
                Thread.Sleep(2000);
                APEM.MocmainWindow.GetSnapshot(Resultpath + "RPLDate.PNG");
                DateTime RPLverifydate = DateTime.Parse(APEM.MocmainWindow.RPLManagementInternalFrame.verifyDate.Text);
                DateTime RPLcertifydate = DateTime.Parse(APEM.MocmainWindow.RPLManagementInternalFrame.certifyDate.Text);
                TimeSpan timeDifference3 = RPLcertifydate.Subtract(RPLcertify);
                Base_Assert.IsTrue(timeDifference3.TotalSeconds <= 2);
                TimeSpan timeDifference4 = RPLcertifydate.Subtract(RPLcertify);
                Base_Assert.IsTrue(timeDifference4.TotalSeconds <= 2);

                //MRverify
                APEM.MocmainWindow.MasterRecipes.Click();
                Thread.Sleep(5000);
                APEM.MocmainWindow.MasterRecipeInterFrame.MRListTable.Row(MRname).Click();
                Thread.Sleep(2000);
                APEM.MocmainWindow.MasterRecipeInterFrame.VerifyButton.Click();
                APEM.CheckParametersDialog.OKButton.Click();
                APEM.VerifyDialog.NoButton.Click();
                APEM.MocmainWindow.ConfirmDialog.PasswordEditor.SendKeys(PassWord.qaone1);
                APEM.MocmainWindow.ConfirmDialog.Reason.SendKeys("Test");
                APEM.MocmainWindow.ConfirmDialog.OK.Click();
                DateTime MRverify = DateTime.Now;
                //Console.WriteLine(MRverify);
                Thread.Sleep(3000);
                //MRcertify
                APEM.MocmainWindow.MasterRecipes.Click();
                Thread.Sleep(5000);
                APEM.MocmainWindow.MasterRecipeInterFrame.MRListTable.Row(MRname).Click();
                Thread.Sleep(2000);
                APEM.MocmainWindow.MasterRecipeInterFrame.CertifyButton.Click();
                APEM.CheckParametersDialog.OKButton.Click();
                APEM.CertifyDialog.YesButton.Click();
                APEM.MocmainWindow.ConfirmDialog.PasswordEditor.SendKeys(PassWord.qaone1);
                APEM.MocmainWindow.ConfirmDialog.Reason.SendKeys("Test");
                APEM.MocmainWindow.ConfirmDialog.OK.Click();
                DateTime MRcertify = DateTime.Now;
                //Console.WriteLine(MRcertify);
                Thread.Sleep(3000);
                APEM.MocmainWindow.MasterRecipes.Click();
                Thread.Sleep(5000);
                APEM.MocmainWindow.MasterRecipeInterFrame.MRListTable.Row(MRname).DoubleClick();
                Thread.Sleep(2000);
                APEM.MocmainWindow.GetSnapshot(Resultpath + "MRDate.PNG");
                DateTime MRverifydate = DateTime.Parse(APEM.MocmainWindow.MasterRecipeDataInterFrame.verifyDate.Text);
                DateTime MRcertifydate = DateTime.Parse(APEM.MocmainWindow.MasterRecipeDataInterFrame.certifyDate.Text);
                TimeSpan timeDifference5 = MRcertifydate.Subtract(MRcertify);
                Base_Assert.IsTrue(timeDifference5.TotalSeconds <= 2);
                TimeSpan timeDifference6 = MRcertifydate.Subtract(MRcertify);
                Base_Assert.IsTrue(timeDifference6.TotalSeconds <= 2);
            }
            finally
            {
                LogStep(@"6.Restone config key ");
                Base_Function.EditConfigKey(Configpath, ConfigKey);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
            }

        }

    }
}