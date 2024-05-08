using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System;
using HP.LFT.SDK;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using System.IO;
using MES_APEM_UFT_Selenium_Auto.Product.SQLplus;
using System.Linq;
using Spire.Pdf;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(491775)]
        [Title("Can print query successfully.")]
        [TestCategory(ProductArea.MOC)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1000000)]

        [TestMethod]
        public void VSTS_491775()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string QueryName = "QUERY491775";

            Application.LaunchMocAndLogin();
            Thread.Sleep(5000);
            APEM.MocmainWindow.Library.QueryDesign.Select();
            Thread.Sleep(2000);
            APEM.MocmainWindow.SQLQueriesInterFrame.AddQuery.Click();
            APEM.MocmainWindow.SQLQueryInterFrame.QueryName.SendKeys(QueryName);
            APEM.MocmainWindow.SQLQueryInterFrame.QueryDescription.SendKeys("for test");
            APEM.MocmainWindow.SQLQueryInterFrame.SQLSentence.SendKeys("SELECT CODE from EBR_ORDER");
            APEM.MocmainWindow.SQLQueryInterFrame.Confirm_Button.Click();
            Thread.Sleep(3000);
            MOC_Fuction.AddReason();
            Thread.Sleep(2000);
            APEM.MocmainWindow.SQLQueryInterFrame._UFT_InterFrame.Close();
            Thread.Sleep(2000);
            APEM.MocmainWindow.SQLQueriesInterFrame.QueryList_Table.Row(QueryName, "Name").Click();
            APEM.MocmainWindow.SQLQueriesInterFrame.PrintReport.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.PrintReportDialog.GetSnapshot(Resultpath + "Query_Print.PNG");
            APEM.MocmainWindow.PrintReportDialog.Print.Click();
            Thread.Sleep(5000);
            APEM.PrintDialog.Print.Click();
            APEM.SavePrintFileWindow.SaveFile.FileNameEditField.SendKeys(QueryName);
            Thread.Sleep(3000);
            APEM.SavePrintFileWindow.SaveFile.Save.Click();
            Thread.Sleep(2000);
            string filePath = "C:\\Users\\qaone1\\Documents\\" + QueryName + ".pdf";
            using (PdfDocument pdfDocument = new PdfDocument())
            {
                pdfDocument.LoadFromFile(filePath);
                for (int i = 0; i < pdfDocument.Pages.Count; i++)
                {
                    PdfPageBase page = pdfDocument.Pages[i];
                    string text = page.ExtractText();
                    Console.WriteLine(text);
                    Base_Assert.IsTrue(text.Contains(QueryName));
                }
            }

        }

    }
}