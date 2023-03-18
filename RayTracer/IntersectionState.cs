using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RayTracer
{
    public class IntersectionState
    {
        public IntersectionState(Intersection intersection, ref Tuple origin,ref Tuple direction, Intersections intersections)
        {
            //instantiate a data structure for storing some precomputed values

            //copy the intersection's properties, for convenience
            T = intersection.T;
            Object = intersection.Object;

            //precompute some useful values
            Point = origin + direction * T;
            EyeV = -direction;
            NormalV = Object.NormalAt(Point);
            ReflectV = direction.Reflect(-NormalV);

            if (NormalV.Dot(EyeV) < 0)
            {
                Inside = true;
                NormalV = -NormalV;
            }


            //do not use double.Epsilon
            OverPoint = Point + NormalV * Constants.Epsilon;
            UnderPoint = Point - NormalV * Constants.Epsilon;

            if (intersections != null)
            {
                ComputeN1N2(intersection, intersections);
            }
        }
        public IntersectionState(Intersection intersection, Ray ray, Intersections intersections)
        {
            //instantiate a data structure for storing some precomputed values

            //copy the intersection's properties, for convenience
            T = intersection.T;
            Object = intersection.Object;

            //precompute some useful values
            Point = ray.Position(T);
            EyeV = -ray.Direction;
            NormalV = Object.NormalAt(ray.Position(T));
            ReflectV = ray.Direction.Reflect(-NormalV);

            if (NormalV.Dot(EyeV) < 0)
            {
                Inside = true;
                NormalV = -NormalV;
            }


            //do not use double.Epsilon
            OverPoint = Point + NormalV * Constants.Epsilon;
            UnderPoint = Point - NormalV * Constants.Epsilon;

            if (intersections != null)
            {
                ComputeN1N2(intersection, intersections);
            }
        }

        private void ComputeN1N2(Intersection intersection, Intersections intersections)
        {
            List<IShape> containers = new List<IShape>();
            foreach (var i in intersections)
            {
                if (i == intersection)
                {
                    N1 = containers.Any() ? containers.Last().Material.RefractiveIndex : 1.0;
                }

                if (containers.Contains(i.Object))
                {
                    containers.Remove(i.Object);
                }
                else
                {
                    containers.Add(i.Object);
                }

                if (i == intersection)
                {
                    N2 = containers.Any() ? containers.Last().Material.RefractiveIndex : 1.0;
                }
            }
        }

        public double Schlick()
        {
            var cos = EyeV.Dot(NormalV);
            if (N1 > N2)
            {
                var n = N1 / N2;
                var sin2T = n * n * (1 - cos * cos);
                if (sin2T > 1) return 1;

                var cos_t = Math.Sqrt(1 - sin2T);
                cos = cos_t;
            }
            var r0 = Math.Pow((N1 - N2) / (N1 + N2), 2);
            return r0 + (1 - r0) * Math.Pow(1 - cos, 5);
        }

        public double T { get; }
        public IShape Object { get; }
        public Tuple Point { get; }
        public Tuple OverPoint { get; }
        public Tuple UnderPoint { get; }
        public Tuple EyeV { get; }
        public Tuple NormalV { get; }
        public bool Inside { get; }
        public Tuple ReflectV { get; }
        public double N1 { get; private set; }
        public double N2 { get; private set; }

    }
}