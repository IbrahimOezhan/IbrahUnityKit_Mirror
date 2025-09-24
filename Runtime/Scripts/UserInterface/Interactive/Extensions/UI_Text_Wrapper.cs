using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Text_Wrapper
{
    private Text legacyText;
    private TextMeshProUGUI tmpText;

    private Mode mode;

    public UI_Text_Wrapper(GameObject target)
    {
        legacyText = target.GetComponent<Text>();
        tmpText = target.GetComponent<TextMeshProUGUI>();

        if (legacyText && tmpText)
        {
            Debug.LogWarning("Error. Both Text Kinds Found. Selecting TMP");
        }

        mode = legacyText ? Mode.LEGACY : (tmpText) ? Mode.TMP : Mode.NONE;
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
        switch (mode)
        {
            case Mode.LEGACY:
                return new(legacyText.preferredWidth, legacyText.preferredHeight);
            case Mode.TMP:
                return new(tmpText.preferredWidth, tmpText.preferredHeight);
        }

        return new();
    }

    private enum Mode
    {
        NONE,
        LEGACY,
        TMP,
    }
}
