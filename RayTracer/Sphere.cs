using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Sphere : IShape
    {
        public  Guid Id { get; }
        private Intersections intersections = new Intersections();

        private Matrix transform = Matrix.Identity();

        public Sphere()
        {
            Id = Guid.NewGuid();
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if(obj is Sphere)
            {
                Sphere typed = (Sphere)obj;
                return typed.Id == this.Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public void SetTransform(Matrix matrix)
        {
            transform = matrix;
        }

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
            var transformedRay = ray.Transform(transform.Inverse());
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
    }
}
