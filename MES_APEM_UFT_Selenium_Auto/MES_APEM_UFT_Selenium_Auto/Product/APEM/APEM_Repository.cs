using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_AuditModule;
using MES_APEM_UFT_Selenium_Auto.Product.MOC_TemplatesModule;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;

namespace MES_APEM_UFT_Selenium_Auto.Product.APEM
{
    public class APEM
    {
        #region AeBRS_Methods
        public static void AeBRSInstaller(bool importGML = false)
        {
            string password = DBInfo.Info["password"];
            Base_Test.LaunchApp(Base_Directory.AeBRSInstallerDir);
            SdkConfiguration config = new SdkConfiguration();
            SDK.Init(config);
            Thread.Sleep(5000);
            APEM.APEMMainWindow.Password.SendKeys(password);
            APEM.APEMMainWindow.EnterPasswordAgain.SendKeys(password);
            APEM.APEMMainWindow.OKButton.ClickSignle();
            Thread.Sleep(5000);
            if (APEM.APEMMainWindow.UID.IsEnabled)
            {
                APEM.APEMMainWindow.UID.SendKeys("123");
            }
            if (importGML is true)
            {
                APEM.APEMMainWindow.ImportGMLTemplates._UFT_CheckBox.Click();
            }
            APEM.APEMMainWindow.OKButton.ClickSignle();
            APEM.CompletedDialog.IsExist(600000);
            APEM.CompletedDialog.OKButton.Click();
            APEM.APEMMainWindow.Close();
            APEM.CloseDialog.YesButton.Click();
        }
        public static void AeBRSClientConfig()
        {
            Base_Test.LaunchApp(Base_Directory.AeBRSClientConfigureDir);
            SdkConfiguration config = new SdkConfiguration();
            SDK.Init(config);
            Thread.Sleep(5000);
            APEM.APEMMainWindow.Password.SendKeys("");

        }
        #endregion
        #region AeBRS Windows
        public static APEMMainWindow APEMMainWindow => new APEMMainWindow("//JavaWindow[@ObjectName = 'Configuration']");


        #endregion
        #region AeBRS Dialog
        public static UFT_Dialog CompletedDialog => new UFT_Dialog("//Dialog[@Title = 'Configuration Process Completed']");
        #endregion
        #region MOC windows

        public static MOCMainWindow MocmainWindow => new MOCMainWindow("//JavaWindow[@ObjectName = 'MOC']");
        public static PhaseExecWindow PhaseExecWindow => new PhaseExecWindow("//JavaWindow[@NativeClass = 'runtime.vm.frame.OperExecFrame']");
        public static MOC_AuditWindow MOCAuditWindow => new MOC_AuditWindow("//JavaWindow[@ObjectName = 'Audit']");
        public static MOC_ConfigWindow MOCConfigWindow => new MOC_ConfigWindow("//JavaWindow[@ObjectName = 'Config']");
        public static MOC_TemplatesWindow MOCTemplatesWindow => new MOC_TemplatesWindow("//JavaWindow[@ObjectName = 'Templates']");
        public static DesignEditorWindow DesignEditorWindow => new DesignEditorWindow("//JavaWindow[@ObjectName = 'Design Editor']");
        public static DesignVerificationWindow DesignVerificationWindow => new DesignVerificationWindow("//JavaWindow[@ObjectName = 'Design verification']");
        public static DesignCompilationWindow DesignCompilationWindow => new DesignCompilationWindow("//JavaWindow[@ObjectName = 'Design compilation']");


        #endregion

