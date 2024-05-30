using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System;
using OpenQA.Selenium;
using HP.LFT.SDK;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Application = MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary.Application;
using System.Linq;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class Mobile_TestCase
    {
        [TestCaseID(962328)]
        [Title("Defect Manager error - ")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_962328()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string OrderName = "Order962328";
            string DeleteName = "DELORDER"; 
            string RPLName = "RPL962328";

            Application.LaunchMocAndLogin();
            LogStep(@"1. import rpl");
            //check rpl exit
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("CASE962328.zip");
            }
            LogStep(@"2. create orders");
            MOC_Fuction.PlanFromRPL(RPLName, OrderName);
            //create delete plan order
            string RPLSelect = RPLName + "#1";
            APEM.MocmainWindow.Orders.ClickSignle();
            Thread.Sleep(2000);
            MOC_Fuction.CheckRowSelection();
            //if exit order DELETE it
            APEM.MocmainWindow.OrderListInternalFrame.Search.SetText(DeleteName);//filter order
            APEM.MocmainWindow.OrderListInternalFrame.Filter_Button.Click();
            var count = APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Rowscount();
            for (int i = 0; i < count; i++)
            {
                APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.SelectRows(i);
                if (APEM.MocmainWindow.OrderListInternalFrame.Delete_Button.IsEnabled)
                {
                    APEM.MocmainWindow.OrderListInternalFrame.Delete_Button.ClickSignle();
                    APEM.MocmainWindow.DeleteOrderDialog.YesButton.Click();
                }
            }
            APEM.MocmainWindow.OrderListInternalFrame.PlanFromRPL_Button.ClickSignle();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderPlanDialog.CodeEditor.SendKeys(DeleteName);
            APEM.MocmainWindow.OrderPlanDialog.DescriptionEditor.SendKeys("test");
            APEM.MocmainWindow.OrderPlanDialog.RPLList.Select(RPLSelect);
            Thread.Sleep(3000);
            APEM.MocmainWindow.OrderPlanDialog.OK.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.AddReasonDialog.Reason.SendKeys("for test");
            APEM.MocmainWindow.AddReasonDialog.OK.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.GetSnapshot(Resultpath + "order exit.PNG");
            LogStep(@"3. login mobile");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            Mobile_Fuction.login();
            LogStep(@"4. Select order to execute");
            Mobile.OrderProcess_Page.OrderSearch.SendKeys(OrderName);
            Thread.Sleep(5000);
            //go to tracking page,execute the phase
            Mobile.OrderProcess_Page.GotoTracking.Click();
            driver.Wait(1000);
            Mobile.OrderTracking_Page.ExecutionButton.Click();
            Thread.Sleep(10000);
            LogStep(@"5. check API function");
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Execute API.PNG");
            Mobile.OrderExecution_Page.DELETE_ORDER_Button.Click();
            Thread.Sleep(5000);
            Mobile.OrderExecution_Page.OKButton.Click();
            Thread.Sleep(5000);
            // check order after click API
            APEM.MocmainWindow.OrderListInternalFrame.Refresh_Button.Click();
            APEM.MocmainWindow.GetSnapshot(Resultpath + "Delete order.PNG");
            Base_Assert.IsTrue(APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Rowscount().Equals(0), "Delete order");
            driver.Close();
            APEM.ExitApplication();
        }

    }
}