using UnityEngine;
using UnityEngine.UI;

namespace IbrahKit
{
    public class UI_Localization_Legacy : UI_Localization
    {
        [SerializeField]
        private Text text;

        protected override void Init()
        {
            if (text == null && !TryGetComponent(out text))
            {
                return;
            }
            base.Init();
        }

        public override void Execute()
        {
            if (!IsInitialized()) return;

            (Text text, Local_Manager manager) = GetText();

            if (manager == null)
            {
                return;
            }

            text.text = manager.GetString(key, fallbackText, parameters.ToArray());
        }

        private (Text, Local_Manager) GetText()
        {
            if (Application.isPlaying)
            {
                return (text, Local_Manager.Instance);
            }
            else
            {
                Local_Manager manager = FindFirstObjectByType<Local_Manager>();

                if (manager == null)
                {
                    UnityEngine.Debug.LogWarning("No Localization_Manager found in scene.");
                    return (text, null);
                }

                return (text != null ? text : GetComponent<Text>(), manager);
            }
        }
    }
}