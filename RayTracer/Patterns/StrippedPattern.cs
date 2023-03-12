using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Patterns
{
    public class StrippedPattern : AbstractPattern
    {
        public StrippedPattern(Color a, Color b) : base(a, b)
        {
        }

        public override Color StipeAt(Tuple point)
        {
            if (Math.Floor(point.X) % 2 == 0) return A;

            return B;
        }
    }
}
