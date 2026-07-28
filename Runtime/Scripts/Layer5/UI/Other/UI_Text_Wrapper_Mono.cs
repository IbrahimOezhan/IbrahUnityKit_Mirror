#region

using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace IbrahKit.UI
{
    public class UI_Text_Wrapper_Mono : MonoBehaviour, ISelfValidator
    {
        private UI_Text_Wrapper text;

        private void Awake()
        {
            text = new(gameObject);
        }

        public void Validate(SelfValidationResult result)
        {
            if (!(gameObject.GetComponent<Text>() || gameObject.GetComponent<TextMeshProUGUI>()))
            {
                result.AddError("The gameobject must contain either a legacy or tmp text component");
            }
        }

        public UI_Text_Wrapper GetText() => text;
    }
}