using UnityEngine;

namespace IbrahKit
{
    public static class Color_Utilities
    {
        public static bool CompareTo(this Color color1, Color color2, float tolerance)
        {
            if (Mathf.Abs(color1.r - color2.r) > tolerance) return true;

            if (Mathf.Abs(color1.b - color2.b) > tolerance) return true;

            if (Mathf.Abs(color1.g - color2.g) > tolerance) return true;

            return false;
        }

        public static string UseOnString(this Color color, string msg)
        {
            return $"<color={color.ToHex()}>{msg}</color>";
        }

        public static string ToHex(this Color color, bool includeAlpha = false)
        {
            Color32 c = color; // Auto-converts to Color32 with 0–255 range
            return includeAlpha ? $"#{c.r:X2}{c.g:X2}{c.b:X2}{c.a:X2}" : $"#{c.r:X2}{c.g:X2}{c.b:X2}";
        }

        public static Color ColorBlend(params Color[] colors)
        {
            if (colors == null)
            {
                Debug.LogWarning("Color list is null");
                return Color.white;
            }

            if (colors.Length == 0)
            {
                Debug.LogWarning("Color list is empty");
                return Color.white;
            }

            if (colors.Length == 1) return colors[0];

            Color newCol = colors[0];

            for (int i = 1; i < colors.Length; i++)
            {
                newCol = Color.Lerp(newCol, colors[i], .5f);
            }

            return newCol;
        }

        public static Color WithAlpha(this Color c, float alpha)
        {
            if (alpha < 0 || alpha > 1)
            {
                Debug.LogWarning($"Alpha with value {alpha} out of bounds for min 0 and max 1");

                alpha = Mathf.Clamp01(alpha);
            }

            return new(c.r, c.g, c.b, alpha);
        }

        public static float Luminance(this Color color)
        {
            return 0.2126f * color.r + 0.7152f * color.g + 0.0722f * color.b;
        }

        public static Color Invert(this Color color)
        {
            return new Color(1 - color.r, 1 - color.g, 1 - color.b, color.a);
        }

        public static float RGBAverage(this Color color)
        {
            return (color.r + color.g + color.b) / 3;
        }
    }
}