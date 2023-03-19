using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace RayTracer
{
    public class Intersection : IComparable<Intersection>
    {
        public double T { get; }
        public IShape Object { get; }

        public Intersection(IShape shape, double t)
        {
            Object = shape;
            T = t;
        }

        public override bool Equals(object obj)
        {
            return obj is Intersection intersection &&
                   T == intersection.T &&
                   EqualityComparer<IShape>.Default.Equals(Object, intersection.Object);
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

        public IntersectionState PrepareComputations(Ray ray, Intersections xs = null)
        {
            return new IntersectionState(this, ray, xs);
        }

    }
}
