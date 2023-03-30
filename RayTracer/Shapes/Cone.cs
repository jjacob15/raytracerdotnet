using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Shapes
{
    public class Cone : AbstractShape
    {
        public double Minimum { get; set; } = double.NegativeInfinity;
        public double Maximum { get; set; } = double.PositiveInfinity;
        public bool Closed { get; set; }

        public override Bounds Box => new Bounds { Min = Tuple.Point(-1, -1, -1), Max = Tuple.Point(1, 1, 1) };

        public Cone(double min, double max, bool closed)
        {
            Minimum = min;
            Maximum = max;
            Closed = closed;
        }

        public Cone()
        {

        }
        public override void IntersectLocal(ref Tuple origin, ref Tuple direction, Intersections intersections)
        {
            var a = direction.X * direction.X - direction.Y * direction.Y + direction.Z * direction.Z;
            var b = 2 * origin.X * direction.X - 2 * origin.Y * direction.Y + 2 * origin.Z * direction.Z;
            var c = origin.X * origin.X - origin.Y * origin.Y + origin.Z * origin.Z;

            if (a.DoubleEqual(0) && b.DoubleEqual(0))
            {
                return;
            }
            if (a.DoubleEqual(0) && !b.DoubleEqual(0))
            {
                var t = -c / 2 * b;
                intersections.Add(new Intersection(this, t));
            }

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

            IntersectCaps(ref origin, ref direction, intersections);
        }
        public override Tuple NormalAtLocal(Tuple localPoint)
        {
            //compute the square of the distance from the y axis
            var dist = localPoint.X * localPoint.X + localPoint.Z * localPoint.Z;
            if (dist < 1 && localPoint.Y >= Maximum - Constants.Epsilon)
                return Tuple.Vector(0, 1, 0);

            if (dist < 1 && localPoint.Y <= Minimum + Constants.Epsilon)
                return Tuple.Vector(0, -1, 0);

            var y = Math.Sqrt(dist);
            if (localPoint.Y > 0)
            {
                y *= -1;
            }

            return Tuple.Vector(localPoint.X, y, localPoint.Z);
        }

        // a helper function to reduce duplication.
        // checks to see if the intersection at `t` is within a radius
        // of 1 (the radius of your cylinders) from the y axis.
        private bool CheckCaps(ref Tuple origin, ref Tuple direction, double t, double y)
        {
            var x = origin.X + t * direction.X;
            var z = origin.Z + t * direction.Z;
            return (x * x + z * z) <= y * y;
        }

        private void IntersectCaps(ref Tuple origin, ref Tuple direction, Intersections intersections)
        {
            //caps only matter if the cylinder is closed, and might possibly be
            //intersected by the ray.
            if (!Closed || direction.Y.DoubleEqual(0))
            {
                return;
            }
            //check for an intersection with the lower end cap by intersecting
            //the ray with the plane at y=cyl.minimum
            var t0 = (Minimum - origin.Y) / direction.Y;
            if (CheckCaps(ref origin, ref direction, t0, Minimum))
            {
                intersections.Add(new Intersection(this, t0));
            }
            //check for an intersection with the upper end cap by intersecting
            //the ray with the plane at y=cyl.maximum

            var t1 = (Maximum - origin.Y) / direction.Y;
            if (CheckCaps(ref origin, ref direction, t1, Maximum))
            {
                intersections.Add(new Intersection(this, t1));
            }
        }
    }
}
