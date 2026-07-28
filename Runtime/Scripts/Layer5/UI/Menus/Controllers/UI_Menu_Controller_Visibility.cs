#region

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    [Serializable]
    public class UI_Menu_Controller_Visibility : UI_Menu_Controller, IMenuControllerVisibility
    {
        private const string DEBUG = "debug";
        private const string PAUSED = "paused";

        [SerializeField, Required]
        private UI_Menu_Controller_Alpha alphaController;

        [SerializeField]
        protected bool preventHideOnPause;

        [OdinSerialize, ReadOnly]
        private HashSet<string> hiddenBy = new();

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
            if (hiddenBy.Remove(value))
            {
                if (hiddenBy.Count == 0)
                {
                    alphaController.PassHiddenCount(hiddenBy.Count);
                }
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

        protected override void OnInit()
        {
        }

        public override void OnMenuEnabled()
        {
            if (!UI_Menu_Manager.TryGet(out UI_Menu_Manager result)) return;
            result.OnHide += GU_Hide;
            result.InvokeHide();
        }

        public override void Lifecycle()
        {
        }

        public override void OnMenuDisabled()
        {
            if (UI_Menu_Manager.TryGet(out UI_Menu_Manager menu))
            {
                menu.OnHide -= GU_Hide;
            }
        }
    }
}