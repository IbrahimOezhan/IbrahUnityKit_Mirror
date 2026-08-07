#region

using System;
using System.Text.Json.Serialization;
using UnityEngine;

#endregion

namespace IbrahKit.Localization
{
    /// <summary>
    ///     Holds the data of a language
    /// </summary>
    [Serializable]
    public class Local_Language
    {
        [JsonInclude, SerializeField] private SystemLanguage sysLang;

        [SerializeField] private TextAsset file;

        [SerializeField] private bool skip;

        public SystemLanguage GetSys() => sysLang;

        public TextAsset GetFile() => file;

        public bool GetSkip() => skip;
    }
}