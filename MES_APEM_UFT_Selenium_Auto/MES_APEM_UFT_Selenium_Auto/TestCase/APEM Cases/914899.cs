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
        [TestCaseID(914899)]
        [Title("UC822684_Soap1.2 support Archive and Restore functions work well in APEM Administrator")]
        [TestCategory(ProductArea.RecipeManagement)]
        [Priority(CasePriority.Critical)]
        [TestCategory(CaseState.Created)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_914899()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string ordername = "SOAP_914899";
            //string ordername = "test1_914899";

            Application.LaunchMocAndLogin();
            Thread.Sleep(5000);
            LogStep(@"1. import templete");
            //check bpl exit
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row("SIMPLE").Existing)
            {
                MOC_TemplatesFunction.Importtemplates("SAMPLE.zip");
            }
            LogStep(@"2. create ORDER and cancell & archive");
            MOC_Fuction.PlanFromRPL("SIMPLE", ordername);
            //view all orders
            APEM.MocmainWindow.OrderListInternalFrame.Visible_Button.ClickSignle();
            APEM.MocmainWindow.RowsViewDialog.ViewAll.Click();
            APEM.MocmainWindow.RowsViewDialog.OK.Click();
            //search order
            APEM.MocmainWindow.OrderListInternalFrame.Search.SetText(ordername);
            APEM.MocmainWindow.OrderListInternalFrame.Filter_Button.Click();
            APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Row("Active").Click();
            //cancell
            APEM.MocmainWindow.OrderListInternalFrame.Cancel_Button.ClickSignle();
            APEM.MocmainWindow.CancelOrderDialog.YesButton.Click();
            MOC_Fuction.AddReason();
            //archive
            APEM.MocmainWindow.OrderListInternalFrame.Archive_Button.ClickSignle();
            APEM.MocmainWindow.ArchiveOrderDialog.YesButton.Click();
            MOC_Fuction.AddReason();
            APEM.MocmainWindow.GetSnapshot(Resultpath + "archive order.PNG");
            LogStep(@"3. open apem admin");
            string servername = System.Net.Dns.GetHostName();//Oliga-2022-2
            string servername2 = Environment.MachineName;//OLIGA-2022-2
            Application.LaunchAPEMAdmin();
            //expand node
            APEM.APEMAdminWindow.TreeView.GetNode("Console Root;Production Execution Administrator").Expand();
            APEM.APEMAdminWindow.TreeView.GetNode($"Console Root;Production Execution Administrator;{servername}").Expand();
            APEM.APEMAdminWindow.TreeView.Select($"Console Root;Production Execution Administrator;{servername};Archive and Restore");
            Thread.Sleep(3000);
            //connect
            action();
            Keyboard.PressKey(Keyboard.Keys.C);
            Thread.Sleep(3000);
            //create archive
            action();
            Keyboard.PressKey(Keyboard.Keys.C);
            APEM.APEMAdminWindow.CreateArchiveDialog.ArchiveName.SetText(ordername);
            APEM.APEMAdminWindow.CreateArchiveDialog.Comments.SetText("test");
            APEM.APEMAdminWindow.CreateArchiveDialog.OK.Click();
            Thread.Sleep(3000);
            //select data
            APEM.APEMAdminWindow.TreeView.GetNode($"Console Root;Production Execution Administrator;{servername};Archive and Restore").Expand();
            APEM.APEMAdminWindow.TreeView.Select($"Console Root;Production Execution Administrator;{servername};Archive and Restore;{ordername}");
            Thread.Sleep(5000);
            action();
            Keyboard.PressKey(Keyboard.Keys.S);
            APEM.APEMAdminWindow.SelectionConditionsDialog.OrderCode.SetText(ordername);
            APEM.APEMAdminWindow.SelectionConditionsDialog.OK.Click();
            APEM.APEMAdminWindow.Opensearchwarning.Yes.Click();
            APEM.APEMAdminWindow.Opensearchwarning.Yes.Click();
            LogStep(@"4. build/delete archive");
            //build/delete archive
            action();
            Keyboard.PressKey(Keyboard.Keys.B);//build
            //Keyboard.PressKey(Keyboard.Keys.D);//delete
            APEM.APEMAdminWindow.ArchiveBuiltDialog.DeleteAll.Click();
            APEM.APEMAdminWindow.ArchiveBuiltDialog.Comments.SendKeys("test");
            APEM.APEMAdminWindow.ArchiveBuiltDialog.OK.Click();
            Thread.Sleep(10000);
            APEM.APEMAdminWindow.GetSnapshot(Resultpath + "delete archive order in apem admin.PNG");
            Base_Assert.IsTrue(APEM.APEMAdminWindow.ListView._STD_ListView.GetVisibleText().Contains("DELET"), "order status");
            //check order delete in moc
            APEM.MocmainWindow.OrderListInternalFrame.Refresh_Button.Click();
            var lists1 = APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Columns("Status");
            APEM.MocmainWindow.GetSnapshot(Resultpath + "delete archive order in moc.PNG");
            Base_Assert.IsFalse(lists1.Any(list => list.Contains("Ext.Archived")));
            LogStep(@"5. restore archive");
            //restore archive
            APEM.APEMAdminWindow.SetActive();
            Thread.Sleep(3000);
            action();
            for (int i = 0; i < 3; i++)
            {
                Keyboard.PressKey(Keyboard.Keys.Down);
            }
            Keyboard.PressKey(Keyboard.Keys.Enter);//restore
            APEM.APEMAdminWindow.ArchiveRestoreDialog.Comments.SendKeys("test");
            APEM.APEMAdminWindow.ArchiveRestoreDialog.OK.Click();
            Thread.Sleep(10000);
            APEM.APEMAdminWindow.GetSnapshot(Resultpath + "resore archive order in apem admin.PNG");
            Base_Assert.IsTrue(APEM.APEMAdminWindow.ListView._STD_ListView.GetVisibleText().Contains("RESTO"), "order status");
            //check order delete in moc
            APEM.MocmainWindow.OrderListInternalFrame.Refresh_Button.Click();
            var lists2 = APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Columns("Status");
            APEM.MocmainWindow.GetSnapshot(Resultpath + "resore archive order in moc.PNG");
            Base_Assert.IsTrue(lists2.Any(list => list.Contains("Ext.Archived")));


            APEM.MocmainWindow.OrderListInternalFrame.Search.SetText("");
            APEM.MocmainWindow.OrderListInternalFrame.Filter_Button.Click();
            APEM.APEMAdminWindow.Close();
            APEM.ExitApplication();

        }
        public void action()
        {
            Keyboard.KeyDown(Keyboard.Keys.Alt);
            Keyboard.PressKey(Keyboard.Keys.A);
            Keyboard.KeyUp(Keyboard.Keys.Alt);
        }
    }
}