using System;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;
using WD_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Text;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {

        [TestCaseID(41180)]
        [Title("material weighing start: weighing can be conducted and material information correctly displayed")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_41180()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            LogStep(@"1. Open Wd client and login");
            Application.LaunchWDAndLogin();
            LogStep(@"2. Open Material Dispening");
            WD.mainWindow.HomeInternalFrame.MaterialDispensing.Click();
            Thread.Sleep(2000);
            LogStep(@"3. select a material and click next");
            WD.mainWindow.Material_SelectionInternalFrame.materialTable.SelectRows(0);
            WD.mainWindow.Material_SelectionInternalFrame.next.Click();
            if (WD.mainWindow.BoothCleanInternalFrame.IsEnabled is true)
            {
                WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
            }
            LogStep(@"4. Assert the first/default method is presented");
            var selectedMethod = WD.mainWindow.ScaleWeightInternalFrame.dispense_method.SelectedItems.ToString();
            var FirstMethod = WD.mainWindow.ScaleWeightInternalFrame.dispense_method.Items[0].Text;
            Base_Assert.AreEqual(selectedMethod, FirstMethod);
            //System.IO.File.WriteAllText("C:/Users/qaone1/Desktop/eee.txt", Methodconnection, Encoding.Default);
            // Console.Write(Methodconnection);
            LogStep(@"5. enter the barcode of source container");
            var DisployMaterial = WD.mainWindow.ScaleWeightInternalFrame.disploylMaeterial._UFT_Label.Text;
            System.IO.File.WriteAllText("C:/Users/qaone1/Desktop/eee.txt", DisployMaterial.ToString(), Encoding.Default);
            //WD.mainWindow.ScaleWeightInternalFrame.barcode.SetText();








        }


    }
}