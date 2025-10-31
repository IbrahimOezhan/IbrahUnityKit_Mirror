using UnityEngine;

namespace IbrahKit
{
    public static class Color_Utilities
    {
        /// <summary>
        /// Compares one color to another and returns whether they are the same or not
        /// </summary>
        /// <param name="color1">The first color</param>
        /// <param name="color2">The second color</param>
        /// <param name="tolerance">How tolerant the comparion should be</param>
        /// <returns>Whether they are the same or not</returns>
        public static bool CompareTo(this Color color1, Color color2, float tolerance)
        {
            if (Mathf.Abs(color1.r - color2.r) > tolerance) return true;

            if (Mathf.Abs(color1.b - color2.b) > tolerance) return true;

            if (Mathf.Abs(color1.g - color2.g) > tolerance) return true;

            return false;
        }

        /// <summary>
        /// Formats a string to use a color
        /// </summary>
        /// <param name="color">The color to use</param>
        /// <param name="value">The string to format</param>
        /// <returns>The formatted string</returns>
        public static string UseOnString(this Color color, string value)
        {
            return $"<color={color.ToHex()}>{value}</color>";
        }

        /// <summary>
        /// Converts a color to its hex value
        /// </summary>
        /// <param name="color">The color to convert</param>
        /// <param name="includeAlpha">Whether to include alpha</param>
        /// <returns>The hex representation of the color</returns>
        public static string ToHex(this Color color, bool includeAlpha = false)
        {
            Color32 c = color; // Auto-converts to Color32 with 0–255 range
            return includeAlpha ? $"#{c.r:X2}{c.g:X2}{c.b:X2}{c.a:X2}" : $"#{c.r:X2}{c.g:X2}{c.b:X2}";
        }

        /// <summary>
        /// Blends multiple colors together
        /// </summary>
        /// <param name="colors">The colors to blend</param>
        /// <returns>The blended color</returns>
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

        /// <summary>
        /// Applies a certain alpha to a color
        /// </summary>
        /// <param name="c">The color</param>
        /// <param name="alpha">The alpha value to apply</param>
        /// <returns>The color with the specified alpha</returns>
        public static Color WithAlpha(this Color c, float alpha)
        {
            if (alpha < 0 || alpha > 1)
            {
                Debug.LogWarning($"Alpha with value {alpha} out of bounds for min 0 and max 1");

                alpha = Mathf.Clamp01(alpha);
            }

            return new(c.r, c.g, c.b, alpha);
        }

        /// <summary>
        /// Gets the luminance of a color
        /// </summary>
        /// <param name="color">The color to use</param>
        /// <returns>The colors luminance</returns>
        public static float Luminance(this Color color)
        {
            return 0.2126f * color.r + 0.7152f * color.g + 0.0722f * color.b;
        }

        /// <summary>
        /// Inverts a color
        /// </summary>
        /// <param name="color">The color to invert</param>
        /// <returns>The inverted color</returns>
        public static Color Invert(this Color color)
        {
            return new Color(1 - color.r, 1 - color.g, 1 - color.b, color.a);
        }

        /// <summary>
        /// Averages the individual RGB values of a color
        /// </summary>
        /// <param name="color">The color to use</param>
        /// <returns>The averge of R, G and B</returns>
        public static float RGBAverage(this Color color)
        {
            return (color.r + color.g + color.b) / 3;
        }
    }
}