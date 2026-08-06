using System;
using UnityEngine;

public class Dialog_Manager<TNode, TElement, TChoice> : MonoBehaviour where TElement : Dialog_Element where TNode : Dialog_Node<TNode,TElement,TChoice> where TChoice : Dialog_Choice<TNode, TElement,TChoice>
{
    public (TNode, TElement,TChoice[], int) Get(TNode node, int index)
    {
       NextType nextType = node.GetNextType(index);

        switch (nextType)
        {
            case NextType.ELEMENT:
                return (node, node.GetElement(index), null, index + 1);
            case NextType.CHAINED_NODE:
                return (node.GetChained(), node.GetElement(0), null,  1);
            case NextType.CHOICE:
                return (node.GetChained(),null,  node.GetChoices() ,  0);
            case NextType.END:
                return (null, null, null, 0);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
