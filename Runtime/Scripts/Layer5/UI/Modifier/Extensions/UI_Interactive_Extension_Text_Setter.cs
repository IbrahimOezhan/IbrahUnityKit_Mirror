#region

using System;
using IbrahKit.Debugging;

#endregion

namespace IbrahKit.UI
{
    [Serializable]
    public class UI_Interactive_Extension_Text_Setter : UI_Interactive_Extension_Text_Modifier
    {
        public UI_Interactive_Extension_Text_Setter(UI_Interactive extension) : base(extension)
        {
            IbrahDebug.Log("Contrsuctor");
        }

        public void SetText(object value)
        {
            if (!Init()) return;

            text.SetText(value.ToString());
        }

        public void AppendText(object value)
        {
            text.Append(value.ToString());
        }

        protected override void CleanupPro()
        {
        }

        protected override void RunPro()
        {
        }
    }
}