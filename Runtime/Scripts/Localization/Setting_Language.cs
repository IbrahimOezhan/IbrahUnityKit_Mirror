namespace IbrahKit
{
    public class Setting_Language : Setting
    {
        public override void Init(string initialValue)
        {
            base.Init(initialValue);
        }

        public override string GetDisplayValue()
        {
            return $"{Local_Manager.Instance.GetCurrent().GetNative()} ({Local_Manager.Instance.GetCurrent().GetSys()})";
        }

        public override void ApplyChanges()
        {
            base.ApplyChanges();

            Local_Manager.Instance.SetNext();

            SetValue(Local_Manager.Instance.CurrentIndex());
        }
    }
}