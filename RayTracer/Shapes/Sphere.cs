using System;

namespace RayTracer.Shapes
{
    public class Sphere : AbstractShape
    {
        public override Bounds Box => new Bounds { Min = Tuple.Point(-1, -1, -1), Max = Tuple.Point(1, 1, 1) };

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

        public override void IntersectLocal(ref Tuple origin, ref Tuple direction, Intersections intersections)
        {

            var sphereToRay = origin - Tuple.Point(0, 0, 0);
            var a = direction.Dot(ref direction);
            var b = 2 * direction.Dot(ref sphereToRay);
            var c = sphereToRay.Dot(ref sphereToRay) - 1;
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
