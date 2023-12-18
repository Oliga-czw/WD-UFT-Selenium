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
        [TestCaseID(915161)]
        [Title("UC822684_Soap1.2: SET_ORDER_DETAILS() API function can work as expected")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_915161()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string order = "Case915161";
            string RPL = "SIMPLE";


            Application.LaunchMocAndLogin();
            LogStep(@"1. import templete");
            //check bpl exit
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(RPL).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("SAMPLE.zip");
            }
            LogStep(@"2. edit path config");
            string oldfile = Base_Directory.ConfigDir + "path.m2r_cfg";
            string oldText = @"BATCH_21_SERVICE_USER = <DOMAIN>\\<USER>";
            string newText = $"BATCH_21_SERVICE_USER = {UserName.qaone1}";
            string oldText2 = @"BATCH_21_SERVICE_PASSWORD =";
            string newText2 = $"BATCH_21_SERVICE_PASSWORD = {PassWord.qaone1}";
            Base_Function.ReplaceTextInNewFile(oldfile, oldfile, oldText, newText);
            Base_Function.ReplaceTextInNewFile(oldfile, oldfile, oldText2, newText2);
            //codify all
            Base_Test.LaunchApp(Base_Directory.Codify_all);
            LogStep(@"3. create an order description like: Initial description ");
            MOC_Fuction.PlanFromRPL(RPL, order, false);

            //check order created
            APEM.MocmainWindow.OrderListInternalFrame.Search.SetText(order);
            APEM.MocmainWindow.OrderListInternalFrame.Filter_Button.Click();
            APEM.MocmainWindow.GetSnapshot(Resultpath + "order created.PNG");

            var lists = APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Columns("Status");
            Base_Assert.IsTrue(lists.Any(list => list.Contains("Planned")), "order status is finished");
            LogStep(@"4. import BP and Execute the BP");
            //check bpl exit
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row("BPL915161").Existing)
            {
                MOC_TemplatesFunction.Importtemplates("BPL915161.zip");
            }
            //Execute BP
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            APEM.MocmainWindow.BPLListInternalFrame.Refresh_Button.ClickSignle();
            APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row("BPL915161").Click();
            APEM.MocmainWindow.BPLListInternalFrame.LoadDesigner_Button.ClickSignle();
            APEM.DesignEditorWindow.ExecuteButton.ClickSignle();
            //add reason
            if (APEM.AuditReasonDialog.IsExist())
            {
                APEM.AuditReasonDialog.Reason.SendKeys("Execute");
                APEM.AuditReasonDialog.OK.Click();
            }
            APEM.DesignEditorWindow.ExecuteMainInternalFrame.SetDetailButton.ClickSignle();
            if (APEM.DesignEditorWindow.MessageInterFrame.message.AttachedText == "Success")
            {
                //log
                APEM.DesignEditorWindow.MessageInterFrame.OKButton.ClickSignle();
            }
            APEM.DesignEditorWindow.ExecuteMainInternalFrame.OKButton.Click();
            APEM.ExecutionFinishedDialog.OKButton.Click();
            APEM.DesignEditorWindow.Close();
            APEM.CloseDialog.YesButton.Click();
            Thread.Sleep(3000);
            LogStep(@"5. check order detail");
            APEM.MocmainWindow.OrderListInternalFrame.Refresh_Button.Click();
            APEM.MocmainWindow.GetSnapshot(Resultpath + "order description changed.PNG");

            var lists2 = APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Columns("Description");
            Base_Assert.IsTrue(lists2.Any(list => list.Contains("Changed description")), "order description is changed");


            APEM.ExitApplication();
        }

    }
}