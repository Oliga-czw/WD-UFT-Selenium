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
        [TestCaseID(927362)]
        [Title("UC822648_Soap1.2:New API dataDelivery() should work as expected")]
        [TestCategory(ProductArea.RecipeManagement)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(1000000)]

        [TestMethod]
        public void VSTS_927362()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string order = "Order927362";
            string BPL = "BPL927362";
            string RPL = "SIMPLE";

            Application.LaunchMocAndLogin();
            Thread.Sleep(5000);
            LogStep(@"1. import templete");
            //check bpl exit
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPL).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("CASE927362.zip");
            }
            LogStep(@"2. create ORDER");
            MOC_Fuction.PlanFromRPL(RPL, order, false);
            LogStep(@"3. start service and ip.21");
            try
            {
                string sqlplus_Server = @"C:\Program Files (x86)\AspenTech\InfoPlus.21\db21\code\sqlplus_server.exe";
                Base_Test.LaunchApp(sqlplus_Server);
                GML_Function.StartIP21();

                LogStep(@"5. edit script and execute");
                string path = Base_Directory.InputDir + "\\SQL_script\\";
                string file1 = path + "axis2 dataDelivery JSON script.txt";
                string file2 = path + "axis2 dataDelivery XML script.txt";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string newfile1 = desktop + "axis2 dataDelivery JSON script.txt";
                string newfile2 = desktop + "axis2 dataDelivery XML script.txt";
                string oldText = "MachineName";
                string newText = Environment.MachineName;
                Base_Function.ReplaceTextInNewFile(file1, newfile1, oldText, newText);
                Base_Function.ReplaceTextInNewFile(file2, newfile2, oldText, newText);
                //open SQLplus
                Application.LaunchSQLPlus();
                //OPEN file1
                SQLplus.SQLplusWindow.Toolbar.PressButton("2");
                Thread.Sleep(3000);
                //input filename
                SQLplus.SQLplusWindow.OpenFile_Dialog.FileName.SetText(newfile1);
                SQLplus.SQLplusWindow.OpenFile_Dialog.Open.Click();
                Thread.Sleep(3000);
                //execute
                SQLplus.SQLplusWindow.Toolbar.PressButton("15");
                Thread.Sleep(10000);
                //check result
                SQLplus.SQLplusWindow.GetSnapshot(Resultpath + "SQLPLUS JSON.PNG");
                string result1 = SQLplus.SQLplusWindow.ResultArea.Text;
                Base_Assert.IsTrue(result1.Contains("YES"), "Result return YES!");
                //check order description
                APEM.MocmainWindow.Orders.ClickSignle();
                MOC_Fuction.CheckRowSelection();
                APEM.MocmainWindow.OrderListInternalFrame.Search.SetText(order);
                APEM.MocmainWindow.OrderListInternalFrame.Filter_Button.Click();
                APEM.MocmainWindow.GetSnapshot(Resultpath + "MOC JSON.PNG");
                var lists1 = APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Columns("Description");
                Base_Assert.IsTrue(lists1.Contains("Change JSON"));
                //OPEN file2
                SQLplus.SQLplusWindow.Toolbar.PressButton("2");
                Thread.Sleep(3000);
                SQLplus.SQLplusWindow.OpenFile_Dialog.FileName.SetText(newfile2);
                SQLplus.SQLplusWindow.OpenFile_Dialog.Open.Click();
                Thread.Sleep(3000);
                SQLplus.SQLplusWindow.Toolbar.PressButton("15");
                Thread.Sleep(10000);
                SQLplus.SQLplusWindow.GetSnapshot(Resultpath + "SQLPLUS XML.PNG");
                string result2 = SQLplus.SQLplusWindow.ResultArea.Text;
                Base_Assert.IsTrue(result2.Contains("YES"), "Result return YES!");
                //check order description
                APEM.MocmainWindow.OrderListInternalFrame.Refresh_Button.Click();
                APEM.MocmainWindow.GetSnapshot(Resultpath + "MOC XML.PNG");
                var lists2 = APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Columns("Description");
                Base_Assert.IsTrue(lists2.Contains("Change XML"));
                APEM.ExitApplication();
            }
            finally
            {
                if (SQLplus.cmdWindow.IsExist())
                {
                    SQLplus.cmdWindow.Close();
                }
                GML_Function.StopIP21();
                if (SQLplus.SQLplusWindow.IsExist())
                {
                    SQLplus.SQLplusWindow.Close();
                }
            }

        }

    }
}