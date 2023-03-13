using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Patterns
{
    public class RingPattern : AbstractPattern
    {
        public RingPattern(Color a, Color b) : base(a, b)
        {
        }

        public override Color PatternAt(Tuple point)
        {
            var distance = Math.Sqrt(point.X * point.X + point.Z * point.Z);
            if (distance % 2 == 0) return A;
            return B;
        }
    }
}
