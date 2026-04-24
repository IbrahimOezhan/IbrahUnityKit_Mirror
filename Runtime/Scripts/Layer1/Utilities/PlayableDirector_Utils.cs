#region

using UnityEngine.Playables;

#endregion

namespace IbrahKit.Utilities
{
    public static class PlayableDirector_Utils
    {
        public static double GetNormalizedTime(this PlayableDirector playable)
        {
            return (playable.time / playable.duration);
        }
    }
}