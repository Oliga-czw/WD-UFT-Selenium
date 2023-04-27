using System;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;
using WD_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Collections;

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
            if (WD.mainWindow.HandingInternalFrame.AcknowledgeButton.IsEnabled is true)
            {
                WD.mainWindow.HandingInternalFrame.AcknowledgeButton.Click();
            }

            LogStep(@"4. Assert the first/default method is presented");
            var selectedMethod = WD.mainWindow.ScaleWeightInternalFrame.dispense_method.SelectedItems.ToString();
            var FirstMethod = WD.mainWindow.ScaleWeightInternalFrame.dispense_method.Items[1].Text;
            Base_Assert.AreEqual(selectedMethod, FirstMethod);
            //System.IO.File.WriteAllText("C:/Users/qaone1/Desktop/eee.txt", Methodconnection, Encoding.Default);
            // Console.Write(Methodconnection);
            LogStep(@"5. enter the barcode of source container");
            var DisployMaterial = WD.mainWindow.ScaleWeightInternalFrame.disploylMaeterial._UFT_Label.Text;
            System.IO.File.WriteAllText("C:/Users/qaone1/Desktop/eee.txt", DisployMaterial.ToString(), Encoding.Default);
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SetText("1072007\n");
            Base_Assert.AreEqual(WD.mainWindow.ScaleWeightInternalFrame.AvailQty._UFT_Label.Text, "800.0 G");
            Base_Assert.AreEqual(WD.mainWindow.ScaleWeightInternalFrame.Lot._UFT_Label.Text,"A124");
            Base_Assert.AreEqual(WD.mainWindow.ScaleWeightInternalFrame.Potency._UFT_Label.Text,"");
            Base_Assert.AreEqual(WD.mainWindow.ScaleWeightInternalFrame.Expiration._UFT_Label.Text, "11/25/23, 5:25:00 PM");
            Base_Assert.AreEqual(WD.mainWindow.ScaleWeightInternalFrame.Status._UFT_Label.Text,"Approved");
            WD.mainWindow.GetSnapshot(Resultpath + "correct_barcode.PNG");
            WD.mainWindow.ScaleWeightInternalFrame.cancel.Click();
            //3.1 enter Incorrect material.
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SetText("445254\n");
            Base_Assert.AreEqual(WD.MessageDialog.Lable.Text, "Container barcode is not recognized. Scan another container.");
            WD.mainWindow.GetSnapshot(Resultpath + "Incorrect_barcode.PNG");
            WD.MessageDialog.OKButton.Click();
            //enter not matching one (optionally) downloaded from ERP: user is warned, and depending on configuration, he is allowed to proceed or not, creating a deviation.
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SetText("X0125001\n");
            Base_Assert.AreEqual(WD.MessageDialog.Lable.Text, "The scanned container is not the required material. Please scan the correct container.");
            WD.mainWindow.GetSnapshot(Resultpath + "not_match_barcode.PNG");
            WD.MessageDialog.OKButton.Click();
            //enter quarantined (except if specifically allowed), or expired lot (from ERP stock).
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SetText("1072006\n");
            Base_Assert.AreEqual(WD.MessageDialog.Lable.Text, "Quarantined lot is not allowed.");
            WD.mainWindow.GetSnapshot(Resultpath + "quarantined_barcode.PNG");
            WD.MessageDialog.OKButton.Click();
            //enter Non-FEFO lot:
            //WD.mainWindow.ScaleWeightInternalFrame.barcode.SetText("445254\n");
            //Base_Assert.AreEqual(WD.MessageDialog.Lable.Text, "Container barcode is not recognized. Scan another container.");
            //enter Non-released material
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SetText("1072001\n");
            Base_Assert.AreEqual(WD.MessageDialog.Lable.Text, "Only approved and allowed quarantined HU can be used.");
            WD.MessageDialog.OKButton.Click();
            LogStep(@"6. start to weigh using the selecte weighing method and exit the dispenstion after weighing is done.");
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SetText("1072007\n");
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            WD.SimulatorWindow.weight.SetText("200");
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.GetSnapshot(Resultpath + "weight_accept.PNG");
            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            //TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            //string XmlNmae =Convert.ToInt64(ts.TotalMilliseconds).ToString();
            string str2 = DateTime.Now.ToString("m/d/yyyy h:mm:ss tt");
            WD_Fuction.Close();
            LogStep(@"7. check the Label for target container and consumption xml, adjustment xml");
            //DirectoryInfo folder = new DirectoryInfo("C:\\ProgramData\\AspenTech\\AeBRS\\WDUpload");
            //ArrayList fileList = new ArrayList();
            
            //foreach (FileInfo file in folder.GetFiles("*.txt"))
            //{
            //    var filename = file.FullName.Remove(13, 4);
            //    var nameStr = Base_File.GetDateTime(filename).ToString("m/d/yyyy h:mm:ss tt");
            //    if (nameStr == str2)
            //    {
            //        fileList.Add(file.FullName);
            //    }
            //}

            //var XMLName = fileList.IndexOf(1);
            //XDocument doc = XDocument.Load("C:\\ProgramData\\AspenTech\\AeBRS\\WDUpload\\"+ fileList.IndexOf(1););


        }


    }
}