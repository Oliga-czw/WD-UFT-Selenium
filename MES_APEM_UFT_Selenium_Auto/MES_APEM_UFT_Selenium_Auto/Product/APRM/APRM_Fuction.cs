using HP.LFT.SDK;
using HP.LFT.SDK.StdWin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.DataBaseWizard;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using MES_APEM_UFT_Selenium_Auto.Product.APRM;

namespace MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary
{
    class APRM_Fuction
    {
        static string server = Environment.MachineName;
        static string database = DBInfo.Info["AeBRS"];
        static string user = DBInfo.Info["username"];
        static string password = DBInfo.Info["password"];

        public static void DropAprmDB()
        {
            //exit aprm db
            bool exits;
            exits = File.Exists(Base_Directory.APRMDir+ @"\AspenBatch_dat1.mdf");
            //stop service
            if (exits)
            {
                StopService("Batch21BcuService");
                StopService("Batch21OdbcServices");
                StopService("Batch21Services");
                APRMSqlHelper aprm = new APRMSqlHelper();
                string delete1 = $"USE master EXEC sp_droplogin [AspenBatch21]";
                string delete2 = $"USE master drop database AspenBatch";
                Console.WriteLine("Delete APRM DB!");
                aprm.ExecuteNonQuery(delete1);
                aprm.ExecuteNonQuery(delete2);
                StartService("Batch21BcuService");
                StartService("Batch21OdbcServices");
                StartService("Batch21Services");
            }
           
        }
        public static void WizardAprmDB()
        {
            //open wizard
            Application.LaunchWizrd();
            Wizard.WizardWindow.next.Click();
            //select aprm
            Wizard.WizardWindow.lstAPRM.Click();
            Wizard.WizardWindow.next.Click();
            Wizard.WizardWindow.next.Click();
            Wizard.WizardWindow.next.Click();
            //select sqlserver
            Wizard.DataLinkPropertiesDialog.DBserver.Click();
            Wizard.DataLinkPropertiesDialog.next.Click();
            Thread.Sleep(2000);
            Wizard.DataLinkPropertiesDialog.servername.SetText(server);
            Wizard.DataLinkPropertiesDialog.username.SetText(user);
            Wizard.DataLinkPropertiesDialog.password.SetText(password);
            Wizard.DataLinkPropertiesDialog.savingPWD.Click();
            Wizard.DataLinkPropertiesDialog.TestConnection.Click();
            Wizard.DataLinkCheckDialog.OK.Click();
            Wizard.DataLinkPropertiesDialog.OK.Click();
            //select folder
            if (!Directory.Exists(Base_Directory.APRMDir))
            {
                Directory.CreateDirectory(Base_Directory.APRMDir);
            }
            Wizard.WizardWindow.DBLocation.SetText(Base_Directory.APRMDir);
            Wizard.WizardWindow.next.Click();
            Wizard.WizardWindow.next.Click();
            //enter batch psssword
            Wizard.WizardWindow.BatchEnterPassword.SetSecure(password);
            Wizard.WizardWindow.BatchVerifyPassword.SetSecure(password);
            Wizard.WizardWindow.next.Click();
            //add domain user
            Wizard.WizardWindow.DomainUser.SetText(UserName.qaone1);
            Wizard.WizardWindow.next.Click();
            //ceate ADSA
            Wizard.WizardWindow.next.Click();
            if (Wizard.ADSAExitDialog.IsExist())
            {
                Wizard.ADSAExitDialog.OK.Click();
                Wizard.WizardWindow.chkCreateADSA.Click();
                Wizard.WizardWindow.next.Click();
            }
            //finish and wait 80 seconds for config conpleted
            Wizard.WizardWindow.Finish.Click();
            //check install finish
            Wizard.WizardWindow.btnClose.WaitUntilEnabled(80000);
            Wizard.WizardWindow.btnClose.Click();
                //log
                //Console.WriteLine("Wizard APRM DB!");
            StopService("Batch21Services");
            StartService("Batch21Services");
        }

