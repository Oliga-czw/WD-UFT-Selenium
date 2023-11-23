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

            Application.LaunchMocAndLogin();
            //Thread.Sleep(5000);
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            Thread.Sleep(5000);
            APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row("PREPARERPL").Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.RPLDesignInternalFrame.VerifyButton.ClickSignle();
            Thread.Sleep(3000);
            APEM.VerifyDialog.NoButton.Click();
            Thread.Sleep(3000);
            //user&password
            APEM.MocmainWindow.ConfirmDialog.PasswordEditor.SendKeys("Aspen111");
            APEM.MocmainWindow.ConfirmDialog.Reason.SendKeys("Test");
            APEM.MocmainWindow.ConfirmDialog.OK.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.RPLDesignInternalFrame.CertifyButton.ClickSignle();
            Thread.Sleep(3000);
            APEM.CertifyDialog.YesButton.Click();
            Thread.Sleep(3000);
            //user & password
            APEM.MocmainWindow.ConfirmDialog.PasswordEditor.SendKeys("Aspen111");
            APEM.MocmainWindow.ConfirmDialog.Reason.SendKeys("Test");
            APEM.MocmainWindow.ConfirmDialog.OK.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.Orders.ClickSignle();
            APEM.MocmainWindow.OrderListInternalFrame.PlanFromRPL_Button.ClickSignle();
            Thread.Sleep(3000);
            APEM.MocmainWindow.OrderPlanDialog.CodeEditor.SendKeys("hulala");
            APEM.MocmainWindow.OrderPlanDialog.DescriptionEditor.SendKeys("test");
            APEM.MocmainWindow.OrderPlanDialog.RPLList.Select("PREPARERPL#1");
            APEM.MocmainWindow.OrderPlanDialog.POEditor.SendKeys("PO");
            APEM.MocmainWindow.OrderPlanDialog.POStepEditor.SendKeys("POStep");
            APEM.MocmainWindow.OrderPlanDialog.ArticleEditor.SendKeys("Article");
            APEM.MocmainWindow.OrderPlanDialog.BatchEditor.SendKeys("Batch");
            APEM.MocmainWindow.OrderPlanDialog.QuantityEditor.SendKeys("123.65");
            APEM.MocmainWindow.OrderPlanDialog.Quantity_unitEditor.SendKeys("kg");
            APEM.MocmainWindow.OrderPlanDialog.DateEditor.SendKeys("12/12/22, 3:23:00 AM");
            APEM.MocmainWindow.OrderPlanDialog.END_DateEditor.SendKeys("5/6/26, 10:23:34 PM");
            APEM.MocmainWindow.OrderPlanDialog.WorkcenterList.Select("ProcessCellLine2");
            Thread.Sleep(3000);
            APEM.MocmainWindow.OrderPlanDialog.Auto_ActivateCheckBox.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.OrderPlanDialog.OK.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.AddReasonDialog.Reason.SendKeys("for test");
            APEM.MocmainWindow.AddReasonDialog.OK.Click();
            Thread.Sleep(3000);
            
           

        }





    }

}
