using System;
using IbrahKit.Dialog;
using UnityEngine;

namespace IbrahKit
{
    public class DialogTrigger : MonoBehaviour
    {
        public SimpleDialogNode node;
        public DynamicDialogController controller;

        private void Start()
        {
            controller.StartDialog(node);
        }
    }
}
