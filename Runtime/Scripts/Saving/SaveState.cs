namespace IbrahKit.Save
{
    /// <summary>
    /// The state of the save/savable
    /// </summary>
    internal enum SaveState
    {
        Valid = 0,
        Outdated = 1,
        Corrupted = 2,
    }
}