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
    public partial class APEM_TestCase
    {
        [TestCaseID(1002790)]
        [Title("Inspired from 966251_Value of a check box is showed as no when it is disabled")]
        [TestCategory(ProductArea.MOC)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1200000)]

        [TestMethod]
        public void VSTS_1002790()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;

            string RPLname = "RPL1002790";
            string Ordername = "ORDER1002790";

            LogStep(@"1.import rpl");
            Application.LaunchMocAndLogin();
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLname).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("CASE1002790.zip");
            }

            MOC_Fuction.PlanFromRPL(RPLname, Ordername);
            LogStep(@"2.Execute in moc");
            APEM.MocmainWindow.WorkstationBP.ClickSignle();
            MOC_Fuction.CheckRowSelection();
            Thread.Sleep(3000);
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText(Ordername);
            APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(15000);
            var value_initial = APEM.PhaseExecWindow.ExecutionInternalFrame.check_value.AttachedText;//Yes
            //deshabilitar_Button
            APEM.PhaseExecWindow.ExecutionInternalFrame.deshabilitar_Button.Click();
            Thread.Sleep(2000);
            APEM.PhaseExecWindow.GetSnapshot(Resultpath + "Click deshabilitar.PNG");
            var value_deshabilitar = APEM.PhaseExecWindow.ExecutionInternalFrame.check_value.AttachedText;//Yes
            Base_Assert.AreEqual(value_deshabilitar, value_initial, "Click deshabilitar");
            Base_Assert.IsFalse(APEM.PhaseExecWindow.ExecutionInternalFrame.check_box1._UFT_CheckBox.IsEnabled);//False
            Base_Assert.AreEqual("Checked", APEM.PhaseExecWindow.ExecutionInternalFrame.check_box1._UFT_CheckBox.State.ToString(), "checkbox State");
            //uncheck checkbox
            APEM.PhaseExecWindow.ExecutionInternalFrame.check_box2._UFT_CheckBox.Click();
            var value_deshabilitar_uncheck = APEM.PhaseExecWindow.ExecutionInternalFrame.check_value.AttachedText;//No
            APEM.PhaseExecWindow.GetSnapshot(Resultpath + "deshabilitar uncheck checkbox.PNG");
            Base_Assert.AreEqual("No", value_deshabilitar_uncheck, "deshabilitar uncheck checkbox");
            //habilitar_Button
            APEM.PhaseExecWindow.ExecutionInternalFrame.habilitar_Button.Click();
            Thread.Sleep(2000);
            APEM.PhaseExecWindow.GetSnapshot(Resultpath + "Click habilitar.PNG");
            var value_habilitar = APEM.PhaseExecWindow.ExecutionInternalFrame.check_value.AttachedText;//No
            Base_Assert.AreEqual(value_habilitar, value_deshabilitar_uncheck, "Click habilitar");
            Base_Assert.IsTrue(APEM.PhaseExecWindow.ExecutionInternalFrame.check_box1._UFT_CheckBox.IsEnabled);//True
            Base_Assert.AreEqual("Unchecked", APEM.PhaseExecWindow.ExecutionInternalFrame.check_box1._UFT_CheckBox.State.ToString(), "checkbox State");
            //check checkbox
            APEM.PhaseExecWindow.ExecutionInternalFrame.check_box1._UFT_CheckBox.Click();
            var value_habilitar_check = APEM.PhaseExecWindow.ExecutionInternalFrame.check_value.AttachedText;//Yes
            APEM.PhaseExecWindow.GetSnapshot(Resultpath + "habilitar check checkbox.PNG");
            Base_Assert.AreEqual(value_initial, value_habilitar_check, "habilitar check checkbox");
            //cancel phase
            APEM.PhaseExecWindow.ExecutionInternalFrame.Cancel_Button.ClickSignle();
            Thread.Sleep(1000);
            APEM.PhaseExecWindow.ConfirmationInternalFrame.YesButton.Click();
            Thread.Sleep(2000);
            APEM.ExitApplication();
            LogStep(@"3.Execute in mobile");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            driver.Wait();
            Mobile_Fuction.login();
            driver.Wait();
            Mobile.OrderProcess_Page.OrderSearch.SendKeys(Ordername);
            Thread.Sleep(1000);
            Mobile.OrderProcess_Page.GotoTracking.Click();
            Thread.Sleep(1000);
            Mobile.OrderTracking_Page.ExecutionButton.Click();
            Thread.Sleep(10000);

            var mobile_initial = Mobile.OrderExecution_Page.check_value.Text();//Yes
            //deshabilitar_Button
            Mobile.OrderExecution_Page.Deshabilitar_Button.Click();
            Thread.Sleep(2000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Mobile Click deshabilitar.PNG");
            var mobile_deshabilitar = Mobile.OrderExecution_Page.check_value.Text();//Yes
            Base_Assert.AreEqual(mobile_deshabilitar, mobile_initial, "Mobile Click deshabilitar");
            Base_Assert.IsFalse(Mobile.OrderExecution_Page.check_box1.isEnable());//False
            Base_Assert.AreEqual("true", Mobile.OrderExecution_Page.check_box1.GetAttribute("aria-checked"), "checkbox State");//checked
            //uncheck checkbox
            Mobile.OrderExecution_Page.check_box_label2.Click();
            Thread.Sleep(2000);
            var mobile_deshabilitar_uncheck = Mobile.OrderExecution_Page.check_value.Text();//No
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "mobile deshabilitar uncheck checkbox.PNG");
            Base_Assert.AreEqual("No", mobile_deshabilitar_uncheck, "mobile deshabilitar uncheck checkbox");
            //habilitar_Button
            Mobile.OrderExecution_Page.Habilitar_Button.Click();
            Thread.Sleep(2000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Mobile Click habilitar.PNG");
            var mobile_habilitar = Mobile.OrderExecution_Page.check_value.Text();//No
            Base_Assert.AreEqual(mobile_habilitar, mobile_deshabilitar_uncheck, "mobile Click habilitar");
            Base_Assert.IsTrue(Mobile.OrderExecution_Page.check_box1.isEnable());//True
            Base_Assert.AreEqual("false", Mobile.OrderExecution_Page.check_box1.GetAttribute("aria-checked"), "checkbox State");
            //check checkbox
            Mobile.OrderExecution_Page.check_box_label1.Click();
            Thread.Sleep(2000);
            var mobile_habilitar_check = Mobile.OrderExecution_Page.check_value.Text();//Yes
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Mobile habilitar check checkbox.PNG");
            Base_Assert.AreEqual(mobile_initial, mobile_habilitar_check, "habilitar check checkbox");
            //cancel phase
            Mobile.OrderExecution_Page.CancelButton.Click();
            Thread.Sleep(2000);
            Mobile.OrderExecution_Page.ConfirmYesButton.Click();
            Thread.Sleep(4000);
            driver.Close();



         


        }

    }
}
