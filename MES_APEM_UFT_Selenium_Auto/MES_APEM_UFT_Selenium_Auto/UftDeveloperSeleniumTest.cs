using HP.LFT.SDK;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System.Windows.Forms;
using System;
using HP.LFT.SDK.StdWin;
using System.Diagnostics;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;
using OpenQA.Selenium;
using System.Drawing;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using System.IO;
<<<<<<< Updated upstream
using MES_APEM_UFT_Selenium_Auto.Product.SQLplus;
using MES_APEM_UFT_Selenium_Auto.Product.APRM;
using System.Collections.Generic;
using System.Linq;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using System.Xml;
=======
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using System.Collections.Generic;
using MES_APEM_UFT_Selenium_Auto.Product.DataBaseWizard;
using OpenQA.Selenium.Interactions;
using Spire.Pdf;
using System.Text;
using Spire.Pdf.Texts;
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream

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


=======
            //Thread.Sleep(3000);
            
            string pdfFilePath = Base_Directory.WDReport + "ORDER_qaone1_Data01102024061943PM55B6284E1489E860.pdf";
            PdfDocument doc = new PdfDocument();
            doc.LoadFromFile(pdfFilePath);
            StringBuilder content = new StringBuilder();

            foreach (PdfPageBase page in doc.Pages)
            {
                //创建一个PdfTextExtractot 对象
                PdfTextExtractor textExtractor = new PdfTextExtractor(page);
                //创建一个 PdfTextExtractOptions 对象
                PdfTextExtractOptions extractOptions = new PdfTextExtractOptions();
                extractOptions.IsExtractAllText = true;
                content.AppendLine(textExtractor.ExtractText(extractOptions));
            }
            
            int lineCount = 0;
            using (StringReader reader = new StringReader(content.ToString()))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lineCount++;
                    if (lineCount == 16) // 跳转到指定行号  
                    {
                        Assert.IsTrue(line.Contains("X0125001"));
                    }
                    else if (lineCount == 19) 
                    {
                        Assert.IsTrue(line.Contains("Begin Source          320.0 G"));
                    }
                    else if (lineCount == 20)
                    {
                        Assert.IsTrue(line.Contains("End Source           300.0 G"));
                    }
                    else if (lineCount == 21)
                    {
                        Assert.IsTrue(line.Contains("Difference          220.0 G"));
                    }
                    else if (lineCount == 24)
                    {
                        Assert.IsTrue(line.Contains("M801890001"));
                    }
                    else if (lineCount == 27)
                    {
                        Assert.IsTrue(line.Contains("0.00%"));
                    }
                }
            }
>>>>>>> Stashed changes
        }





    }

}
