using System;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;
using WD_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Collections;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(29642)]
        [Title("Permission: post condition of permission")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_29642()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            //LogStep(@"1. Open AFW managerClient");
            //Application.LaunchAFW();

            //no view and import
            LogStep(@"1. grant user permision other than view and import permission and apply.");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.admin);
            driver.FindElement("//div[text()='Permissions']").Click();
            Thread.Sleep(5000);
            driver.FindElement("//select/option[@value='Production Execution Administrator']").Click();
            
            string[] permission_list1 = { "View", "Import" };
            Web_Fuction.permissionUpdate(Selenium_Driver._Selenium_Driver, permission_list1);
            
            driver.FindElement("//div[text()='Logoff']").Click();
            Thread.Sleep(2000);
            driver.FindElement("//input[@class='gwt-TextBox']").SendKeys("qae\\qaone1");
            driver.FindElement("//input[@class='gwt-PasswordTextBox']").SendKeys("Aspen111");
            driver.FindElement("//button[@class='Home_Login_Button']").Click();
            Thread.Sleep(1000);
            var tabList = driver.FindElements("//div[@class='Tab_Label']");
            ArrayList Head_tabList = new ArrayList();
            Head_tabList.Add("Home");
            Head_tabList.Add("Material");
            Head_tabList.Add("Equipment");
            Head_tabList.Add("Inventory");
            Head_tabList.Add("Order");
            Head_tabList.Add("Report");
            Head_tabList.Add("Administration");
            //string[] Head_tabList1 = new string[7] { "Home", "Material", "Equipment", "Inventory", "Order", "Report", "Administration" };
            foreach (var tab in tabList)
            {
                Base_Assert.IsTrue(Head_tabList.Contains(tab.Text));
                
            }
            Web_Fuction.RestorePermission(Selenium_Driver._Selenium_Driver);
            //2. user holding no "View" permision.
            LogStep(@"2. user holding no 'View' permision.");
            string[] permission_list2 = {"Material"};
            Web_Fuction.permissionUpdate(Selenium_Driver._Selenium_Driver, permission_list2);
            driver.FindElement("//div[text()='Logoff']").Click();
            Thread.Sleep(2000);
            driver.FindElement("//input[@class='gwt-TextBox']").SendKeys("qae\\qaone1");
            driver.FindElement("//input[@class='gwt-PasswordTextBox']").SendKeys("Aspen111");
            driver.FindElement("//button[@class='Home_Login_Button']").Click();
            Thread.Sleep(1000);
            var tabList2 = driver.FindElements("//div[@class='Tab_Label']");
            foreach (var tab in tabList2)
            {
                Base_Assert.IsTrue(tab.Text != "Material");
            }
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "no_material.PNG");
            Web_Fuction.RestorePermission(Selenium_Driver._Selenium_Driver);
            LogStep(@"3. user having no management and administration permission.");
            //Production Execution User
            driver.FindElement("//select/option[@value='Production Execution User']").Click();
            string[] permission_list3 = { "Materials", "Equipment", "Inventory", "Orders", "Reports", "Deviation Management", "Campaigns", "Administration" };
            foreach (var permission in permission_list3)
            {
                var inputXpath = "//label[text()='" + permission + "']/../input";
                if (permission == "Administration")
                {
                    inputXpath = "//label[text()='Permissions']/../input";
                }
                if (driver.FindElement(inputXpath).GetAttribute("checked") != null)
                {
                    driver.FindElement(inputXpath).Click();
                }
            }
            driver.FindElement("//button[text()='Apply']").Click();
            Thread.Sleep(2000);
            driver.FindElement("//button[text()='OK']").Click();
            driver.FindElement("//div[text()='Logoff']").Click();
            Thread.Sleep(2000);
            driver.FindElement("//input[@class='gwt-TextBox']").SendKeys("qae\\qaone2");
            driver.FindElement("//input[@class='gwt-PasswordTextBox']").SendKeys("Aspen111");
            driver.FindElement("//button[@class='Home_Login_Button']").Click();
            Thread.Sleep(1000);
            var tabList3 = driver.FindElements("//div[@class='Tab_Label']");
            foreach (var tab in tabList3)
            {
                Base_Assert.AreEqual(tab.Text,"Home");
            }
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "only_home.PNG");
            driver.FindElement("//div[text()='Logoff']").Click();
            Thread.Sleep(2000);
            driver.FindElement("//input[@class='gwt-TextBox']").SendKeys("qae\\qaone1");
            driver.FindElement("//input[@class='gwt-PasswordTextBox']").SendKeys("Aspen111");
            driver.FindElement("//button[@class='Home_Login_Button']").Click();
            Thread.Sleep(1000);
            LogStep(@"4. user holding no modify permission.");

            Web_Fuction.gotoTab(WDWebTab.admin);
            driver.FindElement("//div[text()='Permissions']").Click();
            Thread.Sleep(5000);
            driver.FindElement("//select/option[@value='Production Execution User']").Click();
            Web_Fuction.RestorePermission(Selenium_Driver._Selenium_Driver);
            string[] permission_list4 = { "modify"};
            Web_Fuction.permissionUpdate(Selenium_Driver._Selenium_Driver, permission_list4);

            driver.FindElement("//div[text()='Logoff']").Click();
            Thread.Sleep(2000);
            driver.FindElement("//input[@class='gwt-TextBox']").SendKeys("qae\\qaone2");
            driver.FindElement("//input[@class='gwt-PasswordTextBox']").SendKeys("Aspen111");
            driver.FindElement("//button[@class='Home_Login_Button']").Click();
            Thread.Sleep(1000);
            driver.FindElement("//div[text()='Equipment']").Click();
            driver.FindElements("//img[@class='gwt-Image Head_Blue_Style']")[5].Click();
            var inputList2 = driver.FindElements("//input[contains(@class,''WD_TextBox'')]");
            foreach (var inputElement in inputList2)
            {
                Assert.IsNotNull(inputElement.GetAttribute("disabled"));
            }
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "No_modify.PNG");
            driver.Close();
            LogStep(@"5. ADD a new role in AFW");
            
            Application.LaunchAFW();
            Thread.Sleep(6000);
            AFW_Fuction.addRole("qaone","qae\\qaone1");
            if (WD.AFWMainWindow.AFWSubWindow.TreeView.GetNode("Console Root;AFW Security Manager").IsExpanded)
            {
                WD.AFWMainWindow.AFWSubWindow.TreeView._STD_TreeView.Select("Console Root;AFW Security Manager;Roles");
            }
            else
            {
                WD.AFWMainWindow.AFWSubWindow.TreeView.GetNode("Console Root;AFW Security Manager").Expand();

                WD.AFWMainWindow.AFWSubWindow.TreeView._STD_TreeView.Select("Console Root;AFW Security Manager;Roles");
            }
            WD.AFWMainWindow.toolbar.PressButton("&Action");
            WD.AFWMainWindow.AFWAddRoleDialog.nameEditField.SetText("qaone");
            WD.AFWMainWindow.AFWAddRoleDialog.descriptionEditField.SetText("for test");
            WD.AFWMainWindow.AFWAddRoleDialog.OK.Click();
            WD.AFWMainWindow.GetSnapshot(Resultpath + "new_role.PNG");
            AFW_Fuction.closeAFW();

            Selenium_Driver driver1 = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver1);
            driver1.Wait();
            Web_Fuction.login();
            driver1.Wait();
            Web_Fuction.gotoTab(WDWebTab.admin);
            driver1.FindElement("//div[text()='Permissions']").Click();
            Thread.Sleep(5000);
            var role_list = driver1.FindElements("//select/option");
            ArrayList roleNameList = new ArrayList();
            foreach (var roleName in role_list)
            {
                roleNameList.Add(roleName.Text);
            }
            Assert.IsTrue(roleNameList.Contains("qaone"));
            driver.Close();
            Thread.Sleep(2000);
            LogStep(@"6. delete th added role in AFW");
            Application.LaunchAFW();
            Thread.Sleep(6000);
            //AFW_Fuction.addRole("qaone", "qae\\qaone1");
            if (WD.AFWMainWindow.AFWSubWindow.TreeView.GetNode("Console Root;AFW Security Manager").IsExpanded)
            {
                WD.AFWMainWindow.AFWSubWindow.TreeView._STD_TreeView.Select("Console Root;AFW Security Manager;Roles");
            }
            else
            {
                WD.AFWMainWindow.AFWSubWindow.TreeView.GetNode("Console Root;AFW Security Manager").Expand();

                WD.AFWMainWindow.AFWSubWindow.TreeView._STD_TreeView.Select("Console Root;AFW Security Manager;Roles");
            }
           
            var ListView = WD.AFWMainWindow.AFWSubWindow.ListView;
            ListView.Select("qaone");
            WD.AFWMainWindow.toolbar.PressButton("6");
            WD.AFWSecuredDialog.Yes.Click();
            WD.AFWMainWindow.GetSnapshot(Resultpath + "delete_role.PNG");
            AFW_Fuction.closeAFW();

            Selenium_Driver driver2 = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver2);
            driver2.Wait();
            Web_Fuction.login();
            driver2.Wait();
            Web_Fuction.gotoTab(WDWebTab.admin);
            driver2.FindElement("//div[text()='Permissions']").Click();
            Thread.Sleep(5000);
            var role_list1 = driver2.FindElements("//select/option");
            ArrayList roleNameList1 = new ArrayList();
            foreach (var roleName in role_list1)
            {
                roleNameList1.Add(roleName.Text);
            }
            Base_Assert.IsFalse(roleNameList1.Contains("qaone"));
            driver.Close();
            Thread.Sleep(2000);
            
        }


    }
}