using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Ray
    {
        public Ray(Tuple origin, Tuple direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Tuple Origin { get; }
        public Tuple Direction { get; }

        public Tuple Position(double t)
        {
            return Origin + Direction * t;
        }

        public Ray Transform(Matrix matrix)
        {
            return new Ray(Origin * matrix, Direction * matrix);
        }
    }
}
