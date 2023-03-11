using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public static class Constants
    {
        public static double EPSILON = 0.00001;
    }
    public static class DoubleExtensions
    {

        public static bool DoubleEqual(this double x, double y)
        {
            if (Math.Abs(x - y) < Constants.EPSILON)
            {
                return true;
            }
            return false;
        }
    }
}
