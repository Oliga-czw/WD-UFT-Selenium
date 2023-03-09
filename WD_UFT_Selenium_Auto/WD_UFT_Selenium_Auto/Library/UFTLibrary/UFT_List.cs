using System.Collections.Generic;
using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using System.Collections.ObjectModel;

namespace WD_UFT_Selenium_Auto.Library.UFTLibrary
{
    public class UFT_ListPicker
    {
        public IUiObject _UFT_ListPicker
        {
            get;
            set;
        }
        public UFT_ListPicker(IUiObject listPicker)
        {
            _UFT_ListPicker = listPicker;
        }

        public UFT_ListPicker(ITestObject parentObject, string xpath)
        {

            _UFT_ListPicker = UFT_Xpath.GetChildObject<IUiObject>(parentObject, xpath);
        }

        public UFT_List SelectionList
        {
            get
            {
                return new UFT_List(_UFT_ListPicker, "//IList[@ObjectName = 'Part_SelectionBox' and @FullType = 'list view']");
            }
        }

        public UFT_List AvailableList
        {
            get
            {
                return new UFT_List(_UFT_ListPicker, "//IList[@ObjectName = 'Part_OptionsBox' and @FullType = 'list view']");
            }
        }

        public UFT_Button MoveRightButton
        {
            get
            {
                return new UFT_Button(_UFT_ListPicker, "//IButton[@ObjectName = 'Move Right' and @FullType = 'button']");
            }
        }
        public UFT_Button MoveLeftButton
        {
            get
            {
                return new UFT_Button(_UFT_ListPicker, "//IButton[@ObjectName = 'Move Left' and @FullType = 'button']");
            }
        }
        public UFT_Button MoveAllButton
        {
            get
            {
                return new UFT_Button(_UFT_ListPicker, "//IButton[@ObjectName = 'PART_AddAll' and @FullType = 'button']");
            }
        }
        public UFT_Button RemoveAllButton
        {
            get
            {
                return new UFT_Button(_UFT_ListPicker, "//IButton[@ObjectName = 'PART_RemoveAll' and @FullType = 'button']");
            }
        }
        public UFT_Button MoveUpButton
        {
            get
            {
                return new UFT_Button(_UFT_ListPicker, "//IButton[@ObjectName = 'Move Up' and @FullType = 'button']");
            }
        }
        public UFT_Button MoveDownButton
        {
            get
            {
                return new UFT_Button(_UFT_ListPicker, "//IButton[@ObjectName = 'Move Down' and @FullType = 'button']");
            }
        }


    }
    public class UFT_List
    {

        public IList _UFT_IList
        {
            get;
            protected set;
        }
        public UFT_List(ITestObject parentObject, string xpath)
        {

            _UFT_IList = UFT_Xpath.GetChildObject<IList>(parentObject, xpath);
        }
        //public void Select(string items)
        //{
        //    var itemlist = FindChildren<ICheckBox>();
        //    var index = _UFT_IList.Items.IndexOf(items);
        //    itemlist[index].Click();
        //}
        //public void Select(int itemIndex)
        //{
        //    var itemlist = FindChildren<ICheckBox>();
        //    itemlist[itemIndex].Set(CheckedState.Checked);
        //    Base_Assert.IsTrue(itemlist[itemIndex].State == CheckedState.Checked);
        //}

        public void SelectItems(string items)
        {
            _UFT_IList.Select(items);
        }
        public ReadOnlyCollection<IListItem> Items
        {
            get
            {
                return _UFT_IList.Items;
            }
        }
        protected TChild Describe<TChild>(IDescription description) where TChild : class, ITestObject
        {
            return _UFT_IList.Describe<TChild>(description);
        }
        //public List<T> FindChildren<T>() where T : class, ITestObject
        //{
        //    return _UFT_IList.FindChildren<T>();
        //}
        //public UFT_ListHeader Header => new UFT_ListHeader(_UFT_IList);

    }
    //public class UFT_ListHeader
    //{
    //    private IList _UFT_List;
    //    public UFT_HeaderItem UFT_HeaderItem;

    //    public UFT_ListHeader(IList uftlist)
    //    {
    //        _UFT_List = uftlist;
    //    }
    //    public IUiObject GetHeaderItem(string Name)
    //    {
    //        return UFT_Xpath.GetChildObject<IUiObject>(_UFT_List, $"//WPFUiObject[@NativeClass = 'System.Windows.Controls.GridViewColumnHeader' and @Name = '{Name}' ]");
    //    }
    //}
    //public class UFT_HeaderItem : UFT_UiObject
    //{
    //    public IUiObject _UFTHeaderItem
    //    {
    //        get;
    //        protected set;
    //    }
    //    public UFT_HeaderItem(ITestObject parentObject, string xpath) : base(parentObject, xpath)
    //    {
    //        _UFTHeaderItem = UFT_Xpath.GetChildObject<IUiObject>(parentObject, xpath);
    //    }
    //    public UFT_HeaderItem(IUiObject uiobject) : base(uiobject)
    //    {

    //        _UFTHeaderItem = uiobject;
    //    }

    //}

}
