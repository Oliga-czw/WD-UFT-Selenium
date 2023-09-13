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

namespace MES_APEM_UFT_Selenium_Auto.Product.APEM
{
    public class APEM
    {
        #region AeBRS_Methods
        public static void AeBRSInstaller(bool importGML)
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
            if (APEM.APEMMainWindow.UID.IsReadOnly is false) 
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
        public static MOC_AuditWindow MOCAuditWindow => new MOC_AuditWindow("//JavaWindow[@ObjectName = 'Audit']");
        public static MOC_TemplatesWindow MOCTemplatesWindow => new MOC_TemplatesWindow("//JavaWindow[@ObjectName = 'Audit']");
        
        #endregion

        #region MOC Dialog
        public static UFT_Dialog CloseDialog => new UFT_Dialog("//Dialog[@Title = 'Close the Application']");
        public static UFT_Dialog ErrorDialog => new UFT_Dialog("//Dialog[@Title = 'Error']");
        public static UFT_Dialog MessageDialog => new UFT_Dialog("//Dialog[@Title = 'Message']");

        #endregion

       
        #region MOC_Methods
        public static void ExitApplication()
        {
            var aspen = Process.GetProcessesByName("javaw");
            MocmainWindow.SetActive();
            Keyboard.KeyDown(Keyboard.Keys.Alt);
            Keyboard.PressKey(Keyboard.Keys.F4);
            if (CloseDialog.IsExist())
            {
                CloseDialog.YesButton.Click();
            }

            try { aspen[0].WaitForExit(10000); }
            catch { Base_Assert.Fail("Failed to close moc."); }
        }
        #endregion

        #region APEMAdmin windows

        public static APEMAdmin_Window APEMAdminWindow => new APEMAdmin_Window("//Window[@WindowTitleRegExp = 'Aspen Production Execution Manager Administrator.*'] ");


        public static STD_Dialog PropertyDialog => new STD_Dialog("//Dialog[@Text = 'Audit & Compliance Extractor - Properties' and @Index='1']");
        #endregion
    }
}


