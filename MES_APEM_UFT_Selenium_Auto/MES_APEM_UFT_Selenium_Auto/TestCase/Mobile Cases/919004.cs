using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System;
using OpenQA.Selenium;
using HP.LFT.SDK;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Application = MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary.Application;
using System.Linq;
using Keys = OpenQA.Selenium.Keys;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class Mobile_TestCase
    {
        [TestCaseID(919004)]
        [Title("Inspired by customer defect 860899,861378,861369 -- Tranfer_focus() function, selected_index function and return_no action works in ApemMobile")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1200000)]



        [TestMethod]
        public void VSTS_919004()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string ordername = "ORDER919004";
            Application.LaunchMocAndLogin();
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row("BPL919004").Existing)
            {
                MOC_TemplatesFunction.Importtemplates("TEMP919004.zip");
            }
            APEM.MocmainWindow.Orders.ClickSignle();
            Thread.Sleep(2000);
            MOC_Fuction.CheckRowSelection();
            MOC_Fuction.PlanFromRPL("RPL919004", ordername);
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            driver.Wait();
            Mobile_Fuction.login();
            Thread.Sleep(5000);
            Mobile.OrderProcess_Page.OrderSearch.SendKeys(ordername);
            Thread.Sleep(5000);
            //go to tracking page
            Mobile.OrderProcess_Page.GotoTracking.Click();
            Thread.Sleep(1000);
            Mobile.OrderTracking_Page.ExecutionButton.Click();
            Thread.Sleep(10000);
           
            Mobile.OrderExecution_Page.Field.SendKeys(Keys.Control + "A");
            Mobile.OrderExecution_Page.Field.SendKeys("B1");
            Mobile.OrderExecution_Page.Field.SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            Mobile.OrderExecution_Page.OKButton.Click();
            Thread.Sleep(15000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "ErrorDisappears.PNG");
            Base_Assert.IsTrue(Mobile.OrderExecution_Page.message_label.isEnable());
            Mobile.OrderExecution_Page.OKButton.Click();
            Thread.Sleep(15000);
            int no = 0;
            int i = 0;
            foreach (IWebElement head in Mobile.OrderTracking_Page.OrderPhaseTableHeads)
            {
                if (head.Text == "Status")
                {
                    no = i;
                }
                i++;
            }
            Base_Assert.AreEqual(Mobile.OrderTracking_Page.OrderPhaseTableRows[0].FindElements(By.TagName("td"))[no].Text,"Finished");
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Finished.PNG");
            
        }
    }
}