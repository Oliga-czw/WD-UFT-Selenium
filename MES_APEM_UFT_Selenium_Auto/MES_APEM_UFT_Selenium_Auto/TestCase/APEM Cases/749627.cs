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
using System.Windows.Forms;
using Application = MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary.Application;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(749627)]
        [Title("UC730136_New API function 'VIEW_DOC' design and execution in moc")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_749627()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            Application.LaunchMocAndLogin();
            string sourceName = Base_Directory.ProjectDir + "Data\\Input\\TestFile\\test.pdf";
            string directoryPath = @"C:\";
            Base_File.CopyFile(sourceName, directoryPath, true);
            LogStep(@"Create an active order from recipe");
            APEM.MocmainWindow.BPLDesign.Click();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row("BPL749627").Existing)
            {
                MOC_TemplatesFunction.Importtemplates("TEMP749627.zip");
            }
            MOC_Fuction.PlanFromRPL("RPL749627", "ORDER749627");
            APEM.MocmainWindow.WorkstationBP.ClickSignle();
            MOC_Fuction.CheckRowSelection();
            Thread.Sleep(3000);
            LogStep(@"Execute the order");
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText("ORDER749627");
            APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
            //Excution
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(3000);
            LogStep(@"click the ViewDOCURL Button");
            APEM.PhaseExecWindow.ExecutionInternalFrame.ViewDOCURL_Button.Click();
            //press enter
            Thread.Sleep(2000);
            Keyboard.KeyDown(Keyboard.Keys.Enter);
            Thread.Sleep(15000);
            APEM.MocmainWindow.MicrosoftEdgeWindow._STD_Window.Click();
            APEM.MocmainWindow.MicrosoftEdgeWindow.GetSnapshot(Resultpath + "ViewDOCUrl.PNG");
            Keyboard.KeyDown(Keyboard.Keys.Control);
            Keyboard.PressKey(Keyboard.Keys.A);
            Keyboard.KeyUp(Keyboard.Keys.Control);
            Keyboard.KeyDown(Keyboard.Keys.Control);
            Keyboard.PressKey(Keyboard.Keys.C);
            Keyboard.KeyUp(Keyboard.Keys.Control);
            string clipboardText = Clipboard.GetText();
            Console.WriteLine("剪切板中的内容为: " + clipboardText);
            Assert.IsTrue(clipboardText.Contains("This is a test PDF document."));
            //local pdf view
            LogStep(@"click the local pdf view Button");
            APEM.PhaseExecWindow.ExecutionInternalFrame.LocalPdfView_Button.Click();
            Thread.Sleep(3000);
            APEM.PhaseExecWindow.MessageInternalFrame.OKButton.Click();
            //press enter
            Thread.Sleep(2000);
            Keyboard.KeyDown(Keyboard.Keys.Enter);
            Thread.Sleep(15000);
            APEM.MocmainWindow.MicrosoftEdgeWindow._STD_Window.Click();
            APEM.MocmainWindow.MicrosoftEdgeWindow.GetSnapshot(Resultpath + "LocalPdfView.PNG");
            Keyboard.KeyDown(Keyboard.Keys.Control);
            Keyboard.PressKey(Keyboard.Keys.A);
            Keyboard.KeyUp(Keyboard.Keys.Control);
            Keyboard.KeyDown(Keyboard.Keys.Control);
            Keyboard.PressKey(Keyboard.Keys.C);
            Keyboard.KeyUp(Keyboard.Keys.Control);
            string LocalText = Clipboard.GetText();
            Console.WriteLine("剪切板中的内容为: " + LocalText);
            Assert.IsTrue(LocalText.Contains("This is a test PDF document."));
            //shared pdf view
            LogStep(@"click the shared pdf view Button");
            APEM.PhaseExecWindow.ExecutionInternalFrame.SharedUrlview_Button.Click();
            //press enter
            Thread.Sleep(2000);
            Keyboard.KeyDown(Keyboard.Keys.Enter);
            Thread.Sleep(15000);
            APEM.MocmainWindow.MicrosoftEdgeWindow._STD_Window.Click();
            APEM.MocmainWindow.MicrosoftEdgeWindow.GetSnapshot(Resultpath + "SharedPdfView.PNG");
            Keyboard.KeyDown(Keyboard.Keys.Control);
            Keyboard.PressKey(Keyboard.Keys.A);
            Keyboard.KeyUp(Keyboard.Keys.Control);
            Keyboard.KeyDown(Keyboard.Keys.Control);
            Keyboard.PressKey(Keyboard.Keys.C);
            Keyboard.KeyUp(Keyboard.Keys.Control);
            string sharedText = Clipboard.GetText();
            Console.WriteLine("剪切板中的内容为: " + sharedText);
            Assert.IsTrue(sharedText.Contains("Aspen Production Execution Manager"));
            //Error message
            LogStep(@"click the Error message Button");
            APEM.PhaseExecWindow.ExecutionInternalFrame.InvalidError_Button.Click();
            Thread.Sleep(4000);
            APEM.MocmainWindow.GetSnapshot(Resultpath + "InvalidError.PNG");
            var Errormessage = APEM.MocmainWindow.ErrorDialog.Lable.Text;
            Assert.IsTrue(Errormessage.Contains("The URL/URI for VIEW_DOC API is invalid,"));
            APEM.MocmainWindow.ErrorDialog.OKButton.Click();
            Thread.Sleep(2000);
            APEM.PhaseExecWindow.ExecutionInternalFrame.Cancel_Button.ClickSignle();
            Thread.Sleep(2000);
            APEM.PhaseExecWindow.ConfirmationInternalFrame.YesButton.Click();
            APEM.MocmainWindow.MicrosoftEdgeWindow.Close();
        }

    }
}