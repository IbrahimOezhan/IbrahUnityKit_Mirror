using UnityEngine;

namespace IbrahKit
{
    public class UI_Version : UI_Base
    {
        [SerializeField] private UI_Localization localization;

        public override void MenuUpdate()
        {

        }

        public override void OnMenuEnabled()
        {
            if (localization == null && !TryGetComponent(out localization))
            {
                Debug.LogWarning($"No component of type {nameof(UI_Localization)} attached to the game object");
                return;
            }

            localization.SetParam(Application.version);
        }
    }
}