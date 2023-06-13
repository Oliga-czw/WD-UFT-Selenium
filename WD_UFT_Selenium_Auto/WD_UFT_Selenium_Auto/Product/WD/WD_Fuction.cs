
using System;
using System.Diagnostics;
using System.Threading;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;

namespace WD_UFT_Selenium_Auto.Product.WD
{
    class WD_Fuction
    {

        public static void SelectOrderandMaterial(string order, string material)
        {
            WD.mainWindow.HomeInternalFrame.OrderDispensing.Click();
            //need change select function
            WD.mainWindow.DispensingInternalFrame.orderTable.Row(order).Click();
            WD.mainWindow.DispensingInternalFrame.next.Click();
            WD.mainWindow.MaterialInternalFrame.materialTable.Row(material).Click();
            WD.mainWindow.MaterialInternalFrame.next.Click();
            if (WD.mainWindow.BoothCleanInternalFrame.IsExist())
            {
                WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
            }
            if (WD.mainWindow.HandleInformationInterFrame.IsExist())
            {
                WD.mainWindow.HandleInformationInterFrame.Acknowledge.Click();
            }

        }


        public static void FinishDiapense(string method,string barcode)
        {

            WD.mainWindow.ScaleWeightInternalFrame.dispense_method.SelectItems(method);
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys(barcode);
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            //tare
            WD.SimulatorWindow.weight.SetText("10");
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            //weight
            WD.SimulatorWindow.weight.SetText("410");
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            Thread.Sleep(5000);
        }

        public static void SelectMehod(string method, string barcode)
        {

            WD.mainWindow.ScaleWeightInternalFrame.dispense_method.SelectItems(method);
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys(barcode);
            
        }

        public static void FinishManualDiapense(string simulator,string tare,string net)
        {
            WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(simulator);
            //tare
            WD.mainWindow.ScaleWeightInternalFrame.tare_editor.SetText(tare);
            //weight
            WD.mainWindow.ScaleWeightInternalFrame.net_editor.SetText(net,true);
            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            Thread.Sleep(5000);
            //check Finish Dispense
            Base_Assert.IsTrue(WD.mainWindow.Material_SelectionInternalFrame.IsExist() || WD.mainWindow.MaterialInternalFrame.IsExist(), "Finish Dispense");

        }
        public static void FinishNetDiapense(string tare, string net)
        {
            //WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(simulator);
            //zeor
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            //tare
            WD.SimulatorWindow.weight.SetText(tare);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            //weight
            WD.SimulatorWindow.weight.SetText(net);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            Thread.Sleep(5000);
            //check Finish Dispense
            Base_Assert.IsTrue(WD.mainWindow.Material_SelectionInternalFrame.IsExist() || WD.mainWindow.MaterialInternalFrame.IsExist(), "Finish Dispense");
        }

        public static void CleanInventoryData()
        {
            SqlHelper helper = new SqlHelper();
            string delete = $"DELETE FROM EBR_WD_HU";
            helper.ExecuteNonQuery(delete);
            Base_logger.Message("Clean Inventory Data successfully in DB!");
        }
        public static void CleanMaterialData()
        {
            SqlHelper helper = new SqlHelper();
            string delete = $"DELETE FROM EBR_WD_MATERIAL_HANDLING_CHARACTERISTICS WHERE MATERIAL_TAG <> '<Default>'";
            helper.ExecuteNonQuery(delete);
            Base_logger.Message("Clean Material Data successfully in DB!");
        }

