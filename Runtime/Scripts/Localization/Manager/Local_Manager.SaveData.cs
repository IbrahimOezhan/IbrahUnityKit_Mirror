using IbrahKit.Save;
using System;
using System.Text.Json.Serialization;
using UnityEngine;

namespace IbrahKit.Localization
{
    public partial class Local_Manager
    {
        [Serializable]
        private class SaveData : Savable
        {
            [JsonInclude]
            private bool attemptedGetSys;

            [JsonInclude]
            private SystemLanguage currentLanguage;

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