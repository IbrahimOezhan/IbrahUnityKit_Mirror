#region

using UnityEngine.Playables;

#endregion

namespace IbrahKit.Utilities
{
    /// <summary>
    /// Static Utility Class providing utility methods related to the playable director
    /// </summary>
    public static class PlayableDirector_Utils
    {
        public static double GetNormalizedTime(this PlayableDirector playable)
        {
            return (playable.time / playable.duration);
        }
    }
}