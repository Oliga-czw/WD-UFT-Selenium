using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APRM;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
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
            Thread.Sleep(30000);
        }

        public static void ConfigEnviroment(string Path)
        {
            Application.LaunchMocAndLogin();
            APEM.MocmainWindow.Config_moudle.Click();
            APEM.MOCConfigWindow.Import_ReplaceMerge.ClickSignle();
            APEM.MOCConfigWindow.ConfigImportDialog.FileName.SendKeys(Path);
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
            if (IP21.IP21MainWindow.StopDialog.IsExist())
            {
                IP21.IP21MainWindow.StopDialog.OK.Click();
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

            IP21.IP21MainWindow.Close();
        }
        public static void StopIP21()
        {
            Application.LaunchIP21();
            IP21.IP21MainWindow.Stop.Click();
            if (IP21.IP21MainWindow.StopDialog.IsExist())
            {
                IP21.IP21MainWindow.StopDialog.Yes.Click();
            }
            int i = 1;
            while (IP21.IP21MainWindow.BarMessage.Text != "InfoPlus.21 has been stopped successfully." || IP21.IP21MainWindow.BarMessage.Text != "InfoPlus.21 is already stopped.")
            {
                Thread.Sleep(1000);
                i++;
                if (i > 60)
                {
                    break;
                }

            }
            IP21.IP21MainWindow.Close();
        }
        public static void GML_UserTable()
        {
            //Copy pdf 
            Base_File.CopyFolder(Base_Directory.GMLDOCDir, @"C:\DOCs");
            Application.LaunchMocAndLogin();
            APEM.MocmainWindow.Config_moudle.Click();
            APEM.MOCConfigWindow.Table_Definition.ClickSignle();
            //Edit Table_Definition
            APEM.MOCConfigWindow.TableDefinitionInterFrame.Tables.Row("DOCUMENT").Click();
            APEM.MOCConfigWindow.TableDefinitionInterFrame.DataView.ClickSignle();
            //add doc1
            bool S1 = APEM.MOCConfigWindow.TableDataInputInterFrame.Tables.Row("sop_1").Existing;
            bool S2 = APEM.MOCConfigWindow.TableDataInputInterFrame.Tables.Row("sop_2").Existing;
            bool S3 = APEM.MOCConfigWindow.TableDataInputInterFrame.Tables.Row("sop_3").Existing;
            if (!(S1 && S2 && S3))
            {
                APEM.MOCConfigWindow.TableDataInputInterFrame.Insert.ClickSignle();
            }
            if (!S1)
            {
                APEM.MOCConfigWindow.TableDataInputInterFrame.DocumentEditor.SendKeys("sop_1");
                APEM.MOCConfigWindow.TableDataInputInterFrame.DocumentEditor.SendKeys(@"C:\DOCs\sop_1.pdf");
                //add reason
                MOC_Fuction.AddReason_config();
            }
            //add doc2
            if (!S2)
            {
                APEM.MOCConfigWindow.TableDataInputInterFrame.DocumentEditor.SendKeys("sop_2");
                APEM.MOCConfigWindow.TableDataInputInterFrame.DocumentEditor.SendKeys(@"C:\DOCs\sop_2.pdf");
                //add reason
                MOC_Fuction.AddReason_config();
            }
            if (!S3)
            {
                //add doc3
                APEM.MOCConfigWindow.TableDataInputInterFrame.DocumentEditor.SendKeys("sop_3");
                APEM.MOCConfigWindow.TableDataInputInterFrame.DocumentEditor.SendKeys(@"C:\DOCs\sop_3.pdf");
                //add reason
                MOC_Fuction.AddReason_config();
            }
            if (!(S1 && S2 && S3))
            {
                APEM.MOCConfigWindow.TableDataInputInterFrame.Cancel.ClickSignle();
            }
            APEM.MOCConfigWindow.TableDataInputInterFrame.Close.ClickSignle();
            MOC_Fuction.ConfigClose();
            MOC_Fuction.MocClose();
        }

        public static void GML_Workstation()
        {
            Application.LaunchMocAndLogin();
            APEM.MocmainWindow.Config_moudle.Click();
            APEM.MOCConfigWindow.Workstations.ClickSignle();
            //Edit MOC Workstation
            APEM.MOCConfigWindow.WorkstationInterFrame.WorkstationTable.Row("WORKSTATION").Click();
            APEM.MOCConfigWindow.WorkstationInterFrame.Edit.ClickSignle();
            //Assign MOC Workstation to ProcessCellLine2
            APEM.MOCConfigWindow.WorkstationEditInterFrame.Role.SelectItems(AFWRole.Admin);
            APEM.MOCConfigWindow.WorkstationEditInterFrame.Site.SelectItems("Site02");
            APEM.MOCConfigWindow.WorkstationEditInterFrame.ProcessArea.SelectItems("Packing");
            APEM.MOCConfigWindow.WorkstationEditInterFrame.Workcenter.SelectItems("ProcessCellLine2");
            APEM.MOCConfigWindow.WorkstationEditInterFrame.Confirm.ClickSignle();
            //add reason
            MOC_Fuction.AddReason_config();
            APEM.MOCConfigWindow.WorkstationEditInterFrame.Close.ClickSignle();
            //add Server Machine workstation
            string workstation = Environment.MachineName + ".qae.aspentech.com";
            if (!APEM.MOCConfigWindow.WorkstationInterFrame.WorkstationTable.Row(workstation).Existing)
            {
                APEM.MOCConfigWindow.WorkstationInterFrame.Insert.ClickSignle();
                APEM.MOCConfigWindow.WorkstationInsertInterFrame.Workstation.SetText(workstation);
                APEM.MOCConfigWindow.WorkstationInsertInterFrame.Role.SelectItems(AFWRole.Admin);
                APEM.MOCConfigWindow.WorkstationInsertInterFrame.Site.SelectItems("Site02");
                APEM.MOCConfigWindow.WorkstationInsertInterFrame.ProcessArea.SelectItems("Packing");
                APEM.MOCConfigWindow.WorkstationInsertInterFrame.Workcenter.SelectItems("ProcessCellLine2");
                APEM.MOCConfigWindow.WorkstationInsertInterFrame.Confirm.ClickSignle();
                //add reason
                MOC_Fuction.AddReason_config();
                APEM.MOCConfigWindow.WorkstationInsertInterFrame.Close.ClickSignle();
            }
            else
            {
                APEM.MOCConfigWindow.WorkstationInterFrame.WorkstationTable.Row(workstation).Click();
                APEM.MOCConfigWindow.WorkstationInterFrame.Edit.ClickSignle();
                APEM.MOCConfigWindow.WorkstationEditInterFrame.Role.SelectItems(AFWRole.Admin);
                APEM.MOCConfigWindow.WorkstationEditInterFrame.Site.SelectItems("Site02");
                APEM.MOCConfigWindow.WorkstationEditInterFrame.ProcessArea.SelectItems("Packing");
                APEM.MOCConfigWindow.WorkstationEditInterFrame.Workcenter.SelectItems("ProcessCellLine2");
                APEM.MOCConfigWindow.WorkstationEditInterFrame.Confirm.ClickSignle();
                //add reason
                MOC_Fuction.AddReason_config();
                APEM.MOCConfigWindow.WorkstationEditInterFrame.Close.ClickSignle();
            }
            MOC_Fuction.ConfigClose();
            MOC_Fuction.MocClose();
        }


        public static void GML_ConfigAll()
        {
            //mMDM
            GML_mMDMConfig();
            //GML templates
            APEM.AeBRSInstaller(true);
            //AFW
            AFW_Fuction.ReplaceAFWDB();
            //APRM
            GMLAPRMConfig();
            //Environment
            string oldfile = Base_Directory.GMLConfig;
            string newFile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GMLConfig.ini";
            string oldText = "MachineName";
            string newText = Environment.MachineName;
            Base_Function.ReplaceTextInNewFile(oldfile, newFile, oldText, newText);
            GML_Function.ConfigEnviroment(newFile);
            //IP21
            StartIP21();
            //Configure User Table
            GML_UserTable();
            //config IP21 

            //Workstation
            GML_Workstation();
        }


    }
}
