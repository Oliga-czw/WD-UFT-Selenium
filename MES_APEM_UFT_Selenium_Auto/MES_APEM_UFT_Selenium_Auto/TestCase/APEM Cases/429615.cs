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
        [TestCaseID(429615)]
        [Title("Inspired by custom _ Can copy/paste RPLs")]
        [TestCategory(ProductArea.MOC)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_429615()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string RPLName = "2BPLS";
            string RPLNameCopy = "RPL429615";
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
            else
            {
                APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLNameCopy).DoubleClick();
            }
            APEM.MocmainWindow.RPLDesignInternalFrame._UFT_InterFrame.Close();
            Thread.Sleep(2000);
            APEM.MocmainWindow.RPLManagementInternalFrame.RPLTabControl.Select("Phases");
            int rowsCount = APEM.MocmainWindow.RPLManagementInternalFrame.RPLPhasesListTable.Rowscount();
            APEM.MocmainWindow.GetSnapshot(Resultpath + "RPLcopyPhases.PNG");
            Console.WriteLine(rowsCount);
            Base_Assert.IsTrue(rowsCount == 10,"PasteSuccessfully");





        }

    }
}