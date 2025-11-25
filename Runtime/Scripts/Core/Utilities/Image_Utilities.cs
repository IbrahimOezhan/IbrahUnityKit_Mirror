using IbrahKit.Debug;
using Sirenix.Utilities;
using System;
using System.IO;
using UnityEngine;

namespace IbrahKit
{
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

        public static byte[] ImageToByteArray(Sprite sprite)
        {
            if (sprite == null)
            {
                IbrahDebug.LogWarning("Sprite is null");
                return new byte[0];
            }

            return ImageToByteArray(sprite.texture);
        }

        public static byte[] ImageToByteArray(Texture2D texture)
        {
            if (texture == null)
            {
                IbrahDebug.LogWarning("Texture is null");
                return new byte[0];
            }

            return texture.EncodeToPNG();
        }

        public static Sprite ByteArrayToSprite(byte[] bytes, Vector2Int size)
        {
            if (bytes == null)
            {
                IbrahDebug.LogWarning("Bytes array is null");
                return Sprite.Create(new(0, 0), new(0, 0, 0, 0), new(0, 0));
            }

            if (bytes.Length == 0)
            {
                IbrahDebug.LogWarning("Bytes array is empty");
                return Sprite.Create(new(0, 0), new(0, 0, 0, 0), new(0, 0));
            }

            Texture2D texture = ByteArrayToTexture(bytes, size);

            Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            return sprite;
        }

        public static Sprite GrayscaleSprite(Sprite _sprite)
        {
            if (_sprite == null)
            {
                IbrahDebug.LogWarning("Sprite is empty");

                return Sprite.Create(new(0, 0), new(0, 0, 0, 0), new(0, 0));
            }

            Texture2D texture2D = _sprite.texture;

            Color[] pixels = texture2D.GetPixels();

            for (int i = 0; i < pixels.Length; i++)
            {
                float lumincance = pixels[i].Luminance();

                float range = Mathf.Lerp(0, 1, lumincance);

                pixels[i] = new(range, range, range, pixels[i].a);
            }

            Texture2D tex = new(texture2D.width, texture2D.height, texture2D.format, false);

            tex.SetPixels(pixels);

            tex.Apply();

            Rect rect = new(0, 0, tex.width, tex.height);

            return Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f));
        }

        public static Sprite Center(Sprite sprite)
        {
            Texture2D tex = sprite.texture;

            Color[] colors = tex.GetPixels();

            int minX = int.MaxValue;

            int maxX = int.MinValue;

            int minY = int.MaxValue;

            int maxY = int.MinValue;

            for (int y = 0; y < tex.height; y++)
            {
                for (int x = 0; x < tex.width; x++)
                {
                    Color c = colors[GetPixelIndex(x, y, tex.width)];

                    if (c.a != 0)
                    {
                        if (x < minX)
                        {
                            minX = x;
                        }
                        if (x > maxX)
                        {
                            maxX = x;
                        }
                        if (y < minY)
                        {
                            minY = y;
                        }
                        if (y > maxY)
                        {
                            maxY = y;
                        }
                    }
                }
            }

            int xOffset = ((tex.width / 2) - ((maxX - minX) / 2)) - minX;

            int yOffset = ((tex.height / 2) - ((maxY - minY) / 2)) - minY;

            Color transparent = new(0, 0, 0, 0);

            Color[] newColors = new Color[colors.Length];

            newColors.ForEach(a => a = transparent);

            for (int y = 0; y < tex.height; y++)
            {
                for (int x = 0; x < tex.width; x++)
                {
                    int newIndexX = x + xOffset;

                    int newIndexY = y + yOffset;

                    if (newIndexX > 0 && newIndexX < tex.width && newIndexY > 0 && newIndexY < tex.height)
                    {
                        newColors[GetPixelIndex(newIndexX, newIndexY, tex.width)] = colors[GetPixelIndex(x, y, tex.width)];
                    }
                }
            }

            Texture2D newTex = new Texture2D(tex.width, tex.height, TextureFormat.RGBA32, false);

            newTex.SetPixels(newColors);

            newTex.Apply();

            Sprite newSprite = Sprite.Create(newTex, new(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f), sprite.pixelsPerUnit);

            return newSprite;
        }

        public static Texture2D ByteArrayToTexture(byte[] bytes, Vector2Int size)
        {
            if (bytes == null)
            {
                IbrahDebug.LogWarning("Bytes array is null");
                return new(0, 0);
            }

            if (bytes.Length == 0)
            {
                IbrahDebug.LogWarning("Bytes array is empty");
                return new(0, 0);
            }

            Texture2D texture = new(size.x, size.y, TextureFormat.RGBA32, false);

            texture.LoadRawTextureData(bytes);

            texture.Apply();

            return texture;
        }

        public static Texture2D Lerp(Texture2D from, Texture2D to, float t)
        {
            if (from.width != to.width || from.height != to.height)
            {
                IbrahDebug.LogWarning("Textures must have the same size");
                return from;
            }

            Texture2D tex = new(from.width, from.height, TextureFormat.ARGB32, from.mipmapCount > 1);

            Color[] pixelsFrom = from.GetPixels();

            Color[] pixelsTo = to.GetPixels();

            Color[] pixelsNew = new Color[pixelsFrom.Length];

            for (int y = 0; y < tex.height; y++)
            {
                for (int x = 0; x < tex.width; x++)
                {
                    int index = GetPixelIndex(x, y, from.width);

                    pixelsNew[index] = Color_Utilities.ColorBlend(pixelsFrom[index].WithAlpha(t), pixelsTo[index].WithAlpha(1 - t));
                }
            }

            tex.SetPixels(pixelsNew);

            tex.Apply();

            return tex;
        }
    }
}