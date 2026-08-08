#region

using IbrahKit.UI.Modifier;
using IbrahKit.UI.Selectable;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

#endregion

namespace IbrahKit.UI.Menu
{
    public class Menu_Item_Button : MonoBehaviour
    {
        [SerializeField, Required] private UI_Selectable selectable;

        [FormerlySerializedAs("interactive")] [SerializeField]
        private UI_Modifier_Text_Modifier modifier;

        public UI_Modifier_Text_Modifier GetModifier() => modifier;

        public UI_Selectable GetSelectable() => selectable;
    }
}