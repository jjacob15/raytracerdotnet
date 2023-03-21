using System;

namespace RayTracer.Shapes
{
    public class Sphere : AbstractShape
    {
        public static Sphere GlassSphere()
        {
            var s = new Sphere();
            s.Material.Transparency = 1;
            s.Material.RefractiveIndex = 1.5;
            return s;
        }

        public Intersection Intersection(double t)
        {
            return new Intersection(this, t);
        }

        public override void IntersectLocal(Ray ray, Intersections intersections)
        {
            var SphereToRay = ray.Origin - Tuple.Point(0, 0, 0);
            var a = ray.Direction.Dot(ref ray.Direction);
            var b = 2 * ray.Direction.Dot(ref SphereToRay);
            var c = SphereToRay.Dot(ref SphereToRay) - 1;
            var discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                return;
            }

            intersections.Add(new Intersection(this, (-b - Math.Sqrt(discriminant)) / (2 * a)));
            intersections.Add(new Intersection(this, (-b + Math.Sqrt(discriminant)) / (2 * a)));
        }

        public override void IntersectLocal(ref Tuple origin, ref Tuple direction, Intersections intersections)
        {

            var SphereToRay = origin - Tuple.Point(0, 0, 0);
            var a = direction.Dot(ref direction);
            var b = 2 * direction.Dot(ref SphereToRay);
            var c = SphereToRay.Dot(ref SphereToRay) - 1;
            var discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                return;
            }
            var sqrtDiscriminant = Math.Sqrt(discriminant);

            intersections.Add(new Intersection(this, (-b - sqrtDiscriminant) / (2 * a)));
            intersections.Add(new Intersection(this, (-b + sqrtDiscriminant) / (2 * a)));
        }

        public override Tuple NormalAtLocal(Tuple localPoint)
        {
            return localPoint - Tuple.Point(0, 0, 0);
        }
    }
}
