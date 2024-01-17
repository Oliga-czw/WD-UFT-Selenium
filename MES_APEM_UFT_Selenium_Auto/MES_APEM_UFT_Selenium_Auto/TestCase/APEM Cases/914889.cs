using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System;
using HP.LFT.SDK;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using System.IO;
using MES_APEM_UFT_Selenium_Auto.Product.SQLplus;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(914889)]
        [Title("UC822684_Soap1.2 call for SQLPlus")]
        [TestCategory(ProductArea.RecipeManagement)]
        [Priority(CasePriority.Critical)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_914889()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string order1 = "SqlPlusA1";
            string order2 = "SqlPlusA2";
            string RPL = "SIMPLE";

            Application.LaunchMocAndLogin();
            Thread.Sleep(5000);
            LogStep(@"1. import templete");
            //check bpl exit
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(RPL).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("SAMPLE.zip");
            }
            LogStep(@"2. create ORDER");
            MOC_Fuction.PlanFromRPL(RPL, order1, false);
            MOC_Fuction.PlanFromRPL(RPL, order2, false);
            //check order created
            APEM.MocmainWindow.GetSnapshot(Resultpath+"order created.PNG");
            LogStep(@"3. check axis2 jar in folder");
            string directoryPath = @"C:\Program Files\Common Files\AspenTech Shared\Tomcat9.0.27\webapps\AeBRSserver\WEB-INF\lib";
            string searchPattern = "axis2*.jar";
            string[] files = Directory.GetFiles(directoryPath, searchPattern);
            Base_Assert.IsTrue(files.Length > 0, "axis2 exist");
            LogStep(@"4. start service and ip.21");
            string sqlplus_Server = @"C:\Program Files (x86)\AspenTech\InfoPlus.21\db21\code\sqlplus_server.exe";
            Base_Test.LaunchApp(sqlplus_Server);
            GML_Function.StartIP21();
            LogStep(@"5. edit script and execute");
            string path = Base_Directory.InputDir + "\\SQL_script\\";
            string file1 = path + "axis2 basi script0.txt";
            string file2 = path + "axis2 basi script1.txt";
            string file3 = path + "axis2 AFW script0.txt";
            string file4 = path + "axis2 AFW script1.txt";
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string newfile1 = desktop + "axis2 basi script0.txt";
            string newfile2 = desktop + "axis2 basi script1.txt";
            string newfile3 = desktop + "axis2 AFW script0.txt";
            string newfile4 = desktop + "axis2 AFW script1.txt";
            string oldText = "MachineName";
            string newText = Environment.MachineName;
            Base_Function.ReplaceTextInNewFile(file1, newfile1, oldText, newText);
            Base_Function.ReplaceTextInNewFile(file2, newfile2, oldText, newText);
            Base_Function.ReplaceTextInNewFile(file3, newfile3, oldText, newText);
            Base_Function.ReplaceTextInNewFile(file4, newfile4, oldText, newText);
            //open SQLplus
            Application.LaunchSQLPlus();
            //OPEN file1
            SQLplus.SQLplusWindow.Toolbar.PressButton("2");
            //input filename
            SQLplus.SQLplusWindow.OpenFile_Dialog.FileName.SetText(newfile1);
            SQLplus.SQLplusWindow.OpenFile_Dialog.Open.Click();
            //execute
            SQLplus.SQLplusWindow.Toolbar.PressButton("15");
            Thread.Sleep(10000);
            //check result
            SQLplus.SQLplusWindow.GetSnapshot(Resultpath + "basi0.PNG");
            string result1 = SQLplus.SQLplusWindow.ResultArea.Text;
            Base_Assert.IsTrue(result1.Contains("0"),"Result return 0!");
            //OPEN file2
            SQLplus.SQLplusWindow.Toolbar.PressButton("2");
            SQLplus.SQLplusWindow.OpenFile_Dialog.FileName.SetText(newfile2);
            SQLplus.SQLplusWindow.OpenFile_Dialog.Open.Click();
            SQLplus.SQLplusWindow.Toolbar.PressButton("15");
            Thread.Sleep(10000);
            SQLplus.SQLplusWindow.GetSnapshot(Resultpath + "basi1.PNG");
            string result2 = SQLplus.SQLplusWindow.ResultArea.Text;
            Base_Assert.IsTrue(result2.Contains("1"), "Result return 1!");
            //check order1 delete
            var lists1 = APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Columns("Code");
            Base_Assert.IsFalse(lists1.Contains(order1));
            //OPEN file3
            SQLplus.SQLplusWindow.Toolbar.PressButton("2");
            SQLplus.SQLplusWindow.OpenFile_Dialog.FileName.SetText(newfile3);
            SQLplus.SQLplusWindow.OpenFile_Dialog.Open.Click();
            SQLplus.SQLplusWindow.Toolbar.PressButton("15");
            Thread.Sleep(1000);
            SQLplus.SQLplusWindow.GetSnapshot(Resultpath + "AFW0.PNG");
            string result3 = SQLplus.SQLplusWindow.ResultArea.Text;
            Base_Assert.IsTrue(result3.Contains("0"), "Result return 0!");
            //OPEN file4
            SQLplus.SQLplusWindow.Toolbar.PressButton("2");
            SQLplus.SQLplusWindow.OpenFile_Dialog.FileName.SetText(newfile4);
            SQLplus.SQLplusWindow.OpenFile_Dialog.Open.Click();
            SQLplus.SQLplusWindow.Toolbar.PressButton("15");
            Thread.Sleep(10000);
            SQLplus.SQLplusWindow.GetSnapshot(Resultpath + "AFW1.PNG");
            string result4 = SQLplus.SQLplusWindow.ResultArea.Text;
            Base_Assert.IsTrue(result4.Contains("1"), "Result return 1!");
            //check order deleted
            APEM.MocmainWindow.OrderListInternalFrame.Refresh_Button.Click();
            APEM.MocmainWindow.GetSnapshot(Resultpath + "order deleted.PNG");
            var lists2 = APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Columns("Code");
            Base_Assert.IsFalse(lists2.Contains(order2));
            APEM.ExitApplication();
            SQLplus.cmdWindow.Close();
            GML_Function.StopIP21();
            SQLplus.SQLplusWindow.Close();
        }

    }
}