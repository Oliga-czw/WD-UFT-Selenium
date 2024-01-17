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
            string net1 = "454.4";
            string weight1 = "444.4";


            LogStep(@"1. Active Order");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Active order.PNG");
            LogStep(@"2. Check order xml");
            //Open Latest xml and check order status
            string[] files1 = Directory.GetFiles(Base_Directory.WDUploadDir);
            Array.Reverse(files1);
            //edit xml security
            XmlDocument orderXml = new XmlDocument();
            orderXml.Load(files1[0]);
            //get namespace
            string ns = orderXml.DocumentElement.Attributes["xmlns"].Value;
            //add namespace
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(orderXml.NameTable);
            nsMgr.AddNamespace("ns", ns);
            //get ProductionRequestID text 
            XmlNode productionRequestID = orderXml.SelectSingleNode("//ns:ProductionRequestID", nsMgr);
            XmlNode ResponseState = orderXml.SelectSingleNode("//ns:ResponseState", nsMgr);
            Base_Assert.AreEqual(order, productionRequestID.InnerText);
            Base_Assert.AreEqual("Activated", ResponseState.InnerText);
            LogStep(@"3. Finish order dispense");
            Application.LaunchWDAndLogin();
            WD_Fuction.SelectOrderandMaterial(order, material);
            WD_Fuction.SelectMehod(method, barcode);
            WD_Fuction.FinishNetDiapense(tare,net1);
            LogStep(@"4. Check material xml");
            string[] files2 = Directory.GetFiles(Base_Directory.WDUploadDir);
            Array.Reverse(files2);
            //edit xml security
            XmlDocument materialXml = new XmlDocument();
            materialXml.Load(files2[0]);
            //get namespace
            ns = materialXml.DocumentElement.Attributes["xmlns"].Value;
            //add namespace
            nsMgr = new XmlNamespaceManager(materialXml.NameTable);
            nsMgr.AddNamespace("ns", ns);
            //get ProductionRequestID text 
            Base_Assert.AreEqual(order, materialXml.SelectSingleNode("//ns:ProductionRequestID", nsMgr).InnerText);
            Base_Assert.AreEqual(barcode, materialXml.SelectSingleNode("//ns:MaterialSubLotID", nsMgr).InnerText);
            Base_Assert.AreEqual(weight1, materialXml.SelectSingleNode("//ns:QuantityString", nsMgr).InnerText);
            LogStep(@"4.1 Finish order with");
            //M801890002   10  610 ConfirmationDialog  no 77

        }
    }
}
