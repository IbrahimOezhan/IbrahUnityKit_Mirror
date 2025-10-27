namespace IbrahKit
{
    public class Setting_Language : Setting
    {
        public override void Init(string initialValue)
        {
            base.Init(initialValue);

            SetValueRange(new(0, Local_Manager.GetInstance().LanguageCount() - 1));
        }

        public override string GetDisplayValue()
        {
            return $"{Local_Manager.GetInstance().GetCurrent().GetNative()} ({Local_Manager.GetInstance().GetCurrent().GetSys()})";
        }

        public override void ApplyChanges()
        {
            base.ApplyChanges();

            Local_Manager.GetInstance().Set((int)GetValue());
        }
    }
}