        public static void WDImportAprmAdmin()
        {
            string servername = Environment.MachineName;//OLIGA-2022-2
            //open aprm admin
            Application.LaunchAprmAdmin();
            //expand node,check wd batch exit
            APRM.APRMAdminWindow.TreeView.GetNode("Console Root;Production Record Manager").Expand();
            APRM.APRMAdminWindow.TreeView.GetNode("Console Root;Production Record Manager;Data Sources").Expand();
            APRM.APRMAdminWindow.TreeView.GetNode($"Console Root;Production Record Manager;Data Sources;{servername}").Expand();
            APRM.APRMAdminWindow.TreeView.Select($"Console Root;Production Record Manager;Data Sources;{servername};Areas");
            if (APRM.APRMAdminWindow.TreeView.GetNode($"Console Root;Production Record Manager;Data Sources;{servername};Areas").HasChildren)
            {
                APRM.APRMAdminWindow.TreeView.GetNode($"Console Root;Production Record Manager;Data Sources;{servername};Areas").Expand();
                if (APRM.APRMAdminWindow.WeightDispense.Exists())
                {
                    //Base_logger.Info("WeightDispense has already existed");
                    Console.WriteLine("WeightDispense has already existed");
                }
                else
                {
                    //import wd batch
                    APRM.APRMAdminWindow.actionMenuItem.Click();
                    Keyboard.PressKey(Keyboard.Keys.I);
                    APRM.APRMAdminWindow.Open.Filename.SendKeys(Base_Directory.WDBatch);
                    Keyboard.PressKey(Keyboard.Keys.Enter);
                    Thread.Sleep(2000);
                    //check wd batch exit
                    if (APRM.APRMAdminWindow.WeightDispense.Exists())
                    {
                        Console.WriteLine("WeightDispense import successfully");
                    }
                    // Base_Assert.IsTrue(APRM.APRMAdminWindow.WeightDispense.Exists(), "WeightDispense import successfully");

                }
            }
            else
            {
                //import wd batch
                APRM.APRMAdminWindow.actionMenuItem.Click();
                Keyboard.PressKey(Keyboard.Keys.I);
                APRM.APRMAdminWindow.Open.Filename.SendKeys(Base_Directory.WDBatch);
                Keyboard.PressKey(Keyboard.Keys.Enter);
                Thread.Sleep(2000);
                //check wd batch exit
                APRM.APRMAdminWindow.TreeView.GetNode($"Console Root;Production Record Manager;Data Sources;{servername};Areas").Expand();
                //Base_Assert.IsTrue(APRM.APRMAdminWindow.WeightDispense.Exists(), "WeightDispense import successfully");
                if (APRM.APRMAdminWindow.WeightDispense.Exists())
                {
                    //log
                    Console.WriteLine("WeightDispense import successfully");
                }

            }
            APRM.APRMAdminWindow.Close();

        }

