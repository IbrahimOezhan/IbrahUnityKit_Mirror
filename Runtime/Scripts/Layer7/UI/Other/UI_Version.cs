#region

using IbrahKit.UI.Generic;
using IbrahKit.UI.Modifier;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    public class UI_Version : MonoBehaviour, IUIInit
    {
        [SerializeField] private UI_Modifier_Text_Modifier localization;

        public void OnMenuInitBottomUp()
        {
            if (localization != null)
            {
                localization.GetLocalization().SetParam(Application.version);
            }
        }

        public void OnMenuInitTopDown()
        {
        }
    }
}