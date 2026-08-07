using IbrahKit.Localization;
using UnityEngine;

namespace IbrahKit.Dialog
{

    public class Dialog_Choice<TNode, TElement, TChoice> where TElement : Dialog_Element
        where TNode : Dialog_Node<TNode, TElement, TChoice>
        where TChoice : Dialog_Choice<TNode, TElement, TChoice>
    {
        public Local_Key key;
        public TNode node;
    }

}