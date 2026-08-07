#region

using System;
using IbrahKit.UI.Modifier;
using UnityEngine;
using UnityEngine.Playables;

#endregion

namespace IbrahKit.Dialog
{
    public class Dialog_Asset : PlayableAsset
    {
        [SerializeField] private Dialog_Playable_Elements dialog;

        [SerializeField] private UI_Modifier modifier;

        [SerializeField] private int percentageAnim;

        public override Playable CreatePlayable(PlayableGraph _graph, GameObject _owner)
        {
            ScriptPlayable<Dialog_Behaiviour> _playable = ScriptPlayable<Dialog_Behaiviour>.Create(_graph);

            Dialog_Behaiviour behaviour = _playable.GetBehaviour();

            if (!modifier.TryGetExtension(out UI_Modifier_Extension_Text_Setter setter))
            {
                throw new NullReferenceException("Modifier has no text setter");
            }

            behaviour.Init(dialog, setter);

            return _playable;
        }
    }
}