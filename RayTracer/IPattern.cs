using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public interface IPattern : ITransformable
    {
        public Color PatternAt(Tuple point);
        public Color PatternAtShape(IShape shape, ref Tuple worldPoint);
    }
}
