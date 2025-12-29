using UnityEngine;

namespace IbrahKit.UI
{
    [System.Serializable]
    public class Menu_Item_Back : Menu_Item_Button_Base
    {
        protected override bool TrySpawnProPro(RectTransform parent, UI_Menu menu, UI_Menu_Item_Button_Text spawnedButton)
        {
            spawnedButton.GetSelectable().GetStateController().GetOnPressSuccess().AddListener(() =>
            {
                if (UI_Menu_Manager.TryGet(out UI_Menu_Manager result))
                {
                    result.TransitionBack<Menu_Transition_Instant>();
                }
            });

            return true;
        }
    }
}