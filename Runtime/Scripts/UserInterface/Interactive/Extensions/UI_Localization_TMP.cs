using System.Linq;
using TMPro;
using UnityEngine;

namespace IbrahKit
{
    public class UI_Localization_TMP : UI_Localization
    {
        [SerializeField]
        private TextMeshProUGUI text;

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

            (TextMeshProUGUI text, Local_Manager manager) = GetText();

            text.text = manager.GetString(key, fallbackText, parameters.ToArray());
        }

        private (TextMeshProUGUI, Local_Manager) GetText()
        {
            if (Application.isPlaying)
            {
                return (text, Local_Manager.Instance);
            }
            else
            {
                return (text != null ? text : GetComponent<TextMeshProUGUI>(), FindFirstObjectByType<Local_Manager>());
            }
        }
    }
}