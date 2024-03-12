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

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(962481)]
        [Title("WAIT_MESSAGE works in MOC")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_962481()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string RPLName = "RPL962481";
            string OrderName = "Order962481";

            Application.LaunchMocAndLogin();
            LogStep(@"1. import rpl");
            //check rpl exit
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("Case962481.zip");
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
            Stopwatch stopwatch = new Stopwatch();
            APEM.PhaseExecWindow.WaitMessageInterFrame._UFT_InterFrame.WaitUntilExists();
            stopwatch.Start();
            //check Wait message("Test") appears for about 5 seconds.
            APEM.PhaseExecWindow.GetSnapshot(Resultpath + "Wait message.PNG");
            Base_Assert.IsTrue(APEM.PhaseExecWindow.WaitMessageInterFrame.IsExist(), "Wait message exits");
            Base_Assert.AreEqual("TEST", APEM.PhaseExecWindow.WaitMessageInterFrame.Label.Text, "Wait message text");
            //check the message disappears. And the main execution screen displays.
            APEM.PhaseExecWindow.ExecutionInternalFrame._UFT_InterFrame.WaitUntilExists();
            stopwatch.Stop();
            APEM.PhaseExecWindow.GetSnapshot(Resultpath + "MOC Execute screen.PNG");
            Base_Assert.IsTrue(Math.Round(((double)stopwatch.ElapsedMilliseconds) / 1000).Equals(5), "Wait message exits 5 seconds");
            Base_Assert.IsFalse(APEM.PhaseExecWindow.WaitMessageInterFrame.IsExist(), "Wait message disappears");
            Base_Assert.IsTrue(APEM.PhaseExecWindow.ExecutionInternalFrame.IsExist(), "main execution screen exits");
            APEM.PhaseExecWindow.ExecutionInternalFrame.OK_Button.Click();
            Thread.Sleep(3000);
            APEM.ExitApplication();

            

        }

    }
}