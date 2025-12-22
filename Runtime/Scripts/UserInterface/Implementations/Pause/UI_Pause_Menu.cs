using IbrahKit.Settings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IbrahKit.UI
{
    public class UI_Pause_Menu : UI_Menu
    {
        protected override void AfterInit()
        {
            base.AfterInit();

            if (Pause_Manager.TryGet(out Pause_Manager result))
            {
                result.OnPause += OnPause;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (Pause_Manager.TryGet(out Pause_Manager result))
            {
                result.OnPause -= OnPause;
            }
        }

        public void OnPause(bool paused)
        {
            switch (paused)
            {
                case false:
                    GetStateController().Disable();
                    break;
                case true:
                    GetStateController().Enable();
                    break;
            }
        }

        public void MainMenu()
        {
            Pause_Manager.GetInstance().Pause();

            SceneManager.LoadScene(0);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}