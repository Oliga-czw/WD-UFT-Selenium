using HP.LFT.SDK;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System.Windows.Forms;
using System;
using HP.LFT.SDK.UIAPro;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;
using OpenQA.Selenium;
using System.Drawing;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using System.IO;
using MES_APEM_UFT_Selenium_Auto.Product.SQLplus;
using MES_APEM_UFT_Selenium_Auto.Product.APRM;
using System.Collections.Generic;
using System.Linq;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using System.Xml;

namespace MES_APEM_UFT_Selenium_Auto
{
    [TestClass]
    public class UftDeveloperSeleniumTest
    {

        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void TestMethod1()
        {
            //SdkConfiguration config = new SdkConfiguration();
            //SDK.Init(config);

            ////Open Latest xml and check order status
            //string[] files = Directory.GetFiles(Base_Directory.WDUploadDir);
            //Array.Reverse(files);
            ////edit xml security
            //XmlDocument document = new XmlDocument();
            //document.Load(files[0]);


            //XmlNode ordername = document.SelectSingleNode("//ProductionRequestID");//ProductionPerformance/ProductionResponse/ProductionRequestID
            //if (ordername != null)
            //{
            //    string productionRequestID = ordername.InnerText;
            //    Console.WriteLine("ProductionRequestID: " + productionRequestID);
            //}
            //else
            //{
            //    Console.WriteLine("ProductionRequestID node not found.");
            //}

            string xmlString = @"<ProductionPerformance><ProductionResponse><ProductionRequestID>test2</ProductionRequestID><StartTime>
2024-01-11T02:01:18.000Z</StartTime><EndTime></EndTime><SegmentResponse><MaterialActual>
<MaterialDefinitionID>1902</MaterialDefinitionID><MaterialUse>Produced</MaterialUse><Quantity>
<QuantityString>800.000</QuantityString><DataType>decimal</DataType><UnitOfMeasure>G</UnitOfMeasure>
</Quantity></MaterialActual></SegmentResponse><ResponseState>Activated</ResponseState></ProductionResponse></ProductionPerformance>";

            // 创建XmlDocument对象并加载XML字符串  
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);

            // 获取ProductionRequestID节点下的内容  
            XmlNode productionRequestIDNode = doc.SelectSingleNode("//ProductionRequestID");
            if (productionRequestIDNode != null)
            {
                string productionRequestID = productionRequestIDNode.InnerText;
                Console.WriteLine("ProductionRequestID: " + productionRequestID);
            }
            else
            {
                Console.WriteLine("ProductionRequestID node not found.");
            }


        }





    }

}
