using System;

namespace RayTracer.Shapes
{
    public class Sphere : AbstractShape
    {
        public Intersection Intersection(double t)
        {
            return new Intersection(this, t);
        }

        public override Intersections IntersectLocal(Ray ray)
        {
            var intersections = new Intersections();
            var SphereToRay = ray.Origin - Tuple.Point(0, 0, 0);
            var a = ray.Direction.Dot(ray.Direction);
            var b = 2 * ray.Direction.Dot(SphereToRay);
            var c = SphereToRay.Dot(SphereToRay) - 1;
            var discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                return intersections;
            }

            intersections.Add(new Intersection(this, (-b - Math.Sqrt(discriminant)) / (2 * a)));
            intersections.Add(new Intersection(this, (-b + Math.Sqrt(discriminant)) / (2 * a)));

            return intersections;

        }
        
        public override Tuple NormalAtLocal(Tuple localPoint)
        {
            return localPoint - Tuple.Point(0, 0, 0);
        }
    }
}
