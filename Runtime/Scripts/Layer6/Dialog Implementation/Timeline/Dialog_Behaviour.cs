#region

using System;
using System.Collections.Generic;
using System.Linq;
using IbrahKit.UI;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Playables;

#endregion

namespace IbrahKit.Dialog
{
    [Serializable]
    public class Dialog_Behaviour : PlayableBehaviour
    {
        [SerializeField] private Dialog_Playable_Elements dialog;

        private Dictionary<SimpleDialogElement, List<SimpleDialogElement.Token>> dict = new();
        
        public override void OnPlayableCreate(Playable playable)
        {
            base.OnPlayableCreate(playable);
            
            
            
            dict = dialog.GetElements().ToDictionary(x => x,y => y.GetTokens());
            
            Debug.Log(playable.GetTime());
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            base.ProcessFrame(playable, info, playerData);

            string text =
                PlayableDialogController.Get(dict,dialog.GetElements(), playable.GetTime(), playable.GetDuration());
            
            UI_Modifier_Text_Modifier modifier = playerData as UI_Modifier_Text_Modifier;
            
            modifier?.GetStaticSetter().SetText(text);
            
            if (playable.IsDone() || playable.GetTime() >= playable.GetDuration())
            {
                modifier?.GetStaticSetter().SetText("");
            }
        }
    }
}