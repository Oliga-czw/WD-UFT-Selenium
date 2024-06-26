using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using HP.LFT.SDK;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Java = HP.LFT.SDK.Java;
using WPF = HP.LFT.SDK.WPF;

namespace MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary
{
    public class UFT_UiObject
    {
        public Java.IUiObject _UFT_UiObject
        {
            get;
            set;
        }
        public UFT_UiObject(Java.IUiObject editfield)
        {
            _UFT_UiObject = editfield;
        }

        public UFT_UiObject(ITestObject parentObject, string xpath)
        {
            _UFT_UiObject = UFT_Xpath.GetChildObject<Java.IUiObject>(parentObject, xpath);
        }

        public void Click()
        {
            
            _UFT_UiObject.Click();
        }

        public string Text
        {
            get
            {
                return _UFT_UiObject.Text;
            }
        }

        public string GetVisibleText()
        {
            return _UFT_UiObject.GetVisibleText();

        }

        internal void DoubleClick()
        {
            _UFT_UiObject.DoubleClick();
        }
    }

    public class UftDynamicProperty
    {
        //public static UftDynamicProperty ToProperty(dynamic dynamicProperty)
        //{
        //    return new UftDynamicProperty(dynamicProperty);
        //}

        public string Value
        {
            get;
            private set;
        }

        public int ToInt()
        {
            return Convert.ToInt32(double.Parse(Value));
        }

        /// <summary>
        /// index is based on 0
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public UftDynamicProperty Item(int index)
        {
            Console.WriteLine($"Item index {index} --- {_dynamic[index]}");
            return new UftDynamicProperty(_dynamic[index]);
        }

        public List<UftDynamicProperty> ToList()
        {
            int total = _dynamic.Count;

            Console.WriteLine($"Dynamic List count: {total}");

            List<UftDynamicProperty> resultList = new List<UftDynamicProperty>(total);
            for (int index = 0; index < total; index++)
            {
                //Console.WriteLine($"index : {index}");
                var resultItem = new UftDynamicProperty(_dynamic[index]);
                resultList.Add(resultItem);
            }

            return resultList;
        }

        public dynamic ToDynamic()
        {
            return _dynamic;
        }

        public List<UftDynamicProperty> ToEnumerator()
        {
            var enumerator = _dynamic.GetEnumerator();
            List<UftDynamicProperty> resultList = new List<UftDynamicProperty>();
            while (enumerator.MoveNext())
            {
                resultList.Add(new UftDynamicProperty(enumerator.Current));
            }

            return resultList;
        }

        public List<dynamic> ToDynamicEnumerator()
        {
            var enumerator = _dynamic.GetEnumerator();
            List<dynamic> resultList = new List<dynamic>();
            while (enumerator.MoveNext())
            {
                resultList.Add(enumerator.Current);
            }

            return resultList;
        }

        private dynamic _dynamic
        {
            get;
            set;
        }

        //private string[] _MemeberList
        //{
        //    set;
        //    get;
        //}

        public UftDynamicProperty(dynamic dynamicUftProperty)
        {
            Value = (dynamicUftProperty as object).ToString();
            _dynamic = dynamicUftProperty;
        }

        public UftDynamicProperty SubProperty(string propertyName)
        {
            //Console.WriteLine($"Sub Property: {propertyName}");
            return new UftDynamicProperty(_dynamic[propertyName]);
        }

        public UftDynamicProperty Parent => SubProperty("Parent");
        public UftDynamicProperty TemplatedParent => SubProperty("TemplatedParent");

        private string _Value(string subPropertyName)
        {
            if (Existing(subPropertyName))
            {
                return _dynamic[subPropertyName].ToString();
            }

            return null;
        }

        public string ContentValue
        {
            get
            {
                return _Value("Content");
            }
        }

        public string TextValue
        {
            get
            {
                return _Value("Text");
            }
        }

        public string ActualValue
        {
            get
            {
                return ContentValue ?? TextValue;
            }
        }

