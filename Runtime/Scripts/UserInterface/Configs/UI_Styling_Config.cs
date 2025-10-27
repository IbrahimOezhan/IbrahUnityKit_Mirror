using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public class UI_Styling_Config
    {
        public UI_Styling_Config()
        {

        }

        public UI_Styling_Config(Font font, TMP_FontAsset fontAsset, int fontSize, Color fontColor)
        {
            this.fontAsset = fontAsset;

            this.font = font;

            this.fontColor = fontColor;
        }

        [SerializeField] private Color fontColor;

        [SerializeField] private Font font;

        [SerializeField] private TMP_FontAsset fontAsset;

        [SerializeField] private List<ReplacementFont> replacementFonts = new();

        public (Font, Color) GetStyle()
        {
            ReplacementFont font = replacementFonts.Find(x => Local_Manager.GetInstance().GetCurrent().GetSys() == x.GetLanguage());

            if (font != null) return (font.GetFont(), fontColor);

            else return (this.font, fontColor);
        }

        public (TMP_FontAsset, Color) GetStyleTMP()
        {
            ReplacementFont font = replacementFonts.Find(x => Local_Manager.GetInstance().GetCurrent().GetSys() == x.GetLanguage());

            if (font != null) return (font.GetFontAsset(), fontColor);

            else return (fontAsset, fontColor);
        }

        private class ReplacementFont
        {
            [SerializeField] private Font font;

            [SerializeField] private TMP_FontAsset fontAsset;

            [Dropdown(Local_Manager.SYS), SerializeField] private string language;

            public Font GetFont() => font;

            public string GetLanguage() => language;

            public TMP_FontAsset GetFontAsset() => fontAsset;
        }
    }
}