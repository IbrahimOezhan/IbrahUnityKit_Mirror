using UnityEngine;
using UnityEngine.SceneManagement;

namespace IbrahKit.UI
{
    public class Pause_Menu : UI_Menu
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