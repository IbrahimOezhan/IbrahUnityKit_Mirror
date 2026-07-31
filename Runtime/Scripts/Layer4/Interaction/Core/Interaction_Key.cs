#region

using System;
using IbrahKit.Keys;

#endregion

[Serializable]
public class Interaction_Key : Key_Reference<Interaction_Key, Interaction_Table>
{
    private class Key_Processor : Key_Processor<Interaction_Key, Interaction_Table>
    {
    }
}