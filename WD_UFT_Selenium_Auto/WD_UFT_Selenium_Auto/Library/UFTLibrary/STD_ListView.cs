using HP.LFT.SDK;
using HP.LFT.SDK.StdWin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WD_UFT_Selenium_Auto.Library.UFTLibrary
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

        public IListViewItem GetItem(string item)
        {
            
            return _STD_ListView.GetItem(item);
        }
    }
}
