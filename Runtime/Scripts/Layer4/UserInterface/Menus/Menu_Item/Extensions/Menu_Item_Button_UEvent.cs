#region

using UnityEngine;
using UnityEngine.Events;

#endregion

namespace IbrahKit.UI
{
    public class Menu_Item_Button_UEvent : Menu_Item_Button_Base
    {
        [SerializeField] private UnityEvent unityEvent;

        protected override bool TrySpawnProPro(RectTransform parent, UI_Menu menu,
            UI_Menu_Item_Button_Text spawnedButton)
        {
            spawnedButton.GetSelectable().GetStateController().GetOnPressSuccess()
                .AddListener(() => { unityEvent.Invoke(); });

            return true;
        }
    }
}