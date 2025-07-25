using TMPro;
using UnityEngine;

namespace IbrahKit
{
    public class UI_Text_Setter_TMP : UI_Text_Setter
    {
        [SerializeField] private TextMeshProUGUI text;

        protected override void Init()
        {
            if (text == null && !TryGetComponent(out text))
            {
                return;
            }

            base.Init();
        }

        public override void SetText(string text)
        {
            if (!IsInitialized()) return;

            this.text.text = text;

            UpdateUI();
        }
    }
}