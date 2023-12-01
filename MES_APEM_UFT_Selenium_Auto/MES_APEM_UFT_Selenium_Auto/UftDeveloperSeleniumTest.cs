using HP.LFT.SDK;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System.Windows.Forms;
using System;
using HP.LFT.SDK.UIAPro;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;

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




        }





    }

}
