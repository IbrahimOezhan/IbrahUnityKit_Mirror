using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit.UI
{
    [System.Serializable]
    public class UI_Menu_Controller_Visibility : UI_Menu_Controller, IMenuVisibility
    {
        private UI_Menu menu;

        [TabGroup("Menu Settings", order: -1), SerializeField, Required]
        private UI_Menu_Controller_Alpha alphaController;

        [TabGroup("Menu Settings", order: -1), SerializeField]
        protected bool preventHideOnPause;

        [TabGroup("Runtime", order: -1), SerializeField, ReadOnly]
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
            menu.gameObject.SetActive(value);
        }

        private void GU_Hide(bool state)
        {
            if (state) HideBy("Debug");
            else ShowBy("Debug");
        }

        private void OnPause(bool state)
        {
            if (state) HideBy("paused");
            else ShowBy("paused");
        }

        public override void Init(UI_Menu menu)
        {
            this.menu = menu;
        }

        public override void OnMenuEnabled()
        {
            if (!preventHideOnPause && Pause_Manager.GetInstance() != null)
            {
                Pause_Manager.GetInstance().OnPause += OnPause;
                Pause_Manager.GetInstance().UpdatePause();
            }

            if (Game_Utilities.GetInstance() != null)
            {
                Game_Utilities.GetInstance().OnHide += GU_Hide;
                Game_Utilities.GetInstance().UpdateHide();
            }
        }

        public override void Lifecycle()
        {

        }

        public override void OnMenuDisabled()
        {
            if (!preventHideOnPause && Pause_Manager.GetInstance() != null)
            {
                Pause_Manager.GetInstance().OnPause -= OnPause;
            }

            if (Game_Utilities.GetInstance() != null)
            {
                Game_Utilities.GetInstance().OnHide -= GU_Hide;
            }
        }
    }
}