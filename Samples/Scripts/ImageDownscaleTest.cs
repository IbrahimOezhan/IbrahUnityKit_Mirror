using System;
using IbrahKit.Utilities;
using UnityEngine;

public class ImageDownscaleTest : MonoBehaviour
{
   [SerializeField] private Vector2Int _resolution;
   
   [SerializeField] private Texture2D _texture;

   [SerializeField] private SpriteRenderer _spriteRenderer;
   
   private void Awake()
   {
      Texture2D tex = _texture.DownscaleNearest(_resolution);
      
      tex.filterMode = FilterMode.Point;
      tex.Apply();
      
      _spriteRenderer.sprite = tex.ToSprite();
   }
}
