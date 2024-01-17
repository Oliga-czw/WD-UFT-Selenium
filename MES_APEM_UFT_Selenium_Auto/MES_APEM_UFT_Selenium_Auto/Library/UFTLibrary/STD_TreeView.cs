using HP.LFT.SDK;
using HP.LFT.SDK.StdWin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary
{
    public class STD_TreeView
    {
        public ITreeView _STD_TreeView;
        public STD_TreeView(ITreeView treeView)
        {
            _STD_TreeView = treeView;
        }
        public STD_TreeView(ITestObject parentObject, string xpath)
        {
            _STD_TreeView =  STD_Xpath.GetChildObject<ITreeView>(parentObject, xpath);
        }

        public STD_TreeNode Node(string item)
        {
            return new STD_TreeNode(_STD_TreeView, item);
        }

        public ITreeViewNode GetNode(string item)
        {
            return _STD_TreeView.GetNode(item);
        }

        public void Select(string item)
        {
            _STD_TreeView.Select(item);
        }

        public void Click()
        {
            _STD_TreeView.Click();

        }

        
    }
    public class STD_TreeNode
    {
        private ITreeView _STD_TreeView;
        private string _item;
        private const char SemiColonDelimited = ';';

        public STD_TreeNode(ITreeView treeView, string item)
        {
            _STD_TreeView = treeView;
            _item = item;
        }
        public string Path => _STD_TreeView.GetNode(_item).Path;
        public bool IsSelected => _STD_TreeView.GetNode(_item).IsSelected;
        public STD_TreeNode Select()
        {
            if (_item.Contains(SemiColonDelimited))
            {
                //var itemList = _item.Split(SemiColonDelimited);
                //var expandItems = itemList.Where(anyItem => anyItem != itemList.Last());

                string[] itemList = _item.Split(SemiColonDelimited);
                itemList = itemList.Take(itemList.Count() - 1).ToArray();

                string epxandItemPath = "";
                foreach (string eachItem in itemList)
                {
                    epxandItemPath = epxandItemPath + eachItem;
                    for (int i = 0; i < 10; i++)
                    {
                        _STD_TreeView.Select(epxandItemPath);
                        if (_STD_TreeView.GetNode(epxandItemPath).IsSelected)
                        {
                            break;
                        }
                    }
                    _STD_TreeView.GetNode(epxandItemPath).Expand();
                    epxandItemPath = epxandItemPath + ";";
                }
            }

            _STD_TreeView.Select(_item);
            _STD_TreeView.SendKeys(Keys.Return);
            return this;
        }
        public void Click()
        {
            _STD_TreeView.Click();

        }
        public void ExpandAll()
        {
            _STD_TreeView.GetNode(_item).ExpandAll();
            Thread.Sleep(1000);
        }
        public void Collapse()
        {
            _STD_TreeView.GetNode(_item).Collapse();
            Thread.Sleep(1000);
        }
        //public void ShowContextMenu()
        //{
        //    _STD_TreeView.._ShowContextMenu(_item);
        //}
    }
}
