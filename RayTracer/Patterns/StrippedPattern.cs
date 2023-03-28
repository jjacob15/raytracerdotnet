using RayTracer.Shapes;
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

        public StrippedPattern() : this(Color._White, Color._Black)
        {

        }

        public override Color PatternAt(Tuple point)
        {
            if (Math.Abs(Math.Floor(point.X)) % 2 < double.Epsilon) return A;

            return B;
        }
    }
}
