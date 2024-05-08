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
        [TestCaseID(920999)]
        [Title("Inspired by customer defect 860899,861378,861369 -- Tranfer_focus() function, selected_index function and return_no action works in ApemMobile")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1200000)]



        [TestMethod]
        public void VSTS_920999()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string ordername = "ORDER920999";
            Application.LaunchMocAndLogin();
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row("BPL920999").Existing)
            {
                MOC_TemplatesFunction.Importtemplates("TEMP920999.zip");
            }
            APEM.MocmainWindow.Orders.ClickSignle();
            Thread.Sleep(2000);
            MOC_Fuction.CheckRowSelection();
            MOC_Fuction.PlanFromRPL("RPL920999", ordername);
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
            Mobile.OrderExecution_Page.tranfer2field4_input.SendKeys("hahh");
            Mobile.OrderExecution_Page.tranfer2field4_input.SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "transfer2field4.PNG");
            Console.WriteLine(Mobile.OrderExecution_Page.Field4.GetAttribute("class"));
            Base_Assert.IsTrue(Mobile.OrderExecution_Page.Field4.GetAttribute("class").Contains("mat-focused"));
            //select line3
            Mobile.OrderExecution_Page.Selectline3_button.Click();
            Thread.Sleep(2000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "selectLine3.PNG");
            Console.WriteLine(Mobile.OrderExecution_Page.TableRows[2].GetAttribute("class"));
            Base_Assert.IsTrue(Mobile.OrderExecution_Page.TableRows[2].GetAttribute("class").Contains("highlightRow"));
            //Enter any number except 33 to field 3
            Mobile.OrderExecution_Page.Field3_input.SendKeys("222");
            Mobile.OrderExecution_Page.Field3_input.SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "messageAppears.PNG");
            Base_Assert.AreEqual(Mobile.OrderExecution_Page.Execution_Message.Text(), "Only possible to exit with value: 33");
            Mobile.OrderExecution_Page.MessageOK_button.Click();
            //click selectline3
            Mobile.OrderExecution_Page.Selectline3_button.Click();
            Thread.Sleep(2000);
            Base_Assert.AreEqual(Mobile.OrderExecution_Page.Execution_Message.Text(), "Only possible to exit with value: 33");
            Mobile.OrderExecution_Page.MessageOK_button.Click();
            //modify field3
            Mobile.OrderExecution_Page.Field3_input.SendKeys(Keys.Control + "A");
            Mobile.OrderExecution_Page.Field3_input.SendKeys("33");
            Mobile.OrderExecution_Page.Field3_input.SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            Mobile.OrderExecution_Page.Selectline3_button.Click();
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "messageDisappears.PNG");
            Base_Assert.IsFalse(driver.is_element_exist("//p[text()='Only possible to exit with value: 33']"));
            
        }
    }
}