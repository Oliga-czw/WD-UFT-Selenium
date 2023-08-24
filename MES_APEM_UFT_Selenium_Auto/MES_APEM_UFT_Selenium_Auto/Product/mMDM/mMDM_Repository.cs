using HP.LFT.SDK;
using HP.LFT.SDK.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;


namespace MES_APEM_UFT_Selenium_Auto.Product.APEM
{
    public  class mMDM
    {
        #region mMDM windows



        //public static MenuWindow menuWindow => new MenuWindow("//JavaWindow[@Title = 'Aspen Weigh and Dispense Execution']");
        //mMDM Admin
        private static IWindow AdminWizard_Window = Desktop.Describe<IWindow>(new WindowDescription
        {
            WindowTitleRegExp = @"Aspen mMDM Administrator Wizard"
        });
        public static AdminWizard_Window AdminWizardWindow => new AdminWizard_Window(AdminWizard_Window);

        private static IWindow Admin_Window = Desktop.Describe<IWindow>(new WindowDescription
        {
            Text = As.RegExp("Aspen mMDM Administrator.*")
        });
        public static mMDMAdmin_Window mMDMAdminWindow => new mMDMAdmin_Window(Admin_Window);
        //mMDM BulkLoad
        private static IWindow BulkLoad_Window = Desktop.Describe<IWindow>(new WindowDescription
        {
            Text = As.RegExp("Aspen mMDM Bulk Load.*")
        });
        public static mMDMBulkLoad_Window mMDMBulkLoadWindow => new mMDMBulkLoad_Window(BulkLoad_Window);

        private static IWindow BulkLoadWizard_Window = Desktop.Describe<IWindow>(new WindowDescription
        {
            WindowTitleRegExp = @"Bulk Load Getting Started Wizard"
        });
        public static BulkLoadWizard_Window BulkLoadWizardWindow => new BulkLoadWizard_Window(BulkLoadWizard_Window);

        private static IWindow AspenWorkSpace_Window = Desktop.Describe<IWindow>(new WindowDescription
        {
            WindowTitleRegExp = @"Aspen Workspace Connection Admin"
        });

        public static AspenWorkSpace_Window AspenWorkSpaceWindow => new AspenWorkSpace_Window(AspenWorkSpace_Window);
        private static IWindow BulkLoadImportWizard_Window = Desktop.Describe<IWindow>(new WindowDescription
        {
            WindowTitleRegExp = @"Bulk Load Import Wizard"
        });
        public static BulkLoadImportWizard_Window BulkLoadImportWizardWindow => new BulkLoadImportWizard_Window(BulkLoadImportWizard_Window);

        public static IWindow BulkLoadaAfterImport_Window = Desktop.Describe<IWindow>(new WindowDescription
        {
            ObjectName = @"BulkloadForm"
        });
        public static mMDMBulkLoad_Window BulkLoadaAfterImportWindow => new mMDMBulkLoad_Window(BulkLoadaAfterImport_Window);

        #endregion

        #region mMDM Dialog
        public static Success_Dialog SuccessDialog => new Success_Dialog(AdminWizard_Window, "//Dialog[@Text = 'Database Connection']");

        public static OpenFile_Dialog OpenFileDialog => new OpenFile_Dialog("//Dialog[@Index = '0']");

        public static STD_Dialog ImportTimesDialog => new STD_Dialog("//Dialog[@Index = '0']");

        #endregion


        #region mMDM Methods

        //public static void ExitApplication()
        //{
        //    var aspen = Process.GetProcessesByName("javaw");
        //    MocmainWindow.SetActive();
        //    Keyboard.KeyDown(Keyboard.Keys.Alt);
        //    Keyboard.PressKey(Keyboard.Keys.F4);
        //    if (CloseDialog.IsExist())
        //    {
        //        CloseDialog.YesButton.Click();
        //    }

        //    try { aspen[0].WaitForExit(10000); }
        //    catch { Base_Assert.Fail("Failed to close moc."); }
        //}
        #endregion

    }
}


