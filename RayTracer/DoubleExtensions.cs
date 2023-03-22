using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public static class Constants
    {
        public const double Epsilon = 0.00001;
    }
    public static class DoubleExtensions
    {
        public static bool DoubleEqual(this double x, double y) => Math.Abs(x - y) < Constants.Epsilon;
    }
}
