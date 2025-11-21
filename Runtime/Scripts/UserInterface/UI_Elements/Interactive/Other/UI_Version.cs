using UnityEngine;

namespace IbrahKit
{
    public class UI_Version : MonoBehaviour, IMenuUpdateBase
    {
        [SerializeField] private UI_Interactive localization;

        public void OnMenuInit()
        {
            if (localization != null && localization.TryGet(out UI_Interactive_Extension_Localization result))
            {
                result.SetParam(Application.version);

                return;
            }
        }
    }
}