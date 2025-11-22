using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    public static class Math_Utilities
    {
        public static float DegreeFormat(float degree)
        {
            if (degree < 0) return 360 + degree;
            return degree;
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

        public static float Map(float _x, float _in_min, float _in_max, float _out_min, float _out_max)
        {
            return (_x - _in_min) * (_out_max - _out_min) / (_in_max - _in_min) + _out_min;
        }

        public static int GetRandomEvent(List<int> chances)
        {
            if (chances == null)
            {
                IbrahDebug.LogWarning("Passed chances list is null");
                return -1;
            }

            if (chances.Count == 0)
            {
                IbrahDebug.LogWarning("Passed chances list is empty");
                return -1;
            }

            if (chances.Count == 1)
            {
                IbrahDebug.Log("Returned the only element");
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

        public static int LoopNumber(int number, int min, int max)
        {
            return (int)LoopNumber((float)number, (float)min, (float)max);
        }

        public static float LoopNumber(float number, float min, float max)
        {
            if (number < min)
            {
                return max;
            }
            else if (number > max)
            {
                return min;
            }
            return number;
        }

        public static bool IsInRange(int value, int min, int max)
        {
            return value >= min && value <= max;
        }

        public static float Normalize(float value, float min, float max)
        {
            return (value - min) / (max - min);
        }
    }
}