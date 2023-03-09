using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Sphere
    {
        private Intersections intersections = new Intersections();
        public Matrix Transform { get; set; } = Matrix.Identity();
        public Material Material { get; set; } = new Material();

        public void AddIntersection(double t)
        {
            intersections.Add(new Intersection(this, t));
        }

        public Intersection Intersection(double t)
        {
            return new Intersection(this, t);
        }
        public Intersections Intersect(Ray ray)
        {
            intersections = new Intersections();
            var transformedRay = ray.Transform(Transform.Inverse());
            var SphereToRay = transformedRay.Origin - Tuple.Point(0, 0, 0);
            var a = transformedRay.Direction.Dot(transformedRay.Direction);
            var b = 2 * transformedRay.Direction.Dot(SphereToRay);
            var c = SphereToRay.Dot(SphereToRay) - 1;
            var discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                return intersections;
            }

            AddIntersection((-b - Math.Sqrt(discriminant)) / (2 * a));
            AddIntersection((-b + Math.Sqrt(discriminant)) / (2 * a));

            return intersections;
        }

        public Tuple NormalAt(Tuple worldPoint)
        {
            var objectPoint = Transform.Inverse() * worldPoint;
            var objectNormal = objectPoint - Tuple.Point(0, 0, 0);
            var worldNormal = Transform.Inverse().Transpose() * objectNormal;
            worldNormal.W = 0;
            return worldNormal.Normalize();
        }

        public override bool Equals(object obj)
        {
            return obj is Sphere sphere &&
                   EqualityComparer<Matrix>.Default.Equals(Transform, sphere.Transform) &&
                   EqualityComparer<Material>.Default.Equals(Material, sphere.Material);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Transform, Material);
        }
    }
}
