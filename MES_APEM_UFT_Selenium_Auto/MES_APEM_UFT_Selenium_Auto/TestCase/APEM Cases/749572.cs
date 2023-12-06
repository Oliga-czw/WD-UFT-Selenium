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
using System.Linq;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(749572)]
        [Title("UC730328_APEM Web Service ")]
        [TestCategory(ProductArea.RecipeManagement)]
        [Priority(CasePriority.Critical)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_749572()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string order = "Case749572";
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
            LogStep(@"2. start service and ip.21");
            string sqlplus_Server = @"C:\Program Files (x86)\AspenTech\InfoPlus.21\db21\code\sqlplus_server.exe";
            Base_Test.LaunchApp(sqlplus_Server);
            GML_Function.StartIP21();
            LogStep(@"3. edit script and execute");
            string path = Base_Directory.InputDir + "\\SQL_script\\";
            string oldfile = path + "Web service.txt";
            string newFile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Web service.txt";
            string oldText = "MachineName";
            string newText = Environment.MachineName;
            Base_Function.ReplaceTextInNewFile(oldfile, newFile, oldText, newText);
            //open SQLplus
            Application.LaunchSQLPlus();
            //OPEN file1
            SQLplus.SQLplusWindow.Toolbar.PressButton("2");
            //input filename
            SQLplus.SQLplusWindow.OpenFile_Dialog.FileName.SetText(newFile);
            SQLplus.SQLplusWindow.OpenFile_Dialog.Open.Click();
            //execute
            SQLplus.SQLplusWindow.Toolbar.PressButton("15");
            Thread.Sleep(10000);
            //check result
            SQLplus.SQLplusWindow.GetSnapshot(Resultpath + "sql plus.PNG");
            string result = SQLplus.SQLplusWindow.ResultArea.Text;
            string exepect = @"CreateOrder: 1
OrderState: PLAN
OrderState: ACTIVE
OrderState: FINISH
result: 0
Script has completed!";
            Base_Assert.IsTrue(SQLplus.SQLplusWindow.ResultArea.Text.Contains(exepect), "Result return!");

            //check order status 
            APEM.MocmainWindow.Orders.ClickSignle();
            APEM.MocmainWindow.OrderListInternalFrame.Search.SetText(order);
            APEM.MocmainWindow.OrderListInternalFrame.Filter_Button.Click();
            APEM.MocmainWindow.GetSnapshot(Resultpath + "order status.PNG");

            var lists = APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Columns("Status");
            Base_Assert.IsTrue(lists.Any(list => list.Contains("Finished")), "order status is finished");


            APEM.ExitApplication();
            SQLplus.cmdWindow.Close();
            GML_Function.StopIP21();
            SQLplus.SQLplusWindow.Close();
        }

    }
}