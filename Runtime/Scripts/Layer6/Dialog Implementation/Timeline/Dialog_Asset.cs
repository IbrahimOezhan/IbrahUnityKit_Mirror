#region

using System;
using IbrahKit.UI;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

#endregion

namespace IbrahKit.Dialog
{
    [Serializable]
    public class Dialog_Asset : PlayableAsset, ITimelineClipAsset
    {
        [SerializeField] private Dialog_Behaviour behaviour;

        public override Playable CreatePlayable(PlayableGraph _graph, GameObject _owner)
        {
            ScriptPlayable<Dialog_Behaviour> _playable = ScriptPlayable<Dialog_Behaviour>.Create(_graph, behaviour);

            return _playable;
        }

        public ClipCaps clipCaps => ClipCaps.Blending;
    }
}