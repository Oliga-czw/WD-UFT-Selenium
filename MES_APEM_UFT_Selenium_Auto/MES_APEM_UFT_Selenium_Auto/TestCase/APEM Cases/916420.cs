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

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(916420)]
        [Title("UC822684_Soap1.2:SOAP_CALL2_EX() API function should work as expected")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_916420()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            Application.LaunchMocAndLogin();
            Thread.Sleep(5000);
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row("BPL916420").Existing)
            {
                MOC_TemplatesFunction.Importtemplates("BPL916420.zip");
            }
            APEM.MocmainWindow.BPLListInternalFrame.Refresh_Button.ClickSignle();
            APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row("BPL916420").Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLListInternalFrame.LoadDesigner_Button.ClickSignle();
            Thread.Sleep(3000);
            APEM.MocmainWindow.ReadOnly_Dialog.OKButton.Click();
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.ExecuteButton.ClickSignle();
            Thread.Sleep(8000);
            APEM.DesignEditorWindow.ExecuteMainInternalFrame.SOAP_CALL2_EX_Button.Click();
            Thread.Sleep(2000);
            var BPText = APEM.DesignEditorWindow.ExecuteMainInternalFrame.CheckField.Text;
            Console.WriteLine(BPText);
            Assert.IsTrue(BPText.Contains("中央电视台,央视高清电视,中国教育电视台"));
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "BPExecute.PNG");
            APEM.DesignEditorWindow.ExecuteMainInternalFrame._UFT_InterFrame.Close();
            Thread.Sleep(3000);
            APEM.MocmainWindow.ExeCancelDialog.YesButton.Click();
            Thread.Sleep(2000);
            MOC_Fuction.DesignEditorClose();
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            Thread.Sleep(5000);
            MOC_Fuction.AddRPL_OpenDesign("RPL916420", "BPL916420 (Version 1)");
            APEM.DesignEditorWindow.UnitProcedure._UFT_CheckBox.Click();
            Thread.Sleep(8000);
            Base_Function.MouseClick(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            Thread.Sleep(3000);
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject.DoubleClick();
            Thread.Sleep(8000);
            APEM.DesignEditorWindow.Operation._UFT_CheckBox.Click();
            Thread.Sleep(8000);
            Base_Function.MouseClick(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            Thread.Sleep(3000);
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.OperationUiObject.DoubleClick();
            Thread.Sleep(5000);
            APEM.DesignEditorWindow.TabbedPaneControl.Select(1);
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.First_Phase.Click();
            Thread.Sleep(8000);
            Base_Function.MouseClick(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            Thread.Sleep(3000);
            APEM.DesignEditorWindow.SaveButton.ClickSignle();
            APEM.DesignSavedDialog.OKButton.Click();
            MOC_Fuction.DesignEditorClose();
            MOC_Fuction.VerifyRPL("RPL916420");
            MOC_Fuction.CertifyRPL("RPL916420");
            Thread.Sleep(3000);
            MOC_Fuction.PlanFromRPL("RPL916420", "ORDRE916420");
            APEM.MocmainWindow.WorkstationBP.ClickSignle();
            Thread.Sleep(3000);
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText("ORDRE916420");
            APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(10000);
            APEM.PhaseExecWindow.ExecutionInternalFrame.SOAP_CALL2_EX_Button.Click();
            Thread.Sleep(2000);
            var mocText = APEM.PhaseExecWindow.ExecutionInternalFrame.CheckField.Text;
            Console.WriteLine(mocText);
            Assert.IsTrue(mocText.Contains("中央电视台,央视高清电视,中国教育电视台"));
            APEM.PhaseExecWindow.GetSnapshot(Resultpath + "MOCExecute.PNG");
            APEM.PhaseExecWindow.ExecutionInternalFrame.Cancel_Button.ClickSignle();
            Thread.Sleep(2000);
            APEM.PhaseExecWindow.ConfirmationInternalFrame.YesButton.Click();
            Thread.Sleep(6000);
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            driver.Wait();
            Thread.Sleep(5000);
            Mobile_Fuction.login();
            driver.Wait();
            Thread.Sleep(20000);
            Mobile.OrderProcess_Page.OrderSearch.SendKeys("ORDRE916420");
            Thread.Sleep(5000);
            Mobile.OrderProcess_Page.GotoTracking.Click();
            Thread.Sleep(5000);
            Mobile.OrderTracking_Page.ExecutionButton.Click();
            Thread.Sleep(10000);
            Mobile.OrderExecution_Page.SOAP_CALL2_EXButton.Click();
            Thread.Sleep(50000);
            var mobileText = Mobile.OrderExecution_Page.MainField1.GetAttribute("value");
            Assert.IsTrue(mobileText.Contains("中央电视台,央视高清电视,中国教育电视台"));
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "MobileExecute.PNG");
            Mobile.OrderExecution_Page.CancelButton.Click();
            Thread.Sleep(2000);
            Mobile.OrderExecution_Page.ConfirmYesButton.Click();





        }

    }
}