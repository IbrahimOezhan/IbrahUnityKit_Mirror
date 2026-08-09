#region

using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

#endregion

namespace IbrahKit.Dialog
{
    // This is deliberately not made a Singleton because you might be making a split screen game. 
    public class DynamicDialogController : MonoBehaviour
    {
        [SerializeField] private UI_Dialog_Menu menu;

        private DialogInput dialogInput;

        public Action<bool> dialogStateChanged;

        private void Awake()
        {
            dialogInput = new();

            dialogInput.Enable();
        }

        private void OnDisable()
        {
            if (dialogInput != null)
            {
                dialogInput.Disable();

                dialogInput.Dispose();
            }
        }

        public void StartDialog(SimpleDialogNode node)
        {
            StartCoroutine(StartDialogIE(node));
        }

        public IEnumerator StartDialogIE(SimpleDialogNode node)
        {
            dialogStateChanged?.Invoke(true);
            menu.GetStateController().Enable();
            yield return Element(node, 0);
            menu.GetStateController().Disable();
            dialogStateChanged?.Invoke(false);
        }

        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        private IEnumerator Element(SimpleDialogNode node, int i)
        {
            UnityEvent onClick = new();

            if (node.GetElements().Length != 0)
            {
                dialogInput.Map.Continue.performed += OnClick;

                yield return menu.DisplayText(node.GetElement(i), onClick);

                dialogInput.Map.Continue.performed -= OnClick;

                i++;
            }

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
                        yield return Element(node.GetChained(), 0);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                yield return Element(node, i);
            }

            yield break;

            void OnClick(InputAction.CallbackContext context)
            {
                onClick?.Invoke();
                Debug.Log("Invoked");
            }
        }

        private IEnumerator Choice(SimpleDialogChoice[] choices, SimpleDialogNode node)
        {
            SimpleDialogChoice selected = null;

            menu.DisplayChoices(choices, (c) => { selected = c; });

            yield return new WaitWhile(() => selected == null);

            yield return StartCoroutine(Element(selected.node, 0));
        }
    }
}