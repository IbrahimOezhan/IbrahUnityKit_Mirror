using UnityEngine;
using UnityEngine.Events;

namespace IbrahKit
{
    public class UI_Menu_Item_Button_Text : MonoBehaviour
    {
        [SerializeField] private UI_Selectable selec;
        [SerializeField] private UI_Text_Modifier text;

        public UnityEvent Initialize(string value)
        {
            if (text == null)
            {
                Debug.LogError("Text is null");
                return new();
            }

            if (text is UI_Localization local)
            {
                local.SetKey(value);
            }

            if (text is UI_Text_Setter setter)
            {
                setter.SetText(value);
            }

            if (selec == null)
            {
                Debug.LogWarning($"{nameof(selec)} is null. Passing new unity event");
                return new();
            }

            return selec.GetStateController().GetOnPressSuccess();
        }
    }
}