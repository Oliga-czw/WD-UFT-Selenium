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
        [TestCaseID(29671)]
        [Title("IN_ARRAY_ACCEPTS_NULL configure key works well")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1500000)]

        [TestMethod]
        public void VSTS_29671()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string BPLname = "BPL29671";
            string RPLname = "RPL29671";
            string Ordername1 = "ORDER29671_0";
            string Ordername2 = "ORDER29671_1";
            string Configpath = Base_Directory.ConfigDir + "flags.m2r_cfg";
            string ConfigKey = "IN_ARRAY_ACCEPTS_NULL = 0";
            string ConfigKey1 = "IN_ARRAY_ACCEPTS_NULL = 1";
            Base_Function.AddConfigKey(Configpath, ConfigKey);
            //codify all
            Base_Test.LaunchApp(Base_Directory.Codify_all);
            //restart tomcat
            Base_Test.KillProcess("tomcat10");
            Thread.Sleep(30000);
            Base_Function.ResartServices(ServiceName.Tomcat);
            Thread.Sleep(240000);
            try
            {
                Application.LaunchMocAndLogin();
                APEM.MocmainWindow.BPLDesign.ClickSignle();
                if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLname).Existing)
                {
                    MOC_TemplatesFunction.Importtemplates("TEMP29671.zip");
                }
                MOC_Fuction.PlanFromRPL(RPLname, Ordername1);
                APEM.ExitApplication();
                Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
                Mobile_Fuction.gotoApemMobile(driver);
                Mobile_Fuction.login();
                Mobile.OrderProcess_Page.OrderSearch.SendKeys(Ordername1);
                Thread.Sleep(2000);
                //go to tracking page,execute the phase
                Mobile.OrderProcess_Page.GotoTracking.Click();
                Thread.Sleep(2000);
                Mobile.OrderTracking_Page.ExecutionButton.Click();
                Thread.Sleep(10000);
                Mobile.OrderExecution_Page.TestInArray.Click();
                Thread.Sleep(5000);
                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Mobile.PNG");
                Base_Assert.AreEqual(Mobile.OrderExecution_Page.Execution_Message.Text(), "failed");
                Mobile.OrderExecution_Page.MessageOK_button.Click();
                Mobile.OrderExecution_Page.CancelButton.Click();
                Thread.Sleep(2000);
                Mobile.OrderExecution_Page.ConfirmYesButton.Click();
                Thread.Sleep(3000);
                driver.Close();
                //modify the configure key
                Base_Function.EditConfigKey(Configpath, ConfigKey1);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
                Application.LaunchMocAndLogin();
                Thread.Sleep(2000);
                MOC_Fuction.PlanFromRPL(RPLname, Ordername2);
                APEM.MocmainWindow.WorkstationBP.ClickSignle();
                MOC_Fuction.CheckRowSelection();
                Thread.Sleep(3000);
                //LogStep(@"Execute the order");
                APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText(Ordername2);
                APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
                //Excution
                APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution").Click();
                APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
                Thread.Sleep(3000);
                APEM.PhaseExecWindow.ExecutionInternalFrame.Order_state_Button.ClickSignle();
                Thread.Sleep(2000);
                APEM.PhaseExecWindow.GetSnapshot(Resultpath + "MOC.PNG");
                Base_Assert.AreEqual(APEM.PhaseExecWindow.MessageInternalFrame.Label.Text, "success");
                APEM.PhaseExecWindow.MessageInternalFrame.OKButton.Click();
                APEM.PhaseExecWindow.ExecutionInternalFrame.Cancel_Button.ClickSignle();
                Thread.Sleep(1000);
                APEM.PhaseExecWindow.ConfirmationInternalFrame.YesButton.Click();
                Thread.Sleep(2000);
                
                

            }
            finally
            {
                LogStep(@"6.Restone config key ");
                Base_Function.DeleteConfigKey(Configpath, ConfigKey1);
                Base_Function.DeleteConfigKey(Configpath, ConfigKey);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
            }


        }

    }
}