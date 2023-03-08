using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Intersection
    {
        public double T { get; }
        public Sphere Object { get; }

        public Intersection(Sphere shape,double t)
        {
            Object = shape;
            T = t;
        }

        public override bool Equals(object obj)
        {
            if (Object.GetType() != obj.GetType())
                return false;

            return Object.Equals(obj);
        }

        public override int GetHashCode()
        {
            int hash = 17;

            hash = hash * 23 + Object.Id.GetHashCode();
            hash = hash * 23 + T.GetHashCode();

            return hash;
        }
    }
}
