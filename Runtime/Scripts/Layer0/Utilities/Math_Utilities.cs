#region

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

#endregion

namespace IbrahKit.Utilities
{
    /// <summary>
    ///     Static Utility Class providing math related utility methods
    /// </summary>
    public static class Math_Utilities
    {
        public static float DegreeFormat(float degree)
        {
            if (degree < 0) return 360 + degree;
            return degree;
        }

        public static Vector3 WithY(this Vector3 vector, float value)
        {
            return new Vector3(vector.x, value, vector.z);
        }

        public static Vector3 Negate(this Vector3 vector)
        {
            return -vector;
        }

        public static void ForEach(this int i, Action<int> action, bool inclusive = true)
        {
            for (int j = 0; j < (inclusive ? i + 1 : i); j++)
            {
                action.Invoke(i);
            }
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360f) angle += 360f;
            if (angle > 360f) angle -= 360f;
            return Mathf.Clamp(angle, min, max);
        }

        public static float DegreeDifference(float degree1, float degree2)
        {
            return Mathf.Abs(degree1 - degree2);
        }

        public static float DegreeInvert(float degree)
        {
            if (degree < 0) return 360 + degree;
            else return -360 + degree;
        }

        public static (float, float) DegreeShortest(float degree1, float degree2)
        {
            float degree1Inv = DegreeInvert(degree1);

            float degree2Inv = DegreeInvert(degree2);

            float ab12 = DegreeDifference(degree1, degree2);

            float ab1Inv2 = DegreeDifference(degree1Inv, degree2);

            float ab1I2Inv = DegreeDifference(degree1, degree2Inv);

            if (ab12 < ab1Inv2 && ab12 < ab1I2Inv) return (degree1, degree2);

            if (ab1Inv2 < ab12 && ab1Inv2 < ab1I2Inv) return (degree1Inv, degree2);

            return (degree1, degree2Inv);
        }

        public static float Map(float x, Vector4 ranges)
        {
            return Map(x, ranges.x, ranges.y, ranges.z, ranges.w);
        }

        public static float Map(float x, float inMin, float inMax, float outMin, float outMax)
        {
            return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }

        public static int GetRandomEvent(List<int> chances)
        {
            if (chances == null)
            {
                throw new NullReferenceException("Passed chances list is null");
            }

            if (chances.Count == 0)
            {
                throw new ArgumentOutOfRangeException("Passed chances list is empty");
            }

            if (chances.Count == 1)
            {
                Debug.Log("Returned the only element");
                return 0;
            }

            List<int> startAt = new();

            int totalValue = -1;

            for (int i = 0; i < chances.Count; i++)
            {
                startAt.Add(totalValue + 1);
                totalValue += chances[i];
            }

            int rdm = Random.Range(0, totalValue);

            for (int i = 0; i < chances.Count; i++)
            {
                if (rdm >= startAt[i] && rdm < startAt[i] + chances[i])
                {
                    return i;
                }
            }

            return Random.Range(0, chances.Count);
        }

        public static int Loop(this int number, int min, int max)
        {
            return (int)((float)number).Loop(min, max);
        }

        public static float Loop(this float number, float min, float max)
        {
            if (number < min)
            {
                return max;
            }

            if (number > max)
            {
                return min;
            }

            return number;
        }

        public static float LerpWithVector2(Vector2 vector, float t)
        {
            return Mathf.Lerp(vector.x, vector.y, t);
        }

        public static float GetRandom(this Vector2 vector)
        {
            return Random.Range(vector.x, vector.y);
        }

        public static int GetRandom(this Vector2Int vector)
        {
            return Random.Range(vector.x, vector.y + 1);
        }

        public static bool IsInRange(this int value, int min, int max)
        {
            return value >= min && value <= max;
        }

        public static float Normalize(this float value, float min = 0, float max = 1)
        {
            return (value - min) / (max - min);
        }
    }
}