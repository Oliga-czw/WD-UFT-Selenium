using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;
using WD_UFT_Selenium_Auto.Product.WD;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(818432)]
        [Title("Test")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_818432()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test1";
            string material = WDMaterial.X0125;
            string method = WDMethod.Manual;
            string barcode = "X0125001";
            string simulator = "simulator***manual";
            string tare = "10";
            string net = "400";

            LogStep(@"1. Open WD web and login");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            LogStep(@"2. Go to admin and deviation");
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.Deviations.Click();
            LogStep(@"3. Change deviation");
            List<ReadOnlyCollection<IWebElement>> table = new List<ReadOnlyCollection<IWebElement>>();
            Selenium_WebElement deviation_table = Web.Administration_Page.deviation_table;
            table = Web_Fuction.get_table(deviation_table);
            int r = Web_Fuction.get_select_rowIndex(table, "Manual material identification");
            int c = Web_Fuction.get_select_colIndex(table, "Double Signature");
            table[r][c].Click();
            Web_Fuction.administration_Apply("Apply Deviation Configuration Successful");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "DeviationSetting.PNG");
            LogStep(@"4. Go to equipment and add a disconnection simulator");
            Web_Fuction.gotoTab(WDWebTab.equipment);
            Web_Fuction.select_scale("simulator");
            Web.Equipment_Page.scale_copy_row.Click();
            driver.Wait();
            Web.Equipment_Page.simultor_name.Clear();
            Web.Equipment_Page.simultor_name.SendKeys("simulator***manual");
            Web.Equipment_Page.simultor_status._Selenium_WebElement.FindElement(By.XPath("//option[text()='Disconnected']")).Click();
            Web.Equipment_Page.Apply.Click();
            LogStep(@"5. Open Wd client and select manual");
            Application.LaunchWDAndLogin();
            WD_Fuction.SelectOrderandMaterial(order, material);
            WD_Fuction.SelectMehod(method, barcode);
            Thread.Sleep(5000);
            LogStep(@"6. Check deviation");
            WD.mainWindow.GetSnapshot(Resultpath + "WDDeviation-1.PNG");
            WD.DeviationDialog.UserID.SetText(UserName.qaone1);
            WD.DeviationDialog.Password.SetText(PassWord.qaone1);
            WD.DeviationDialog.OK.Click();
            WD.mainWindow.GetSnapshot(Resultpath + "WDDeviation-2.PNG");
            WD.DeviationDialog.UserID.SetText(UserName.qaone1);
            WD.DeviationDialog.Password.SetText(PassWord.qaone1);
            WD.DeviationDialog.OK.Click();
            Base_Assert.AreEqual(WD.MessageDialog.Lable.Text, "User is the same as the previous one.");
            WD.MessageDialog.OKButton.Click();
            WD.DeviationDialog.UserID.SetText(UserName.qaone2);
            WD.DeviationDialog.Password.SetText(PassWord.qaone2);
            WD.DeviationDialog.OK.Click();
            LogStep(@"7. FinishManualDispense");
            WD_Fuction.FinishManualDiapense(simulator, tare, net);

        } 

    }
}
