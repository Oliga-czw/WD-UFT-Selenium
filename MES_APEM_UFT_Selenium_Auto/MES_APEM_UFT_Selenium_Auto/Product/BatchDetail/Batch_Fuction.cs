using HP.LFT.SDK;
using HP.LFT.SDK.StdWin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;


namespace MES_APEM_UFT_Selenium_Auto.Product.APRM
{
    class Batch_Fuction
    {
        public static void closeBatch()
        {
            //APRM.AFWMainWindow.Close();
            //APRM.AFWCloseDialog.Yes.Click();
            //if (APRM.AFWMainWindow.AFWCloseDialog.IsExist())
            //{
            //    APRM.AFWMainWindow.AFWCloseDialog.Yes.Click();
            //}
        }

        public static void findBatch(string ordername)
        {
            //find batch and click
            Point point = new Point(170, 5);
            Location location = new Location(Position.TopLeft, point);
            //APRM.BatchMainWindow.Toolbar.MouseMove(location);
            ClickArgs clickArgs = new ClickArgs();
            clickArgs.Location = location;
            
            APRM.BatchMainWindow.Toolbar.Click(clickArgs);
            //input ordername 
            string a = APRM.BatchMainWindow.FindBatchWindow.Textbox.Text;
            for (int i = 0; i < a.Length; i++)
            {
                APRM.BatchMainWindow.FindBatchWindow.Textbox.SendKeys(HP.LFT.SDK.Keys.Delete);
            }
            APRM.BatchMainWindow.FindBatchWindow.Textbox.SendKeys(ordername);
            APRM.BatchMainWindow.FindBatchWindow.OK.Click();
        }
        /// <summary>
        /// Input "WeighDispense" or "Batch".
        /// "WeighDispense" is deafult.
        /// </summary>
        /// <param name="Area">"WeighDispense" or "Batch"</param>
        public static void setOptionData(string Area = "WeighDispense")
        {
            //open option
            APRM.BatchMainWindow.SetActive();
            Keyboard.KeyDown(Keyboard.Keys.Alt);
            Keyboard.KeyDown(Keyboard.Keys.T);
            Keyboard.PressKey(Keyboard.Keys.O);
            Thread.Sleep(1000);
            Keyboard.KeyUp(Keyboard.Keys.Alt);
            Keyboard.KeyUp(Keyboard.Keys.T);
            Thread.Sleep(1000);
            //set option
            APRM.BatchMainWindow.OptionDialog.DataSource.Select(Environment.MachineName);
            APRM.BatchMainWindow.OptionDialog.DataArea.Select(Area);
            APRM.BatchMainWindow.OptionDialog.SetAsDefaultButton.Click();
            APRM.BatchMainWindow.OptionDialog.OK.Click();
            //log
            Console.WriteLine($"Default area is setting with '{Area}' successfully.");

        }

    }
}
