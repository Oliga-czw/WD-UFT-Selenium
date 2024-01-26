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
        [TestCaseID(916442)]
        [Title("UC822684_Soap1.2: SOAP_CALL2() API function should work as expected")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_916442()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string ordername = "ORDER916442";
            string bpl = "BPL916420";//same data with 916420
            string ExpectText = "中央电视台,央视高清电视,中国教育电视台";

            string Configpath = Base_Directory.ConfigDir + "flags.m2r_cfg";
            string ConfigKey1 = @"WS_URL = http://ws.webxml.com.cn/webservices/ChinaTVprogramWebService.asmx?WSDL";
            string ConfigKey2 = @"WS_URI = http://WebXml.com.cn/";

            try
            {
                //set config in flag
                Base_Function.AddConfigKey(Configpath, ConfigKey1);
                Base_Function.AddConfigKey(Configpath, ConfigKey2);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
                //restart tomcat
                Base_Function.ResartServices(ServiceName.Tomcat);
                //wait for tomcat start completely.
                Thread.Sleep(300000);
                LogStep(@"1. import BPL");
                Application.LaunchMocAndLogin();
                APEM.MocmainWindow.BPLDesign.ClickSignle();
                if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(bpl).Existing)
                {
                    MOC_TemplatesFunction.Importtemplates($"{bpl}.zip");
                }
                LogStep(@"2. Execute BPL");
                APEM.MocmainWindow.BPLListInternalFrame.Refresh_Button.ClickSignle();
                APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(bpl).Click();
                APEM.MocmainWindow.BPLListInternalFrame.LoadDesigner_Button.ClickSignle();
                Thread.Sleep(1000);
                APEM.MocmainWindow.ReadOnly_Dialog.OKButton.Click();
                Thread.Sleep(1000);
                APEM.DesignEditorWindow.ExecuteButton.ClickSignle();
                Thread.Sleep(5000);
                APEM.DesignEditorWindow.ExecuteMainInternalFrame.SOAP_CALL2_Button.Click();
                Thread.Sleep(1000);
                var BPText = APEM.DesignEditorWindow.ExecuteMainInternalFrame.CheckField.Text;
                Console.WriteLine(BPText);
                Assert.IsTrue(BPText.Contains(ExpectText));
                APEM.DesignEditorWindow.GetSnapshot(Resultpath + "BPExecute.PNG");
                APEM.DesignEditorWindow.ExecuteMainInternalFrame._UFT_InterFrame.Close();
                APEM.MocmainWindow.ExeCancelDialog.YesButton.Click();
                MOC_Fuction.DesignEditorClose();

                LogStep(@"3. create order and execute");
                MOC_Fuction.PlanFromRPL("RPL916420", ordername);
                APEM.MocmainWindow.WorkstationBP.ClickSignle();
                MOC_Fuction.CheckRowSelection();
                Thread.Sleep(2000);
                APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText(ordername);
                APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
                APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
                APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
                Thread.Sleep(5000);
                APEM.PhaseExecWindow.ExecutionInternalFrame.SOAP_CALL2_Button.Click();
                Thread.Sleep(2000);
                var mocText = APEM.PhaseExecWindow.ExecutionInternalFrame.CheckField.Text;
                Console.WriteLine(mocText);
                Assert.IsTrue(mocText.Contains(ExpectText));
                APEM.PhaseExecWindow.GetSnapshot(Resultpath + "MOCExecute.PNG");
                APEM.PhaseExecWindow.ExecutionInternalFrame.Cancel_Button.ClickSignle();
                Thread.Sleep(1000);
                APEM.PhaseExecWindow.ConfirmationInternalFrame.YesButton.Click();
                Thread.Sleep(2000);
                APEM.ExitApplication();
                LogStep(@"4. execute order in mobile");
                Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
                Mobile_Fuction.gotoApemMobile(driver);
                driver.Wait();
                Mobile_Fuction.login();
                driver.Wait();
                Mobile.OrderProcess_Page.OrderSearch.SendKeys(ordername);
                Thread.Sleep(1000);
                Mobile.OrderProcess_Page.GotoTracking.Click();
                Thread.Sleep(1000);
                Mobile.OrderTracking_Page.ExecutionButton.Click();
                Thread.Sleep(5000);
                Mobile.OrderExecution_Page.SOAP_CALL2_Button.Click();
                Thread.Sleep(6000);
                var mobileText = Mobile.OrderExecution_Page.MainField1.GetAttribute("value");
                Assert.IsTrue(mobileText.Contains(ExpectText));
                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "MobileExecute.PNG");
                Mobile.OrderExecution_Page.CancelButton.Click();
                Thread.Sleep(2000);
                Mobile.OrderExecution_Page.ConfirmYesButton.Click();

                driver.Close();
                
            }
            finally
            {
                LogStep(@"4.delete config key ");
                Base_Function.DeleteConfigKey(Configpath, ConfigKey1);
                Base_Function.DeleteConfigKey(Configpath, ConfigKey2);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
            }
            


        }

    }
}