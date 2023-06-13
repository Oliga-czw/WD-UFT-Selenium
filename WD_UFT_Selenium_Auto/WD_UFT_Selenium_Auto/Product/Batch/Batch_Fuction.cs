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
            //WD.AFWMainWindow.Close();
            //WD.AFWCloseDialog.Yes.Click();
            //if (WD.AFWMainWindow.AFWCloseDialog.IsExist())
            //{
            //    WD.AFWMainWindow.AFWCloseDialog.Yes.Click();
            //}
        }

        public static void findBatch(string ordername)
        {
            //find batch and click
            Point point = new Point(170, 5);
            Location location = new Location(Position.TopLeft, point);
            //WD.BatchMainWindow.Toolbar.MouseMove(location);
            ClickArgs clickArgs = new ClickArgs();
            clickArgs.Location = location;
            WD.BatchMainWindow.Toolbar.Click(clickArgs);
            //input ordername 
            string a = WD.BatchMainWindow.FindBatchWindow.Textbox.Text;
            for (int i = 0; i < a.Length; i++)
            {
                WD.BatchMainWindow.FindBatchWindow.Textbox.SendKeys(HP.LFT.SDK.Keys.Delete);
            }
            WD.BatchMainWindow.FindBatchWindow.Textbox.SendKeys(ordername);
            WD.BatchMainWindow.FindBatchWindow.OK.Click();
        }

        
    }
}
