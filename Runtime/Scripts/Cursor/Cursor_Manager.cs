using UnityEngine;

namespace IbrahKit
{
    public class Cursor_Manager : Manager_DDOL<Cursor_Manager>
    {
        private void Update()
        {
            if(Cursor_Visibilty_Manager.TryGet(out Cursor_Visibilty_Manager result))
            {
                Cursor.lockState = result.IsVisible() ? CursorLockMode.Confined : CursorLockMode.Locked;
                Cursor.visible = result.IsVisible();
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}

