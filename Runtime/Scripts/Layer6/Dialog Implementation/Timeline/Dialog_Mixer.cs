using IbrahKit.UI;
using UnityEngine;
using UnityEngine.Playables;

public class Dialog_Mixer : PlayableBehaviour
{
 public override void ProcessFrame(Playable playable, FrameData info, object playerData)
 {
     UI_Modifier_Text_Modifier modifier = playerData as UI_Modifier_Text_Modifier;

     bool none = true;
     
     for (int i = 0; i < playable.GetInputCount(); i++)
     {
         if (playable.GetInputWeight(i) > 0)
         {
             none = false;
         }
     }

     if (none)
     {
         modifier.GetStaticSetter().SetText("");
     }
     

 }
}
