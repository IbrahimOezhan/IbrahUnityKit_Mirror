using UnityEngine;
using UnityEngine.Events;

namespace IbrahKit
{
    public class Menu_Item_Button_UEvent : Menu_Item_Button_Base
    {
        [SerializeField] private UnityEvent unityEvent;

        protected override bool TrySpawnPro(RectTransform parent, UI_Menu menu, out GameObject go)
        {
            bool res = base.TrySpawnPro(parent, menu, out go);

            if (res) return false;

            spawnedButton.Initialize(value).AddListener(() => { unityEvent.Invoke(); });

            return true;
        }
    }
}

