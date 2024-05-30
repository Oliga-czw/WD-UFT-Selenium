using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System;
using HP.LFT.SDK;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using System.Windows.Forms;
using Application = MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary.Application;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(1364821)]
        [Title("Inspired by Customer Defect 459555 -- the second MR should be certified successfully.")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1500000)]

        [TestMethod]
        public void VSTS_1364821()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string Configpath = Base_Directory.ConfigDirx86 + "config.m2r_cfg";
            string ConfigKey = "PFC_CENTER_ALIGNMENT = 0";
            string RPLname = "RPL1364821";
            string ordername = "ORDER1364821";

            try
            { 
                Base_Function.AddConfigKey(Configpath, ConfigKey);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_allx86);
                Thread.Sleep(10000);
                Application.LaunchMocAndLogin();
                APEM.MocmainWindow.RPLDesign.ClickSignle();
                if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLname).Existing)
                {
                    MOC_TemplatesFunction.Importtemplates("TEMP1364821.zip");
                }
                MOC_Fuction.PlanFromRPL(RPLname, ordername);
                APEM.MocmainWindow.RPLDesign.ClickSignle();
                APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLname).Click();
                APEM.MocmainWindow.RPLDesignInternalFrame.LoadDesigner_Button.Click();
                Thread.Sleep(3000);
                if (APEM.MocmainWindow.ReadOnly_Dialog.IsExist())
                {
                    APEM.MocmainWindow.ReadOnly_Dialog.OKButton.Click();
                }
                Thread.Sleep(1000);
                APEM.DesignEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject.DoubleClick();
                Thread.Sleep(5000);
                APEM.DesignEditorWindow.PFCDesignAppInternalFrame.OperationUiObject.DoubleClick();
                Thread.Sleep(2000);
                var RPLx1 = APEM.DesignEditorWindow.PFCDesignAppInternalFrame.LinkUiObject0.AbsoluteLocation.X;
                var RPLx2 = APEM.DesignEditorWindow.PFCDesignAppInternalFrame.LinkUiObject.AbsoluteLocation.X;
                Console.WriteLine(RPLx1);
                Console.WriteLine(RPLx2);
                APEM.DesignEditorWindow.GetSnapshot(Resultpath + "RPLDesign.PNG");
                Base_Assert.AreEqual(RPLx1, RPLx2);
                APEM.DesignEditorWindow.Close();
                APEM.CloseDialog.YesButton.Click();
                APEM.ChangesDesignDialog.NoButton.Click();
                //OrderDesign
                APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Row(ordername).Click();
                Thread.Sleep(3000);
                APEM.MocmainWindow.OrderListInternalFrame.LoadDesigner_Button.ClickSignle();
                Thread.Sleep(2000);
                APEM.DesignEditorWindow.PFCDesignAppInternalFrame.UnitProcedureUiObject.DoubleClick();
                Thread.Sleep(5000);
                APEM.DesignEditorWindow.PFCDesignAppInternalFrame.OperationUiObject.DoubleClick();
                Thread.Sleep(2000);
                var orderx1 = APEM.DesignEditorWindow.PFCDesignAppInternalFrame.LinkUiObject0.AbsoluteLocation.X;
                var orderx2 = APEM.DesignEditorWindow.PFCDesignAppInternalFrame.LinkUiObject.AbsoluteLocation.X;
                APEM.DesignEditorWindow.GetSnapshot(Resultpath + "OrderDesign.PNG");
                Base_Assert.AreEqual(orderx1, orderx2);
                Console.WriteLine(orderx1);
                Console.WriteLine(orderx2);
                APEM.DesignEditorWindow.Close();
                APEM.CloseDialog.YesButton.Click();
                APEM.ChangesDesignDialog.NoButton.Click();
                //ordertracking
                APEM.MocmainWindow.OrderTracking.Click();
                Thread.Sleep(2000);
                APEM.MocmainWindow.OrderTrackingInternalFrame.CodeEditor.SetText(ordername);
                APEM.MocmainWindow.OrderTrackingInternalFrame.Filterbutton.Click();
                Thread.Sleep(2000);
                Thread.Sleep(2000);
                APEM.MocmainWindow.OrderTrackingInternalFrame.OrderTable.Row("Active", "Status").DoubleClick();
                Thread.Sleep(2000);
                APEM.MocmainWindow.OrderTrackingPFCInternalFrame.UnitProcedureUiObject.DoubleClick();
                Thread.Sleep(4000);
                APEM.MocmainWindow.OrderTrackingPFCInternalFrame.OperationUiObject.DoubleClick();
                Thread.Sleep(4000);
                var orderTrackingx1 = APEM.MocmainWindow.OrderTrackingPFCInternalFrame.Link0.AbsoluteLocation.X;
                var orderTrackingx2 = APEM.MocmainWindow.OrderTrackingPFCInternalFrame.Link1.AbsoluteLocation.X;
                APEM.MocmainWindow.GetSnapshot(Resultpath + "OrderTrackingDesign.PNG");
                Base_Assert.AreEqual(orderTrackingx1, orderTrackingx2);
                Console.WriteLine(orderTrackingx1);
                Console.WriteLine(orderTrackingx2);
                APEM.MocmainWindow.OrderTrackingPFCInternalFrame._UFT_InterFrame.Close();
            }
            finally
            {
                LogStep(@"6.Restone config key ");
                Base_Function.DeleteConfigKey(Configpath, ConfigKey);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_allx86);
            }


        }

    }
}