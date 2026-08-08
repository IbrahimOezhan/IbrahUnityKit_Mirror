#region

using System;
using UnityEngine;

#endregion

namespace IbrahKit.Input.Cursor
{
    [CreateAssetMenu(fileName = "NewCustomSpriteStyle", menuName = "IbrahKit/CustomSpriteStyle")]
    public class Cursor_Custom_Sprite_Style : ScriptableObject
    {
        [SerializeField] private Sprite none;
        [SerializeField] private Sprite hovering;
        [SerializeField] private Sprite pressing;

        public Sprite Get(Cursor_Controller_State_Custom.CursorClickState state)
        {
            return state switch
            {
                Cursor_Controller_State_Custom.CursorClickState.None => none,
                Cursor_Controller_State_Custom.CursorClickState.Hovering => hovering,
                Cursor_Controller_State_Custom.CursorClickState.Down => pressing,
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };
        }
    }
}