using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RayTracer
{
    public struct Ray
    {
        public Ray(Tuple origin, Tuple direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Tuple Origin;
        public Tuple Direction;

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
