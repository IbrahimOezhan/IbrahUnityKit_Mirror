#region

using IbrahKit.UI.Modifier;
using IbrahKit.UI.Selectable;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

#endregion

namespace IbrahKit.UI.Menu
{
    public abstract class Menu_Item_Button : MonoBehaviour
    {
        [SerializeField, Required] private UI_Selectable selectable;

        [FormerlySerializedAs("interactive")] [SerializeField]
        private UI_Modifier modifier;

        public void Initialize(string value)
        {
            modifier.TryGetExtension(out UI_Modifier_Extension_Text_Modifier text);

            switch (text)
            {
                case UI_Modifier_Extension_Localization local:
                    local.SetKey(value);
                    break;
                case UI_Modifier_Extension_Text_Setter setter:
                    setter.SetText(value);
                    break;
            }
        }

        public UI_Selectable GetSelectable() => selectable;
    }
}