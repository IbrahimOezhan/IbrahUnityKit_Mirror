using IbrahKit.Localization;
using UnityEngine;

namespace IbrahKit.Dialog
{

    public class Dialog_Element
    {
        [SerializeField] private Local_Key key;

        public string GetString()
        {
            return Local_Manager.GetInstance().GetString(key);
        }
    }

}