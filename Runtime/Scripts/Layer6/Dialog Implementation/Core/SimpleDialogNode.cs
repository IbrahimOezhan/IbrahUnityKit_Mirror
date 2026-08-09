using UnityEngine;

namespace IbrahKit.Dialog
{
    [CreateAssetMenu(menuName = "IbrahKit/Dialog/Simple Dialog Node",
        fileName = "NewSimpleDialogNode")]
    public class SimpleDialogNode : Dialog_Node<SimpleDialogNode, SimpleDialogElement, SimpleDialogChoice>
    {
    }
}