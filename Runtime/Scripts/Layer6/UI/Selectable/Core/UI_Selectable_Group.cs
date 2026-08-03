#region

using System.Collections.Generic;
using IbrahKit.Input.Cursor;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace IbrahKit.UI.Selectable
{
    public class UI_Selectable_Group : MonoBehaviour
    {
        [SerializeField, ReadOnly] private List<UI_Selectable> selectables = new();

        [SerializeField] private bool deselectOnClickAnywhere;

        private void OnEnable()
        {
            if (Cursor_Input_Manager.TryGet(out Cursor_Input_Manager result))
            {
                result.GetLMB().performed += OnLMB;
            }
        }

        private void OnDisable()
        {
            if (Cursor_Input_Manager.TryGet(out Cursor_Input_Manager result))
            {
                result.GetLMB().performed -= OnLMB;
            }
        }

        public void OnLMB(InputAction.CallbackContext ctx)
        {
            if (!deselectOnClickAnywhere) return;

            if (!Cursor_Input_Manager.TryGet(out Cursor_Input_Manager result) || result.IsOverUIReceiver()) return;

            foreach (var selectable in selectables)
            {
                selectable.GetStateController().PressedStop();
            }
        }

        public bool Add(UI_Selectable selectable)
        {
            if (selectables.Contains(selectable)) return false;
            selectables.Add(selectable);
            return true;
        }

        public bool Remove(UI_Selectable selectable)
        {
            if (!selectables.Contains(selectable)) return false;
            selectables.Remove(selectable);
            return true;
        }

        public void OnSelect(UI_Selectable selected)
        {
            foreach (var selectable in selectables)
            {
                if (selectable != selected)
                {
                    selectable.GetStateController().PressedStop();
                }
            }
        }
    }
}