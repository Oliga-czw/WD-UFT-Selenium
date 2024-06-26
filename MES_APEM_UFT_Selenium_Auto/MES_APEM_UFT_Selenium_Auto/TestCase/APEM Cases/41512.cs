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
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(41512)]
        [Title("Using CREATE_ORDER_FROM_PROCEDURE_LOGIC() API to create order")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_41512()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string RPLname = "RPL41512";
            string ordername = "ORDER41512";

            Application.LaunchMocAndLogin();
            //check RPL exit
            APEM.MocmainWindow.RPLDesign.Click();
            Thread.Sleep(2000);
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLname).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("TEMP41512.zip");
            }
            APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLname).Click();
            Thread.Sleep(2000);
            if(APEM.MocmainWindow.RPLDesignInternalFrame.VerifyButton.IsEnabled is false)
            {
                APEM.MocmainWindow.Orders.ClickSignle();
                Thread.Sleep(2000);
                MOC_Fuction.CheckRowSelection();
                APEM.MocmainWindow.OrderListInternalFrame.Search.SetText(ordername);//filter order
                APEM.MocmainWindow.OrderListInternalFrame.Filter_Button.Click();
                Thread.Sleep(2000);
                APEM.MocmainWindow.OrderListInternalFrame.Cancel_Button.Click();
                Thread.Sleep(2000);
                APEM.MocmainWindow.CancelOrderDialog.YesButton.Click();
                Thread.Sleep(2000);
                MOC_Fuction.AddReason();
                APEM.MocmainWindow.RPLVerify.Click();
                APEM.MocmainWindow.RPLVerificationInternalFrame.EditRPL_Button.Click();
                Thread.Sleep(2000);
                APEM.SwitchToEditingDialog.YesButton.Click();
                Thread.Sleep(2000);
                APEM.DeleteOrderDialog.YesButton.Click();
                Thread.Sleep(2000);
                MOC_Fuction.AddReason();
                APEM.MocmainWindow.RPLVerificationInternalFrame._UFT_InterFrame.Close();
                APEM.MocmainWindow.RPLDesign.Click();
                Thread.Sleep(2000);
                APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLname).Click();
                Thread.Sleep(2000);
            }
            APEM.MocmainWindow.RPLDesignInternalFrame.VerifyButton.Click();
            Thread.Sleep(3000);
            APEM.VerifyDialog.YesButton.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.OrderPlanDialog.CodeEditor.SendKeys(ordername);
            APEM.MocmainWindow.OrderPlanDialog.DescriptionEditor.SendKeys("test");
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderPlanDialog.Auto_ActivateCheckBox.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.OrderPlanDialog.OK.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.AddReasonDialog.Reason.SendKeys("for test");
            APEM.MocmainWindow.AddReasonDialog.OK.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.RPLDesignInternalFrame.SearchEditor.SetText(RPLname);
            APEM.MocmainWindow.RPLDesignInternalFrame.Filterbutton.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.GetSnapshot(Resultpath + "createOrderFromRPL.PNG");
            Base_Assert.AreEqual(APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.GetCell(0, "Logic Status").Value, "Verifying");
            APEM.MocmainWindow.RPLDesignInternalFrame.SearchEditor.SetText("");
            APEM.MocmainWindow.RPLDesignInternalFrame.Filterbutton.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.WorkstationBP.ClickSignle();
            MOC_Fuction.CheckRowSelection();
            Thread.Sleep(3000);
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText(ordername);
            APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
            //Execute order to create new order
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(10000);
            APEM.PhaseExecWindow.ExecutionInternalFrame.CreateOrder_Button.Click();
            Thread.Sleep(2000);
            APEM.PhaseExecWindow.ExecutionInternalFrame.Cancel_Button.ClickSignle();
            Thread.Sleep(2000);
            APEM.PhaseExecWindow.ConfirmationInternalFrame.YesButton.Click();
            Thread.Sleep(6000);
            APEM.MocmainWindow.Orders.ClickSignle();
            Thread.Sleep(2000);
            MOC_Fuction.CheckRowSelection();
            APEM.MocmainWindow.OrderListInternalFrame.Search.SetText("APIORDER");//filter order
            APEM.MocmainWindow.OrderListInternalFrame.Filter_Button.Click();
            APEM.MocmainWindow.GetSnapshot(Resultpath + "createOrderOK.PNG");
            Base_Assert.IsTrue(APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Row("APIORDER").Existing);
            int count1 = APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Rowscount();
            LogStep(@"execute on Mobile");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            Mobile_Fuction.login();
            Mobile.OrderProcess_Page.OrderSearch.SendKeys(ordername);
            Thread.Sleep(5000);
            //go to tracking page,execute the phase
            Mobile.OrderProcess_Page.GotoTracking.Click();
            Thread.Sleep(2000);
            Mobile.OrderTracking_Page.ExecutionButton.Click();
            Thread.Sleep(10000);
            Mobile.OrderExecution_Page.Createorder.Click();
            Thread.Sleep(2000);
            var version = Mobile.OrderExecution_Page.Createorder_Version.Text();
            Mobile.OrderExecution_Page.CancelButton.Click();
            Thread.Sleep(2000);
            Mobile.OrderExecution_Page.ConfirmYesButton.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.OrderListInternalFrame.Filter_Button.Click();
            APEM.MocmainWindow.GetSnapshot(Resultpath + "createOrderOK_from_Mobile.PNG");
            Base_Assert.IsTrue(APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Row("2","Rep.").Existing);
            int count2 = APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Rowscount();
            Base_Assert.IsTrue(count1 + 1 == count2,"create an order");
        }



    }


}