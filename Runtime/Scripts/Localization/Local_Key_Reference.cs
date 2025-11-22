namespace IbrahKit
{
    [System.Serializable]
    public class Local_Key_Reference : Key_Reference
    {
        public static implicit operator string(Local_Key_Reference reference)
        {
            return reference?.key;
        }

        public static implicit operator Local_Key_Reference(string value)
        {
            return new Local_Key_Reference { key = value };
        }
    }
}