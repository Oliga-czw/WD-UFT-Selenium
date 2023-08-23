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
        public static void AeBRSInstaller()
        {
            
        }
        #endregion
        #region AeBRS Windows
        public static APEMMainWindow APEMMainWindow => new APEMMainWindow("//JavaWindow[@ObjectName = 'Configuration']");
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


