#region

using UnityEngine.Playables;

#endregion

namespace IbrahKit.Dialog
{
    public class Dialog_Behaiviour : PlayableBehaviour
    {
        public Dialog_SO dialog = null;

        public int percentageAnim;

        public override void OnBehaviourPlay(Playable _playable, FrameData _info)
        {
            double _totalDur = _playable.GetDuration();

            double _animTime = _totalDur / 100 * percentageAnim;

            double _showTime = _totalDur / 100 * (100 - percentageAnim);

            //dialog.GetDialog().SetStaticTime((float)_animTime);

            //dialog.GetDialog().automaticContinueAfterEnd = (float)_showTime;

            //Dialog_Manager.GetInstance().StartDialog(dialog.GetDialog());
        }
    }
}