        public UftDynamicProperty ActualParent
        {
            get
            {
                return Parent ?? TemplatedParent;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public UftDynamicProperty ItemsOrChhildren
        {
            get
            {
                if (Existing("Items"))
                {
                    return SubProperty("Items");
                }

                if (Existing("Children"))
                {
                    return SubProperty("Children");
                }

                return null;
            }
        }

        public UftDynamicProperty Content
        {
            get
            {
                return SubProperty("Content");
            }
        }

        public static void FindDynamic(dynamic _dynamic, string partType, ref dynamic outDynamic)
        {
            string actualName = "!!!No Name!!!";
            if (PropertyExists(_dynamic, "Name"))
            {
                actualName = _dynamic.Name;
            }
            Console.WriteLine($"PrintInformation: {_dynamic}, Name: {actualName}");

            if (_dynamic.ToString().Contains(partType))
            {
                outDynamic = _dynamic;
                return;
            }

            if (PropertyExists(_dynamic, "Content"))
            {
                var dynamicContent = _dynamic["Content"];
                Console.WriteLine($"Content--------{dynamicContent}");
                FindDynamic(dynamicContent, partType, ref outDynamic);
            }

            if (PropertyExists(_dynamic, "Child"))
            {
                var dynamicContent = _dynamic["Child"];
                Console.WriteLine($"Child--------{dynamicContent}");
                FindDynamic(dynamicContent, partType, ref outDynamic);
            }

            dynamic dynamicChildren = null;
            int totalCount = 0;

            if (PropertyExists(_dynamic, "Items"))
            {
                dynamicChildren = _dynamic.Items;
                totalCount = dynamicChildren.Count;
                Console.WriteLine($"{dynamicChildren} --- Items Count: {totalCount}");
            }

            if (PropertyExists(_dynamic, "Children"))
            {
                dynamicChildren = _dynamic.Children;
                totalCount = dynamicChildren.Count;
                Console.WriteLine($"{dynamicChildren} --- Children Count: {totalCount}");
            }

            if (PropertyExists(_dynamic, "Panes"))
            {
                dynamicChildren = _dynamic.Panes.Items;
                totalCount = dynamicChildren.Count;
                Console.WriteLine($"{dynamicChildren} --- Panes.Items Count: {totalCount}");
            }

            for (int index = 0; index < totalCount; index++)
            {
                if (dynamicChildren[index].ToString().Contains("System.Windows.Shapes") == false)
                {
                    FindDynamic(dynamicChildren[index], partType, ref outDynamic);
                }
            }
        }


        public static bool PropertyExists(dynamic _dynamic, string propertyName)
        {
            try
            {
                var test = _dynamic[propertyName];
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static dynamic Find(dynamic _dynamic, string type)
        {
            if (PropertyExists(_dynamic, "Content"))
            {
                if (_dynamic["Content"].ToString().Trim().ToLower().Contains(type.Trim().ToLower()))
                {
                    return _dynamic["Content"];
                }
            }

            return false;
        }

        public bool Existing(string subPropertyName)
        {
            return PropertyExists(_dynamic, subPropertyName);
        }

        public void PrintProperties()
        {
            Console.WriteLine($"------- Native Class: {_dynamic.ToString()} with Dynamic Properties -------");
            DynamicObject dynamicObject = _dynamic as DynamicObject;
            var varList = dynamicObject.GetDynamicMemberNames();
            varList.ToList().ForEach(eachVar => Console.WriteLine($"{eachVar} --- {_dynamic[eachVar]} --- {_dynamic[eachVar].GetType()}"));
            Console.WriteLine($"--------------------------------");
        }

        public void PrintMethods()
        {
            Console.WriteLine($"------- Dynamic Methods -------");
            DynamicObject dynamicObject = _dynamic as DynamicObject;
            var varList = dynamicObject.GetDynamicMemberNames();
            varList.Where(anyVar => anyVar.Contains("(")).ToList()
                .ForEach(eachVar => Console.WriteLine(eachVar));
        }

        public void PrintMembers()
        {
            Console.WriteLine($"------- Dynamic Members -------");
            DynamicObject dynamicObject = _dynamic as DynamicObject;
            var varList = dynamicObject.GetDynamicMemberNames().Where(anyVar => !anyVar.Contains("("));
            varList.ToList().ForEach(eachVar => Console.WriteLine($"{eachVar}"));
        }
    }

    public static class UFT_Extention
    {
        //public static void _PrintNativeProperties(this HP.LFT.SDK.Java.IUiObjectBase _IUiObjectBase)
        //{
        //    var propertyList = _IUiObjectBase._GetPropertyList();
        //    var memberList = (_IUiObjectBase.NativeObject.MEMBERS as string[]).ToList();
        //    Console.WriteLine($"--------NativeClass: {_IUiObjectBase.NativeClass}------------");
        //    Console.WriteLine($"--------Properties Count: {propertyList.Count}, Members Count: {memberList.Count}------------");
        //    foreach (var eachProperty in propertyList)
        //    {
        //        if (exceptionNativeAttributes.Where(anyExceptionProperty => eachProperty.Contains(anyExceptionProperty)).Count() == 0)
        //        {
        //            int propertyIndex = memberList.IndexOf(eachProperty);
        //            object eachValue = _IUiObjectBase._GetNativeProperty(eachProperty);
        //            Console.WriteLine($"{propertyIndex} - {eachProperty} : {eachValue.ToString()}");
        //        }
        //    }
        //}

        public static void PrintProperties(this Java.IUiObjectBase _IUiObjectBase)
        {
            var dynamicObject = new UftDynamicProperty(_IUiObjectBase.NativeObject);
            dynamicObject.PrintProperties();
        }
        public static void Printmember(this Java.IUiObjectBase _IUiObjectBase)
        {
            var dynamicObject = new UftDynamicProperty(_IUiObjectBase.NativeObject);
            dynamicObject.PrintMembers();
        }
    }
}
