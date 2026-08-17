using System;
using IbrahKit.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class ImageDownscaleTest : MonoBehaviour
{
   [SerializeField] private Vector2Int _resolution;
   
   [SerializeField] private Texture2D _texture;

   [SerializeField] private Image image;
   
   private void Awake()
   {
      Texture2D tex = _texture.DownscaleNearest(_resolution);
      
      tex.filterMode = FilterMode.Point;
      
      tex.Apply();
      
      image.sprite = tex.ToSprite();
   }
}
