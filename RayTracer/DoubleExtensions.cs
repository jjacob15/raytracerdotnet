using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public static class DoubleExtensions
    {

        public static bool DoubleEqual(this double x, double y)
        {
            if (Math.Abs(x - y) < double.Epsilon)
            {
                return true;
            }
            return false;
        }
    }
}
