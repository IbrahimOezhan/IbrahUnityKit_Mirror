using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public class UI_Menu_Controller_Visibility : UI_Menu_Controller, IMenuVisibility
    {
        private UI_Menu menu;

        [TabGroup("Menu Settings", order: -1), SerializeField,Required]
        private CanvasGroup enabledGroup;

        [TabGroup("Menu Settings", order: -1), SerializeField, Required]
        private CanvasGroup hiddenGroup;

        [TabGroup("Menu Settings", order: -1), SerializeField]
        protected bool preventHideOnPause;

        [TabGroup("Runtime", order: -1), SerializeField, ReadOnly]
        private HashSet<string> hiddenBy = new();

        public void SetInteractable(bool value)
        {
            enabledGroup.blocksRaycasts = value;
        }

        public void SetAlpha(float value)
        {
            value = Mathf.Clamp01(value);

            enabledGroup.alpha = value;
        }

        public void HideBy(string value)
        {
            if (hiddenBy.Add(value))
            {
                hiddenGroup.alpha = 0;
            }
        }

        public void ShowBy(string value)
        {
            if (hiddenBy.Remove(value))
            {
                if (hiddenBy.Count == 0)
                {
                    hiddenGroup.alpha = 1;
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