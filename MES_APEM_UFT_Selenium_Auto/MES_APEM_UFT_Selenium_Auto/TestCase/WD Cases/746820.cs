using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using System.IO;
using MES_APEM_UFT_Selenium_Auto.Product.APRM;
using System.Text.RegularExpressions;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(746820)]
        [Title("The \"Start_Time\" and \"End_Time\" should show when define to true in extractor profile")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(2000000)]

        [TestMethod]
        public void VSTS_746820()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order1 = "test1";
            string order2 = "test2";
            string order3 = "test3";

            string dataAeBRS = Base_Directory.DataAeBRS + @"\AuditAndComplianceExtractor.Profile.xml";
            string XML1 = Base_Directory.InputDir + @"\TT.xml";
            string XML2 = Base_Directory.InputDir + @"\TF.xml";
            string XML3 = Base_Directory.InputDir + @"\FT.xml";

            //APRM 
            APRM_Fuction.InitailAPRMWD();
            try
            {
                Base_File.CopyFile(XML1, dataAeBRS);
                //restart AACM
                Base_Function.ResartServices("AtAuditAndComplianceExtractor");
                Base_Function.ResartServices("AtAuditAndComplianceServer");
                //config apemadmin
                APRM_Fuction.ConfigAPEMAdmin();
                LogStep(@"1. Open WD web and login");
                Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
                Web_Fuction.gotoWDWeb(driver);
                driver.Wait();
                Web_Fuction.login();
                driver.Wait();
                LogStep(@"2. Active order");
                Web_Fuction.gotoTab(WDWebTab.order);
                Web_Fuction.active_order(order1);
                Web_Fuction.active_order(order2);
                Web_Fuction.active_order(order3);
                driver.Close();
                LogStep(@"3. Open WD client and finish dispense");
                //TT
                Application.LaunchWDAndLogin();
                WD_Fuction.FinishOrder(order1);
                //Kitting
                WD_Fuction.OrderKitting(order1);
                WD_Fuction.Close();
                Thread.Sleep(60000);
                //check APRM batch
                Application.LaunchBatchDetailDisplay();
                Batch_Fuction.findBatch(order1);
                //wait for loading
                Thread.Sleep(40000);
                //X0125Accept :Begin_Source_Gross is 1000 and End_Source_Gross is 556
                APRM.BatchMainWindow.TreeView.Select("Batch");
                //wait for loading
                Thread.Sleep(5000);
                APRM.BatchMainWindow.GetSnapshot(Resultpath + "Start and End time.PNG");
                string text = APRM.BatchMainWindow.ListView._STD_ListView.GetVisibleText();
                Base_Assert.IsTrue(Regex.IsMatch(text, "Start.Time"), "Start time");
                Base_Assert.IsTrue(Regex.IsMatch(text, "End.Time"), "End time");
                APRM.BatchMainWindow.Close();
                //TF
                Base_File.CopyFile(XML2, dataAeBRS);
                //restart AACM
                Base_Function.ResartServices("AtAuditAndComplianceExtractor");
                Base_Function.ResartServices("AtAuditAndComplianceServer");
                //config apemadmin
                APRM_Fuction.ConfigAPEMAdmin();
                //WD
                Application.LaunchWDAndLogin();
                WD_Fuction.FinishOrder(order2);
                //Kitting
                WD_Fuction.OrderKitting(order2);
                WD_Fuction.Close();
                Thread.Sleep(60000);
                //check APRM batch
                Application.LaunchBatchDetailDisplay();
                Batch_Fuction.findBatch(order2);
                //wait for loading
                Thread.Sleep(40000);
                //X0125Accept :Begin_Source_Gross is 1000 and End_Source_Gross is 556
                APRM.BatchMainWindow.TreeView.Select("Batch");
                //wait for loading
                Thread.Sleep(5000);
                APRM.BatchMainWindow.GetSnapshot(Resultpath + "Start time.PNG");
                string text2 = APRM.BatchMainWindow.ListView._STD_ListView.GetVisibleText();
                Base_Assert.IsTrue(Regex.IsMatch(text2, "Start.Time"), "Start time");
                Base_Assert.IsFalse(Regex.IsMatch(text2, "End.Time"), "End time");
                APRM.BatchMainWindow.Close();
                //FT
                Base_File.CopyFile(XML3, dataAeBRS);
                //restart AACM
                Base_Function.ResartServices("AtAuditAndComplianceExtractor");
                Base_Function.ResartServices("AtAuditAndComplianceServer");
                //config apemadmin
                APRM_Fuction.ConfigAPEMAdmin();
                //WD
                Application.LaunchWDAndLogin();
                WD_Fuction.FinishOrder(order3);
                //Kitting
                WD_Fuction.OrderKitting(order3);
                WD_Fuction.Close();
                Thread.Sleep(60000);
                //check APRM batch
                Application.LaunchBatchDetailDisplay();
                Batch_Fuction.findBatch(order3);
                //wait for loading
                Thread.Sleep(40000);
                //X0125Accept :Begin_Source_Gross is 1000 and End_Source_Gross is 556
                APRM.BatchMainWindow.TreeView.Select("Batch");
                //wait for loading
                Thread.Sleep(5000);
                APRM.BatchMainWindow.GetSnapshot(Resultpath + "End time.PNG");
                string text3 = APRM.BatchMainWindow.ListView._STD_ListView.GetVisibleText();
                Base_Assert.IsFalse(Regex.IsMatch(text3, "Start.Time"), "Start time");
                Base_Assert.IsTrue(Regex.IsMatch(text3, "End.Time"), "End time");
                APRM.BatchMainWindow.Close();
            }
            finally
            {
                Base_File.CopyFile(XML1, dataAeBRS);
                //restart AACM
                Base_Function.ResartServices("AtAuditAndComplianceExtractor");
                Base_Function.ResartServices("AtAuditAndComplianceServer");
            }
        }


     
    }
}
