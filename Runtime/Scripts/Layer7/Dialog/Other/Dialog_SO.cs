#region

using UnityEngine;

#endregion

namespace IbrahKit.Dialog
{
    [CreateAssetMenu(fileName = "NewDialog", menuName = "IbrahKit/Dialog")]
    public class Dialog_SO : ScriptableObject
    {
        [SerializeField] private Dialog_Element dialog;

        public Dialog_Element GetDialog()
        {
            return dialog;
        }
    }
}