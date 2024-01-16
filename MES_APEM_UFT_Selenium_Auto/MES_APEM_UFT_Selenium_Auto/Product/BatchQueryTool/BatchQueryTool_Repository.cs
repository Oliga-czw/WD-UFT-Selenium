using HP.LFT.SDK;
using HP.LFT.SDK.WPF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using static MES_APEM_UFT_Selenium_Auto.Product.WD.ClassMainWindow;

namespace MES_APEM_UFT_Selenium_Auto.Product.SQLplus
{
    public  class BatchQueryTool
    {
        #region SQLplus windows



        public static BatchQueryToolWindow BatchQueryToolWindow => new BatchQueryToolWindow("//Window[@Text = 'Aspen Production Record Manager Batch Query Tool.*']");


        #endregion

        #region SLM Dialog
        //public static UFT_Dialog CloseDialog => new UFT_Dialog("//Dialog[@Title = 'Close the Application']");

        #endregion


        #region SQLPlus_Methods
        public static void SelectBatchOption()
        {
            //sure to release Alt
            Keyboard.KeyUp(Keyboard.Keys.Alt);
            var Option_point = BatchQueryTool.BatchQueryToolWindow._STD_Window.Location;
            Console.WriteLine(Option_point.X + "," + Option_point.Y);
            Option_point.X += 160;
            Option_point.Y += 40;
            Mouse.Click(Option_point);
            Keyboard.PressKey(Keyboard.Keys.O);
        }



        

        public static void SelectBatchArea(string BatchArea)
        {
            for (int i = 0; i < 5; i++)
            {
                BatchQueryTool.BatchQueryToolWindow.OptionsWindow.BatchArea.SendKeys(BatchArea);
                if (BatchQueryTool.BatchQueryToolWindow.OptionsWindow.BatchArea.Text == BatchArea)
                {
                    BatchQueryTool.BatchQueryToolWindow.OptionsWindow.OK.DoubleClick();
                    break;
                }
            }
        }
        public static void NewQuery()
        {
            //open new query
            BatchQueryTool.BatchQueryToolWindow.SetActive();
            Keyboard.KeyDown(Keyboard.Keys.Control);
            Keyboard.PressKey(Keyboard.Keys.N);
            Keyboard.KeyUp(Keyboard.Keys.Control);
            //dialog message
            if (BatchQueryTool.BatchQueryToolWindow.NoDefaultData_Dialog.IsExist())
            {
                BatchQueryTool.BatchQueryToolWindow.NoDefaultData_Dialog.OK.Click();
            }
            //click time
            var pointT = BatchQueryTool.BatchQueryToolWindow.ConfigQueryWindow._STD_Window.Location;
            Console.WriteLine(pointT.X + "," + pointT.Y);
            pointT.X += 210;
            pointT.Y += 40;
            Mouse.Click(pointT);
            //uncheck time
            BatchQueryTool.BatchQueryToolWindow.ConfigQueryWindow.SpecifyTimeCheckBox.Click();
            //click advance
            var point = BatchQueryTool.BatchQueryToolWindow.ConfigQueryWindow._STD_Window.Location;
            Console.WriteLine(point.X + "," + point.Y);
            point.X += 400;
            point.Y += 40;
            Mouse.Click(point);
            //send range
            BatchQueryTool.BatchQueryToolWindow.ConfigQueryWindow.Start.SendKeys("1");
            BatchQueryTool.BatchQueryToolWindow.ConfigQueryWindow.End.SendKeys("10000");
            BatchQueryTool.BatchQueryToolWindow.ConfigQueryWindow.OK.Click();
            //Execute
            Keyboard.PressKey(Keyboard.Keys.F9);
            Thread.Sleep(8000);
        }


        #endregion

    }
}


