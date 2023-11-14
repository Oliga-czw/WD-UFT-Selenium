using HP.LFT.SDK;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System.Windows.Forms;
using System;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using Application = MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary.Application;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;
using OpenQA.Selenium;

namespace MES_APEM_UFT_Selenium_Auto
{
    [TestClass]
    public class UftDeveloperSeleniumTest
    {

        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void TestMethod1()
        {
            SdkConfiguration config = new SdkConfiguration();
            SDK.Init(config);
            Thread.Sleep(3000);

            //Application.LaunchMocAndLogin();
            //Thread.Sleep(5000);
            //APEM.MocmainWindow.BPLDesign.ClickSignle();
            //APEM.MocmainWindow.BPLListInternalFrame.AddBPL_Button.ClickSignle();
            //Thread.Sleep(4000);
            //APEM.MocmainWindow.BPLDataInternalFrame.BPLName.SendKeys("TESTBP");
            //APEM.MocmainWindow.BPLDataInternalFrame.BPLDescription.SendKeys("for test");
            //APEM.MocmainWindow.BPLDataInternalFrame.ConfirmChanges_Button.ClickSignle();
            //if (APEM.MocmainWindow.AddReasonDialog.IsExist())
            //{
            //    APEM.MocmainWindow.AddReasonDialog.Reason.SendKeys("for UFT test");
            //    APEM.MocmainWindow.AddReasonDialog.OK.Click();
            //}
            //Thread.Sleep(4000);
            //APEM.MocmainWindow.BPLDataInternalFrame.TabbedPaneControl.Select("Basic Phases");
            //Thread.Sleep(3000);
            //APEM.MocmainWindow.BPLDataInternalFrame.AddBP_Button.ClickSignle();
            //Thread.Sleep(2000);
            // APEM.MocmainWindow.BPLDataInternalFrame.NoEditor.SendKeys("1");
            //Thread.Sleep(2000);
            //Keyboard.PressKey(Keyboard.Keys.Enter);
            //APEM.MocmainWindow.BPLDataInternalFrame.NoEditor.SendKeys("testBp");
            //Thread.Sleep(2000);
            //Keyboard.PressKey(Keyboard.Keys.Enter);
            //Thread.Sleep(2000);
            //APEM.MocmainWindow.BPLDataInternalFrame.NoEditor.SendKeys("for test");
            //Thread.Sleep(2000);
            //Keyboard.PressKey(Keyboard.Keys.Enter);
            //Thread.Sleep(2000);
            //APEM.MocmainWindow.BPLDataInternalFrame.WebCheckBox.Click();
            //Thread.Sleep(2000);
            //APEM.MocmainWindow.BPLDataInternalFrame.ConfirmChanges_Button.ClickSignle();
            //Thread.Sleep(2000);
            //APEM.MocmainWindow.AddReasonDialog.Reason.SendKeys("test");
            //APEM.MocmainWindow.AddReasonDialog.OK.Click();
            //APEM.MocmainWindow.BPLDataInternalFrame.CancelChanges_Button.ClickSignle();
            //Thread.Sleep(2000);
            //APEM.MocmainWindow.BPLDataInternalFrame.LoadDesigner_Button.ClickSignle();
            //Thread.Sleep(3000);
            //MOC_Fuction.ImportCHKDesign("SOAPCALL01_1.CHK");
            //APEM.DesignEditorWindow.ExecuteButton.ClickSignle();
            //Thread.Sleep(8000);
            //APEM.DesignEditorWindow.ExecuteMainInternalFrame.SOAP_CALL2_EX_Button.Click();
            //Thread.Sleep(2000);
            //var testText = APEM.DesignEditorWindow.ExecuteMainInternalFrame.CheckField.Text;
            //Console.WriteLine(testText);
            //Assert.IsTrue(testText.Contains("中央电视台,央视高清电视,中国教育电视台"));
            ////APEM.DesignEditorWindow.GetSnapshot(Resultpath + "BPExecute.PNG");
            //APEM.DesignEditorWindow.ExecuteMainInternalFrame.Cancel_Button.ClickSignle();
            //Thread.Sleep(2000);
            //APEM.DesignEditorWindow.ConfirmationInternalFrame.YesButton.Click();
            //Thread.Sleep(6000);
            //APEM.ExecutionFinishedDialog.OKButton.Click();
            //APEM.DesignEditorWindow.SaveButton.ClickSignle();
            //APEM.DesignSavedDialog.OKButton.Click();
            //Thread.Sleep(2000);
            //MOC_Fuction.DesignEditorClose();
            //APEM.MocmainWindow.BPLDataInternalFrame.TabbedPaneControl.Select("BPL Data");
            //Thread.Sleep(3000);
            //APEM.MocmainWindow.BPLDataInternalFrame.MakeUsable_Button.ClickSignle();
            //if (APEM.MocmainWindow.AddReasonDialog.IsExist())
            //{
            //    APEM.MocmainWindow.AddReasonDialog.Reason.SendKeys("for UFT test");
            //    APEM.MocmainWindow.AddReasonDialog.OK.Click();
            //}
            //Thread.Sleep(2000);
            //APEM.MocmainWindow.RPLDesign.ClickSignle();
            //Thread.Sleep(5000);
            //MOC_Fuction.AddRPL_OpenDesign("916420A", "TESTBP (Version 1)");
            //APEM.DesignEditorWindow.UnitProcedure._UFT_CheckBox.Click();
            //Thread.Sleep(8000);
            //Base_Function.MouseClick(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            //Thread.Sleep(3000);
            //APEM.DesignEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject.DoubleClick();
            //Thread.Sleep(8000);
            //APEM.DesignEditorWindow.Operation._UFT_CheckBox.Click();
            //Thread.Sleep(8000);
            //Base_Function.MouseClick(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            //Thread.Sleep(3000);
            //APEM.DesignEditorWindow.PFCDesignAppInternalFrame.OperationUiObject.DoubleClick();
            //Thread.Sleep(5000);
            //APEM.DesignEditorWindow.TabbedPaneControl.Select(1);
            //Thread.Sleep(2000);
            //APEM.DesignEditorWindow.First_Phase.Click();
            //Thread.Sleep(8000);
            //Base_Function.MouseClick(APEM.DesignEditorWindow.PFCDesignAppInternalFrame.ControlLinkUiObject._UFT_UiObject.AbsoluteLocation);
            //Thread.Sleep(3000);
            //APEM.DesignEditorWindow.SaveButton.ClickSignle();
            //APEM.DesignSavedDialog.OKButton.Click();
            //MOC_Fuction.DesignEditorClose();
            //APEM.MocmainWindow.RPLManagementInternalFrame.VerifyButton.ClickSignle();
            //Thread.Sleep(3000);
            //APEM.VerifyDialog.YesButton.Click();
            //Thread.Sleep(3000);
            //APEM.MocmainWindow.OrderPlanDialog.CodeEditor.SendKeys("ORDRE916420");
            //APEM.MocmainWindow.OrderPlanDialog.DescriptionEditor.SendKeys("test for 916420");
            //APEM.MocmainWindow.OrderPlanDialog.POEditor.SendKeys("PO");
            //APEM.MocmainWindow.OrderPlanDialog.POStepEditor.SendKeys("POStep");
            //APEM.MocmainWindow.OrderPlanDialog.ArticleEditor.SendKeys("Article");
            //APEM.MocmainWindow.OrderPlanDialog.BatchEditor.SendKeys("Batch");
            //APEM.MocmainWindow.OrderPlanDialog.QuantityEditor.SendKeys("123.65");
            //APEM.MocmainWindow.OrderPlanDialog.Quantity_unitEditor.SendKeys("kg");
            //APEM.MocmainWindow.OrderPlanDialog.DateEditor.SendKeys("12/12/22, 3:23:00 AM");
            //APEM.MocmainWindow.OrderPlanDialog.END_DateEditor.SendKeys("5/6/26, 10:23:34 PM");
            //APEM.MocmainWindow.OrderPlanDialog.WorkcenterList.Select("ProcessCellLine2");
            //Thread.Sleep(3000);
            //APEM.MocmainWindow.OrderPlanDialog.Auto_ActivateCheckBox.Click();
            //Thread.Sleep(3000);
            //APEM.MocmainWindow.OrderPlanDialog.OK.Click();
            //Thread.Sleep(3000);
            //APEM.MocmainWindow.AddReasonDialog.Reason.SendKeys("for test");
            //APEM.MocmainWindow.AddReasonDialog.OK.Click();
            //Thread.Sleep(3000);
            //APEM.MocmainWindow.WorkstationBP.ClickSignle();
            //Thread.Sleep(3000);
            //APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText("ORDRE916420");
            //APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
            //APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            //APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            //Thread.Sleep(10000);
            //APEM.PhaseExecWindow.ExecutionInternalFrame.SOAP_CALL2_EX_Button.Click();
            //Thread.Sleep(2000);
            //var testText = APEM.PhaseExecWindow.ExecutionInternalFrame.CheckField.Text;
            //Console.WriteLine(testText);
            //Assert.IsTrue(testText.Contains("中央电视台,央视高清电视,中国教育电视台"));
            //APEM.PhaseExecWindow.GetSnapshot(Resultpath + "MOCExecute.PNG");
            //APEM.PhaseExecWindow.ExecutionInternalFrame.Cancel_Button.ClickSignle();
            //Thread.Sleep(2000);
            //APEM.PhaseExecWindow.ConfirmationInternalFrame.YesButton.Click();
            //Thread.Sleep(6000);
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            driver.Wait();
            Thread.Sleep(5000);
            Mobile_Fuction.login();
            driver.Wait();
            driver.FindElement(By.XPath,"//div[contains(text(),'X_ORDER')]/../..//td/a/mat-icon").Click();







            //SendKeys.SendWait("{Tab}");
            //Keyboard.PressKey(Keyboard.Keys.Enter);
            //Thread.Sleep(3000);
            //APEM.MocmainWindow.BPLDataInternalFrame.NoEditor.SendKeys("for test");
            //SendKeys.SendWait("{Enter}");
            //Keyboard.PressKey(Keyboard.Keys.Enter);



            //APEM.MocmainWindow.BPLDataInternalFrame.MakeUsable_Button.ClickSignle();
            //if (APEM.MocmainWindow.AddReasonDialog.IsExist())
            //{
            //    APEM.MocmainWindow.AddReasonDialog.Reason.SendKeys("for UFT test");
            //    APEM.MocmainWindow.AddReasonDialog.OK.Click();
            //}
        }





    }

}
