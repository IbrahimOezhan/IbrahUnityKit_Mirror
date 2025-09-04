using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public class Menu_Item_Quit : Menu_Item_Button_Base
    {
        public override void Spawn(RectTransform parent, UI_Menu menu)
        {
            base.Spawn(parent, menu);
            spawnedButton.Initialize(value).AddListener(() =>
            {
                Application.Quit();
            });
        }
    }
}