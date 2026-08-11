using System;
using IbrahKit.Dialog;
using IbrahKit.UI.Menu;
using UnityEngine;

namespace IbrahKit
{
    public class DialogTrigger : MonoBehaviour
    {
        public SimpleDialogNode node;
        public DynamicDialogController controller;
        public UI_Menu main;

        public void StartDialog()
        {
            main.Disable();
            controller.StartDialog(node);
        }
    }
}
