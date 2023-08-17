using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary
{
    public static class UFT_Xpath
    {
        public static List<Dictionary<string, string>> DescriptionDictionaryList
        {
            get;
            private set;
        }
        public static List<UiObjectDescription> DescriptionList
        {
            get;
            private set;
        }
        public static List<WindowDescription> WindowDescriptionList
        {
            get;
            private set;
        }

        public static T GetDesktopObject<T>(string xpath, bool throwObjectNotFoundException = true, int timeout = 1000) where T : class, ITopLevelObject
        {
            T desktopObject = null;

            DescriptionDictionaryList = XpathToDictionaryList(xpath);
            DescriptionList = DictionaryToDescriptionList(DescriptionDictionaryList);

            for (int i = 0; i < 30; i++)
            {
                foreach (IDescription description in DescriptionList)
                {
                    desktopObject = Desktop.Describe<T>(description);


                    if (desktopObject != null)
                        return desktopObject;
                    else
                        Thread.Sleep(timeout);
                }

            }

            if (throwObjectNotFoundException)
            {
                throw new Exception($"The operation timed out. Please check xpath format and object are valid");
            }
            else
            {
                return desktopObject;
            }

        }
        //Window
        public static T GetDesktopWindow<T>(string xpath, bool throwObjectNotFoundException = true, int timeout = 1000) where T : class, ITopLevelObject
        {
            T desktopObject = null;

            DescriptionDictionaryList = XpathToDictionaryList(xpath);
            WindowDescriptionList = WindowDictionaryToDescriptionList(DescriptionDictionaryList);

            for (int i = 0; i < 30; i++)
            {
                foreach (IDescription description in WindowDescriptionList)
                {
                    desktopObject = Desktop.Describe<T>(description);


                    if (desktopObject != null)
                        return desktopObject;
                    else
                        Thread.Sleep(timeout);
                }

            }

            if (throwObjectNotFoundException)
            {
                throw new Exception($"The operation timed out. Please check xpath format and object are valid");
            }
            else
            {
                return desktopObject;
            }

        }
        //Dialog
        public static T GetDesktopDialog<T>(string xpath, bool throwObjectNotFoundException = true, int timeout = 1000) where T : class, ITopLevelObject
        {
            T desktopObject = null;

            DescriptionDictionaryList = XpathToDictionaryList(xpath);
            WindowDescriptionList = WindowDictionaryToDescriptionList(DescriptionDictionaryList);

            for (int i = 0; i < 30; i++)
            {
                foreach (IDescription description in WindowDescriptionList)
                {
                    desktopObject = Desktop.Describe<T>(description);


                    if (desktopObject != null)
                        return desktopObject;
                    else
                        Thread.Sleep(timeout);
                }

            }

            if (throwObjectNotFoundException)
            {
                throw new Exception($"The operation timed out. Please check xpath format and object are valid");
            }
            else
            {
                return desktopObject;
            }

        }
        public static T GetDialogChildObject<T>(ITestObject parentObject, string xpath, bool throwObjectNotFoundException = true) where T : class, ITestObject
        {
            T childObject = null;
            List<string> partXpathList = SplitXpath(xpath);

            if (partXpathList.Count == 1)
            {
                childObject = GetDialogFromParentObject<T>(parentObject, partXpathList[0]);
                return childObject;
            }

            else if (partXpathList.Count > 1)
            {
                IDialog middleObject = GetDialogFromParentObject<IDialog>(parentObject, partXpathList[0]);
                for (int i = 1; i < partXpathList.Count - 1; i++)
                {
                    middleObject = GetDialogFromParentObject<IDialog>(middleObject, partXpathList[i]);
                }
                childObject = GetDialogFromParentObject<T>(middleObject, partXpathList[partXpathList.Count - 1]);
                return childObject;
            }

            if (throwObjectNotFoundException)
            {
                throw new Exception($"Please check xpath format and parent object are valid.");
            }
            else
            {
                return childObject;
            }
        }
        private static T GetDialogFromParentObject<T>(ITestObject parentObject, string partXpath, bool throwObjectNotFoundException = true) where T : class, ITestObject
        {
            T childObject = null;
            DescriptionDictionaryList = XpathToDictionaryList(partXpath);
            WindowDescriptionList = WindowDictionaryToDescriptionList(DescriptionDictionaryList);

            for (int i = 0; i < 30; i++)
            {
                foreach (IDescription description in WindowDescriptionList)
                {
                    childObject = parentObject.Describe<T>(description);

                    //if (childObject.Exists(1))
                    return childObject;
                }
            }
            //return childObject;
            throw new Exception($"The operation timed out. Please check the xpath ( {partXpath} ) and parent object are valid.");
        }

        public static T GetChildObject<T>(ITestObject parentObject, string xpath, bool throwObjectNotFoundException = true) where T : class, ITestObject
        {
            T childObject = null;
            List<string> partXpathList = SplitXpath(xpath);

            if (partXpathList.Count == 1)
            {
                childObject = GetObjectFromParentObject<T>(parentObject, partXpathList[0]);
                return childObject;
            }

            else if (partXpathList.Count > 1)
            {
                IUiObject middleObject = GetObjectFromParentObject<IUiObject>(parentObject, partXpathList[0]);
                for (int i = 1; i < partXpathList.Count - 1; i++)
                {
                    middleObject = GetObjectFromParentObject<IUiObject>(middleObject, partXpathList[i]);
                }
                childObject = GetObjectFromParentObject<T>(middleObject, partXpathList[partXpathList.Count - 1]);
                return childObject;
            }

            if (throwObjectNotFoundException)
            {
                throw new Exception($"Please check xpath format and parent object are valid.");
            }
            else
            {
                return childObject;
            }
        }

        private static T GetObjectFromParentObject<T>(ITestObject parentObject, string partXpath, bool throwObjectNotFoundException = true) where T : class, ITestObject
        {
            T childObject = null;
            DescriptionDictionaryList = XpathToDictionaryList(partXpath);
            DescriptionList = DictionaryToDescriptionList(DescriptionDictionaryList);

            for (int i = 0; i < 30; i++)
            {
                foreach (IDescription description in DescriptionList)
                {
                    childObject = parentObject.Describe<T>(description);

                    //if (childObject.Exists(1))
                        return childObject;
                }
            }
            //return childObject;
            throw new Exception($"The operation timed out. Please check the xpath ( {partXpath} ) and parent object are valid.");
        }

        public static List<string> SplitXpath(string xpath)
        {
            List<string> partXpathList = new List<string>(xpath.Split(']'));
            partXpathList.RemoveAt(partXpathList.Count - 1);
            for (int i = 0; i < partXpathList.Count; i++)
            {
                partXpathList[i] = partXpathList[i] + "]";
            }
            return partXpathList;
        }
        public static List<string> SingleXpathToDescription(string xpath)
        {
            List<string> objectDescription = new List<string>();
            string propName = xpath.Split(new char[2] { '@', '=' })[1].Trim();
            string propValue = xpath.Split('\'')[1].Trim();

            if (propValue.Contains("@") || propValue.Contains("]"))
                throw new Exception("The format of xpath is not valid");

            objectDescription.Add(propName);
            objectDescription.Add(propValue);

            return objectDescription;
        }
        public static List<Dictionary<string, string>> XpathToDictionaryList(string partXpath)
        {
            //Regex number = new Regex(@"[\d]");
            Dictionary<string, string> descriptionDictionary = new Dictionary<string, string>();
            List<Dictionary<string, string>> descriptionDictionaryList = new List<Dictionary<string, string>>();

            //if (number.IsMatch(xpath) && !xpath.Contains("@"))//it means just use index as indentification
            //{
            //    string[] temp = xpath.Split(new char[2] { '[', ']' });
            //    string index = temp[temp.Length - 2];
            //    descriptionDictionary.Add("Index", index);
            //    descriptionDictionaryList.Add(descriptionDictionary);
            //}

            if (partXpath.Contains("' and @") || partXpath.Contains("' or @"))
            {
                List<string> xpathList1 = new List<string>(Regex.Split(partXpath, "' or "));
                xpathList1.ForEach(item1 =>
                {
                    List<string> xpathList2 = new List<string>(Regex.Split(item1, "' and "));
                    xpathList2.ForEach(item2 =>
                    {
                        List<string> temp = SingleXpathToDescription(item2);

                        descriptionDictionary.Add(temp[0], temp[1]);
                    });


                    Dictionary<string, string> tempDictionary = new Dictionary<string, string>();

                    List<string> propNameList = descriptionDictionary.Keys.ToList();
                    foreach (string propName in propNameList)
                    {
                        string propValue = descriptionDictionary[propName];
                        tempDictionary.Add(propName, propValue);
                    }

                    descriptionDictionaryList.Add(tempDictionary);
                    descriptionDictionary.Clear();
                });
            }
            else
            {
                List<string> temp = SingleXpathToDescription(partXpath);
                descriptionDictionary.Add(temp[0], temp[1]);
                descriptionDictionaryList.Add(descriptionDictionary);
            }
            return descriptionDictionaryList;
        }
        public static List<UiObjectDescription> DictionaryToDescriptionList(List<Dictionary<string, string>> descriptionDictionaryList)
        {
            List<UiObjectDescription> objectDescriptionList = new List<UiObjectDescription>();
            for (int i = 0; i < descriptionDictionaryList.Count; i++)
            {
                UiObjectDescription objectDescription = new UiObjectDescription();
                Dictionary<string, string> descriptionDictionary = descriptionDictionaryList[i];
                List<string> propNameList = descriptionDictionary.Keys.ToList();
                foreach (string propName in propNameList)
                {
                    string propValue = descriptionDictionary[propName];
                    if (propValue.Contains("*"))
                    {
                        propValue = propValue.Replace("*", ".*");
                        switch (propName)
                        {
                            case "Index":
                                objectDescription.Index = uint.Parse(propValue);
                                break;
                            case "Text":
                                objectDescription.Text = As.RegExp(propValue);
                                break;
                            case "ObjectName":
                                objectDescription.ObjectName = As.RegExp(propValue);
                                break;
                            case "TagName":
                                objectDescription.TagName = As.RegExp(propValue);
                                break;
                            case "Path":
                                objectDescription.Path = As.RegExp(propValue);
                                break;
                            case "NativeClass":
                                objectDescription.NativeClass = As.RegExp(propValue);
                                break;
                            case "AttachedText":
                                objectDescription.AttachedText = As.RegExp(propValue);
                                break;
                            case "Label":
                                objectDescription.Label = propValue;
                                break;
                            case "IsFocused":
                                objectDescription.IsFocused = Convert.ToBoolean(propValue);
                                break;
                            case "IsWrapped":
                                objectDescription.IsWrapped = Convert.ToBoolean(propValue);
                                break;
                            default:
                                throw new Exception($"Cannot Find the Property Name '{propName}', please confirm the format of xpath is valid");
                        }
                    }
                    else
                    {
                        switch (propName)
                        {
                            case "Index":
                                objectDescription.Index = uint.Parse(propValue);
                                break;
                            case "Text":
                                objectDescription.Text = propValue;
                                break;
                            case "ObjectName":
                                objectDescription.ObjectName = propValue;
                                break;
                            case "TagName":
                                objectDescription.TagName = As.RegExp(propValue);
                                break;
                            case "Path":
                                objectDescription.Path = As.RegExp(propValue);
                                break;
                            case "NativeClass":
                                objectDescription.NativeClass = propValue;
                                break;
                            case "AttachedText":
                                objectDescription.AttachedText = As.RegExp(propValue);
                                break;
                            case "Label":
                                objectDescription.Label = propValue;
                                break;
                            case "IsFocused":
                                objectDescription.IsFocused = Convert.ToBoolean(propValue);
                                break;
                            case "IsWrapped":
                                objectDescription.IsWrapped = Convert.ToBoolean(propValue);
                                break;
                            default:
                                throw new Exception($"Cannot Find the Property Name '{ propName }', please confirm the format of xpath is valid");
                        }
                    }
                }
                objectDescriptionList.Add(objectDescription);
            }
            return objectDescriptionList;
        }
        //windowDescription
        public static List<WindowDescription> WindowDictionaryToDescriptionList(List<Dictionary<string, string>> descriptionDictionaryList)
        {
            List<WindowDescription> objectDescriptionList = new List<WindowDescription>();
            for (int i = 0; i < descriptionDictionaryList.Count; i++)
            {
                WindowDescription objectDescription = new WindowDescription();
                Dictionary<string, string> descriptionDictionary = descriptionDictionaryList[i];
                List<string> propNameList = descriptionDictionary.Keys.ToList();
                foreach (string propName in propNameList)
                {
                    string propValue = descriptionDictionary[propName];
                    if (propValue.Contains("*"))
                    {
                        propValue = propValue.Replace("*", ".*");
                        switch (propName)
                        {
                            case "Index":
                                objectDescription.Index = uint.Parse(propValue);
                                break;
                            case "Title":
                                objectDescription.Title = As.RegExp(propValue);
                                break;
                            case "ObjectName":
                                objectDescription.ObjectName = As.RegExp(propValue);
                                break;
                            case "TagName":
                                objectDescription.TagName = As.RegExp(propValue);
                                break;
                            case "Path":
                                objectDescription.Path = As.RegExp(propValue);
                                break;
                            case "NativeClass":
                                objectDescription.NativeClass = As.RegExp(propValue);
                                break;
                            case "AttachedText":
                                objectDescription.AttachedText = As.RegExp(propValue);
                                break;
                            case "IsFocused":
                                objectDescription.IsFocused = Convert.ToBoolean(propValue);
                                break;
                            default:
                                throw new Exception($"Cannot Find the Property Name '{propName}', please confirm the format of xpath is valid");
                        }
                    }
                    else
                    {
                        switch (propName)
                        {
                            case "Index":
                                objectDescription.Index = uint.Parse(propValue);
                                break;
                            case "Title":
                                objectDescription.Title = propValue;
                                break;
                            case "ObjectName":
                                objectDescription.ObjectName = propValue;
                                break;
                            case "TagName":
                                objectDescription.TagName = As.RegExp(propValue);
                                break;
                            case "Path":
                                objectDescription.Path = As.RegExp(propValue);
                                break;
                            case "NativeClass":
                                objectDescription.NativeClass = propValue;
                                break;
                            case "AttachedText":
                                objectDescription.AttachedText = As.RegExp(propValue);
                                break;
                            case "IsFocused":
                                objectDescription.IsFocused = Convert.ToBoolean(propValue);
                                break;
                            default:
                                throw new Exception($"Cannot Find the Property Name '{ propName }', please confirm the format of xpath is valid");
                        }
                    }
                }
                objectDescriptionList.Add(objectDescription);
            }
            return objectDescriptionList;
        }
        //DialogDescription
        public static List<DialogDescription> WindowDialogToDescriptionList(List<Dictionary<string, string>> descriptionDictionaryList)
        {
            List<DialogDescription> objectDescriptionList = new List<DialogDescription>();
            for (int i = 0; i < descriptionDictionaryList.Count; i++)
            {
                DialogDescription objectDescription = new DialogDescription();
                Dictionary<string, string> descriptionDictionary = descriptionDictionaryList[i];
                List<string> propNameList = descriptionDictionary.Keys.ToList();
                foreach (string propName in propNameList)
                {
                    string propValue = descriptionDictionary[propName];
                    if (propValue.Contains("*"))
                    {
                        propValue = propValue.Replace("*", ".*");
                        switch (propName)
                        {
                            case "Title":
                                objectDescription.Title = As.RegExp(propValue);
                                break;
                            case "ObjectName":
                                objectDescription.ObjectName = As.RegExp(propValue);
                                break;
                            case "TagName":
                                objectDescription.TagName = As.RegExp(propValue);
                                break;
                            case "Path":
                                objectDescription.Path = As.RegExp(propValue);
                                break;
                            case "NativeClass":
                                objectDescription.NativeClass = As.RegExp(propValue);
                                break;
                            case "AttachedText":
                                objectDescription.AttachedText = As.RegExp(propValue);
                                break;
                            case "IsFocused":
                                objectDescription.IsFocused = Convert.ToBoolean(propValue);
                                break;
                            default:
                                throw new Exception($"Cannot Find the Property Name '{propName}', please confirm the format of xpath is valid");
                        }
                    }
                    else
                    {
                        switch (propName)
                        {
                            case "Index":
                                objectDescription.Index = uint.Parse(propValue);
                                break;
                            case "Title":
                                objectDescription.Title = propValue;
                                break;
                            case "ObjectName":
                                objectDescription.ObjectName = propValue;
                                break;
                            case "TagName":
                                objectDescription.TagName = As.RegExp(propValue);
                                break;
                            case "Path":
                                objectDescription.Path = As.RegExp(propValue);
                                break;
                            case "NativeClass":
                                objectDescription.NativeClass = propValue;
                                break;
                            case "AttachedText":
                                objectDescription.AttachedText = As.RegExp(propValue);
                                break;
                            case "IsFocused":
                                objectDescription.IsFocused = Convert.ToBoolean(propValue);
                                break;
                            default:
                                throw new Exception($"Cannot Find the Property Name '{ propName }', please confirm the format of xpath is valid");
                        }
                    }
                }
                objectDescriptionList.Add(objectDescription);
            }
            return objectDescriptionList;
        }
        public static void ToTestTargetXpathIsValid(string targetXpath)
        {
            var dictionaryList = UFT_Xpath.XpathToDictionaryList(targetXpath);

            foreach (var dictionary in dictionaryList)
            {
                List<string> propNameList = dictionary.Keys.ToList();

                foreach (string propName in propNameList)
                {
                    string propValue = dictionary[propName];
                    Console.WriteLine($"{propName} = {propValue}");
                }
            }
        }
    }
}
