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
using System.Text.RegularExpressions;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(788897)]
        [Title("WAIT_MESSAGE works in MOC")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_788897()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string RPLName = "RPL788897";
            string OrderName = "Order788897";

            string Path = Base_Directory.ConfigDirx86 + "config.m2r_cfg";
            string DebugPath = "";
            string key = "SQL_DM,";
            string search = "DEBUG_KEYS = ";
            //Add debug key
            Base_Function.AddConfigKeyInRightPlace(Path, key, search);
            //codify all
            Base_Test.LaunchApp(Base_Directory.Codify_all);
            //restart tomcat
            Base_Test.KillProcess("tomcat10");
            Thread.Sleep(30000);
            Base_Function.ResartServices(ServiceName.Tomcat);
            Thread.Sleep(60000);
            try
            {
                Application.LaunchMocAndLogin();
                LogStep(@"1. import rpl");
                //check rpl exit
                APEM.MocmainWindow.RPLDesign.ClickSignle();
                if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName).Existing)
                {
                    MOC_TemplatesFunction.Importtemplates("CASE788897.zip");
                }
                LogStep(@"2. create orders");
                MOC_Fuction.PlanFromRPL(RPLName, OrderName);
                LogStep(@"3. Execute orders");
                APEM.MocmainWindow.WorkstationBP.Click();
                MOC_Fuction.CheckRowSelection();
                Thread.Sleep(2000);
                APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText(OrderName);
                APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
                APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
                APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
                Thread.Sleep(5000);
                APEM.PhaseExecWindow.ExecutionInternalFrame.LaunchThread_Button.Click();
                Thread.Sleep(5000);
                APEM.PhaseExecWindow.ExecutionInternalFrame.setvar_Button.Click();
                APEM.PhaseExecWindow.GetSnapshot(Resultpath + "MOCExecute.PNG");
                Thread.Sleep(2000);
                APEM.PhaseExecWindow.ExecutionInternalFrame.OK_Button.Click();
                Thread.Sleep(2000);
                APEM.PhaseExecWindow.MessageInternalFrame.OKButton.Click();
                APEM.PhaseExecWindow.ExecutionInternalFrame.Exit_Button.Click();
                Thread.Sleep(2000);
                APEM.ExitApplication();
                LogStep(@"3. check debug file");
                string directoryPath = Base_Directory.MOCDebugDir;
                string searchPattern = "*MOC*";
                string StartPattern = "EBR_SHARED_VARS.*START";
                string EndPattern = "EBR_SHARED_VARS.*End";

                var fileInfos = Directory.GetFiles(directoryPath, searchPattern)
                    .Select(file => new FileInfo(file))
                    .OrderByDescending(fileInfo => fileInfo.LastWriteTime)
                    .ToList();
                string FileName = fileInfos.First().FullName;
                string fileContent = File.ReadAllText(FileName);
                MatchCollection matches1 = Regex.Matches(fileContent, StartPattern);
                MatchCollection matches2 = Regex.Matches(fileContent, EndPattern);
                Base_Assert.IsTrue(matches1==matches2,"Start End number is same.");
            }
            finally
            {
                //restore data
                Base_Function.DeleteConfigKey(Path, key);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
                //restart tomcat
                Base_Test.KillProcess("tomcat10");
                Thread.Sleep(30000);
                Base_Function.ResartServices(ServiceName.Tomcat);
                Thread.Sleep(60000);
            }



        }

    }
}