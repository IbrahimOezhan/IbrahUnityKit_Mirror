#region

using IbrahKit.UI;
using IbrahKit.UI.Modifier;
using UnityEngine.Playables;

#endregion

namespace IbrahKit.Dialog
{
    public class Dialog_Behaiviour : PlayableBehaviour
    {
        private Dialog_Playable_Elements dialog = null;
        private UI_Modifier_Text_Modifier setter = null;

        public void Init(Dialog_Playable_Elements dialog, UI_Modifier_Text_Modifier setter)
        {
            this.dialog = dialog;
            this.setter = setter;
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            base.ProcessFrame(playable, info, playerData);

            string text =
                PlayableDialogController.Get(dialog.GetElements(), playable.GetTime(), playable.GetDuration());

            setter.GetStaticSetter().SetText(text);
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            base.OnBehaviourPause(playable, info);

            if (playable.IsDone() || playable.GetTime() >= playable.GetDuration())
            {
                setter.GetStaticSetter().SetText("");
            }
        }
    }
}