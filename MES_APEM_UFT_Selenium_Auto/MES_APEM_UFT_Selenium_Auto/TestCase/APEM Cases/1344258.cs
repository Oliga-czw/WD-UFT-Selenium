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
using System.Diagnostics;
using System.IO;
using MES_APEM_UFT_Selenium_Auto.Product.WD;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(1344258)]
        [Title("UC1334216_Users have 'Production Execution Designer' permission can only have BPL/RPL design permission")]
        [TestCategory(ProductArea.RecipeManagement)]
        [Priority(CasePriority.Low)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_1344258()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";

            string sourceName1 = Base_Directory.ProjectDir + @"Data\Input\AFWDB.mdb";
            string sourceName2 = Base_Directory.ProjectDir + @"Data\Input\AFWDB_1344258.mdb";

            try
            {
                LogStep(@"1. Repalce AFW DB");
                Process.Start("cmd.exe", "/c iisreset");
                Thread.Sleep(10000);
                string directoryPath = @"C:\Program Files (x86)\AspenTech\Local Security\Access97"+ @"\AFWDB.mdb";
                File.Copy(sourceName2, directoryPath, true);
                Thread.Sleep(10000);
                //start tomcat
                Base_Test.KillProcess("tomcat10");
                Thread.Sleep(10000);
                Base_Function.ResartServices(ServiceName.Tomcat);
                Thread.Sleep(60000);
                Base_Function.ResartServices(ServiceName.AFW);
                Thread.Sleep(10000);
                Base_Function.ResartServices(ServiceName.AFW);
                Thread.Sleep(10000);
                //start tomcat
                Base_Test.KillProcess("tomcat10");
                Thread.Sleep(60000);
                Base_Function.ResartServices(ServiceName.Tomcat);
                Thread.Sleep(100000);
                Base_Function.ResartServices(ServiceName.AFW);
                Thread.Sleep(60000);
                Base_Function.ResartServices(ServiceName.AFW);
                Thread.Sleep(60000);
                LogStep(@"2. login moc");
                Base_Test.LaunchApp(Base_Directory.MOCDir);
                SdkConfiguration config = new SdkConfiguration();
                SDK.Init(config);
                APEM.MocmainWindow.LogonInternalFrame.userNameEditor.SetText(UserName.qaone2);
                APEM.MocmainWindow.LogonInternalFrame.passwordEditor.SetSecure(PassWord.qaone2);
                APEM.MocmainWindow.LogonInternalFrame.loginbutton.ClickSignle();
                Thread.Sleep(2000);
                APEM.MocmainWindow.GetSnapshot(Resultpath + "Only_Design.PNG");
                Base_Assert.IsTrue(APEM.MocmainWindow.RPLDesign.IsEnabled);
                Base_Assert.IsTrue(APEM.MocmainWindow.BPLDesign.IsEnabled);
                Base_Assert.IsTrue(APEM.MocmainWindow.MasterRecipes.IsEnabled);
                Base_Assert.IsTrue(APEM.MocmainWindow.RPLVerify.IsEnabled);
                Base_Assert.IsFalse(APEM.MocmainWindow.Orders.IsEnabled);
                Base_Assert.IsFalse(APEM.MocmainWindow.Templates_moudle.IsEnabled);
                Base_Assert.IsFalse(APEM.MocmainWindow.Audit_moudle.IsEnabled);
                Base_Assert.IsFalse(APEM.MocmainWindow.Config_moudle.IsEnabled);
                APEM.MocmainWindow.Library.ActiveDocDesign.Select();
                Thread.Sleep(2000);
                Base_Assert.IsTrue(APEM.MocmainWindow.DocumentFrame.IsEnabled);
                APEM.MocmainWindow.Library.QueryDesign.Select();
                Thread.Sleep(2000);
                Base_Assert.IsTrue(APEM.MocmainWindow.SQLQueriesInterFrame.IsEnabled);
                
            }
            finally
            {
                LogStep(@"6.Restone AFWDB ");
                Process.Start("cmd.exe", "/c iisreset");
                string directoryPath = @"C:\Program Files (x86)\AspenTech\Local Security\Access97";
                Base_File.CopyFile(sourceName1, directoryPath, true);
                Thread.Sleep(10000);
                Base_Function.ResartServices(ServiceName.AFW);
                Thread.Sleep(10000);
                Base_Function.ResartServices(ServiceName.AFW);
                Thread.Sleep(10000);
                //start tomcat
                Base_Test.KillProcess("tomcat10");
                Thread.Sleep(10000);
                Base_Function.ResartServices(ServiceName.Tomcat);
                Thread.Sleep(60000);
            }

        }

    }
}