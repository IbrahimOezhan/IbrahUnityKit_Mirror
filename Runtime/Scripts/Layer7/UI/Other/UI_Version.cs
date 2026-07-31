#region

using IbrahKit.UI.Menu;
using IbrahKit.UI.Modifier;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    public class UI_Version : MonoBehaviour, IMenuInit
    {
        [SerializeField] private UI_Modifier localization;

        public void OnMenuInit(UI_Menu menu)
        {
            if (localization != null && localization.TryGetExtension(out UI_Modifier_Extension_Localization result))
            {
                result.SetParam(Application.version);

                return;
            }
        }
    }
}