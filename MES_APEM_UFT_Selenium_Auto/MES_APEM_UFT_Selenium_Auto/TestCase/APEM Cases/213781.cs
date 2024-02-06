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
        [TestCaseID(213781)]
        [Title("CQ00274848: Certified Master Recipe don't allow parameter values to be changed")]
        [TestCategory(ProductArea.MOC)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_213781()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string RPLName = "2BPLS";
            string RPLNameCopy = "RPL213781";
            string MRName = "MR213781";
            string selectRPL = RPLNameCopy + "#1";
            Application.LaunchMocAndLogin();
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            Thread.Sleep(2000);
            //check RPL exit
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLNameCopy).Existing)
            {
                APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName).Click();
                //copy and paste
                APEM.MocmainWindow.RPLDesignInternalFrame.Copy_Button.ClickSignle();
                APEM.MocmainWindow.RPLDesignInternalFrame.Paste_Button.ClickSignle();
                APEM.MocmainWindow.RPLManagementInternalFrame.RPLName.SetText(RPLNameCopy);
                APEM.MocmainWindow.RPLManagementInternalFrame.ConfirmChanges_Button.ClickSignle();
                MOC_Fuction.AddReason();
                //verify and certify RPL
                MOC_Fuction.VerifyRPL(RPLNameCopy);
                MOC_Fuction.CertifyRPL(RPLNameCopy);
            }
            //create MR with the RPL
            APEM.MocmainWindow.MasterRecipes.ClickSignle();
            Thread.Sleep(2000);
            if (!APEM.MocmainWindow.MasterRecipeInterFrame.MRListTable.Row(MRName).Existing)
            {
                APEM.MocmainWindow.MasterRecipeInterFrame.Add_Button.ClickSignle();
                APEM.MocmainWindow.MasterRecipeDataInterFrame.Name.SetText(MRName);
                APEM.MocmainWindow.MasterRecipeDataInterFrame.Description.SetText("test parameter");
                APEM.MocmainWindow.MasterRecipeDataInterFrame.RPLList.SelectItems(selectRPL);
                APEM.MocmainWindow.MasterRecipeDataInterFrame.ConfirmChanges_Button.ClickSignle();
                MOC_Fuction.AddReason();
                MOC_Fuction.VerifyMR();
                MOC_Fuction.CertifyMR();
            }
            else
            {
                APEM.MocmainWindow.MasterRecipeInterFrame.MRListTable.Row(MRName).DoubleClick();
            }
            APEM.MocmainWindow.MasterRecipeDataInterFrame.MRTabControl.Select("Parameters");
            Thread.Sleep(2000);
            APEM.MocmainWindow.MasterRecipeDataInterFrame.ParametersTable.Row("{1,2,34,234}", "Value").DoubleClick();
            Thread.Sleep(2000);
            APEM.MocmainWindow.GetSnapshot(Resultpath + "Parameters.PNG");
            Base_Assert.IsTrue(APEM.MocmainWindow.MasterRecipeDataInterFrame.message.Text.Contains("Cannot edit this row."));





        }

    }
}