#region

using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
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
            if (Cursor_Manager.TryGet(out Cursor_Manager result))
            {
                result.GetCursorInput().GetLMB().performed += OnLMB;
            }
        }

        private void OnDisable()
        {
            if (Cursor_Manager.TryGet(out Cursor_Manager result))
            {
                result.GetCursorInput().GetLMB().performed -= OnLMB;
            }
        }

        public void OnLMB(InputAction.CallbackContext ctx)
        {
            if (!deselectOnClickAnywhere) return;

            if (!Cursor_Manager.TryGet(out Cursor_Manager result) || result.GetCursorReceiver()
                    .IsOverUIReceiver(EventSystem.current, result.GetCursorInput().GetMousePos())) return;

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