#region

using System;

#endregion

namespace IbrahKit.UI.Modifier
{
    [Serializable]
    public class UI_Modifier_Text_Setter
    {
        private UI_Modifier_Text_Modifier modifier;

        public UI_Modifier_Text_Setter(UI_Modifier_Text_Modifier modifier)
        {
            this.modifier = modifier;
        }

        public void SetText(object value)
        {
            modifier.GetTextWrapper().SetText((string)value);
        }

        public void AppendText(object value)
        {
            modifier.GetTextWrapper().Append(value.ToString());
        }
    }
}