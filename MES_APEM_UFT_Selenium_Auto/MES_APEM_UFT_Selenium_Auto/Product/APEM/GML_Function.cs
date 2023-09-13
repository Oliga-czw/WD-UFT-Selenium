using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APRM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MES_APEM_UFT_Selenium_Auto.Product.APEM
{
    class GML_Function
    {
        public static void RestoreDataBase()
        {
            string database = "master";
            string sourceName1 = Base_Directory.ProjectDir + "Data\\Input\\GML_DATABASE\\aebrs.BAK'";
            string sourceName2 = Base_Directory.ProjectDir + "Data\\Input\\GML_DATABASE\\APEMMMDMMVT.BAK'";
            SqlHelper helper = new SqlHelper();
            string SQL1 = $" ALTER DATABASE AeBRS SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE AeBRS";
            string SQL2 = $"RESTORE DATABASE AeBRS FROM DISK = '" + sourceName1;
            string SQL3 = $"RESTORE DATABASE APEMMMDMMVT FROM DISK = '" + sourceName2;
            helper.ExecuteNonQuery(SQL1, database);
            helper.ExecuteNonQuery(SQL2, database);
            helper.ExecuteNonQuery(SQL3, database);
        }
        public static void RestoreDBData()
        {
            string database = "master";
            SqlHelper helper = new SqlHelper();
            string SQL1 = @"DROP LOGIN  AEBRS";
            string SQL2 = @"use master
                            EXEC sp_addrolemember 'db_datawriter', 'AEBRS'
                            EXEC sp_addrolemember 'db_datareader', 'AEBRS'
                            EXEC sp_addrolemember 'db_owner', 'AEBRS'";
            string SQL3 = @"use aebrs
                            exec sp_change_users_login 'update_one', 'aebrs', 'aebrs'";
            helper.ExecuteNonQuery(SQL1, database);
            helper.ExecuteNonQuery(SQL2, database);
            helper.ExecuteNonQuery(SQL3);
        }

        public static void GML_mMDMConfig()
        {
            mMDM_Fuction.GML_Configure_mMDM_Admin_1();
            mMDM_Fuction.GML_Configure_mMDM_BulkLoad();
            mMDM_Fuction.GML_Configure_mMDM_Editor();
            mMDM_Fuction.GML_Configure_mMDM_Admin_2();
        }
        public static void GMLAPRMConfig()
        {
            //exit aprm db?
            bool exits;
            exits = File.Exists(Base_Directory.APRMDir + @"\AspenBatch_dat1.mdf");
            if (!exits)
            {
                APRM_Fuction.WizardAprmDB();
            }
            APRM_Fuction.GMLImportAprmAdmin();
            //set BatchDetailDisplay area
            Application.LaunchBatchDetailDisplay();
            Batch_Fuction.setOptionData("Batch");
            APRM.APRM.BatchMainWindow.Close();
        }

        public static void GML_ConfigEnviroment()
        {
            Application.LaunchMocAndLogin();
            APEM.MocmainWindow.Config_moudle.Click();
            APEM.MOCConfigWindow.Import_ReplaceMerge.ClickSignle();
            APEM.MOCConfigWindow.ConfigImportDialog.FileName.SendKeys(Base_Directory.GMLConfig);
            //Add reason
            MOC_Fuction.AddReason_config();
            MOC_Fuction.ConfigClose();
            MOC_Fuction.MocClose();
        }
        public static void StartIP21()
        {
            Application.LaunchIP21();
            IP21.IP21MainWindow.Start.Click();
            if (IP21.IP21MainWindow.ShutDownDialog.IsExist())
            {
                IP21.IP21MainWindow.ShutDownDialog.OK.Click();
            }
            int i = 1;
            while(IP21.IP21MainWindow.BarMessage.Text != "InfoPlus.21 has been started successfully.")
            {
                Thread.Sleep(1000);
                i++;
                if (i > 60)
                {
                    break;
                }
                    
            }
            if (i < 60)
            {
                //log success
                Assert.AreEqual(IP21.IP21MainWindow.BarMessage.Text, "InfoPlus.21 has been started successfully.");
            }

            if (IP21.IP21MainWindow.IsExist())
            {
                IP21.IP21MainWindow.Close();
            }
        }

    }
}
