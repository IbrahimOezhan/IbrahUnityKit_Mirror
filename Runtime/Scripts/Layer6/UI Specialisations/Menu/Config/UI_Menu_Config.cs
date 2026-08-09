#region

using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    [CreateAssetMenu(fileName = "NewUIMenuConfig", menuName = "IbrahKit/UI_Menu_Config")]
    public class UI_Menu_Config : Config<UI_Menu_Config>
    {
        [SerializeField] private Menu_Item_Button menuButtonPrefab;

        [SerializeField] private Menu_Item_Button staticMenuButtonPrefab;

        public Menu_Item_Button GetMenuButton() => menuButtonPrefab;

        public Menu_Item_Button GetMenuButtonStatic() => staticMenuButtonPrefab;
    }
}