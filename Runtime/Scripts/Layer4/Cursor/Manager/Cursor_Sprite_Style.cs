using System;
using UnityEngine;
using UnityEngine.UI;

namespace IbrahKit.Input
{
    [Serializable]
    public class Cursor_Sprite_Style
    {
        [SerializeField] private Sprite none;
        [SerializeField] private Sprite hovering;
        [SerializeField] private Sprite pressing;

        public void Set(Image renderer, Cursor_Sprite_Manager.CursorState state)
        {
            switch (state)
            {
                case Cursor_Sprite_Manager.CursorState.None:
                    renderer.sprite = none;
                    break;
                case Cursor_Sprite_Manager.CursorState.Hovering:
                    renderer.sprite = hovering;
                    break;
                case Cursor_Sprite_Manager.CursorState.Down:
                    renderer.sprite = pressing;
                    break;
            }
        }
    }
}