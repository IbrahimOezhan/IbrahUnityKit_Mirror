using IbrahKit.Dialog;
using IbrahKit.UI;
using IbrahKit.UI.Modifier;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackClipType(typeof(Dialog_Asset))]
[TrackBindingType(typeof(UI_Modifier_Text_Modifier))]
public class Dialog_Track : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<Dialog_Mixer>.Create(graph, inputCount);
    }
}
