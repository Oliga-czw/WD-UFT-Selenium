using HP.LFT.SDK.StdWin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;

namespace WD_UFT_Selenium_Auto.Product.WD
{
    class AFW_Fuction
    {
        public static void closeAFW()
        {
            WD.AFWMainWindow.Close();
            WD.AFWCloseDialog.Yes.Click();
            if (WD.AFWMainWindow.AFWCloseDialog.IsExist())
            {
                WD.AFWMainWindow.AFWCloseDialog.Yes.Click();

            }
        }

        public static void addRole(string role,string account)
        {
            if (WD.AFWMainWindow.AFWSubWindow.TreeView.GetNode("Console Root;AFW Security Manager").IsExpanded)
            {
                WD.AFWMainWindow.AFWSubWindow.TreeView._STD_TreeView.Select("Console Root;AFW Security Manager;Roles");
            }
            else
            {
                WD.AFWMainWindow.AFWSubWindow.TreeView.GetNode("Console Root;AFW Security Manager").Expand();

                WD.AFWMainWindow.AFWSubWindow.TreeView._STD_TreeView.Select("Console Root;AFW Security Manager;Roles");
            }
            WD.AFWMainWindow.AFWSubWindow.ListView.Select(role);
            WD.AFWMainWindow.toolbar.PressButton("6");
            WD.AFWPropertyDialog.tab.Select("Members");
            ReadOnlyCollection<IListViewItem> items = WD.AFWPropertyDialog.ListView._STD_ListView.Items;
            bool need_add = true;
            foreach (IListViewItem item in items)
            {
                Console.WriteLine(item.Text.ToLower());
                if (item.Text.ToLower() == account)
                {
                    need_add = false;
                    break;
                }
            }
            if (need_add)
            {
                WD.AFWPropertyDialog.Add.Click();
                WD.AFWSelectUserDialog.inputbox.SendKeys(account);
                WD.AFWSelectUserDialog.OK.Click();
                Thread.Sleep(5000);
                Base_logger.Message($"Add {account} sucessfully.");
            }
            else
            {
                Base_logger.Message($"{account} exited.");
            }
            WD.AFWPropertyDialog.OK.Click();
        }
        public static void removeRole(string role, string account)
        {
            if (WD.AFWMainWindow.AFWSubWindow.TreeView.GetNode("Console Root;AFW Security Manager").IsExpanded)
            {
                WD.AFWMainWindow.AFWSubWindow.TreeView._STD_TreeView.Select("Console Root;AFW Security Manager;Roles");
            }
            else
            {
                WD.AFWMainWindow.AFWSubWindow.TreeView.GetNode("Console Root;AFW Security Manager").Expand();

                WD.AFWMainWindow.AFWSubWindow.TreeView._STD_TreeView.Select("Console Root;AFW Security Manager;Roles");
            }
            WD.AFWMainWindow.AFWSubWindow.ListView.Select(role);
            WD.AFWMainWindow.toolbar.PressButton("6");
            Thread.Sleep(1000);
            WD.AFWPropertyDialog.tab.Select("Members");
            ReadOnlyCollection<IListViewItem> items = WD.AFWPropertyDialog.ListView._STD_ListView.Items;
            bool need_remove = false;
            string user = "";
            foreach (IListViewItem item in items)
            {
                Console.WriteLine(item.Text.ToLower());
                if (item.Text.ToLower() == account)
                {
                    user = item.Text;
                    need_remove = true;
                    break;
                }
            }
            if (need_remove)
            {
                WD.AFWPropertyDialog.ListView.Select(user);
                WD.AFWPropertyDialog.Remove.Click();
                Thread.Sleep(1000);
                Base_logger.Message($"Remove {account} sucessfully.");
            }
            else
            {
                Base_logger.Message($"{account} not exited.");
            }
            WD.AFWPropertyDialog.OK.Click();
        }
    }
}
