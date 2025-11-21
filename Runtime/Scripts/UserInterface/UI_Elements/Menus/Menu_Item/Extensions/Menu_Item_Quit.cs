using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public class Menu_Item_Quit : Menu_Item_Button_Base
    {
        protected override bool TrySpawnPro(RectTransform parent, UI_Menu menu, out GameObject go)
        {
            bool result = base.TrySpawnPro(parent, menu, out go);

            if (!result) return false;

            spawnedButton.Initialize(value).AddListener(() =>
            {
                Application.Quit();
            });

            return true;
        }
    }
}