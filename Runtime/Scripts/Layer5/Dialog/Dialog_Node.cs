#region

using System;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.Dialog
{
    public abstract class Dialog_Node<TNode, TElement, TChoice> : ScriptableObject where TElement : Dialog_Element
        where TNode : Dialog_Node<TNode, TElement, TChoice>
        where TChoice : Dialog_Choice<TNode, TElement, TChoice>
    {
        [SerializeField] private TElement[] elements;

        [SerializeField] private DialogEnd endMode;

        [SerializeField, ShowIf(nameof(endMode), DialogEnd.CHOICE)]
        private TChoice[] choices;

        [SerializeField, ShowIf(nameof(endMode), DialogEnd.CHAIN)]
        private TNode chained;

        public NextType GetNextType(int index)
        {
            if (index >= 0 && index < elements.Length)
            {
                return NextType.ELEMENT;
            }

            switch (endMode)
            {
                case DialogEnd.NONE:
                    return NextType.END;
                case DialogEnd.CHOICE:
                    return NextType.CHOICE;
                case DialogEnd.CHAIN:
                    return NextType.CHAINED_NODE;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public DialogEnd GetEndMode() => endMode;

        public TElement[] GetElements() => elements;

        public TChoice[] GetChoices()
        {
            return choices;
        }

        public TNode GetChained()
        {
            return chained;
        }

        public TElement GetElement(int i)
        {
            return elements[i];
        }
    }

    public enum NextType
    {
        ELEMENT,
        CHAINED_NODE,
        CHOICE,
        END
    }

    public enum DialogEnd
    {
        NONE,
        CHOICE,
        CHAIN
    }
}