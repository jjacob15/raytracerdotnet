using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Patterns
{
    public interface IPattern : ITransformable
    {
        public Color PatternAt(Tuple point);
        public Color PatternAtShape(IShape shape, Tuple worldPoint);
    }
}