        #region MOC Dialog
        public static UFT_Dialog CloseDialog => new UFT_Dialog("//Dialog[@Title = 'Close the Application']");
        public static UFT_Dialog ReactivateDialog => new UFT_Dialog("//Dialog[@Title = 'Reactivate']");
        public static UFT_Dialog CancelBPDialog => new UFT_Dialog("//Dialog[@Title = 'Cancel']");
        public static UFT_Dialog ActivateDialog => new UFT_Dialog("//Dialog[@Title = 'Activate Order']");
        public static UFT_Dialog DisableBPDialog => new UFT_Dialog("//Dialog[@Title = 'Disable Phase']");
        public static UFT_Dialog ErrorDialog => new UFT_Dialog("//Dialog[@Title = 'Error']");
        public static UFT_Dialog MessageDialog => new UFT_Dialog("//Dialog[@Title = 'Message']");
        public static UFT_Dialog CutElementDialog => new UFT_Dialog("//Dialog[@Title = 'Cut Element']");
        public static UFT_Dialog LoseCopiedDialog => new UFT_Dialog("//Dialog[@Title = 'Lose Copied Element']");
        public static UFT_Dialog AuditReasonDialog => new UFT_Dialog("//Dialog[@Title = 'Audit Reason']");
        
        public static UFT_Dialog DesignSavedDialog => new UFT_Dialog("//Dialog[@Title = 'Design Is Saved']");
        public static UFT_Dialog DesignVerificationDialog => new UFT_Dialog("//Dialog[@Title = 'Design verification']");
        public static UFT_Dialog DesignCompilationDialog => new UFT_Dialog("//Dialog[@Title = 'Design compilation']");
        public static UFT_Dialog VerifyDialog => new UFT_Dialog("//Dialog[@Title = 'Verify']");
        public static UFT_Dialog VerificationConfirmDialog => new UFT_Dialog("//Dialog[@Title = 'Verification confirmation']");
        public static UFT_Dialog CertifyConfirmDialog => new UFT_Dialog("//Dialog[@Title = 'Approval confirmation']"); 
        public static UFT_Dialog CertifyDialog => new UFT_Dialog("//Dialog[@Title = 'Certify*']");
        public static UFT_Dialog BPLCertifyDialog => new UFT_Dialog("//Dialog[@Title = 'Certified']");
        public static UFT_Dialog ExecutionFinishedDialog => new UFT_Dialog("//Dialog[@Title = 'Execution Finished']");
        public static UFT_Dialog RowSelectionDialog => new UFT_Dialog("//Dialog[@Title = 'Row selection']");
        public static UFT_Dialog DeleteEventLogDialog => new UFT_Dialog("//Dialog[@Title = 'Delete Event Log']");
        public static UFT_Dialog CheckParametersDialog => new UFT_Dialog("//Dialog[@Title = 'Check Parameters']");
        public static UFT_Dialog ConfirmFileReplaceDialog => new UFT_Dialog("//Dialog[@Title = 'Confirm File Replace']"); 
        public static UFT_Dialog ExecuteTemplateDialog => new UFT_Dialog("//Dialog[@Title = 'Execute the Template']");
        public static UFT_Dialog RepeatedTemplateDialog => new UFT_Dialog("//Dialog[@Title = 'Repeated Template']");
        public static UFT_Dialog SaveChangesDialog => new UFT_Dialog("//Dialog[@Title = 'Save Changes ...']");
        public static UFT_Dialog ChangesDesignDialog => new UFT_Dialog("//Dialog[@Title = 'Changes in Design']");
        public static UFT_Dialog InvalidImage => new UFT_Dialog("//Dialog[@Title = 'Invalid Image']");
        public static UFT_Dialog BPLEditDialog => new UFT_Dialog("//Dialog[@Title = 'Edit']");
        public static UFT_Dialog SwitchToEditingDialog => new UFT_Dialog("//Dialog[@Title = 'Switch to Editing']");
        public static UFT_Dialog DeleteOrderDialog => new UFT_Dialog("//Dialog[@Title = 'Delete Order']");
        public static UFT_Dialog DeleteRowDialog => new UFT_Dialog("//Dialog[@Title = 'Delete current row']");
        #endregion



        #region MOC_Methods
        public static void ExitApplication()
        {
            var aspen = Process.GetProcessesByName("javaw");
            MocmainWindow.SetActive();
            MocmainWindow.Close();
            if (CloseDialog.IsExist())
            {
                CloseDialog.YesButton.Click();
            }

            try { aspen[0].WaitForExit(10000); }
            catch { Base_Assert.Fail("Failed to close moc."); }
        }

