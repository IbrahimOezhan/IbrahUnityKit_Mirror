#region

using IbrahKit.Input.Cursor;
using IbrahKit.Manager;
using UnityEngine;

#endregion

public class Cursor_Manager : Manager_Global<Cursor_Manager>
{
    [SerializeReference] private Cursor_Controller_State cursor_state_controller;
    private Cursor_Controller_Input cursor_input_controller;
    private Cursor_Controller_Receiver cursor_receiver_controller;

    protected override void InstanceAwake()
    {
        base.InstanceAwake();
        
        cursor_input_controller.Init();
    }

    protected override void InstanceDestroy()
    {
        base.InstanceDestroy();
        
        cursor_input_controller.Destroy();
    }

    private void Update()
    {
        Camera camera = Camera.main;
        
        cursor_state_controller.Run();
    }

    public Cursor_Controller_Input GetCursorInput() => cursor_input_controller;

    public Cursor_Controller_Receiver GetCursorReceiver() => cursor_receiver_controller;

    public Cursor_Controller_State GetCursorState() => cursor_state_controller;

    public Camera GetCamera() => null;
}