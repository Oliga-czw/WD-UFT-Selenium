using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;
using WD_UFT_Selenium_Auto.Product.WD;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        //public string CaseID => this.GetType().Name == null ? throw new ArgumentNullException() : TestCaseManage.GetCase(this.TestContext.TestName).CaseID;
        //public string CaseID = "001";

        
        [TestCaseID(888999)]
        [Title("Test")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Created)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_888999()
        {
            string order = "test1";
            string material = "X0125";
            LogStep(@"1. Open wd and login");
            Application.LaunchWDAndLogin();
            LogStep(@"2. Open web and login");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            LogStep(@"3. go and admin and clean rules");
            Web_Fuction.gotoTab(WDWebTab.admin);
            Base_logger.Info("Test");
            string path = Base_Directory.ResultsDir + "001" + "admin" + ".PNG";
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, path);
            driver.Wait();
            Web.Administration_Page.CleaningRules.Click();

            //Base_logger.GenerateLogFile("test");
            Base_Assert.AreEqual(Web.Administration_Page.CleaningRules._Selenium_WebElement.Text, "Cleaning Rules") ;
            //Base_logger.SaveLogFile();
            Web.Administration_Page.States.Click();

            string path2 = Base_Directory.ResultsDir + "001" + "Weight" + ".PNG";
            WD_Fuction.SelectOrderandMaterial(order, material);
            WD.mainWindow.GetSnapshot(path2);
            




            //SdkConfiguration config = new SdkConfiguration();
            //SDK.Init(config);
            //var ordersTable = Desktop.Describe<IWindow>(new WindowDescription
            //{
            //    Title = @"Aspen Weigh and Dispense Execution"
            //})
            //   .Describe<IInternalFrame>(new InternalFrameDescription { })
            //   .Describe<ITable>(new TableDescription
            //   {
            //       AttachedText = @"Orders:"
            //   });
            //string rowText = "test1";
            //ITableRow resultRow = ordersTable.Rows.Where(anyRow
            //        => anyRow.Cells.Any(anyCell => rowText.Trim() == anyCell.Value.ToString().Trim()))
            //        .FirstOrDefault();

            //string name1 = resultRow.Cells[0].Value.ToString();
            //Console.WriteLine("name1" + name1);
            //resultRow.Cells[0].DragAndDropOn(resultRow.Cells[0]);
            //IReadOnlyList<string> headers = new List<string>();
            //headers = ordersTable.ColumnHeaders;
            //headers.Count();
            //int count = 0;
            //foreach ( string header in headers)
            //{
            //    count += 1;
            //    Console.WriteLine(count+header);
            //}
        }

     

      
    }
}
