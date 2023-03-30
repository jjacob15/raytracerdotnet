using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Bounds
    {
        public Tuple Min { get; set; } = Tuple.Point(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);
        public Tuple Max { get; set; } = Tuple.Point(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity);
    }
}
