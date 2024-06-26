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
using Keys = OpenQA.Selenium.Keys;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(540082)]
        [Title("Inspired by customer defect 534604,539031 - refreshable label and API LIST_MESSAGE execute in BPL test mode")]
        [TestCategory(ProductArea.MOC)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1500000)]

        [TestMethod]
        public void VSTS_540082()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string RPLname = "RPL540082";
            string Ordername = "ORDER540082";
            string BPLname = "BPL540082_1";
            Library.BaseLibrary.Application.LaunchMocAndLogin();
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLname).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("TEMP540082.zip");
            }
            APEM.MocmainWindow.BPLDesign.Click();
            APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLname, "Name").Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLListInternalFrame.LoadDesigner_Button.Click();
            Thread.Sleep(2000);
            MOC_Fuction.ImportCHKDesign("Refreshable_BP.CHK");
            APEM.DesignEditorWindow.ExecuteButton.Click();
            Thread.Sleep(2000);
            MOC_Fuction.AddReason();
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "BPrefreshable.PNG");
            var time1 = APEM.DesignEditorWindow.ExecuteMainInternalFrame.RefreshableBP.AttachedText;
            Thread.Sleep(2000);
            var time2 = APEM.DesignEditorWindow.ExecuteMainInternalFrame.RefreshableBP.AttachedText;
            DateTime dateTime1 = DateTime.Parse(time1);
            DateTime dateTime2 = DateTime.Parse(time2);
            TimeSpan timeDifference = dateTime2.Subtract(dateTime1);
            Base_Assert.AreEqual(timeDifference.Seconds, 2);
            APEM.DesignEditorWindow.ExecuteMainInternalFrame.Cancel_Button.Click();
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.ConfirmationInternalFrame.YesButton.Click();
            APEM.ExecutionFinishedDialog.OKButton.Click();
            //List
            MOC_Fuction.ImportCHKDesign("WEB_BP.CHK");
            APEM.DesignEditorWindow.ExecuteButton.Click();
            Thread.Sleep(2000);
            MOC_Fuction.AddReason();
            Thread.Sleep(2000);
            APEM.ErrorDialog.OKButton.Click();
            Thread.Sleep(2000);
            APEM.InvalidImage.OKButton.Click();
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "List.PNG");
            //base_Assert();
            APEM.DesignEditorWindow.ExecuteMainInternalFrame.Cancel_Button.Click();
            APEM.ExecutionFinishedDialog.OKButton.Click();
            MOC_Fuction.DesignEditorClose();
            Thread.Sleep(2000);
            //create an order
            APEM.MocmainWindow.Orders.ClickSignle();
            Thread.Sleep(2000);
            MOC_Fuction.CheckRowSelection();
            APEM.MocmainWindow.OrderListInternalFrame.Search.SetText("");//filter order
            APEM.MocmainWindow.OrderListInternalFrame.Filter_Button.Click();
            Thread.Sleep(3000);
            MOC_Fuction.PlanFromRPL(RPLname, Ordername);
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            Mobile_Fuction.login();
            Mobile.OrderProcess_Page.OrderSearch.SendKeys(Ordername);
            Thread.Sleep(2000);
            //go to tracking page,execute the phase
            Mobile.OrderProcess_Page.GotoTracking.Click();
            driver.Wait(1000);
            Mobile.OrderTracking_Page.ExecutionButton.Click();
            Thread.Sleep(2000);
            Mobile.OrderExecution_Page.HU_field.SendKeys("234");
            Mobile.OrderExecution_Page.HU_field.SendKeys(Keys.Enter);
            Thread.Sleep(7000);
            Mobile.OrderExecution_Page.ResetHU_button.Click();
            Thread.Sleep(2000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "reset.PNG");
            //Console.WriteLine("hauhua:" + Mobile.OrderExecution_Page.HU_field.GetAttribute("value"));
            Base_Assert.AreEqual(Mobile.OrderExecution_Page.HU_field.GetAttribute("value"),"");
            Mobile.OrderExecution_Page.CancelButton.Click();
            Thread.Sleep(2000);
            Mobile.OrderExecution_Page.ConfirmYesButton.Click();

            driver.Close();



        }

    }
}