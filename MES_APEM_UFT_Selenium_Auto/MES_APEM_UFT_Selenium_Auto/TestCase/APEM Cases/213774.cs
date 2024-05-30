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
using System.Drawing.Imaging;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(213774)]
        [Title("CQ00290611: Active documents can print expression values well")]
        [TestCategory(ProductArea.MOC)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_213774()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string DOCname = "DOC213774";
            Library.BaseLibrary.Application.LaunchMocAndLogin();
            APEM.MocmainWindow.Library.ActiveDocDesign.Select();
            if (!APEM.MocmainWindow.DocumentFrame.ListTable.Row(DOCname).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("CASE213774.zip");
            }
            APEM.MocmainWindow.DocumentFrame.ListTable.Row(DOCname, "Name").Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.DocumentFrame.Execute_Button.Click();
            Thread.Sleep(5000);
            Base_Function.CaptureScreenToFile(Resultpath + "DOCExecute.PNG", ImageFormat.Png);
            Keyboard.KeyDown(Keyboard.Keys.Control);
            Keyboard.PressKey(Keyboard.Keys.A);
            Keyboard.KeyUp(Keyboard.Keys.Control);
            Keyboard.KeyDown(Keyboard.Keys.Control);
            Keyboard.PressKey(Keyboard.Keys.C);
            Keyboard.KeyUp(Keyboard.Keys.Control);
            string clipboardText = Clipboard.GetText();
            Console.WriteLine("剪切板中的内容为: " + clipboardText);
            Console.WriteLine(clipboardText);
            Assert.IsTrue(clipboardText.Contains("Test"));
        }

    }
}