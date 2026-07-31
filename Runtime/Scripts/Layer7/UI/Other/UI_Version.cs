#region

using IbrahKit.UI.Generic;
using IbrahKit.UI.Modifier;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    public class UI_Version : MonoBehaviour, IUIInit
    {
        [SerializeField] private UI_Modifier localization;

        public void OnMenuInitBottomUp()
        {
            if (localization != null && localization.TryGetExtension(out UI_Modifier_Extension_Localization result))
            {
                result.SetParam(Application.version);

                return;
            }
        }

        public void OnMenuInitTopDown()
        {
        }
    }
}