namespace IbrahKit.Settings
{
    public class Setting_Float : Setting_Number<float>, ISettingNumber
    {
        public Setting_Float(float value) : base(value)
        {
        }

        public override void Decrement()
        {
            throw new System.NotImplementedException();
        }

        public override void Increment()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsMax()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsMin()
        {
            throw new System.NotImplementedException();
        }

        public override bool TrySetValue(float value)
        {
            throw new System.NotImplementedException();
        }
    }
}