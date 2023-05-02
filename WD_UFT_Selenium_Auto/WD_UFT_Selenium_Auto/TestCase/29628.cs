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
            driver.FindElement("//input[@name='Code_Code']").Click();
            driver.FindElement("//input[@name='Code_Code']").SendKeys("testStates");
            driver.FindElement("//button[text()='Apply']").Click();

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
            driver.FindElement("//input[@name='Code_Code']").Click();
            driver.FindElement("//input[@name='Code_Code']").SendKeys("current.cleaningDate:= NOW()");
            driver.FindElement("//select[@name='Code_OnYes']/ption[@values='Code_OnYes']").Click();
            driver.FindElement("//button[text()='Apply']").Click();

            // add an Evevt
            driver.FindElement("//div[text()='Transitions']").Click();
            Thread.Sleep(3000);
            var beforeAdded_events = driver.FindElements("//*[@id='WDView']/table/tbody/tr[3]/td/table/tbody/tr/td[2]/table/tbody/tr[2]/td/table/tbody/tr/td/table/tbody/tr/td/table/tbody/tr[@class !='List_Background_Color']");
            Assert.AreEqual(beforeAdded_events.Count(), 6);
            driver.FindElement("//a[text()='Add an Event']").Click();
            driver.FindElement("//input[@name='CleanRule_Event']").Click();
            driver.FindElement("//input[@name='CleanRule_Event']").SendKeys(Keys.Control + "A");
            driver.FindElement("//input[@name='CleanRule_Event']").SendKeys("testRules");
            driver.FindElement("//input[@name='CleanRule_Event']").SendKeys(Keys.Enter);
            driver.FindElement("//tr[@id='clicked_Row_Style']/td[5]//button[@class='gwt-Button']").Click();
            driver.FindElement("//select[@name='CleanRule_Action']/option[text()='testRules']").Click();
            driver.FindElement("//select[@name='CleanRule_State']/option[text()='testStates']").Click();
            //save event
            driver.FindElement("//button[text()='Save Rules']").Click();
            Thread.Sleep(2000);
            Assert.AreEqual(driver.FindElement("//div[@class='gwt-Label Alert_Label']").Text, "Clean rules saved successfully.");
            driver.FindElement("//button[@class='gwt-Button OkStyle']").Click();
            //refresh
            


            driver.FindElement("//div[text()='Effective date:']/../../td[2]//input").SendKeys("12/7/22, 10:32:36 PM");
            driver.FindElement("//button[text()='Commit Rules']").Click();
            Thread.Sleep(2000);

            // add a type
            driver.FindElement("//div[text()='Types']").Click();
            Thread.Sleep(3000);
            driver.FindElement("//a[text()='Add a Type']").Click();
            driver.FindElement("//input[@name='CleanRule_Type']").Click();
            driver.FindElement("//input[@name='CleanRule_Type']").SendKeys(Keys.Control + "A");
            driver.FindElement("//input[@name='CleanRule_Type']").SendKeys(Keys.Delete);
            driver.FindElement("//input[@name='CleanRule_Type']").SendKeys("testRules");
            driver.FindElement("//textarea[@name='Description']").Click();
            driver.FindElement("//textarea[@name='Description']").SendKeys("for test");
            driver.FindElement("//select[@name='CleanRule_Event']/option[text()='Full Clean']").Click();
            driver.FindElement("//textarea[@name='CleanRule_Instructions']").Click();
            driver.FindElement("//textarea[@name='Description']").SendKeys("for test");
            driver.FindElement("//button[text()='Apply']").Click();

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
            driver.FindElement("//button[@class='gwt-Button OkStyle']").Click();
            //test rules
            driver.FindElement("//button[text()='Test Rules...']").Click();
            Thread.Sleep(2000);
            driver.FindElement("//select/option[text()='Usable']").Click();//testRules
            driver.FindElement("//select/option[text()='testRules']").Click();
            driver.FindElement("//td[text()='ExpDate']/../td[3]/input").SendKeys("10/1/23, 10:00:56 PM");
            driver.FindElement("//button[text()='Test Transition']").Click();
            Thread.Sleep(2000);
            Assert.AreEqual(driver.FindElement("//div[text()='Final State:']/../../td[2]/div").Text, "Available");

            driver.FindElement("//button[text()='Close']").Click();
        }

  
    }
}