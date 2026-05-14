#region

using System;
using IbrahKit.Dialog;
using UnityEngine;

#endregion

namespace IbrahKit
{
    [Serializable]
    public class Dialog_Element
    {
        [SerializeField]
        private Dialog_Sub_Element[] subElements;

        public Dialog_Sub_Element[] GetSubElements() => subElements;
    }
}