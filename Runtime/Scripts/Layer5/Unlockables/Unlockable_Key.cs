#region

using System;
using IbrahKit.Keys;
using UnityEngine;

#endregion

[Serializable]
public class Unlockable_Key : Key_Reference<Unlockable_Key, Unlockable_Table>
{
    private class Processor : Key_Processor{}
}