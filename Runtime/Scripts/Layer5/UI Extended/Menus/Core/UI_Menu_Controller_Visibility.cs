#region

using System.Collections.Generic;
using IbrahKit.UI.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    public partial class UI_Menu
    {
        private const string DEBUG = "debug";
        private const string PAUSED = "paused";

        [SerializeField, Required] private UI_Alpha_Controller alphaController;

        [SerializeField] protected bool preventHideOnPause;

        [OdinSerialize, ReadOnly] private HashSet<string> hiddenBy = new();

        public void SetInteractable(bool value)
        {
            alphaController.SetInteractable(value);
        }

        public void SetEnabledAlpha(float value)
        {
            value = Mathf.Clamp01(value);

            alphaController.SetEnabledAlpha(value);
        }

        public void HideBy(string value)
        {
            if (hiddenBy.Add(value))
            {
                alphaController.PassHiddenCount(hiddenBy.Count);
            }
        }

        public void ShowBy(string value)
        {
            if (!hiddenBy.Remove(value)) return;

            if (hiddenBy.Count == 0)
            {
                alphaController.PassHiddenCount(hiddenBy.Count);
            }
        }

        public void SetActive(bool value)
        {
            alphaController.gameObject.SetActive(value);
        }

        private void GU_Hide(bool state)
        {
            if (state) HideBy(DEBUG);
            else ShowBy(DEBUG);
        }

        private void OnPause(bool state)
        {
            if (state) HideBy(PAUSED);
            else ShowBy(PAUSED);
        }
    }
}