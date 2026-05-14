#region

using UnityEngine;
using UnityEngine.Playables;

#endregion

namespace IbrahKit.Dialog
{
    public class Dialog_Asset : PlayableAsset
    {
        [SerializeField] private Dialog_SO dialog;

        [SerializeField] private int percentageAnim;

        public override Playable CreatePlayable(PlayableGraph _graph, GameObject _owner)
        {
            ScriptPlayable<Dialog_Behaiviour> _playable = ScriptPlayable<Dialog_Behaiviour>.Create(_graph);

            Dialog_Behaiviour _dialogBehaviour = _playable.GetBehaviour();

            _dialogBehaviour.dialog = dialog;

            _dialogBehaviour.percentageAnim = percentageAnim;

            return _playable;
        }
    }
}