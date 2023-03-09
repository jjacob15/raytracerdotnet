using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace RayTracer
{
    public class Intersection : IComparable<Intersection>
    {
        public double T { get; }
        public Sphere Object { get; }

        public Intersection(Sphere shape, double t)
        {
            Object = shape;
            T = t;
        }

        public override bool Equals(object obj)
        {
            return obj is Intersection intersection &&
                   T == intersection.T &&
                   EqualityComparer<Sphere>.Default.Equals(Object, intersection.Object);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(T, Object);
        }

        public int CompareTo([AllowNull] Intersection other)
        {
            if (other is null) return 1;

            if (T > other.T) return 1;
            if (T < other.T) return -1;
            return 0;
        }

        public IntersectionState PrepareComputations(Ray ray)
        {
            //return new IntersectionState(T, Object, ray.Position(T), -ray.Direction, Object.NormalAt(ray.Position(T)));
            return new IntersectionState(this, ray);
        }
    }
}
