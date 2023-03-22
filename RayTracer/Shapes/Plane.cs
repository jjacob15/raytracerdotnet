using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Shapes
{
    public class Plane : AbstractShape
    {
        public override void IntersectLocal(Ray ray, Intersections intersections)
        {
            if (Math.Abs(ray.Direction.Y) < Constants.Epsilon)
            {
                return;
            }

            var t = -ray.Origin.Y / ray.Direction.Y;
            intersections.Add(new Intersection(this, t));
        }

        public override void IntersectLocal(ref Tuple origin, ref Tuple direction, Intersections intersections)
        {
            if (Math.Abs(direction.Y) < Constants.Epsilon)
            {
                return;
            }

            var t = -origin.Y / direction.Y;
            intersections.Add(new Intersection(this, t));
        }

        public override Tuple NormalAtLocal(Tuple localPoint)
        {
            return Tuple.Vector(0, 1, 0);
        }
    }
}
