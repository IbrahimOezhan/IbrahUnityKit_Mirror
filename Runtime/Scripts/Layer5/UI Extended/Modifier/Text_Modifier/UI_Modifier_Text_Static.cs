#region

using System;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Modifier
{
    [Serializable]
    public class UI_Modifier_Text_Static
    {
        private UI_Text_Wrapper textWrapper;

        public void SetText(object value)
        {
            textWrapper.SetText((string)value);
        }

        public void AppendText(object value)
        {
            textWrapper.Append(value.ToString());
        }

        public void SetTextWrapper(UI_Text_Wrapper wrapper)
        {
            textWrapper =  wrapper;
        }
    }
}