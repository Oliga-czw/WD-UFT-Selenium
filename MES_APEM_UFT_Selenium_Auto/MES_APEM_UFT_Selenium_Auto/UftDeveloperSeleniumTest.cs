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
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using System.Collections.Generic;
using MES_APEM_UFT_Selenium_Auto.Product.DataBaseWizard;
using OpenQA.Selenium.Interactions;
using Spire.Pdf;
using System.Text;
using Spire.Pdf.Texts;

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
        }





    }

}
