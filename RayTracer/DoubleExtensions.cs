using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public static class DoubleExtensions
    {
        private const double EPSILON = 0.00001;

        public static bool DoubleEqual(this double x, double y)
        {
            if (Math.Abs(x - y) < EPSILON)
            {
                return true;
            }
            return false;
        }
    }
}
