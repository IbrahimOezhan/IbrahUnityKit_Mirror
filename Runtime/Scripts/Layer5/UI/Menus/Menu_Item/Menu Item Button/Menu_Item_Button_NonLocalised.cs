#region

using IbrahKit.UI;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

public class Menu_Item_Button_NonLocalised : MonoBehaviour
{
    [SerializeField] private UI_Modifier modifier;

    public void Validate(SelfValidationResult result)
    {
        if (modifier == null || !modifier.TryGetExtension(out UI_Modifier_Extension_Text_Setter _))
        {
            result.AddError("UI_Interactive_Extension_Text_Setter Not Found");
        }
    }
}