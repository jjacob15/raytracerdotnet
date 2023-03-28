using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Shapes
{
    public class Cylinder : AbstractShape
    {
        public override void IntersectLocal(ref Tuple origin, ref Tuple direction, Intersections intersections)
        {
            var a = direction.X * direction.X + direction.Z * direction.Z;
            if (a.DoubleEqual(0))
                return;

            var b = 2 * origin.X * direction.X + 2 * origin.Z * direction.Z;
            var c = origin.X * origin.X + origin.Z * origin.Z - 1;

            var disc = b * b - 4 * a * c;

            if (disc < 0)
                return;


            var t0 = (-b - Math.Sqrt(disc)) / (2 * a);
            var t1 = (-b + Math.Sqrt(disc)) / (2 * a);

            intersections.Add(new Intersection(this, t0));
            intersections.Add(new Intersection(this, t1));
        }

        public override Tuple NormalAtLocal(Tuple localPoint)
        {
            return Tuple.Vector(localPoint.X, 0, localPoint.Z);
        }
    }
}
