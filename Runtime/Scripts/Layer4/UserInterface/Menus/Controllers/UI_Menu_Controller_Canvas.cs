#region

using System;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit
{
    [RequireComponent(typeof(Canvas))]
    public class UI_Menu_Controller_Canvas : MonoBehaviour
    {
        public Action OnFocusOrResolutionChanged;

        [SerializeField, Required] private Canvas canvas;

        public Canvas GetCanvas() => canvas;

        private void OnRectTransformDimensionsChange()
        {
            OnFocusOrResolutionChanged?.Invoke();
        }

        private void OnApplicationFocus(bool _focus)
        {
            OnFocusOrResolutionChanged?.Invoke();
        }
    }
}