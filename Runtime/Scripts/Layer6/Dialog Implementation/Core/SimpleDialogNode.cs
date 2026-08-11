#region

using UnityEngine;

#endregion

namespace IbrahKit.Dialog
{
    [CreateAssetMenu(menuName = "IbrahKit/Dialog/SimpleDialogNode",
        fileName = "NewSimpleDialogNode")]
    public class SimpleDialogNode : Dialog_Node<SimpleDialogNode, SimpleDialogElement, SimpleDialogChoice>
    {
    }
}