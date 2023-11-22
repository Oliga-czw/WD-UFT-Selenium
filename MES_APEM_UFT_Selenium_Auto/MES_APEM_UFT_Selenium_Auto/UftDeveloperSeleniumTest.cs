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
using System.Drawing;

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
            //APEM.MocmainWindow.RPLDesign.ClickSignle();
            //Thread.Sleep(5000);
            //APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row("HASDHSV").Click();
            //Thread.Sleep(2000);
            //APEM.MocmainWindow.RPLDesignInternalFrame.LoadDesigner_Button.ClickSignle();
            //Thread.Sleep(5000);
            //APEM.DesignEditorWindow.PFCDesignAppInternalFrame.Script.Click();
            ////copy
            //APEM.DesignEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject.Click();
            //APEM.DesignEditorWindow.CopyButton.ClickSignle();
            //if (APEM.LoseCopiedDialog.IsExist())
            //{
            //    APEM.LoseCopiedDialog.YesButton.Click();
            //}
            //Thread.Sleep(5000);
            //APEM.DesignEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject.DoubleClick();
            //Thread.Sleep(2000);
            //APEM.DesignEditorWindow.PFCDesignAppInternalFrame.FirstLink.Click();
            //Thread.Sleep(2000);
            //APEM.DesignEditorWindow.PasteButton.ClickSignle();
            //if (APEM.DesignEditorWindow.PasteRenamedDialog.IsExist())
            //{
            //    APEM.DesignEditorWindow.PasteRenamedDialog.Close();
            //}
            ////APEM.DesignEditorWindow.GetSnapshot(Resultpath + "PasteOnOperation.PNG");
            //Thread.Sleep(5000);
            //APEM.DesignEditorWindow.PFCDesignAppInternalFrame.OperationUiObject.DoubleClick();
            //Thread.Sleep(2000);
            //APEM.DesignEditorWindow.PFCDesignAppInternalFrame.LinkUiObject.Click();
            //Thread.Sleep(2000);
            //APEM.DesignEditorWindow.PasteButton.ClickSignle();
            //if (APEM.DesignEditorWindow.PasteRenamedDialog.IsExist())
            //{
            //    APEM.DesignEditorWindow.PasteRenamedDialog.Close();
            //}
            ////APEM.DesignEditorWindow.GetSnapshot(Resultpath + "PasteOnPhase.PNG");
            //APEM.DesignEditorWindow.PFCDesignAppInternalFrame.RPLDesignForm.Click();

            // Test1: failed
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.RPLDesignForm.Click();
            Thread.Sleep(2000);

            //Keyboard.KeyDown(Keyboard.Keys.LeftControl);

            // Test2:
            //Keyboards.Control.Press();

            var frame = APEM.DesignEditorWindow.PFCDesignAppInternalFrame;

            frame.LinkUiObject.Click();
            Thread.Sleep(500);
            Keyboards.Esc.Click();
            Thread.Sleep(2000);

            frame.LinkUiObject.Click();
            Thread.Sleep(500);

            Keyboards.LControlKey.Press();
            Thread.Sleep(500);

            //frame.LinkUiObject.Click();
            //Thread.Sleep(500);
            ////Keyboards.Esc.Click();

            //Keyboards.Control.Press();
            //Keyboard.KeyDown(Keyboard.Keys.LeftControl);
            //Thread.Sleep(2000);
            frame.PhaseUiObject1.Click();
            Thread.Sleep(2000);

            ////Keyboard.KeyUp(Keyboard.Keys.LeftControl);
            Keyboards.LControlKey.Release();

            //Point oper1 = new Point{ X = 350, Y = 143 };
            //Point oper2 = new Point { X = 1241, Y = 513 };
            //Mouse.ButtonDown(oper1);
            //Thread.Sleep(2000);
            //Mouse.Move(oper2);
            //Mouse.ButtonUp(oper1);
            // Mouse.DragAndDrop(oper1,oper2);
            //APEM.DesignEditorWindow.PFCDesignAppInternalFrame.RPLDesignForm.Click();
            //APEM.DesignEditorWindow.PFCDesignAppInternalFrame._UFT_InterFrame.MouseDrag(350, 143, 1241, 513, "LEFT");

        }





    }

}