        public static void CleanWeighHistory()
        {
            SqlHelper helper = new SqlHelper();
            string delete = $"DELETE FROM EBR_WD_WEIGH_HISTORY";
            helper.ExecuteNonQuery(delete);
            Base_logger.Message("Clean weigh history Data successfully in DB!");
        }
        public static void CleanOrdersData()
        {
            SqlHelper helper = new SqlHelper();
            string delete = $"DELETE FROM EBR_WD_ERP_ORDER";
            string delete2 = $"DELETE FROM EBR_WD_ERP_ORDER_BOM";
            helper.ExecuteNonQuery(delete);
            helper.ExecuteNonQuery(delete2);
            Base_logger.Message("Clean Orders Data successfully in DB!");
        }
        public static void Close()
        {

            WD.mainWindow.Close();
            WD.CloseDialog.YesButton.Click();

        }
        public static void Bulkload(string file)
        {
            string path = Base_Directory.WDBulkload;
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = false;
            p.Start();
            p.StandardInput.WriteLine("cd " + path);
            string no = file.Substring(0, 2);
            string project = Base_Directory.ProjectDir + "Data\\Input\\BulkLoad\\";
            string xml = $"wdbulkloadtool -w localhost -i{no} \"{project}{file}\"";
            p.StandardInput.WriteLine(xml + "&exit");
            string output = p.StandardOutput.ReadLine();
            p.WaitForExit();
            p.Close();
            Base_logger.Message("impot xml" + output);
        }
        public static void Bulkload(Array files)
        {
            string path = Base_Directory.WDBulkload;
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = false;
            p.Start();
            p.StandardInput.WriteLine("cd " + path);
            foreach (string file in files)
            {
                string no = file.Substring(0, 2);
                string project = Base_Directory.ProjectDir + "Data\\Input\\BulkLoad\\";
                string xml = $"wdbulkloadtool -w localhost -i{no} \"{project}{file}\"";
                p.StandardInput.WriteLine(xml);
                Thread.Sleep(3000);
            }
            p.StandardInput.WriteLine("exit");
            string output = p.StandardOutput.ReadToEnd();
            Base_logger.Message("impot xml" + output);
            p.WaitForExit();
            p.Close();
            
        }
        public static void Bulkload_Overwrite(string file)
        {
            string path = Base_Directory.WDBulkload;
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = false;
            p.Start();
            p.StandardInput.WriteLine("cd " + path);
            string no = file.Substring(0, 2);
            string project = Base_Directory.ProjectDir + "Data\\Input\\BulkLoad\\";
            string xml = $"wdbulkloadtool -w localhost -i{no}o \"{project}{file}\"";
            p.StandardInput.WriteLine(xml + "&exit");
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            p.Close();
            Base_logger.Message("impot sucessfully" + output);
        }
        public static void Bulkload_Overwrite(Array files)
        {
            string path = Base_Directory.WDBulkload;
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = false;
            p.Start();
            p.StandardInput.WriteLine("cd " + path);
            foreach (string file in files)
            {
                string no = file.Substring(0, 2);
                string project = Base_Directory.ProjectDir + "Data\\Input\\BulkLoad\\";
                string xml = $"wdbulkloadtool -w localhost -i{no}o \"{project}{file}\"";
                p.StandardInput.WriteLine(xml);
                Thread.Sleep(3000);
            }
            p.StandardInput.WriteLine("exit");
            string output = p.StandardOutput.ReadToEnd();
            Base_logger.Message("impot xml" + output);
            p.WaitForExit();
            p.Close();
        }
        public static void WDSign()
        {
            string path = Base_Directory.WDBulkload;
            string cmd = "sign.cmd";
            //Console.WriteLine(cmd);
            Process p = new Process();
            //process.StartInfo.FileName = cmd;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = false;
            p.Start();
            p.StandardInput.WriteLine("cd " + path);
            p.StandardInput.WriteLine(cmd + "&exit");
            string output = p.StandardOutput.ReadLine();
            p.WaitForExit();
            p.Close();
            Base_logger.Message("Sign sucessfully" + output);
            Thread.Sleep(15000);
        }

        public static void initial_data()
        {
            WD_Fuction.CleanOrdersData();
            CleanWeighHistory();
            string signature = "10 aspen wd signautres bulk load.xml";
            string deviation = "14 aspen wd deviation bulk load.xml";
            string orders = "07 aspen wd orders bulk load.xml";
            string booth_group = "17 aspen wd booth groups bulk load.xml";

            string scale = "02 aspen wd scales bulk load.xml";
            string booth = "01 aspen wd booths bulk load.xml";
            string permission = "09 aspen wd permissions bulk load.xml";
            string inventory = "05 aspen wd inventory bulk load.xml";
            string[] files = new string[] { signature, deviation, orders, booth_group };
            string[] overwrite = new string[] { booth, scale, permission, inventory };
            WD_Fuction.Bulkload(files);
            Thread.Sleep(5000);
            WD_Fuction.Bulkload_Overwrite(overwrite);
            Thread.Sleep(5000);
            //WD_Fuction.WDSign();
        }
    }

    
}
