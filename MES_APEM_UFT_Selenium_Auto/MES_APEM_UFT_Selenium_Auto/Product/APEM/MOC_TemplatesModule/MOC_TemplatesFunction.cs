using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule
{
    class MOC_TemplatesFunction
    {
        public static void Importtemplates(string filename)
        {
            APEM.MocmainWindow.Templates_moudle.ClickSignle();
            Thread.Sleep(3000);
            APEM.MOCTemplatesWindow.Templates.Import.Select();
            Thread.Sleep(4000);
            APEM.MOCTemplatesWindow.FileImport.HomeButton.ClickSignle();
            Thread.Sleep(4000);
            APEM.MOCTemplatesWindow.FileImport.LookInList._UFT_IList.ActivateItem("This PC");
            APEM.MOCTemplatesWindow.FileImport.LookInList._UFT_IList.ActivateItem("Local Disk (C:)");
            string InputFile = Base_Directory.InputDir + "\\Template\\"+ filename;
            Console.WriteLine(InputFile);
            string[] sArray = InputFile.Split('\\');
            for (int i = 1; i < sArray.Length; i++)
            {
                Console.WriteLine(sArray[i].ToString());
                APEM.MOCTemplatesWindow.FileImport.LookInList._UFT_IList.ActivateItem(sArray[i].ToString());
                //APEM.MOCTemplatesWindow.FileImport.FileImportButton.ClickSignle();
            }
            if (APEM.MOCTemplatesWindow.LogWindow.IsExist(10))
            {
                APEM.MOCTemplatesWindow.LogWindow.Close();
            }
            APEM.MOCTemplatesWindow.Check_Template.ClickSignle();
            if (APEM.MOCTemplatesWindow.LogWindow.IsExist(10))
            {
                APEM.MOCTemplatesWindow.LogWindow.Close();
            }
            APEM.MOCTemplatesWindow.Execute_Template.ClickSignle();
            if (APEM.MOCTemplatesWindow.LogWindow.IsExist(360))
            {
                APEM.MOCTemplatesWindow.LogWindow.Close();
            }
            if (APEM.ExecuteTemplateDialog.IsExist())
            {
                APEM.ExecuteTemplateDialog.OKButton.Click();
            }
            APEM.MOCTemplatesWindow.Close();
            APEM.CloseDialog.YesButton.Click();

        }
    }
}