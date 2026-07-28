using IbrahKit.UI;
using Sirenix.OdinInspector;
using UnityEngine;

public class Menu_Item_Button_Localised : Menu_Item_Button, ISelfValidator
{
    [SerializeField] private UI_Modifier modifier;
    
    public void Validate(SelfValidationResult result)
    {
        if (modifier == null || !modifier.TryGetExtension(out UI_Modifier_Extension_Localization _))
        {
            result.AddError("UI_Interactive_Extension_Localization Not Found");
        }
    }
}
