using System;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;
using WD_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Text;
using OpenQA.Selenium;
using System.Linq;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {

        [TestCaseID(29628)]
        [Title("Administration Cleaning Rules:cleaning rules-transitions")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_29628()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.admin);
            driver.FindElement("//div[text()='Cleaning Rules']").Click();
            Thread.Sleep(3000);
            //add a state
            driver.FindElement("//div[text()='States']").Click();
            Thread.Sleep(3000);
            driver.FindElement("//a[text()='Add a State']").Click();
            Thread.Sleep(3000);
            driver.FindElement("//input[@name='CleanRule_State']").Click();
            driver.FindElement("//input[@name='CleanRule_State']").SendKeys(Keys.Control + "A");
            driver.FindElement("//input[@name='CleanRule_State']").SendKeys(Keys.Delete);
            driver.FindElement("//input[@name='CleanRule_State']").SendKeys("testStates");
            driver.FindElement("//textarea[@name='Description']").Click();
            driver.FindElement("//textarea[@name='Description']").SendKeys("for test");
            var code = driver.FindElement("//input[@name='Code_Code']");
            driver.execute_script("arguments[0].scrollIntoView();", code);
            code.Click();
            code.SendKeys("testStates");
            driver.FindElement("//button[text()='Apply']").Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "state.PNG");
            Thread.Sleep(3000);
            // add an Evevt
            Web.Administration_Page.Transitions.Click();
            Thread.Sleep(3000);
            Web.CleanRules_Page.Add_Event.Click();
            Web.CleanRules_Page.cleanRules_Event.Click();
            Web.CleanRules_Page.cleanRules_Event.SendKeys(Keys.Control + "A");
            Web.CleanRules_Page.cleanRules_Event.SendKeys("testEvent");
            Web.CleanRules_Page.cleanRules_Event.SendKeys(Keys.Enter);
            Web.CleanRules_Page.Edit_Button.Click();
            Web.CleanRules_Page.Use_Action.Click();
            Web.CleanRules_Page.Usable_state.Click();
            Web.CleanRules_Page.Dialog_OK.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "event.PNG");
            // add a type
            driver.FindElement("//div[text()='Types']").Click();
            Thread.Sleep(3000);
            driver.FindElement("//a[text()='Add a Type']").Click();
            driver.FindElement("//input[@name='CleanRule_Type']").Click();
            driver.FindElement("//input[@name='CleanRule_Type']").SendKeys(Keys.Control + "A");
            driver.FindElement("//input[@name='CleanRule_Type']").SendKeys(Keys.Delete);
            driver.FindElement("//input[@name='CleanRule_Type']").SendKeys("testType");
            driver.FindElement("//textarea[@name='Description']").Click();
            driver.FindElement("//textarea[@name='Description']").SendKeys("for test");
            driver.FindElement("//select[@name='CleanRule_Event']/option[text()='Full Clean']").Click();
            driver.FindElement("//textarea[@name='CleanRule_Instructions']").Click();
            driver.FindElement("//textarea[@name='Description']").SendKeys("for test");
            driver.FindElement("//button[text()='Apply']").Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "type.PNG");
            //add an Action
            driver.FindElement("//div[text()='Actions']").Click();
            Thread.Sleep(3000);
            driver.FindElement("//a[text()='Add an Action']").Click();
            Thread.Sleep(3000);
            driver.FindElement("//input[@name='CleanRule_Action']").Click();
            driver.FindElement("//input[@name='CleanRule_Action']").SendKeys(Keys.Control + "A");
            driver.FindElement("//input[@name='CleanRule_Action']").SendKeys(Keys.Delete);
            driver.FindElement("//input[@name='CleanRule_Action']").SendKeys("testRulesAction");
            driver.FindElement("//textarea[@name='CleanRule_Comment']").Click();
            driver.FindElement("//textarea[@name='CleanRule_Comment']").SendKeys("for test");
            
            var Action_Code = driver.FindElement("//select[@name='Code_OnYes']/../../td[1]");
            driver.execute_script("arguments[0].scrollIntoView();", Action_Code);
            Action_Code.Click();
            var ActionCode = driver.FindElement("//input[@name='Code_Code']");
            ActionCode.SendKeys("current.cleaningDate:= NOW()");
            ActionCode.SendKeys(Keys.Tab);
           // driver.FindElement("//select[@name='Code_OnYes']").Click();
            driver.FindElement("//select[@name='Code_OnYes']/option[text()='Yes']").Click();
            driver.FindElement("//button[text()='Apply']").Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "action.PNG");
            //save rules
            driver.FindElement("//button[text()='Save Rules']").Click();
            Thread.Sleep(2000);
            Assert.AreEqual(driver.FindElement("//div[@class='gwt-Label Alert_Label']").Text, "Clean rules saved successfully.");
            driver.FindElement("//button[@class='gwt-Button OkStyle']").Click();
            // commit rules
            driver.FindElement("//div[text()='Effective date:']/../../td[2]//input").SendKeys("12/3/23, 10:32:36 PM");
            driver.FindElement("//button[text()='Commit Rules']").Click();
            Thread.Sleep(2000);
            Assert.AreEqual(driver.FindElement("//div[@class='gwt-Label Alert_Label']").Text, "Clean rules committed successfully.");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "commit.PNG");
            driver.FindElement("//button[@class='gwt-Button OkStyle']").Click();
            //test rules
            driver.FindElement("//button[text()='Test Rules...']").Click();
            Thread.Sleep(2000);
            driver.FindElement("//div[text()='Initial State:']/../../td[2]/select/option[text()='Usable']").Click();//testRules  Initial State:
            driver.FindElement("//div[text()='Event:']/../../td[2]/select/option[text()='testEvent']").Click();
            //driver.FindElement("//td[text()='ExpDate']/../td[3]/input").SendKeys("10/1/23, 10:00:56 PM");
            driver.FindElement("//button[text()='Test Transition']").Click();
            Thread.Sleep(2000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "test_Rules.PNG");
            Base_Assert.AreEqual(driver.FindElement("//div[text()='Final State:']/../../td[2]/div").Text, "Clean");

            driver.FindElement("//button[text()='Close']").Click();
        }

  
    }
}