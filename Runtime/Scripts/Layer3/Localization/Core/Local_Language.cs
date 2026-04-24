#region

using System;
using System.Text.Json.Serialization;
using UnityEngine;

#endregion

namespace IbrahKit.Localization
{
    public partial class Local_Manager
    {
        [Serializable]
        public class Local_Language
        {
            [JsonInclude, SerializeField] private string sysLang;

            [JsonInclude, SerializeField] private string nativeLocal;

            [SerializeField] private bool skip;

            public bool IsValid(out SystemLanguage result)
            {
                return Enum.TryParse(sysLang, out result);
            }

            public SystemLanguage GetSystemLanguage()
            {
                return Enum.Parse<SystemLanguage>(sysLang);
            }

            public string GetSys()
            {
                return sysLang;
            }

            public string GetNative()
            {
                return nativeLocal;
            }

            public bool GetSkip()
            {
                return skip;
            }

            public override string ToString()
            {
                return sysLang;
            }
        }
    }
}