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
using WD_UFT_Selenium_Auto.Library.BaseLibrary;


namespace WD_UFT_Selenium_Auto.Product.WD
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
        public static void setOptionData()
        {
            //open option
            APRM.BatchMainWindow.SetActive();
            Keyboard.KeyDown(Keyboard.Keys.Alt);
            Keyboard.KeyDown(Keyboard.Keys.T);
            Keyboard.PressKey(Keyboard.Keys.O);
            Keyboard.KeyUp(Keyboard.Keys.Alt);
            Keyboard.KeyUp(Keyboard.Keys.T);

            //set option
            APRM.BatchMainWindow.OptionDialog.DataSource.Select(Environment.MachineName);
            APRM.BatchMainWindow.OptionDialog.DataArea.Select("WeighDispense");
            APRM.BatchMainWindow.OptionDialog.SetAsDefaultButton.Click();
            APRM.BatchMainWindow.OptionDialog.OK.Click();
            Base_logger.Info("Default area is setting with 'WeighDispense' successfully.");

        }

    }
}
