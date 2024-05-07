﻿using Microsoft.Win32;
using System.Collections;


namespace PlayEuwRusClient
{
    public class SystemLanguage
    {
        public string getSystemLanguage()
        {
            string lng = "";

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop");
            if (key != null)
            {

                object anArray = key.GetValue("PreferredUILanguages");
                IEnumerable enumerable = anArray as IEnumerable;
                if (enumerable != null)
                {
                    foreach (object element in enumerable)
                    {
                        if (element.ToString() != "")
                        {
                            lng = element.ToString();
                        }
                    }
                }
            }
            key.Close();
            return lng;
        }
    }
}