        public static void GMLImportAprmAdmin()
        {
            string servername = Environment.MachineName;//OLIGA-2022-2
            //open aprm admin
            Application.LaunchAprmAdmin();
            //expand node,check wd batch exit
            APRM.APRMAdminWindow.TreeView.GetNode("Console Root;Production Record Manager").Expand();
            APRM.APRMAdminWindow.TreeView.GetNode("Console Root;Production Record Manager;Data Sources").Expand();
            APRM.APRMAdminWindow.TreeView.GetNode($"Console Root;Production Record Manager;Data Sources;{servername}").Expand();
            APRM.APRMAdminWindow.TreeView.Select($"Console Root;Production Record Manager;Data Sources;{servername};Areas");
            Console.WriteLine(APRM.APRMAdminWindow.TreeView.GetNode($"Console Root;Production Record Manager;Data Sources;{servername};Areas").HasChildren);
            if (APRM.APRMAdminWindow.TreeView.GetNode($"Console Root;Production Record Manager;Data Sources;{servername};Areas").HasChildren)
            {
                APRM.APRMAdminWindow.TreeView.GetNode($"Console Root;Production Record Manager;Data Sources;{servername};Areas").Expand();
                //batch
                if (APRM.APRMAdminWindow.Batch.Exists())
                {
                    //Base_logger.Info("Batch has already existed");
                    Console.WriteLine("Batch has already existed");
                }
                else
                {
                    //import APEM batch
                    //BatchArea
                    APRM.APRMAdminWindow.actionMenuItem.Click();
                    Keyboard.PressKey(Keyboard.Keys.I);
                    APRM.APRMAdminWindow.Open.Filename.SendKeys(Base_Directory.BatchArea);
                    Keyboard.PressKey(Keyboard.Keys.Enter);
                    Thread.Sleep(2000);
                    //check APEM batch exit
                    if (APRM.APRMAdminWindow.Batch.Exists())
                    {
                        Console.WriteLine("Batch import successfully");
                    }
                    // Base_Assert.IsTrue(APRM.APRMAdminWindow.Batch.Exists(), "Batch import successfully");
                }
                //Equipment
                if (APRM.APRMAdminWindow.Equipment.Exists())
                {
                    //Base_logger.Info("Equipment has already existed");
                    Console.WriteLine("Equipment has already existed");
                }
                else
                {
                    //import APEM batch
                    //EquipmentArea
                    APRM.APRMAdminWindow.actionMenuItem.Click();
                    Keyboard.PressKey(Keyboard.Keys.I);
                    APRM.APRMAdminWindow.Open.Filename.SendKeys(Base_Directory.EquipmentArea);
                    Keyboard.PressKey(Keyboard.Keys.Enter);
                    Thread.Sleep(2000);
                    //check APEM batch exit
                    if (APRM.APRMAdminWindow.Equipment.Exists())
                    {
                        Console.WriteLine("Equipment import successfully");
                    }
                    // Base_Assert.IsTrue(APRM.APRMAdminWindow.Equipment.Exists(), "Equipment import successfully");
                }
            }
            else
            {
                //import APEM batch
                //BatchArea
                APRM.APRMAdminWindow.actionMenuItem.Click();
                Keyboard.PressKey(Keyboard.Keys.I);
                APRM.APRMAdminWindow.Open.Filename.SendKeys(Base_Directory.BatchArea);
                Keyboard.PressKey(Keyboard.Keys.Enter);
                Thread.Sleep(2000);
                //EquipmentArea
                APRM.APRMAdminWindow.actionMenuItem.Click();
                Keyboard.PressKey(Keyboard.Keys.I);
                APRM.APRMAdminWindow.Open.Filename.SendKeys(Base_Directory.EquipmentArea);
                Keyboard.PressKey(Keyboard.Keys.Enter);
                Thread.Sleep(2000);
                //check APEM batch exit
                APRM.APRMAdminWindow.TreeView.GetNode($"Console Root;Production Record Manager;Data Sources;{servername};Areas").Expand();
                //Base_Assert.IsTrue(APRM.APRMAdminWindow.WeightDispense.Exists(), "WeightDispense import successfully");
                if (APRM.APRMAdminWindow.Batch.Exists()&& APRM.APRMAdminWindow.Equipment.Exists())
                {
                    //log
                    Console.WriteLine("Batch and Equipment import successfully");
                }

            }
            APRM.APRMAdminWindow.Close();
        }
            public static void ConfigAPEMAdmin()
        {
            string servername = System.Net.Dns.GetHostName();//Oliga-2022-2
            string servername2 = Environment.MachineName;//OLIGA-2022-2
            //open apemadmin
            Application.LaunchAPEMAdmin();
            //expand node
            APEM.APEMAdminWindow.TreeView.GetNode("Console Root;Production Execution Administrator").Expand();
            APEM.APEMAdminWindow.TreeView.GetNode($"Console Root;Production Execution Administrator;{servername}").Expand();
            APEM.APEMAdminWindow.TreeView.Select($"Console Root;Production Execution Administrator;{servername};Extraction Service");
            APEM.APEMAdminWindow.TreeView.GetNode($"Console Root;Production Execution Administrator;{servername};Extraction Service").Expand();
            APEM.APEMAdminWindow.TreeView.Select($"Console Root;Production Execution Administrator;{servername};Extraction Service;Server");
            //check if extract running
            if (!APEM.APEMAdminWindow.ListView._STD_ListView.GetVisibleText().Contains("Stopped")) {
                Keyboard.KeyDown(Keyboard.Keys.Alt);
                Keyboard.PressKey(Keyboard.Keys.A);
                Keyboard.KeyUp(Keyboard.Keys.Alt);
                Keyboard.PressKey(Keyboard.Keys.S);
                Thread.Sleep(1000);
            }
            //open property
            Keyboard.KeyDown(Keyboard.Keys.Alt);
            Keyboard.PressKey(Keyboard.Keys.A);
            Keyboard.KeyUp(Keyboard.Keys.Alt);
            Keyboard.PressKey(Keyboard.Keys.P);
            Keyboard.PressKey(Keyboard.Keys.Enter);
            //set property
            APEM.APEMAdminWindow.ExtractorProperty.SetupButton.Click();
            //set db
            Wizard.DataLinkPropertiesDialog.TabControl.Select("Provider");
            Wizard.DataLinkPropertiesDialog.DBserver.Click();
            Wizard.DataLinkPropertiesDialog.next.Click();
            Thread.Sleep(2000);
            Wizard.DataLinkPropertiesDialog.servername.SetText("Localhost");
            Wizard.DataLinkPropertiesDialog.username.SetText(database);
            Wizard.DataLinkPropertiesDialog.password.SetText(password);
            Wizard.DataLinkPropertiesDialog.savingPWD.SetState(CheckedState.Checked);
            Thread.Sleep(1000);
            Wizard.DataLinkPropertiesDialog.DataBase.SetText(database);
            Wizard.DataLinkPropertiesDialog.TestConnection.Click();
            Wizard.DataLinkCheckDialog.OK.Click();
            Wizard.DataLinkPropertiesDialog.OK.Click();
            //test connection
            APEM.APEMAdminWindow.ExtractorProperty.TestConnectionButton.Click();
            if(APEM.PropertyDialog.StaticText.Text== "Test connection succeeded.")
            {
                APEM.PropertyDialog.OK.Click();
                Console.WriteLine("Test connection successful !");
            }
            else
            {
                Console.WriteLine("Test connection failed !");
            }
            //set data source and area
            APEM.APEMAdminWindow.ExtractorProperty.DataSource.Select(servername2);
            APEM.APEMAdminWindow.ExtractorProperty.DataArea.Select("WeighDispense");
            APEM.APEMAdminWindow.ExtractorProperty.OK.Click();
            //start extract
            APEM.APEMAdminWindow.TreeView.Select($"Console Root;Production Execution Administrator;{servername};Extraction Service;Server");
            Keyboard.KeyDown(Keyboard.Keys.Alt);
            Keyboard.PressKey(Keyboard.Keys.A);
            Keyboard.KeyUp(Keyboard.Keys.Alt);
            Keyboard.PressKey(Keyboard.Keys.S);
            Thread.Sleep(1000);
            //check extarct start
            if(APEM.APEMAdminWindow.ListView._STD_ListView.GetVisibleText().Contains("Processing Tables"))
            {
                Console.WriteLine("Start extraction server success!");
            }
            else
            {
                Console.WriteLine("Failed to start extraction server !");
            }
            APEM.APEMAdminWindow.Close();
        }
        
