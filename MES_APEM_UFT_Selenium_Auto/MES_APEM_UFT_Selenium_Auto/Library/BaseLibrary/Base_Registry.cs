using Microsoft.Win32;
using System;

namespace MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary
{
    class Base_Registry
    {

        /// <summary>
        /// Registry Editor HKey local Machine
        /// </summary>
        public RegistryKey HKLM
        {
            get
            {
                RegistryKey KeyBase = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                return KeyBase;
            }
        }

        /// <summary>
        /// Registry Editor HKey Current User
        /// </summary>
        public RegistryKey HKCU
        {
            get
            {
                RegistryKey KeyBase = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                return KeyBase;
            }
        }

        public string APlusVersion
        {
            get
            {
                object value = null;
                value = HKLM.OpenSubKey(@"Software\AspenTech\Aspen Plus\CurVer\").GetValue(string.Empty);

                if (value == null)
                    throw new Exception("No APlus version version found in the registry!");

                return value.ToString();
            }
        }

        /// <summary>
        /// Get Aspen product version by product name. 
        /// e.g. Aspen Plus, Aspen Properties, Aspen Custom Modeler,Aspen Dynamics
        /// </summary>
        public string GetAspenProductVersion(string productName)
        {
            object value = null;
            value = HKLM.OpenSubKey($"Software\\AspenTech\\{productName}\\CurVer\\").GetValue(string.Empty);

            if (value == null)
                throw new Exception("No product found in the registry!");

            return value.ToString();
        }

        /// <summary>
        /// Get Aspen product version by product name. 
        /// e.g. Aspen Plus, Aspen Properties, Aspen Custom Modeler,Aspen Utilities
        /// </summary>
        public string GetValueData(string productName, string valueName)
        {
            //var valueData = HKLM.OpenSubKey($"SOFTWARE\\AspenTech\\{productName}\\" + GetAspenProductVersion(productName) + @"\mm").GetValue(valueName);
            var valueData = HKLM.OpenSubKey($"SOFTWARE\\AspenTech\\{productName}").GetValue(valueName);
            if (valueData == null)
                throw new Exception("AspenTech has no key value:" + valueName + "for" + productName + "in the registry!");
            return valueData.ToString();
        }

    }
}
