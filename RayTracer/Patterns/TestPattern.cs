using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Patterns
{
    public class TestPattern : AbstractPattern
    {
        public override Color PatternAt(Tuple point)
        {
            return new Color(point.X, point.Y, point.Z);
        }
    }
}
