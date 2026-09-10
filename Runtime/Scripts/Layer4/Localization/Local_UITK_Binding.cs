using System.Collections;
using IbrahKit.Localization;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

[UxmlObject]
public partial class Local_UITK_Binding : CustomBinding
{
    [UxmlAttribute] public Local_UITK_SO key;

    public Local_UITK_Binding()
    {
        updateTrigger = BindingUpdateTrigger.EveryUpdate;
    }

    protected override BindingResult Update(in BindingContext context)
    {
        if (context.targetElement is not Label visualElement) return new BindingResult(BindingStatus.Failure, "Target is Invalid");;
        
        visualElement.text = Local_Manager.GetInstance().GetString(key.GetKey());
        
        return new BindingResult(BindingStatus.Success);
    }
}