        public static void setServerAndConfig()
        {
            Base_Test.LaunchApp(Base_Directory.MOCDir);
            SdkConfiguration config = new SdkConfiguration();
            SDK.Init(config);
            Thread.Sleep(5000);
            if (AeBRS.AeBRSConfigureWindow.IsExist())
            {
                AeBRS.AeBRSConfigureWindow.ServerName.SetText(Environment.MachineName);
                AeBRS.AeBRSConfigureWindow.OkButton.Click();
                Thread.Sleep(3000);
            }
            if (APEM.RegistrationWindow.IsExist(30))
            {
                APEM.RegistrationWindow.doNotShowCheckBox.Click();
                APEM.RegistrationWindow.Close();
                Thread.Sleep(2000);
            }
            //add workstation
            APEM.MocmainWindow.LogonInternalFrame.userNameEditor.SetText(UserName.qaone1);
            APEM.MocmainWindow.LogonInternalFrame.passwordEditor.SetSecure(PassWord.qaone1);
            APEM.MocmainWindow.LogonInternalFrame.loginbutton.ClickSignle();
            Thread.Sleep(2000);
            APEM.MocmainWindow.Config_moudle.Click();
            APEM.MOCConfigWindow.Workstations.ClickSignle();
            //add Server Machine workstation
            string workstation = Environment.MachineName + ".qae.aspentech.com";
            if (!APEM.MOCConfigWindow.WorkstationInterFrame.WorkstationTable.Row(workstation).Existing)
            {
                APEM.MOCConfigWindow.WorkstationInterFrame.Insert.ClickSignle();
                APEM.MOCConfigWindow.WorkstationInsertInterFrame.Workstation.SetText(workstation);
                APEM.MOCConfigWindow.WorkstationInsertInterFrame.Role.SelectItems(AFWRole.Admin);
                APEM.MOCConfigWindow.WorkstationInsertInterFrame.Confirm.ClickSignle();
                //add reason
                MOC_Fuction.AddReason_config();
                APEM.MOCConfigWindow.WorkstationInsertInterFrame.Close.ClickSignle();
            }
            MOC_Fuction.ConfigClose();
            //chagne order/order tracking view all
            APEM.MocmainWindow.Orders.ClickSignle();
            MOC_Fuction.CheckRowSelection();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderListInternalFrame.Visible_Button.ClickSignle();
            APEM.MocmainWindow.RowsToViewDialog.ViewAll.Click();
            APEM.MocmainWindow.RowsToViewDialog.OK.Click();
            APEM.MocmainWindow.OrderTracking.ClickSignle();
            Thread.Sleep(2000);
            MOC_Fuction.CheckRowSelection();
            APEM.MocmainWindow.OrderTrackingInternalFrame.StatusFilterButton.ClickSignle();
            APEM.MocmainWindow.RowsToViewDialog.ViewAll.Click();
            APEM.MocmainWindow.RowsToViewDialog.OK.Click();
            //import template
            MOC_TemplatesFunction.Importtemplates("TEMP.zip");
            APEM.ExitApplication();
        } 
        #endregion

        #region APEMAdmin windows

        public static APEMAdmin_Window APEMAdminWindow => new APEMAdmin_Window("//Window[@WindowTitleRegExp = 'Aspen Production Execution Manager Administrator*'] ");


        //491775
        public static SavePrintFile_Window SavePrintFileWindow => new SavePrintFile_Window("//Window[@WindowClassRegExp = 'SunAwtFrame");


        public static STD_Dialog PropertyDialog => new STD_Dialog("//Dialog[@Text = 'Audit & Compliance Extractor - Properties' and @Index='1']");
        public static STD_Dialog PrintDialog => new STD_Dialog("//Dialog[@WindowTitleRegExp = 'Print']");

        #endregion


        private static HP.LFT.SDK.WPF.IWindow aspenONERegistrationWindow = Desktop.Describe<HP.LFT.SDK.WPF.IWindow>(new HP.LFT.SDK.WPF.WindowDescription
        {
            ObjectName = @"aspenONE Registration"
        });

        public static RegistrationWindow RegistrationWindow => new RegistrationWindow(aspenONERegistrationWindow);




} 

}


