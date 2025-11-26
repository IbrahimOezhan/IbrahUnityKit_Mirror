using IbrahKit.Debug;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IbrahKit.UI
{
    [System.Serializable]
    public class UI_Text_Wrapper
    {
        private readonly Text legacyText;

        private readonly TextMeshProUGUI tmpText;

        private readonly Mode mode;

        public UI_Text_Wrapper(GameObject target)
        {
            legacyText = target.GetComponent<Text>();

            tmpText = target.GetComponent<TextMeshProUGUI>();

            if (legacyText != null) mode = Mode.LEGACY;
            else if (tmpText != null) mode = Mode.TMP;
            else mode = Mode.NONE;
        }

        public void SetText(string value)
        {
            switch (mode)
            {
                case Mode.LEGACY:
                    legacyText.text = value;
                    break;
                case Mode.TMP:
                    tmpText.text = value;
                    break;
            }
        }

        public void Append(string value)
        {
            SetText(GetText() + value);
        }

        public string GetText()
        {
            switch (mode)
            {
                case Mode.LEGACY : return legacyText.text;
                case Mode.TMP: return tmpText.text;
            }

            return string.Empty;
        }

        public void SetColor(Color c)
        {
            switch (mode)
            {
                case Mode.LEGACY:
                    legacyText.color = c;
                    break;
                case Mode.TMP:
                    tmpText.color = c;
                    break;
            }
        }

        public Vector2 GetPreferredSize()
        {
            return mode switch
            {
                Mode.LEGACY => new(legacyText.preferredWidth, legacyText.preferredHeight),
                Mode.TMP => new(tmpText.preferredWidth, tmpText.preferredHeight),
                _ => new(),
            };
        }

        public Mode GetMode() => mode;

        public static void Validate(SelfValidationResult result, GameObject content)
        {
            Text legacyText = content.GetComponent<Text>();

            TextMeshProUGUI tmpText = content.GetComponent<TextMeshProUGUI>();

            if (legacyText && tmpText)
            {
                result.AddError("Error. Both Text Kinds Found");
            }

            if (!(legacyText || tmpText))
            {
                result.AddError("Error. No Text Kinds Found");
            }
        }

        public enum Mode
        {
            NONE,
            LEGACY,
            TMP,
        }
    }
}