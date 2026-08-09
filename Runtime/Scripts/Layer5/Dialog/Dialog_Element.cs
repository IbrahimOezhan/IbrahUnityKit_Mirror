#region

using System;
using IbrahKit.Localization;
using UnityEngine;

#endregion

namespace IbrahKit.Dialog
{
    [Serializable]
    public class Dialog_Element
    {
        [SerializeField] private Local_Key key;

        public string GetString()
        {
             Local_Manager.TryGetString(key,out string result, "fallback");
             return result;
        }
    }
}