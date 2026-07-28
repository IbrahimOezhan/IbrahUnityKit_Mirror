#region

using System;
using System.Text.Json.Serialization;
using IbrahKit.Save;
using UnityEngine;

#endregion

namespace IbrahKit.Localization
{
    public partial class Local_Manager
    {
        /// <summary>
        ///     Holds the save data for the localization manager
        /// </summary>
        [Serializable]
        private class SaveData : Savable
        {
            [JsonInclude] private bool attemptedGetSys;

            [JsonInclude] private SystemLanguage currentLanguage;

            public bool SetAttempt()
            {
                bool previous = attemptedGetSys;

                attemptedGetSys = true;

                return previous;
            }

            public SystemLanguage GetLanguage()
            {
                return currentLanguage;
            }

            public void SetLanguage(SystemLanguage language)
            {
                currentLanguage = language;
            }
        }
    }
}