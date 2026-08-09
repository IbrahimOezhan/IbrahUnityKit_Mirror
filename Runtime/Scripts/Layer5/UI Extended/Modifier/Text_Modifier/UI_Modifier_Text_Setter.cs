#region

using System;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Modifier
{
    [Serializable]
    public class UI_Modifier_Text_Setter
    {
        [SerializeField] private UI_Modifier_Text_Modifier modifier;

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