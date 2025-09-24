using UnityEngine;
using UnityEngine.Events;

namespace IbrahKit
{
    public class Menu_Item_UEvent : Menu_Item_Button_Base
    {
        [SerializeField] private UnityEvent unityEvent;

        public override bool Spawn(RectTransform parent, UI_Menu menu)
        {
            bool result = base.Spawn(parent, menu);

            if (!result) return false;

            spawnedButton.Initialize(value).AddListener(() => { unityEvent.Invoke(); });

            return true;
        }
    }
}

