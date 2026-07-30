#region

using System;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace IbrahKit.Input
{
    [Serializable]
    public class Cursor_Sprite_Style
    {
        [SerializeField] private Sprite none;
        [SerializeField] private Sprite hovering;
        [SerializeField] private Sprite pressing;

        public void Set(Image renderer, Cursor_Custom_Manager.CursorClickState state)
        {
            switch (state)
            {
                case Cursor_Custom_Manager.CursorClickState.None:
                    renderer.sprite = none;
                    break;
                case Cursor_Custom_Manager.CursorClickState.Hovering:
                    renderer.sprite = hovering;
                    break;
                case Cursor_Custom_Manager.CursorClickState.Down:
                    renderer.sprite = pressing;
                    break;
            }
        }
    }
}