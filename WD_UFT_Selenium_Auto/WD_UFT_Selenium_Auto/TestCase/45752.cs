using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

using System.Threading;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;
using WD_UFT_Selenium_Auto.Product.WD;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(45752)]
        [Title("scale check report")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Created)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
  
        public void VSTS_45752()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string xml = "02 aspen wd scales_45752 bulk load.xml";
            string weight1 = "100";
            string weight2 = "500";

            LogStep(@"1. import Signature xml");
            WD_Fuction.Bulkload(xml);
            WD_Fuction.WDSign();
            LogStep(@"2. Open WD client and do scale check");
            Application.LaunchWDAndLogin();
            WD.mainWindow.HomeInternalFrame.ScaleChecking.Click();
            WD.mainWindow.ScaleCheckInternalFrame.startcheck.Click();
            WD.mainWindow.ScaleCheckInternalFrame.startcheck.DoubleClick();
            //check weight1
            WD.mainWindow.CheckWeightInternalFrame.zero.Click();
            WD.SimulatorWindow.weight.SetText(weight1);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.CheckWeightInternalFrame.readScale.ClickSignle();
            //check weight2
            WD.mainWindow.CheckWeightInternalFrame.zero.Click();
            WD.SimulatorWindow.weight.SetText(weight2);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.CheckWeightInternalFrame.readScale.ClickSignle();
            //get check result
            var table = WD.mainWindow.CheckWeightInternalFrame.checkTable;
            var header = WD.mainWindow.CheckWeightInternalFrame.checkTable.Columns;
            var data_list = new List<List<string>>();
            for (int i = 0; i < table.Rowscount(); i++)
            {
                var data = new List<string>();
                foreach (var head in header)
                {
                    if (table.GetCell(i, head).Value.ToString() == "")
                    {
                        if (i == 0)
                        {
                            data.Add("100g load");
                        }
                        if (i == 1)
                        {
                            data.Add("500g load");
                        }

                    }
                    else
                    {
                        data.Add(table.GetCell(i, head).Value.ToString());
                    }

                }
                data_list.Add(data);
            }
            WD.mainWindow.GetSnapshot(Resultpath + "scale check.PNG");
            WD.mainWindow.CheckWeightInternalFrame.accept.Click();
            //get execute time
            DateTime execute_time = DateTime.Now;
            LogStep(@"3. Open WD web and login");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            LogStep(@"4. Go to scale check report");
            Web_Fuction.gotoTab(WDWebTab.report);
            Web.Report_Page.ScaleCheck.Click();
            LogStep(@"5. set criteria ");
            var Booth = driver.FindElements("//select")[2];
            var Type = driver.FindElements("//select")[0];
            var Operator = driver.FindElements("//select")[3];
            var Status = driver.FindElements("//select")[1];
            var Scale = driver.FindElements("//select")[4];
            Web.Report_Page.Start_Time.Click();
            driver.FindElement("//button[text()='Zero']").Click();
            driver.Wait();
            Booth.FindElement(By.XPath("//option[text()='booth1']")).Click();
            Type.FindElement(By.XPath("//option[text()='STD-weekly']")).Click();
            Operator.FindElement(By.XPath("//option[text()='qaone1(qaone1)']")).Click();
            Web.Report_Page.End_Time.Click();
            driver.FindElement("//button[text()='Now']").Click();
            Status.FindElement(By.XPath("//option[text()='Success']")).Click();
            Scale.FindElement(By.XPath("//option[text()='simulator']")).Click();
            driver.Wait();
            Web.Report_Page.Generate_Report.Click();
            LogStep(@"6.check report");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "scale check Report.PNG");
            var column = new List<string>() { "Booth", "Type", "Operator", "Status", "Scale" };
            var datatext = new List<string>() { "booth1", "STD-weekly", "qaone1(qaone1)", "Success", "simulator" };
            //check date
            Web_Fuction.check_report_date(execute_time);
            //check data
            Web_Fuction.Check_report(column, datatext);
            Web_Fuction.Check_report_inner(data_list, 2);
            Thread.Sleep(2000);
            LogStep(@"7.save pdf and print ");
            Web.Report_Page.SaveAs.Click();
            Thread.Sleep(2000);
            Web.Report_Page.Print.Click();
            //wait for screenshot and download
            Thread.Sleep(5000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Print Report.PNG");
            driver.FindElement("//button[text()='Close']").Click();
            //The searchList can't contain ' '
            var searchList = new List<string>() { "booth1", "STD-weekly", "qaone1(qaone1)", "Success", "simulator" };
            Web_Fuction.Check_PDF(Base_Directory.DownloadFileDir, "*SCALECHECK*", searchList);
            var containList = new List<string>() { "ID", "Description", "Min", "Max", "Actual" };
            Web_Fuction.Check_PDF_inner(Base_Directory.DownloadFileDir, "*SCALECHECK*", containList);
            driver.Close();
            WD_Fuction.Close();
        }
    }
}
