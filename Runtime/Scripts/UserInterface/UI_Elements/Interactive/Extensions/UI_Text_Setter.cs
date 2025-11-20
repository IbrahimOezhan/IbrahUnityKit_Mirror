using UnityEngine;

namespace IbrahKit
{
    public class UI_Text_Setter : UI_Text_Modifier
    {
        public UI_Text_Setter(GameObject go) : base(go)
        {

        }

        public void SetText(object value)
        {
            if (!Init()) return;

            text.SetText(value.ToString());
        }

        protected override void CleanupPro()
        {

        }

        protected override void RunPro()
        {

        }
    }
}