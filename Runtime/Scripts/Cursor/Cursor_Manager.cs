using UnityEngine;

namespace IbrahKit
{
    public class Cursor_Manager : Manager_Base<Cursor_Manager>
    {
        private void Update()
        {
            Cursor.lockState = Cursor_Visibilty_Manager.Instance.IsVisible() ? CursorLockMode.Confined : CursorLockMode.Locked;
            Cursor.visible = Cursor_Visibilty_Manager.Instance.IsVisible();
        }
    }
}

