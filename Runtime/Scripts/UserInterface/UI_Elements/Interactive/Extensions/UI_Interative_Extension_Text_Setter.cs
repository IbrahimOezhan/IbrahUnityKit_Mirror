namespace IbrahKit.UI
{
    [System.Serializable]
    public class UI_Interative_Extension_Text_Setter : UI_Interative_Extension_Text_Modifier
    {
        public UI_Interative_Extension_Text_Setter(UI_Interactive extension) : base(extension)
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