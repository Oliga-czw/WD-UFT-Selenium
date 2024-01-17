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
        [TestCaseID(38243)]
        [Title("Can print PDF when set LABEL_PRINT_TO_PDF_FILE = 1 in the configuration file")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_38243()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test2";
            string material = WDMaterial.X0125;
            string method = WDMethod.Net;
            string barcode = "X0125001";
            string tare = "15";
            string net = "459.4";
            string Configpath = Base_Directory.ConfigDir + "flags.m2r_cfg";
            string ConfigKey = "LABEL_PRINT_TO_PDF_FILE = 1";

            try
            {
                LogStep(@"1. Set key in config file");
                Base_Function.AddConfigKey(Configpath,ConfigKey);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
                LogStep(@"2. Active orders");
                Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
                Web_Fuction.gotoWDWeb(driver);
                driver.Wait();
                Web_Fuction.login();
                driver.Wait();
                Web_Fuction.gotoTab(WDWebTab.order);
                Web_Fuction.active_order(order);
                driver.Close();
                LogStep(@"3. Open WD client");
                Application.LaunchWDAndLogin();
                WD_Fuction.SelectOrderandMaterial(order, material);
                WD_Fuction.SelectMehod(method, barcode);
                WD_Fuction.FinishNetDiapense(tare, net);
                WD_Fuction.Close();
                LogStep(@"3. Check print file");
                string[] files = Directory.GetFiles(Base_Directory.LabelPrintFileDir, "*Label-*");
                Base_Assert.IsTrue(files.Length > 0, "pdf exit");
                //Move pdf to result
                Base_File.MoveFile(files[0], Resultpath+"print label.pdf");
            }
            finally
            {
                LogStep(@"4.delete config key ");
                Base_Function.DeleteConfigKey(Configpath, ConfigKey);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
            }
            

        }

    }
}
