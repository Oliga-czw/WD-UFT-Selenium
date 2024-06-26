using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(91525)]
        [Title("booth cleaning: material dispensing_Material Dispensing")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Low)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        //defect1376618
        //[TestMethod]
        public void VSTS_91525()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID+"-";
            string order = "test2";
            string material = WDMaterial.X0125;
            string method = WDMethod.Net;
            string barcode = "X0125001";
            string tare = "15";
            string net = "459.4";


            LogStep(@"1. Check clean rules");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.CleaningRules.Click();
            Web.Administration_Page.Types.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Clean rules-Types-Setting.PNG");
            LogStep(@"2. Active order");
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            LogStep(@"3. Open WD client and do booth clean");
            Application.LaunchWDAndLogin();
            WD.mainWindow.HomeInternalFrame.BoothCleaning.Click();
            WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
            //get execute time
            DateTime execute_time = DateTime.Now;
            //check go to home page
            Base_Assert.IsTrue(WD.mainWindow.HomeInternalFrame.IsEnabled);
            LogStep(@"4. do an material dispensing");
            WD.mainWindow.HomeInternalFrame.MaterialDispensing.Click();
            LogStep(@"5. select the material");
            WD.mainWindow.Material_SelectionInternalFrame.materialTable.Row(material).Click();
            var Required_value = WD.mainWindow.Material_SelectionInternalFrame.materialTable._UFT_Table.GetCell(0, "Clean Required").Value;
            WD.mainWindow.Material_SelectionInternalFrame.next.Click();
            if (Required_value.ToString() == "Yes")
            {
                WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
            }
            WD_Fuction.SelectMehod(method, barcode);
            WD_Fuction.FinishNetDiapense(tare,net);
            LogStep(@"6. launch BoothCleaning again,check the Current Status and Last dispensed fields");
            //back to home pagee
            WD.mainWindow.Material_SelectionInternalFrame.HomeButton.Click();
            WD.mainWindow.HomeInternalFrame.BoothCleaning.Click();
            WD.mainWindow.GetSnapshot(Resultpath + "Update information with last time entered.PNG");
            string Status1 = WD.mainWindow.BoothCleanInternalFrame.Status._UFT_Label.Text;
            string PreviousClean1 = WD.mainWindow.BoothCleanInternalFrame.PreviousClean._UFT_Label.Text;
            string Material1 = WD.mainWindow.BoothCleanInternalFrame.Material._UFT_Label.Text;
            string Order1 = WD.mainWindow.BoothCleanInternalFrame.Order._UFT_Label.Text;
            string Product1 = WD.mainWindow.BoothCleanInternalFrame.Product._UFT_Label.Text;

            //string[] data1 = new string[] { Status1, PreviousClean1, Material1, Order1, Product1 };
            //string[] data_actual = new string[] { Status1, Material1, Order1, Product1 };
            //string[] data_expect = new string[] {"Clean for X0125" , "X0125   X0125 Description,order , "1902 25mM HEPS, 100mM NaCI, pH 8.00" };
            Base_Assert.AreEqual("Clean for X0125", Status1, "Update information with last time entered");
            Base_Assert.AreEqual("X0125   X0125 Description", Material1, "Update information with last time entered");
            Base_Assert.AreEqual(order, Order1, "Update information with last time entered");
            Base_Assert.AreEqual("1902 25mM HEPS, 100mM NaCI, pH 8.00", Product1, "Update information with last time entered");
            //select a cleaning type again, but click Home
            WD.mainWindow.BoothCleanInternalFrame.HomeButton.Click();
            //check go to home page
            Base_Assert.IsTrue(WD.mainWindow.HomeInternalFrame.IsEnabled);
            LogStep(@"7. launch BoothCleaning again,check the Current Status and Last dispensed fields");
            WD.mainWindow.HomeInternalFrame.BoothCleaning.Click();
            WD.mainWindow.GetSnapshot(Resultpath + "no information updated.PNG");
            string Status2 = WD.mainWindow.BoothCleanInternalFrame.Status._UFT_Label.Text;
            string PreviousClean2 = WD.mainWindow.BoothCleanInternalFrame.PreviousClean._UFT_Label.Text;
            string Material2 = WD.mainWindow.BoothCleanInternalFrame.Material._UFT_Label.Text;
            string Order2 = WD.mainWindow.BoothCleanInternalFrame.Order._UFT_Label.Text;
            string Product2 = WD.mainWindow.BoothCleanInternalFrame.Product._UFT_Label.Text;
            //string[] data2 = new string[] { Status2, PreviousClean2, Material2, Order2, Product2 };
            Base_Assert.AreEqual(Status2, Status1, "Update information with last time entered");
            Base_Assert.AreEqual(Material2, Material1, "Update information with last time entered");
            Base_Assert.AreEqual(Order2, Order1, "Update information with last time entered");
            Base_Assert.AreEqual(Product2, Product1, "Update information with last time entered");
            Base_Assert.AreEqual(PreviousClean2, PreviousClean1, "Update information with last time entered");
            //back home
            WD.mainWindow.BoothCleanInternalFrame.HomeButton.Click();
            LogStep(@"8. Change booth status to 'In use'.");
            Web_Fuction.gotoTab(WDWebTab.equipment);
            Web_Fuction.edit_booth("booth1");
            Web.Equipment_Page.booth_status.select_option("In Use");
            Web.Equipment_Page.Apply.Click();
            //check booth clean error pops up 
            WD.mainWindow.HomeInternalFrame.BoothCleaning.Click();
            WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
            WD.mainWindow.GetSnapshot(Resultpath + "Booth Clean error.PNG");
            string errorMessage = WD.MessageDialog.Lable.Text;
            Base_Assert.AreEqual("Run booth clean rule engine failed. The reason is: No type is available.",errorMessage,"error pops up");
            WD.MessageDialog.OKButton.Click();
            WD_Fuction.Close();
            LogStep(@"9. go to booth clean report");
            Web_Fuction.gotoTab(WDWebTab.report);
            Web.Report_Page.Cleaning.Click();
            //set criteria
            var Booth = driver.FindElements("//select")[0];
            var Type = driver.FindElements("//select")[1];
            var Operator = driver.FindElements("//select")[2];
            Web.Report_Page.Start_Time.Click();
            driver.FindElement("//button[text()='Zero']").Click();
            driver.Wait();
            Booth.FindElement(By.XPath("//option[text()='booth1']")).Click();
            Type.FindElement(By.XPath("//option[text()='Full Clean']")).Click();
            Operator.FindElement(By.XPath($"//option[text()='{userNameforReport.qaone1}']")).Click();
            Web.Report_Page.End_Time.Click();
            driver.FindElement("//button[text()='Now']").Click();
            driver.Wait();
            Web.Report_Page.Generate_Report.Click();
            //5.check report
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Clean Report.PNG");
            var column = new List<string>() { "Booth", "Type", "Operator" };
            var datatext = new List<string>() { "booth1", "Full Clean", userNameforReport.qaone1 };
            //check date
            Web_Fuction.check_report_date(execute_time);
            //check data
            Web_Fuction.Check_report(column, datatext);

            driver.Close();
            
        }
    }
}
