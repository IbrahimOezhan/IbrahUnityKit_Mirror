using UnityEngine;
using UnityEngine.SceneManagement;

namespace IbrahKit
{
    public class Menu_Pause : UI_Menu
    {
        public void MainMenu()
        {
            Pause_Manager.GetInstance().Pause();
            SceneManager.LoadScene(0);
        }

        public void OpenSettings()
        {
            Settings_Manager.GetInstance().OpenSettings(this);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}