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
        [TestCaseID(909084)]
        [Title("UC822679_Contextual error tracking for BPL compile")]
        [TestCategory(ProductArea.MOC)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1500000)]

        [TestMethod]
        public void VSTS_909084()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string BPLname = "BPL909084"; 
            Application.LaunchMocAndLogin();
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            Thread.Sleep(3000);
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLname).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("TEMP909084.zip");
            }
            APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLname).Click();
            APEM.MocmainWindow.BPLListInternalFrame.LoadDesigner_Button.Click();
            Thread.Sleep(3000);
            APEM.DesignEditorWindow.PFCDesignAppInternalFrame.mainWindow.DoubleClick();
            APEM.DesignEditorWindow.Build.Compile.Select();
            Thread.Sleep(2000);
            MOC_Fuction.AddReason();
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "CompileError.PNG");
            Base_Assert.AreEqual(APEM.DesignCompilationWindow._UFT_Window.IsEnabled, true);
            Thread.Sleep(3000);
            var listCompileMeaasge1 = APEM.DesignCompilationWindow.ErrorList._UFT_IList.GetVisibleText();
            Console.WriteLine(listCompileMeaasge1);
            Base_Assert.IsTrue(listCompileMeaasge1.Contains("E334:Method not found in method the expression main.Refreshable!"));
            APEM.DesignCompilationWindow.Close();
            MOC_Fuction.DesignEditorClose();
            Thread.Sleep(3000);
            APEM.MocmainWindow.BPLListInternalFrame.MakeUsable_Button.ClickSignle();
            Thread.Sleep(3000);
            APEM.MocmainWindow.GetSnapshot(Resultpath + "VerifyError.PNG");
            Base_Assert.AreEqual(APEM.MocmainWindow.DesignVerificationInternalFrame._UFT_InterFrame.IsEnabled, true);
            var listCompileMeaasge2 = APEM.MocmainWindow.DesignVerificationInternalFrame.ErrorList._UFT_IList.GetVisibleText();
            Console.WriteLine(listCompileMeaasge2);
            Base_Assert.IsTrue(listCompileMeaasge2.Contains("Error:E334:Method not found in method the expression main.RefreshableDisplayLabelO"));
            APEM.MocmainWindow.DesignVerificationInternalFrame.ErrorList._UFT_IList.ActivateItem("Error:E334:Method not found in method the expression main.RefreshableDisplayLabel0 , near (@ @ @ main.RefreshableDisplayLabel0.exp:=time.now())");
            Thread.Sleep(3000);
            Base_Assert.IsTrue(APEM.DesignEditorWindow._UFT_Window.IsEnabled);
            MOC_Fuction.DesignEditorClose();
            Thread.Sleep(3000);
            APEM.MocmainWindow.DesignVerificationInternalFrame._UFT_InterFrame.Close();

        }

    }
}