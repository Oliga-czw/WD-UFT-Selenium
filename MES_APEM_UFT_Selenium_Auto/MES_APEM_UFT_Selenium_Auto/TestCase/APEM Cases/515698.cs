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
        [TestCaseID(515698)]
        [Title("Inspired from defect 404641 -- script execution")]
        [TestCategory(ProductArea.RecipeManagement)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1500000)]

        [TestMethod]
        public void VSTS_515698()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string ordername = "ORDER515698_2";
            Library.BaseLibrary.Application.LaunchMocAndLogin();
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row("RPL515698").Existing)
            {
                MOC_TemplatesFunction.Importtemplates("TEMP515698.zip");
            }
            MOC_Fuction.PlanFromRPL("RPL515698", ordername);
            MOC_Fuction.CheckRowSelection();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTracking.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingInternalFrame.CodeEditor.SetText(ordername);
            APEM.MocmainWindow.OrderTrackingInternalFrame.Filterbutton.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderTrackingInternalFrame.RefreshButton.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.GetSnapshot(Resultpath + "Execute_finished.PNG");
            Base_Assert.IsTrue(APEM.MocmainWindow.OrderTrackingInternalFrame.OrderTable.GetCell(0,"Status").Value.Equals("Finished"));



        }

    }
}