        public static void InitailAPRM()
        {
            APRM_Fuction.DropAprmDB();
            APRM_Fuction.WizardAprmDB();
            APRM_Fuction.WDImportAprmAdmin();
            //set BatchDetailDisplay area
            Application.LaunchBatchDetailDisplay();
            Batch_Fuction.setOptionData();
            APRM.BatchMainWindow.Close();
        }

        public static void FirstInitailAPRM()
        {
            //exit aprm db
            bool exits;
            exits = File.Exists(Base_Directory.APRMDir + @"\AspenBatch_dat1.mdf");
            if (!exits)
            {
                APRM_Fuction.WizardAprmDB();
            }
            APRM_Fuction.WDImportAprmAdmin();
            APRM_Fuction.ConfigAPEMAdmin();
        }

        public static void GMLAPRMConfig()
        {
            //exit aprm db?
            bool exits;
            exits = File.Exists(Base_Directory.APRMDir + @"\AspenBatch_dat1.mdf");
            if (!exits)
            {
                WizardAprmDB();
            }
            GMLImportAprmAdmin();
            //set BatchDetailDisplay area
            Application.LaunchBatchDetailDisplay();
            Batch_Fuction.setOptionData("Batch");
            APRM.BatchMainWindow.Close();
        }


        public static void StopService(string serviceName)
        {
            //service
            //string serviceName = "Tomcat9";
            int timeoutMilliseconds = 200000;
            ServiceController service = new ServiceController(serviceName);
            TimeSpan timeout;
            if (service.Status == ServiceControllerStatus.Running)
            {
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            }

        }

        public static void StartService(string serviceName)
        {
            //service
            //string serviceName = "Tomcat9";
            int timeoutMilliseconds = 200000;
            ServiceController service = new ServiceController(serviceName);
            TimeSpan timeout;
            if (service.Status == ServiceControllerStatus.Stopped)
            {
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }

        }



        
    }
}
