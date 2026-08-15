#region

using System;
using System.IO;
using UnityEngine;

#endregion

namespace IbrahKit.Utilities
{
    /// <summary>
    ///     Static Utility Class providing image related utility methods
    /// </summary>
    public static class Image_Utilities
    {
        private const string FORMAT = "yyyy-MM-dd HH-mm-ss";

        public static void Screenshot()
        {
            string fileName = "Screenshot-" + DateTime.Now.ToString(FORMAT) + ".png";

            string screenshotsPath = Path.Combine(FileSystem_Utilities.GetGamePath(), "Screenshots");

            if (!Directory.Exists(screenshotsPath)) Directory.CreateDirectory(screenshotsPath);

            ScreenCapture.CaptureScreenshot(Path.Combine(screenshotsPath, fileName));
        }

        public static int GetPixelIndex(int x, int y, int width)
        {
            return x + (y * width);
        }

        public static byte[] ToByteArray(this Sprite sprite)
        {
            if (sprite == null)
            {
                throw new NullReferenceException("Sprite is null");
            }

            return sprite.texture.ToByteArray();
        }

        public static byte[] ToByteArray(this Texture2D texture)
        {
            if (texture == null)
            {
                throw new NullReferenceException("Texture is null");
            }

            return texture.EncodeToPNG();
        }

        public static Sprite ByteArrayToSprite(byte[] bytes, Vector2Int size, TextureFormat format = TextureFormat.RGBA32, bool mipChain = false)
        {
            if (bytes == null)
            {
                throw new NullReferenceException("Bytes array is null");
            }

            if (bytes.Length == 0)
            {
                throw new Exception("Bytes array is empty");
            }

            Texture2D texture = ByteArrayToTexture(bytes, size, format, mipChain);
            
            return texture.ToSprite();
        }
        
        public static Texture2D ByteArrayToTexture(byte[] bytes, Vector2Int size, TextureFormat format, bool mipChain)
        {
            if (bytes == null)
            {
                throw new NullReferenceException("Bytes array is null");
            }

            if (bytes.Length == 0)
            {
                throw new Exception("Bytes array is empty");
            }

            Texture2D texture = new(size.x, size.y, format, mipChain);

            texture.LoadRawTextureData(bytes);

            texture.Apply();

            return texture;
        }

        public static Sprite ToSprite(this Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));
        }

        public static Sprite Grayscale(this Sprite sprite)
        {
            if (sprite == null)
            {
                throw new NullReferenceException("Sprite is null");
            }

            Texture2D texture2D = sprite.texture;

            Color[] pixels = texture2D.GetPixels();

            for (int i = 0; i < pixels.Length; i++)
            {
                float luminance = pixels[i].Luminance();

                float range = Mathf.Lerp(0, 1, luminance);

                pixels[i] = new(range, range, range, pixels[i].a);
            }

            Texture2D tex = new(texture2D.width, texture2D.height, texture2D.format, false);

            tex.SetPixels(pixels);

            tex.Apply();

            Rect rect = new(0, 0, tex.width, tex.height);

            return Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f));
        }


        public static Sprite Center(this Sprite sprite)
        {
            Texture2D tex = sprite.texture;

            Color[] colors = tex.GetPixels();

            Vector4Int minMax = new Vector4Int(int.MaxValue, int.MinValue, int.MaxValue, int.MinValue);

            for (int y = 0; y < tex.height; y++)
            {
                for (int x = 0; x < tex.width; x++)
                {
                    Color c = colors[GetPixelIndex(x, y, tex.width)];

                    if (c.a == 0) continue;

                    if (x < minMax[0])
                    {
                        minMax[0] = x;
                    }

                    if (x > minMax[1])
                    {
                        minMax[1] = x;
                    }

                    if (y < minMax[2])
                    {
                        minMax[2] = y;
                    }

                    if (y > minMax[3])
                    {
                        minMax[3] = y;
                    }
                }
            }

            int xOffset = (tex.width / 2) - ((minMax[1] - minMax[0]) / 2) - minMax[0];

            int yOffset = (tex.height / 2) - ((minMax[3] - minMax[2]) / 2) - minMax[2];

            Color transparent = new(0, 0, 0, 0);

            Color[] newColors = new Color[colors.Length];

            for (var i = 0; i < colors.Length; i++)
            {
                colors[i] = transparent;
            }

            for (int y = 0; y < tex.height; y++)
            {
                for (int x = 0; x < tex.width; x++)
                {
                    int newIndexX = x + xOffset;

                    int newIndexY = y + yOffset;

                    if (newIndexX > 0 && newIndexX < tex.width && newIndexY > 0 && newIndexY < tex.height)
                    {
                        newColors[GetPixelIndex(newIndexX, newIndexY, tex.width)] =
                            colors[GetPixelIndex(x, y, tex.width)];
                    }
                }
            }

            Texture2D newTex = new(tex.width, tex.height, tex.format, false);

            newTex.SetPixels(newColors);

            newTex.Apply();

            Sprite newSprite = Sprite.Create(newTex, new(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f),
                sprite.pixelsPerUnit);

            return newSprite;
        }

        public static Texture2D DownscaleNearest(this Texture2D from, Vector2Int newSize)
        {
            Texture2D to = new Texture2D(newSize.x, newSize.y, from.format, from.mipmapCount > 1)
            {
                filterMode = from.filterMode,
                
            };

            float scaleX = from.width / (float) newSize.x;
            float scaleY = from.height / (float) newSize.y;

            for (int y = 0; y < newSize.y; y++)
            {
                float srcY = Mathf.Floor((float)(y + 0.5) * scaleY);
                srcY = Mathf.Clamp(srcY, 0, from.height - 1);

                for (int x = 0; x < newSize.x; x++)
                {
                    float srcX = Mathf.Floor((float)(x + 0.5) * scaleX);
                    srcX = Mathf.Clamp(srcX, 0, from.width - 1);
                    
                    to.SetPixel(x,y,from.GetPixel((int)srcX,(int)srcY));
                }
            }
            
            to.Apply();

            return to;
        }

        public static Texture2D Lerp(Texture2D from, Texture2D to, float t)
        {
            if (from.width != to.width || from.height != to.height)
            {
                throw new ArgumentOutOfRangeException(
                    $"Textures must be the same size: from: {from.width}x{from.height} to: {to.width}x{to.height}");
            }

            Texture2D tex = new(from.width, from.height, from.format, from.mipmapCount > 1);

            Color[] pixelsFrom = from.GetPixels();

            Color[] pixelsTo = to.GetPixels();

            Color[] pixelsNew = new Color[pixelsFrom.Length];

            for (int y = 0; y < tex.height; y++)
            {
                for (int x = 0; x < tex.width; x++)
                {
                    int index = GetPixelIndex(x, y, from.width);

                    pixelsNew[index] = Color_Utilities.ColorBlend(pixelsFrom[index].WithAlpha(t),
                        pixelsTo[index].WithAlpha(1 - t));
                }
            }

            tex.SetPixels(pixelsNew);

            tex.Apply();

            return tex;
        }
    }
}