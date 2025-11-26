using Sirenix.OdinInspector;
using UnityEngine;

namespace IbrahKit.UI
{
    [System.Serializable]
    public class Menu_Item_Menu : Menu_Item_Button_Base
    {
        [SerializeField] private Menu_Change_Type changeType;

        [ShowIf(nameof(changeType), Menu_Change_Type.REFERENCE), SerializeField] private UI_Menu menuReference;

        protected override bool TrySpawnProPro(RectTransform parent, UI_Menu menu, UI_Menu_Item_Button_Text spawnedButton)
        {
            switch (changeType)
            {
                case Menu_Change_Type.REFERENCE:
                    spawnedButton.GetSelectable().GetStateController().GetOnPressSuccess().AddListener(() =>
                    {
                        menu.GetStateController().Transition<Menu_Transition_Instant>(menuReference);
                    });
                    break;
            }

            return true;
        }

        private enum Menu_Change_Type
        {
            REFERENCE,
        }
    }
}