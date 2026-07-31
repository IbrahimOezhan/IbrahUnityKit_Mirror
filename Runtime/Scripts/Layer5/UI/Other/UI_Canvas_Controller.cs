#region

using System;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Generic
{
    [RequireComponent(typeof(Canvas))]
    public class UI_Canvas_Controller : MonoBehaviour
    {
        [SerializeField, Required] private Canvas canvas;
        public Action OnFocusOrResolutionChanged;

        private void OnApplicationFocus(bool _focus)
        {
            OnFocusOrResolutionChanged?.Invoke();
        }

        private void OnRectTransformDimensionsChange()
        {
            OnFocusOrResolutionChanged?.Invoke();
        }

        public Canvas GetCanvas() => canvas;
    }
}