using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System;
using HP.LFT.SDK;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using System.Diagnostics;
using System.IO;
using MES_APEM_UFT_Selenium_Auto.Product.WD;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(33951)]
        [Title("User without AFW permission can not access to MOC, APEM mobile, WD web and WD client")]
        [TestCategory(ProductArea.RecipeManagement)]
        [Priority(CasePriority.Low)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_33951()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";

            string sourceName1 = Base_Directory.ProjectDir + @"Data\Input\AFWDB.mdb";
            string sourceName2 = Base_Directory.ProjectDir + @"Data\Input\AFWNODB.mdb";

            try
            {
                LogStep(@"1. Repalce AFW DB");
                Process.Start("cmd.exe", "/c iisreset");
                Thread.Sleep(10000);
                string directoryPath = @"C:\Program Files (x86)\AspenTech\Local Security\Access97"+ @"\AFWDB.mdb";
                File.Copy(sourceName2, directoryPath, true);
                Thread.Sleep(10000);
                Base_Function.ResartServices(ServiceName.AFW);
                Thread.Sleep(10000);
                Base_Function.ResartServices(ServiceName.AFW);
                Thread.Sleep(10000);
                //start tomcat
                Base_Test.KillProcess("tomcat10");
                Thread.Sleep(10000);
                Base_Function.ResartServices(ServiceName.Tomcat);
                Thread.Sleep(60000);
                LogStep(@"2. login moc");
                Application.LaunchMocAndLogin();
                //no permission
                APEM.MocmainWindow.GetSnapshot(Resultpath + "MOC login error.PNG");
                string message1 = "You do not have permission to login. Please contact your system administrator to access.";
                Base_Assert.AreEqual(message1, APEM.ErrorDialog.Lable.Text, "MOC login error");
                APEM.ErrorDialog.OKButton.Click();
                APEM.ExitApplication();
                LogStep(@"3. login wd");
                Application.LaunchWDAndLogin();
                //no permission
                WD.mainWindow.GetSnapshot(Resultpath + "WD login error.PNG");
                string message2 = "User has insufficient permission.";
                Base_Assert.AreEqual(message2, WD.MessageDialog.Lable.Text, "WD login error");
                WD.MessageDialog.OKButton.Click();
                WD_Fuction.Close();
                LogStep(@"4. login wd web");
                Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
                Web_Fuction.gotoWDWeb(driver);
                driver.Wait();
                Web_Fuction.login();
                Thread.Sleep(10000);
                //no permission-Home tab
                Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "WD web login Home.PNG");
                Base_Assert.IsTrue(Web.Main_Page.Tabs.Count()==1,"Home tab");
                Base_Assert.AreEqual("Home", Web.Main_Page.Tabs.getElement(0).Text,"Home Tab");
                driver.Close();
                LogStep(@"4. login mobile");
                Selenium_Driver driver2 = new Selenium_Driver(Browser.chrome);
                Mobile_Fuction.gotoApemMobile(driver2);
                Mobile_Fuction.login();
                //no permission
                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Mobile login error.PNG");
                string message4 = "You do not have permission to login. Please contact your system administrator to access.";
                Base_Assert.AreEqual(message4, Mobile.Login_Page.error.Text(), "Mobile login error");
                driver2.Close();
            }
            finally
            {
                LogStep(@"6.Restone AFWDB ");
                Process.Start("cmd.exe", "/c iisreset");
                string directoryPath = @"C:\Program Files (x86)\AspenTech\Local Security\Access97";
                Base_File.CopyFile(sourceName1, directoryPath, true);
                Thread.Sleep(10000);
                Base_Function.ResartServices(ServiceName.AFW);
                Thread.Sleep(10000);
                Base_Function.ResartServices(ServiceName.AFW);
                Thread.Sleep(10000);
                //start tomcat
                Base_Test.KillProcess("tomcat10");
                Thread.Sleep(10000);
                Base_Function.ResartServices(ServiceName.Tomcat);
                Thread.Sleep(60000);
            }

        }

    }
}