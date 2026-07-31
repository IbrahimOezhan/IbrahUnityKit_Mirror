#region

using System;
using IbrahKit.Keys;

#endregion

namespace IbrahKit.Interaction
{
    [Serializable]
    public class Interaction_Key : Key_Reference<Interaction_Key, Interaction_Key_Table>
    {
        public static implicit operator Interaction_Key(string value)
        {
            return new Interaction_Key { key = value };
        }

        private new class Key_Processor : Key_Reference<Interaction_Key, Interaction_Key_Table>.Key_Processor
        {
        }
    }
}