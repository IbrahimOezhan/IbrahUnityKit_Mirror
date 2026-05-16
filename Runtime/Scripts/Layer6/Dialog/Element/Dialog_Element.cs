#region

using System;
using UnityEngine;

#endregion

namespace IbrahKit.Dialog
{
    [Serializable]
    public class Dialog_Element
    {
        [SerializeField]
        private Dialog_Sub_Element[] subElements;

        public Dialog_Sub_Element[] GetSubElements() => subElements;
    }
}