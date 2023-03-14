using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Patterns
{
    public abstract class AbstractPattern : IPattern
    {
        public Color A { get; }
        public Color B { get; }

        public Matrix Transform { get; set; } = Matrix.Identity();

        public abstract Color PatternAt(Tuple point);
        public Color PatternAtShape(IShape shape, Tuple worldPoint)
        {
            var objectPoint = shape.Transform.Inverse() * worldPoint;
            var patternPoint = Transform.Inverse() * objectPoint;
            return PatternAt(patternPoint);
        }

        public AbstractPattern()
        {

        }
        public AbstractPattern(Color a, Color b)
        {
            A = a;
            B = b;
        }
    }
}
