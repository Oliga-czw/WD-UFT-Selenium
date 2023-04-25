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
        [TestCaseID(106684)]
        [Title("V8.8.6_CQ00739262:Any scale check failures should be recorded in scale check report")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Created)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
  
        public void VSTS_106684()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string xml = "10 aspen wd signautres_106684 bulk load.xml";
            string weight1 = "200";


            LogStep(@"1. import Signature xml");
            WD_Fuction.Bulkload(xml);
            WD_Fuction.WDSign();
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.Signatures.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Scale Check Standardization Signature-Setting.PNG");
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
            //get execute time
            DateTime execute_time = DateTime.Now;
            //check signature
            WD.mainWindow.GetSnapshot(Resultpath + "Scale Check Standardization Signature.PNG");
            WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone1);
            WD.mainWindow.Dialog.Comment.SetText("Scale Check Standardization Signature test");
            WD.mainWindow.Dialog.OK.Click();
            Thread.Sleep(2000);
            //leave the checking screen
            WD.MessageDialog.OKButton.Click();
            Thread.Sleep(2000);
            Base_Assert.IsTrue(WD.mainWindow.ScaleCheckInternalFrame.IsExist(), "exit scale check");
            WD_Fuction.Close();
            LogStep(@"3. Go to scale check report");
            Web_Fuction.gotoTab(WDWebTab.report);
            Web.Report_Page.ScaleCheck.Click();
            LogStep(@"4. set criteria ");
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
            Status.FindElement(By.XPath("//option[text()='Failure']")).Click();
            Scale.FindElement(By.XPath("//option[text()='simulator']")).Click();
            driver.Wait();
            Web.Report_Page.Generate_Report.Click();
            Thread.Sleep(2000);
            LogStep(@"5.check report");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "scale check failure Report.PNG");
            //check date
            Web_Fuction.check_report_date(execute_time);
            var head_list = new List<string>();
            var head = Web.Report_Page.Report_Table._Selenium_WebElement.FindElements(By.XPath("//table[@class='Report_Paper_Border_Shading']/tbody/tr[4]/td/table/tbody/tr/td/div//a[@class='Report_Head_Style']"));
            //get head list
            foreach (var h in head)
            {
                head_list.Add(h.Text);
            }
            var row = Web.Report_Page.Report_Table._Selenium_WebElement.FindElements(By.XPath("//table[@class='Report_Paper_Border_Shading']/tbody/tr[4]/td/table/tbody/tr/td[@class='Inner_Column_Left']/.."))[0];
            var single_row_text = new List<string>();
            var cells = row.FindElements(By.CssSelector("td.Inner_Column_Left"));
            foreach (var cell in cells)
            {
                single_row_text.Add(cell.Text);
            }
            var columns = new List<string>() { "Operator", "Comment" };
            var datatexts = new List<string>() { "qaone1(qaone1)", "Scale Check Standardization Signature test" };
            //check selected data
            for (int i = 0; i < columns.Count; i++)
            {
                int number = head_list.IndexOf(columns[i]);
                string datatext = datatexts[i];
                Base_Assert.AreEqual(datatext, single_row_text[number]);
            }
            LogStep(@"6.save pdf and print ");
            Thread.Sleep(2000);
            Web.Report_Page.SaveAs.Click();
            Thread.Sleep(2000);
            Web.Report_Page.Print.Click();
            //wait for screenshot and download
            Thread.Sleep(5000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Print Report.PNG");
            driver.FindElement("//button[text()='Close']").Click();
            //The searchList can't contain ' '
            var searchList = new List<string>() { "booth1", "STD-weekly", "qaone1(qaone1)", "Failure", "simulator" };
            Web_Fuction.Check_PDF(Base_Directory.DownloadFileDir, "*SCALECHECK*", searchList);
            driver.Close();
            LogStep(@"7.no signature and initial data");
            WD_Fuction.initial_data();
            LogStep(@"8. Open WD client and do scale check");
            Application.LaunchWDAndLogin();
            WD.mainWindow.HomeInternalFrame.ScaleChecking.Click();
            WD.mainWindow.ScaleCheckInternalFrame.startcheck.Click();
            WD.mainWindow.ScaleCheckInternalFrame.startcheck.DoubleClick();
            //check weight1
            WD.mainWindow.CheckWeightInternalFrame.zero.Click();
            WD.SimulatorWindow.weight.SetText(weight1);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.CheckWeightInternalFrame.readScale.ClickSignle();
            //get execute time
            DateTime execute_time2 = DateTime.Now;
            //no signature and leave the checking screen
            WD.MessageDialog.OKButton.Click();
            Thread.Sleep(2000);
            Base_Assert.IsTrue(WD.mainWindow.ScaleCheckInternalFrame.IsExist(), "exit scale check");
            WD_Fuction.Close();
            LogStep(@"9. Go to scale check report");
            Selenium_Driver driver2 = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver2);
            driver2.Wait();
            Web_Fuction.login();
            driver2.Wait();
            Web_Fuction.gotoTab(WDWebTab.report);
            Web.Report_Page.ScaleCheck.Click();
            LogStep(@"4. set criteria ");
            var Booth2 = driver2.FindElements("//select")[2];
            var Type2 = driver2.FindElements("//select")[0];
            var Operator2 = driver2.FindElements("//select")[3];
            var Status2 = driver2.FindElements("//select")[1];
            var Scale2 = driver2.FindElements("//select")[4];
            Web.Report_Page.Start_Time.Click();
            driver2.FindElement("//button[text()='Zero']").Click();
            driver2.Wait();
            Booth2.FindElement(By.XPath("//option[text()='booth1']")).Click();
            Type2.FindElement(By.XPath("//option[text()='STD-weekly']")).Click();
            Operator2.FindElement(By.XPath("//option[text()='qaone1(qaone1)']")).Click();
            Web.Report_Page.End_Time.Click();
            driver2.FindElement("//button[text()='Now']").Click();
            Status2.FindElement(By.XPath("//option[text()='Failure']")).Click();
            Scale2.FindElement(By.XPath("//option[text()='simulator']")).Click();
            driver2.Wait();
            Web.Report_Page.Generate_Report.Click();
            Thread.Sleep(2000);
            LogStep(@"10.check report-no comment");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "scale check failure Report-no comment.PNG");
            //check date
            Web_Fuction.check_report_date(execute_time2);
            var head_list2 = new List<string>();
            var head2 = Web.Report_Page.Report_Table._Selenium_WebElement.FindElements(By.XPath("//table[@class='Report_Paper_Border_Shading']/tbody/tr[4]/td/table/tbody/tr/td/div//a[@class='Report_Head_Style']"));
            //get head list
            foreach (var h in head2)
            {
                head_list2.Add(h.Text);
            }
            var row2 = Web.Report_Page.Report_Table._Selenium_WebElement.FindElements(By.XPath("//table[@class='Report_Paper_Border_Shading']/tbody/tr[4]/td/table/tbody/tr/td[@class='Inner_Column_Left']/.."))[0];
            var single_row_text2 = new List<string>();
            var cells2 = row2.FindElements(By.CssSelector("td.Inner_Column_Left"));
            foreach (var cell in cells2)
            {
                single_row_text2.Add(cell.Text);
            }
            var columns2 = new List<string>() { "Operator", "Comment" };
            var datatexts2 = new List<string>() { "qaone1(qaone1)", "" };
            //check selected data
            for (int i = 0; i < columns2.Count; i++)
            {
                int number = head_list2.IndexOf(columns2[i]);
                string datatext = datatexts2[i];
                Base_Assert.AreEqual(datatext, single_row_text2[number]);
            }
            LogStep(@"11.save pdf and print-no comment ");
            Web.Report_Page.SaveAs.Click();
            Thread.Sleep(2000);
            Web.Report_Page.Print.Click();
            //wait for screenshot and download
            Thread.Sleep(5000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Print Report-no comment.PNG");
            driver2.FindElement("//button[text()='Close']").Click();
            //The searchList can't contain ' '
            var searchList2 = new List<string>() { "booth1", "STD-weekly", "qaone1(qaone1)", "Failure", "simulator" };
            Web_Fuction.Check_PDF(Base_Directory.DownloadFileDir, "*SCALECHECK*", searchList2);
            driver2.Close();
        }
    }
}
