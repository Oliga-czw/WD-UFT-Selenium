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
        [TestCaseID(553157)]
        [Title("Inspired from customer defect 544853 - SET_OPER_STATE functione")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1500000)]

        [TestMethod]
        public void VSTS_553157()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string RPLname = "RPL553157";
            string Ordername = "ORDER553157";
            string Ordername1 = "TEST002";
            Library.BaseLibrary.Application.LaunchMocAndLogin();
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLname).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("TEMP553157.zip");
            }
            APEM.MocmainWindow.Orders.ClickSignle();
            Thread.Sleep(2000);
            MOC_Fuction.CheckRowSelection();
            APEM.MocmainWindow.OrderListInternalFrame.Search.SetText("");//filter order
            APEM.MocmainWindow.OrderListInternalFrame.Filter_Button.Click();
            Thread.Sleep(3000);
            MOC_Fuction.PlanFromRPL("FOR_STATUS", Ordername1);
            Thread.Sleep(2000);
            MOC_Fuction.PlanFromRPL(RPLname, Ordername);
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            Mobile_Fuction.login();
            Mobile.OrderProcess_Page.OrderSearch.SendKeys(Ordername);
            Thread.Sleep(2000);
            //go to tracking page,execute the phase
            Mobile.OrderProcess_Page.GotoTracking.Click();
            driver.Wait(1000);
            Mobile.OrderTracking_Page.ExecutionButton.Click();
            Thread.Sleep(2000);
            var SET_OPER_STATE = Mobile.OrderExecution_Page.SQL_SELECT_ONEButton;
            SET_OPER_STATE.Click();
            Thread.Sleep(2000);
            Mobile.OrderExecution_Page.MessageOK_button.Click();
            Mobile.OrderExecution_Page.CancelButton.Click();
            Thread.Sleep(2000);
            Mobile.OrderExecution_Page.ConfirmYesButton.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.OrderListInternalFrame.Search.SetText(Ordername1);//filter order
            APEM.MocmainWindow.OrderListInternalFrame.Filter_Button.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Row("Initiated","Status").Click();
            APEM.MocmainWindow.OrderListInternalFrame.OrderTabControl.Select("Phases");
            APEM.MocmainWindow.GetSnapshot(Resultpath + "SET_OPER_STATE");
            Thread.Sleep(2000);
            Base_Assert.AreEqual(APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.GetCell(0, "Status").Value, "Finished");
            
            driver.Close();



        }

    }
}