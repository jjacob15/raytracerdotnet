using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Shapes
{
    public class Plane : AbstractShape
    {
        public override Intersections IntersectLocal(Ray ray)
        {
            if (Math.Abs(ray.Direction.Y)< double.Epsilon)
            {
                return new Intersections();
            }

            var t = -ray.Origin.Y / ray.Direction.Y;
            return new Intersections(new Intersection(this, t));
        }

        public override Intersections IntersectLocal(ref Tuple origin, ref Tuple direction)
        {
            if (Math.Abs(direction.Y) < double.Epsilon)
            {
                return new Intersections();
            }

            var t = -origin.Y / direction.Y;
            return new Intersections(new Intersection(this, t));
        }

        public override Tuple NormalAtLocal(Tuple localPoint)
        {
            return Tuple.Vector(0, 1, 0);
        }
    }
}
