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
using Application = MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary.Application;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(477376)]
        [Title("Inspired by Customer Defect 459555 -- the second MR should be certified successfully.")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1500000)]

        [TestMethod]
        public void VSTS_477376()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string Configpath = Base_Directory.ConfigDir + "flags.m2r_cfg";
            string ConfigKey = "EFFECTIVE_RECIPE_VERSION=1";
            string ConfigKey1 = "EFFECTIVE_RECIPE_VERSION=0";
            string RPLName = "FOR_STATUS";
            string MRname = "MR477376";
            string RPLSelect = RPLName + "#1";
            try
            { 
                Base_Function.EditConfigKey(Configpath, ConfigKey);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
                Application.LaunchMocAndLogin();
                APEM.MocmainWindow.MasterRecipes.ClickSignle();
                Thread.Sleep(2000);
                APEM.MocmainWindow.MasterRecipeInterFrame.Add_Button.ClickSignle();
                APEM.MocmainWindow.MasterRecipeDataInterFrame.Name.SetText(MRname);
                APEM.MocmainWindow.MasterRecipeDataInterFrame.Description.SetText("for test");
                APEM.MocmainWindow.MasterRecipeDataInterFrame.RPLList.SelectItems(RPLSelect);
                APEM.MocmainWindow.MasterRecipeDataInterFrame.ConfirmChanges_Button.ClickSignle();
                MOC_Fuction.AddReason();
                MOC_Fuction.VerifyMR();
                MOC_Fuction.CertifyMR();
                APEM.MocmainWindow.GetSnapshot(Resultpath + "create_verify_certifyOK.PNG");
                Base_Assert.IsFalse(APEM.MocmainWindow.MasterRecipeDataInterFrame.VerifyButton.IsEnabled);
                Base_Assert.IsFalse(APEM.MocmainWindow.MasterRecipeDataInterFrame.CertifyButton.IsEnabled);
                APEM.MocmainWindow.MasterRecipeDataInterFrame._UFT_InterFrame.Close();
                APEM.MocmainWindow.MasterRecipes.ClickSignle();
                Thread.Sleep(2000);
                APEM.MocmainWindow.MasterRecipeInterFrame.MRListTable.Row(MRname, "Name").Click();
                APEM.MocmainWindow.MasterRecipeInterFrame.Copy_Button.Click();
                APEM.MocmainWindow.MasterRecipeInterFrame.Paste_Button.Click();
                Thread.Sleep(2000);
                APEM.MocmainWindow.MasterRecipeDataInterFrame.Name.SetText(MRname + "_copy");
                APEM.MocmainWindow.MasterRecipeDataInterFrame.ConfirmChanges_Button.ClickSignle();
                MOC_Fuction.AddReason();
                MOC_Fuction.VerifyMR();
                MOC_Fuction.CertifyMR();
                APEM.MocmainWindow.GetSnapshot(Resultpath + "copy_verify_certifyOK.PNG");
                Base_Assert.IsFalse(APEM.MocmainWindow.MasterRecipeDataInterFrame.VerifyButton.IsEnabled);
                Base_Assert.IsFalse(APEM.MocmainWindow.MasterRecipeDataInterFrame.CertifyButton.IsEnabled);

            }
            finally
            {
                LogStep(@"6.Restone config key ");
                Base_Function.EditConfigKey(Configpath, ConfigKey1);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
            }


        }

    }
}