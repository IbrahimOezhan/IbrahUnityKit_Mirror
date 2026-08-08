#region

using IbrahKit.UI;
using UnityEngine;
using UnityEngine.Playables;

#endregion

namespace IbrahKit.Dialog
{
    public class Dialog_Asset : PlayableAsset
    {
        [SerializeField] private Dialog_Playable_Elements dialog;

        [SerializeField] private UI_Modifier_Text_Modifier modifier;

        [SerializeField] private int percentageAnim;

        public override Playable CreatePlayable(PlayableGraph _graph, GameObject _owner)
        {
            ScriptPlayable<Dialog_Behaiviour> _playable = ScriptPlayable<Dialog_Behaiviour>.Create(_graph);

            Dialog_Behaiviour behaviour = _playable.GetBehaviour();

            behaviour.Init(dialog, modifier);

            return _playable;
        }
    }
}