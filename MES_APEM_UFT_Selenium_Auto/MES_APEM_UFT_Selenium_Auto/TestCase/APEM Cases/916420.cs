using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System;
using HP.LFT.SDK;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;

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
            APEM.MocmainWindow.BPLListInternalFrame.AddBPL_Button.ClickSignle();
            Thread.Sleep(4000);
            APEM.MocmainWindow.BPLDataInternalFrame.BPLName.SendKeys("TESTBP");
            APEM.MocmainWindow.BPLDataInternalFrame.BPLDescription.SendKeys("for test");
            APEM.MocmainWindow.BPLDataInternalFrame.ConfirmChanges_Button.ClickSignle();
            if (APEM.MocmainWindow.AddReasonDialog.IsExist())
            {
                APEM.MocmainWindow.AddReasonDialog.Reason.SendKeys("for UFT test");
                APEM.MocmainWindow.AddReasonDialog.OK.Click();
            }
            Thread.Sleep(4000);
            APEM.MocmainWindow.BPLDataInternalFrame.TabbedPaneControl.Select("Basic Phases");
            Thread.Sleep(3000);
            APEM.MocmainWindow.BPLDataInternalFrame.AddBP_Button.ClickSignle();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLDataInternalFrame.NoEditor.SendKeys("1");
            Thread.Sleep(2000);
            Keyboard.PressKey(Keyboard.Keys.Enter);
            APEM.MocmainWindow.BPLDataInternalFrame.NoEditor.SendKeys("testBp");
            Thread.Sleep(2000);
            Keyboard.PressKey(Keyboard.Keys.Enter);
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLDataInternalFrame.NoEditor.SendKeys("for test");
            Thread.Sleep(2000);
            Keyboard.PressKey(Keyboard.Keys.Enter);
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLDataInternalFrame.WebCheckBox.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLDataInternalFrame.ConfirmChanges_Button.ClickSignle();
            Thread.Sleep(2000);
            APEM.MocmainWindow.AddReasonDialog.Reason.SendKeys("test");
            APEM.MocmainWindow.AddReasonDialog.OK.Click();
            APEM.MocmainWindow.BPLDataInternalFrame.CancelChanges_Button.ClickSignle();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLDataInternalFrame.LoadDesigner_Button.ClickSignle();
            Thread.Sleep(3000);
            MOC_Fuction.ImportCHKDesign("SOAPCALL01_1.CHK");
            APEM.DesignEditorWindow.ExecuteButton.ClickSignle();
            Thread.Sleep(8000);
            APEM.DesignEditorWindow.ExecuteMainInternalFrame.SOAP_CALL2_EX_Button.Click();
            Thread.Sleep(2000);
            var BPText = APEM.DesignEditorWindow.ExecuteMainInternalFrame.CheckField.Text;
            Console.WriteLine(BPText);
            Assert.IsTrue(BPText.Contains("中央电视台,央视高清电视,中国教育电视台"));
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "BPExecute.PNG");
            APEM.DesignEditorWindow.ExecuteMainInternalFrame.Cancel_Button.ClickSignle();
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.ConfirmationInternalFrame.YesButton.Click();
            Thread.Sleep(6000);
            APEM.ExecutionFinishedDialog.OKButton.Click();
            APEM.DesignEditorWindow.SaveButton.ClickSignle();
            APEM.DesignSavedDialog.OKButton.Click();
            Thread.Sleep(2000);
            MOC_Fuction.DesignEditorClose();
            APEM.MocmainWindow.BPLDataInternalFrame.TabbedPaneControl.Select("BPL Data");
            Thread.Sleep(3000);
            APEM.MocmainWindow.BPLDataInternalFrame.MakeUsable_Button.ClickSignle();
            if (APEM.MocmainWindow.AddReasonDialog.IsExist())
            {
                APEM.MocmainWindow.AddReasonDialog.Reason.SendKeys("for UFT test");
                APEM.MocmainWindow.AddReasonDialog.OK.Click();
            }
            Thread.Sleep(2000);
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            Thread.Sleep(5000);
            MOC_Fuction.AddRPL_OpenDesign("916420A", "TESTBP (Version 1)");
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
            MOC_Fuction.VerifyRPL("916420A");
            MOC_Fuction.CertifyRPL("916420A");
            Thread.Sleep(3000);
            MOC_Fuction.PlanFromRPL("916420A", "ORDRE916420");
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