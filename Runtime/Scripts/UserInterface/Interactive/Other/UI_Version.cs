using UnityEngine;

namespace IbrahKit.UI
{
    public class UI_Version : MonoBehaviour, IMenuUpdate
    {
        [SerializeField] private UI_Interactive localization;

        public void OnMenuInit(UI_Menu menu)
        {
            if (localization != null && localization.TryGet(out UI_Interactive_Extension_Localization result))
            {
                result.SetParam(Application.version);

                return;
            }
        }
    }
}