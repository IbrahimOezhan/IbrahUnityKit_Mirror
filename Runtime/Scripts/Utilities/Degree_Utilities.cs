using UnityEngine;

namespace IbrahKit
{
    public class Degree_Utilities
    {
        public static float Format(float degree)
        {
            if (degree < 0) return 360 + degree;
            return degree;
        }

        public static float Difference(float degree1, float degree2)
        {
            return Mathf.Abs(degree1 - degree2);
        }

        public static float Invert(float degree)
        {
            if (degree < 0) return 360 + degree;
            else return -360 + degree;
        }

        public static (float, float) Shortest(float degree1, float degree2)
        {
            float degree1Inv = Invert(degree1);
            float degree2Inv = Invert(degree2);

            float ab12 = Difference(degree1, degree2);
            float ab1Inv2 = Difference(degree1Inv, degree2);
            float ab1I2Inv = Difference(degree1, degree2Inv);

            if (ab12 < ab1Inv2 && ab12 < ab1I2Inv) return (degree1, degree2);
            if (ab1Inv2 < ab12 && ab1Inv2 < ab1I2Inv) return (degree1Inv, degree2);

            return (degree1, degree2Inv);
        }
    }
}