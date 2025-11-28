namespace IbrahKit.Localization
{
    [System.Serializable]
    public class Local_Key : Key_Reference
    {
        public static implicit operator string(Local_Key reference)
        {
            return reference?.key;
        }

        public static implicit operator Local_Key(string value)
        {
            return new Local_Key { key = value };
        }

        private class Local_Key_Processor : Key_Processor<Local_Key>
        {
            public override string GetDBName()
            {
                return Local_Manager.DROP;
            }
        }
    }
}