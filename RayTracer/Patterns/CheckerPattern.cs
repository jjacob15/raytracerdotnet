using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Patterns
{
    public class CheckerPattern : AbstractPattern
    {
        public CheckerPattern(Color a, Color b) : base(a, b)
        {
        }
        public CheckerPattern() : this(Color._Black, Color._White)
        {

        }

        public override Color PatternAt(Tuple point)
        {
            //var d = Math.Floor(point.X) + Math.Floor(point.Y) + Math.Floor(point.Z);
            //if (Math.Floor(d) % 2 < Constants.Epsilon)
            //{
            //    return A;
            //}
            var d = Math.Floor(point.X) + Math.Floor(point.Y) + Math.Floor(point.Z);
            if (Math.Abs(d % 2) < Constants.Epsilon)
            {
                return A;
            }
            return B;
        }
    }
}
