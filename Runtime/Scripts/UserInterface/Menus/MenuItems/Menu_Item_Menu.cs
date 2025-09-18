using Sirenix.OdinInspector;
using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public class Menu_Item_Menu : Menu_Item_Button_Base
    {
        [SerializeField] private Menu_Change_Type changeType;

        [ShowIf(nameof(changeType), Menu_Change_Type.REFERENCE), SerializeField] private UI_Menu menuReference;

        public override bool Spawn(RectTransform parent, UI_Menu menu)
        {
            base.Spawn(parent, menu);

            switch (changeType)
            {
                case Menu_Change_Type.REFERENCE:
                    spawnedButton.Initialize(value).AddListener(() =>
                    {
                        menu.GetStateController().Transition<Menu_Transition_Instant>(menuReference);
                    });
                    break;
            }

            return true;
        }

        public enum Menu_Change_Type
        {
            REFERENCE,
        }
    }
}