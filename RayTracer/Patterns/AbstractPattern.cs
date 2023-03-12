using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Patterns
{
    public abstract class AbstractPattern
    {
        public Color A { get; }
        public Color B { get; }

        public abstract Color StipeAt(Tuple point);

        public AbstractPattern(Color a, Color b)
        {
            A = a;
            B = b;
        }
    }
}
