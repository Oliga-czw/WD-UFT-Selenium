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
        [TestCaseID(554188)]
        [Title("V10.0.3_Show User state in workstation module")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1500000)]

        [TestMethod]
        public void VSTS_554188()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string BPLname = "BPL554188";
            string RPLname = "FOR_STATUS";
            string Ordername = "ORDER554188";
            Library.BaseLibrary.Application.LaunchMocAndLogin();
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLname).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("TEMP554188.zip");
            }
            MOC_Fuction.PlanFromRPL(RPLname, Ordername);
            APEM.MocmainWindow.BPLDesign.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLname, "Name").Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLListInternalFrame.LoadDesigner_Button.Click();
            Thread.Sleep(1000);
            if (APEM.MocmainWindow.ReadOnly_Dialog.IsExist())
            {
                APEM.MocmainWindow.ReadOnly_Dialog.OKButton.Click();
            }
            Thread.Sleep(1000);
            APEM.DesignEditorWindow.ExecuteButton.ClickSignle();
            Thread.Sleep(5000);
            APEM.DesignEditorWindow.ExecuteMainInternalFrame.User_state.Click();
            Thread.Sleep(1000);
            APEM.DesignEditorWindow.ExecuteMainInternalFrame._UFT_InterFrame.Close();
            APEM.MocmainWindow.ExeCancelDialog.YesButton.Click();
            Thread.Sleep(2000);
            MOC_Fuction.DesignEditorClose();
            Thread.Sleep(2000);
            APEM.MocmainWindow.WorkstationBP.ClickSignle();
            MOC_Fuction.CheckRowSelection();
            Thread.Sleep(2000);
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText(Ordername);
            APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.GetSnapshot(Resultpath + "UserStatus.PNG");
            Console.WriteLine(APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.GetCell(0, "User Status").Value);
            Base_Assert.AreEqual(APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.GetCell(0, "User Status").Value, "qaone1");
            



        }

    }
}