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
        [TestCaseID(916388)]
        [Title("UC822684_Soap1.2(AeBRS_BPC):CDM_GET_EQM_CLASS_DESCRIPTION() function should work as expected")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_916388()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string ordername = "ORDER916388";
            string bpl = "BPL916388";
            string Classtext = "Geographical Address Class";


            string Flagpath = Base_Directory.ConfigDir + "flags.m2r_cfg";
            string Path = Base_Directory.ConfigDir + "path.m2r_cfg";
            string Modelpath = Base_Directory.ConfigDir + "resource_model.m2r_cfg";

            string ConfigKey1 = $"SOAP_SERVER_HTTP_AEBRSBPC  = http://{Environment.MachineName}:8080/AeBRSserver/services/AeBRS_BPC";
            string ConfigKey2 = @"SOAP_SERVER_URI_AEBRSBPC = http://www.aspentech.com";
            string ConfigKey3 = @"CDM_RESOURCE_SERVICE_ENABLE = 1";
            string ConfigKey4 = @"BPC_USER = Administrator";
            string ConfigKey5 = $"BPC_PSSW = {PassWord.admin}";

            try
            {
                //set config in flag
                Base_Function.AddConfigKey(Path, ConfigKey1);
                Base_Function.AddConfigKey(Path, ConfigKey2);
                Base_Function.EditConfigKey(Flagpath, ConfigKey3);
                Base_Function.EditConfigKey(Modelpath, ConfigKey4);
                Base_Function.EditConfigKey(Modelpath, ConfigKey5);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
                //restart tomcat
                Base_Function.ResartServices(ServiceName.Tomcat);

                LogStep(@"1. create mMDM");
                Application.LaunchmMDMAdmin();
                mMDM.AdminWizardWindow.Next.Click();
                //select DB
                mMDM.AdminWizardWindow.DirectDbConn.Click();
                mMDM.AdminWizardWindow.Next.Click();
                mMDM.AdminWizardWindow.Finish.Click();
                //Wait mMDM Administrator Wizard re-pops up.
                //select DB server
                mMDM.AdminWizardWindow.SQLServer.Click();
                mMDM.AdminWizardWindow.ServerName.SendKeys(Environment.MachineName);
                mMDM.AdminWizardWindow.Next.Click();
                //set username,password and test
                mMDM.AdminWizardWindow.UserName.SetText(DBInfo.Info["username"]);
                mMDM.AdminWizardWindow.Password.SetSecure(DBInfo.Info["password"]);
                mMDM.AdminWizardWindow.Test.Click();
                //check success
                string message = mMDM.SuccessDialog.Message.Text;
                Assert.AreEqual("Successful!", message);
                mMDM.SuccessDialog.OK.Click();
                //input DB name
                mMDM.AdminWizardWindow.DBName.SendKeys("ODM916388");
                mMDM.AdminWizardWindow.GetSnapshot(Resultpath + "create mMDM database.PNG");
                if (mMDM.AdminWizardWindow.SampleDatabase.IsEnabled)
                {
                    mMDM.AdminWizardWindow.SampleDatabase.Click();
                    mMDM.AdminWizardWindow.Create.Click();
                    mMDM.AdminWizardWindow.Next.Click();
                    mMDM.AdminWizardWindow.Next.Click();
                    //finish
                    mMDM.AdminWizardWindow.Finish.Click();
                    //wait 
                    Thread.Sleep(15000);
                    //check sucess
                    Assert.IsTrue(mMDM.mMDMAdminWindow.CreatingDatabaseWindow.Exists(3000));
                    mMDM.mMDMAdminWindow.CreatingDatabaseWindow.Close();
                }
                else
                {
                    mMDM.AdminWizardWindow.Next.Click();
                    mMDM.AdminWizardWindow.Next.Click();
                    //finish
                    mMDM.AdminWizardWindow.Finish.Click();

                }
                Thread.Sleep(2000);
                mMDM.mMDMAdminWindow.Close();
                LogStep(@"2. open mMDM editor check adress");
                Application.LaunchmMDMEditor();
                //select Class;Classes
                mMDM.EditorWindow.mMDMTreeView.GetNode("Aspen mMDM;Class").Expand();
                mMDM.EditorWindow.mMDMTreeView.Select("Aspen mMDM;Class;Classes");
                //Double click Adress and check value
                mMDM.EditorWindow.GetSnapshot(Resultpath + "Address in mMDM.PNG");
                mMDM.EditorWindow.mMDMTable.CustomGrid.DataGridView.ActivateCell(0, "Description");
                string name = mMDM.EditorDefinitionWindow.Name.Text;
                string description = mMDM.EditorDefinitionWindow.Description.Text;
                Assert.IsTrue(name == "Address" && description == "Geographical Address Class", "Class description");
                mMDM.EditorDefinitionWindow.Close();
                mMDM.EditorWindow.Close();



                LogStep(@"3. import BPL");
                Application.LaunchMocAndLogin();
                APEM.MocmainWindow.BPLDesign.ClickSignle();
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
                APEM.DesignEditorWindow.ExecuteMainInternalFrame.BPC_Button.Click();
                Thread.Sleep(1000);
                var BPText = APEM.DesignEditorWindow.ExecuteMainInternalFrame.CheckField.Text;
                Assert.IsTrue(BPText.Contains(Classtext));
                APEM.DesignEditorWindow.GetSnapshot(Resultpath + "BPExecute.PNG");
                APEM.DesignEditorWindow.ExecuteMainInternalFrame._UFT_InterFrame.Close();
                APEM.MocmainWindow.ExeCancelDialog.YesButton.Click();
                MOC_Fuction.DesignEditorClose();

                LogStep(@"3. create order and execute");
                MOC_Fuction.PlanFromRPL("RPL916388", ordername);
                APEM.MocmainWindow.WorkstationBP.ClickSignle();
                MOC_Fuction.CheckRowSelection();
                Thread.Sleep(2000);
                APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText(ordername);
                APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
                APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
                APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
                Thread.Sleep(5000);
                APEM.PhaseExecWindow.ExecutionInternalFrame.BPC_Button.Click();
                Thread.Sleep(2000);
                var mocText = APEM.PhaseExecWindow.ExecutionInternalFrame.CheckField.Text;
                Console.WriteLine(mocText);
                Assert.IsTrue(mocText.Contains(Classtext));
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
                Mobile.OrderExecution_Page.BPC_Button.Click();
                Thread.Sleep(6000);
                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "MobileExecute.PNG");
                var mobileText = Mobile.OrderExecution_Page.MainField0.GetAttribute("value");
                Assert.IsTrue(mobileText.Contains(Classtext));
                Mobile.OrderExecution_Page.CancelButton.Click();
                Thread.Sleep(2000);
                Mobile.OrderExecution_Page.ConfirmYesButton.Click();

                driver.Close();
                
            }
            finally
            {
                LogStep(@"4.delete config key ");
                Base_Function.DeleteConfigKey(Path, ConfigKey1);
                Base_Function.DeleteConfigKey(Path, ConfigKey2);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
            }
            


        }

    }
}