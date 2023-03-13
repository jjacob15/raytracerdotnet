using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Patterns
{
    public class GradientPattern : AbstractPattern
    {
        public GradientPattern(Color a, Color b) : base(a, b)
        {
        }

        public override Color PatternAt(Tuple point)
        {
            var distance = B - A;
            var fraction = point.X - Math.Floor(point.X);
            return  A + distance * fraction;
        }
    }
}
