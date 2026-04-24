#region

using System.Collections.Generic;
using IbrahKit.Input;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

#endregion

namespace IbrahKit.UI
{
    public class UI_Selectable_Group : MonoBehaviour
    {
        [SerializeField, ReadOnly] private List<UI_Selectable> selectables = new();

        [SerializeField] private bool deselectOnClickAnywhere;

        private void OnEnable()
        {
            if (Cursor_Input_Manager.TryGet(out Cursor_Input_Manager result))
            {
                result.OnLMB += OnLMB;
            }
        }

        private void OnDisable()
        {
            if (Cursor_Input_Manager.TryGet(out Cursor_Input_Manager result))
            {
                result.OnLMB -= OnLMB;
            }
        }

        public void OnLMB()
        {
            if (!deselectOnClickAnywhere) return;

            if (!Cursor_Input_Manager.TryGet(out Cursor_Input_Manager result) ||
                result.CursorOverUI(EventSystem.current)) return;

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