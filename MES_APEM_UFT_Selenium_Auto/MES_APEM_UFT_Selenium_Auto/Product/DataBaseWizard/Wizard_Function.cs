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

namespace MES_APEM_UFT_Selenium_Auto.Product.DataBaseWizard
{
    class Wizard_Fuction
    {
        static string server = Environment.MachineName;
        static string user = DBInfo.Info["username"];
        static string password = DBInfo.Info["password"];
        public static void CreateApemDB(string ApemDBName)
        {
            Application.LaunchWizrd();
            Wizard.WizardWindow.next.Click();
            //select aprm
            Wizard.WizardWindow.lstAPEM.Click();
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
            ////select folder
            Wizard.WizardWindow.DBName.SetText(ApemDBName);
            if (!Directory.Exists(Base_Directory.DBDir))
            {
                Directory.CreateDirectory(Base_Directory.DBDir);
            }
            Wizard.WizardWindow.DBLocation.SetText(Base_Directory.DBDir);
            Wizard.WizardWindow.next.Click();
            Wizard.WizardWindow.next.Click();
            //enter batch psssword
            Wizard.WizardWindow.BatchEnterPassword.SetSecure(password);
            Wizard.WizardWindow.BatchVerifyPassword.SetSecure(password);
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
            Thread.Sleep(80000);
            //check install finish
            if (Wizard.WizardWindow.btnClose.IsEnabled)
            {
                Wizard.WizardWindow.btnClose.Click();
            }
            else
            {
                //wait another 50 seconds
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(5000);
                    if (Wizard.WizardWindow.btnClose.IsEnabled)
                    {
                        Wizard.WizardWindow.btnClose.Click();
                        break;
                    }
                }

            }
        }
    }    
}
