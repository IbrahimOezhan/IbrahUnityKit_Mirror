namespace IbrahKit.UI
{
    public interface IMenuVisibility
    {
        public void SetAlpha(float value);

        public void SetInteractable(bool value);

        public void HideBy(string value);

        public void ShowBy(string value);

        public void SetActive(bool value);
    }
}