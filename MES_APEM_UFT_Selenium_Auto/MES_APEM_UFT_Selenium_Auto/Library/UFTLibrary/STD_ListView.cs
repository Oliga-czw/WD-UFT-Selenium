﻿using HP.LFT.SDK;
using HP.LFT.SDK.StdWin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary
{
    public class STD_ListView
    {
        public IListView _STD_ListView;

        public STD_ListView(IListView listView)
        {
            _STD_ListView = listView;
        }
        public STD_ListView(ITestObject parentObject, string xpath)
        {
            _STD_ListView = STD_Xpath.GetChildObject<IListView>(parentObject, xpath);
        }


        public void Select(string item)
        {
            _STD_ListView.Select(item);
        }
        public void ActivateItem(string item)
        {
            _STD_ListView.ActivateItem(item);
        }

        public IListViewItem GetItem(string item)
        {
            
            return _STD_ListView.GetItem(item);
        }
        public ReadOnlyCollection<IListViewItem> Items()
        {

            return _STD_ListView.Items;
        }
    }
}
