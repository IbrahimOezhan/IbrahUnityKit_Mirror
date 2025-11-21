namespace IbrahKit
{
    [System.Serializable]
    public abstract class UI_Interactive_Extension : Extension
    {
        protected UI_Interactive interactive;

        protected UI_Interactive_Extension(UI_Interactive extension) : base(extension)
        {
            interactive = extension;
        }
    }
}