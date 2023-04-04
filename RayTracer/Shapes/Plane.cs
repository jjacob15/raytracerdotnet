using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Shapes
{
    public class Plane : AbstractShape
    {
        public override Bounds Box { get; } = new Bounds { Min = Tuple.Point(double.NegativeInfinity, 0, double.NegativeInfinity), Max = Tuple.Point(double.PositiveInfinity, 0, double.PositiveInfinity) };

        public override void IntersectLocal(ref Tuple origin, ref Tuple direction, Intersections intersections)
        {
            if (Math.Abs(direction.Y) < Constants.Epsilon)
            {
                return;
            }

            var t = -origin.Y / direction.Y;
            intersections.Add(new Intersection(this, t));
        }

        public override Tuple NormalAtLocal(Tuple localPoint, Intersection hit = null)
        {
            return Tuple.Vector(0, 1, 0);
        }
    }
}
