using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using System.IO;
using System.Xml;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(31383)]
        [Title("Accept Dispensation, check the order status, Material consumption xml, and Inventoy Adjustment xml")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Critical)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
  
        public void VSTS_31383()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string order = "test1";
            string material = WDMaterial.X0125;
            string method = WDMethod.Net;
            string barcode = "X0125001";
            string tare = "10";
            string net = "300";


            LogStep(@"1. Active Order");
           
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Active order.PNG");
            //Open Latest xml and check order status
            string[] files = Directory.GetFiles(Base_Directory.WDUploadDir);
            Array.Reverse(files);
            //edit xml security
            XmlDocument document = new XmlDocument();
            document.Load(files[0]);
            //get namespace
            string ns = document.DocumentElement.Attributes["xmlns"].Value;
            //add namespace
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(document.NameTable);
            nsMgr.AddNamespace("ns", ns);
            // 获取ProductionRequestID节点下的内容  
            XmlNode ordername = document.SelectSingleNode("//ns:ProductionRequestID", nsMgr);
            if (ordername != null)
            {
                string productionRequestID = ordername.InnerText;
                Console.WriteLine("ProductionRequestID: " + productionRequestID);
            }
            else
            {
                Console.WriteLine("ProductionRequestID node not found.");
            }



            LogStep(@"2. Open WD client and do scale check");
            Application.LaunchWDAndLogin();
            WD.mainWindow.HomeInternalFrame.ScaleChecking.Click();
            WD.mainWindow.ScaleCheckInternalFrame.startcheck.Click();
            WD.mainWindow.ScaleCheckInternalFrame.startcheck.DoubleClick();
            //check weight1
            WD.mainWindow.CheckWeightInternalFrame.zero.Click();
            WD.SimulatorWindow.weight.SetText(net);
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
      
        }
    }
}
