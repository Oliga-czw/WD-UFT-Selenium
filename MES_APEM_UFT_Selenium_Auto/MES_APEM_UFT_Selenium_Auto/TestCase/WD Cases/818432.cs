using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(818432)]
        [Title("Inspired from customer defect 786934 - Manual Weighing second e-Signature - username field is locked out")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Created)]
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
            string net = "444.4";
            string xml = "14 aspen wd deviation_818432 bulk load.xml";


            LogStep(@"1. Open WD web and login");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            LogStep(@"2. Active order");
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            LogStep(@"3. Change deviation");
            WD_Fuction.Bulkload(xml);

            //old function to change deviation
            //List<ReadOnlyCollection<IWebElement>> table = new List<ReadOnlyCollection<IWebElement>>();
            //Selenium_WebElement deviation_table = Web.Administration_Page.deviation_table;
            //table = Web_Fuction.get_table(deviation_table);
            //int r = Web_Fuction.get_select_rowIndex(table, "Manual material identification");
            //int c = Web_Fuction.get_select_colIndex(table, "Double Signature");
            //table[r][c].Click();
            //Web_Fuction.administration_Apply("Apply Deviation Configuration Successful");

            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.Deviations.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "DeviationSetting.PNG");
            LogStep(@"4. Go to equipment and add a disconnection simulator");
            Web_Fuction.gotoTab(WDWebTab.equipment);
            Web_Fuction.select_scale("simulator");
            Web.Equipment_Page.scale_copy_row.Click();
            driver.Wait();
            Web.Equipment_Page.simultor_name.Clear();
            Web.Equipment_Page.simultor_name.SendKeys("simulator***manual");
            Web.Equipment_Page.simultor_status.select_option("Disconnected");
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
            WD.mainWindow.GetSnapshot(Resultpath + "same user error.PNG");
            WD.MessageDialog.OKButton.Click();
            WD.DeviationDialog.UserID.SetText(UserName.qaone2);
            WD.DeviationDialog.Password.SetText(PassWord.qaone2);
            WD.DeviationDialog.OK.Click();
            LogStep(@"7. FinishManualDispense");
            WD_Fuction.FinishManualDiapense(simulator, tare, net);
            driver.Close();
            WD_Fuction.Close();
        } 

    }
}
