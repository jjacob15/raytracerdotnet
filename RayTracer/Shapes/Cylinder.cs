using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Shapes
{
    public class Cylinder : AbstractShape
    {
        public double Minimum { get; set; } = double.NegativeInfinity;
        public double Maximum { get; set; } = double.PositiveInfinity;
        public bool Closed { get; set; }

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

            if (t0 > t1)
            {
                double t = t0;
                t0 = t1;
                t1 = t;
            }

            var y0 = origin.Y + t0 * direction.Y;

            if (Minimum < y0 && y0 < Maximum)
            {
                intersections.Add(new Intersection(this, t0));
            }
            var y1 = origin.Y + t1 * direction.Y;

            if (Minimum < y1 && y1 < Maximum)
            {
                intersections.Add(new Intersection(this, t1));
            }
        }

        public override Tuple NormalAtLocal(Tuple localPoint)
        {
            return Tuple.Vector(localPoint.X, 0, localPoint.Z);
        }
    }
}
