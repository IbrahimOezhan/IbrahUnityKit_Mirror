using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IbrahKit.Dialog
{

    public class DynamicDialogController : MonoBehaviour
    {
        private DialogInput dialogInput;
        [SerializeField] private UI_Dialog_Choice_Menu choiceMenu;

        private void Awake()
        {
            dialogInput = new();
            
            dialogInput.Enable();
        }

        public void StartDialog(SimpleDialogNode node)
        {

        }

        public void Next()
        {

        }

        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        private IEnumerator Display(SimpleDialogElement element, SimpleDialogNode node, int i)
        {
            Action onClick = null;

            dialogInput.Map.Continue.performed += OnClick;
            
            yield return choiceMenu.DisplayText(element, onClick);

            dialogInput.Map.Continue.performed -= OnClick;
            
            i++;

            if (i == node.GetElements().Length)
            {
                switch (node.GetEndMode())
                {
                    case DialogEnd.NONE:
                        break;
                    case DialogEnd.CHOICE:
                        yield return Choice(node.GetChoices(), node);
                        break;
                    case DialogEnd.CHAIN:
                        yield return Display(node.GetChained().GetElement(0), node.GetChained(), 0);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                yield return Display(node.GetElement(i), node, i);
            }

            yield break;
            
            void OnClick(InputAction.CallbackContext context)
            {
                onClick?.Invoke();
            }
        }

        private IEnumerator Choice(SimpleDialogChoice[] choices, SimpleDialogNode node)
        {
            SimpleDialogChoice selected = null;

            choiceMenu.Init(choices, (c) => { selected = c; });

            yield return new WaitWhile(() => selected == null);

            yield return StartCoroutine(Display(selected.node.GetElement(0), selected.node, 0));
        }
    }
}