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

            (bool exists, TextMeshProUGUI text, Local_Manager manager) = GetText();

            if (!exists) return;

            text.text = manager.GetString(key, fallbackText, parameters.ToArray());
        }

        private (bool,TextMeshProUGUI, Local_Manager) GetText()
        {
            TextMeshProUGUI text = Application.isPlaying ? this.text : GetComponent<TextMeshProUGUI>();

            Local_Manager.TryGet(out Local_Manager result);

            return (result != null && text != null, text, result);
        }
    }
}