#region

using System;
using IbrahKit.Keys;

#endregion

[Serializable]
public class Unlockable_Key : Key_Reference<Unlockable_Key, Unlockable_Table>
{
    private class Processor : Key_Processor
    {
    }
}