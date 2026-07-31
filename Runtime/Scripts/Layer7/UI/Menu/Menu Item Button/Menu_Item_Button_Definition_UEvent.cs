#region

using UnityEngine;
using UnityEngine.Events;

#endregion

namespace IbrahKit.UI.Menu
{
    public class Menu_Item_Button_Definition_UEvent : Menu_Item_Button_Definition
    {
        [SerializeField] private UnityEvent unityEvent;

        protected override bool AfterSpawn(RectTransform parent, UI_Menu menu, Menu_Item_Button spawnedButton)
        {
            spawnedButton.GetSelectable().GetStateController().GetOnPressSuccess().AddListener(unityEvent.Invoke);

            return true;
        }
    }
}