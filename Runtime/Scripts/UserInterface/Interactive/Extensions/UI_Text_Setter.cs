namespace IbrahKit
{
    public class UI_Text_Setter : UI_Text_Modifier
    {
        public void SetText(object value)
        {
            if (!IsInitialized()) return;

            text.SetText(value.ToString());

            UpdateUI();
        }

        public override int GetOrder()
        {
            return 0;
        }
    }
}