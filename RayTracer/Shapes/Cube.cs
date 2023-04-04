using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Shapes
{
    public class Cube : AbstractShape
    {
        public override Bounds Box => new Bounds { Min = Tuple.Point(-1, -1, -1), Max = Tuple.Point(1, 1, 1) };

        public override void IntersectLocal(ref Tuple origin, ref Tuple direction, Intersections intersections)
        {
            (double xmin, double xmax) = CheckAxis(origin.X, direction.X);
            (double ymin, double ymax) = CheckAxis(origin.Y, direction.Y);

            var tmin = Math.Max(xmin, ymin);
            var tmax = Math.Min(xmax, ymax);

            if (tmin > tmax) return;


            (double zmin, double zmax) = CheckAxis(origin.Z, direction.Z);

            tmin = Math.Max(tmin, zmin);
            tmax = Math.Min(tmax, zmax);

            if (tmin > tmax) return;

            intersections.Add(new Intersection(this, tmin));
            intersections.Add(new Intersection(this, tmax));
        }

        private (double tMin, double tMax) CheckAxis(double origin, double direction)
        {
            var tMinNumerator = (-1 - origin);
            var tMaxNumerator = 1 - origin;

            double tMin;
            double tMax;
            if (Math.Abs(direction) >= Constants.Epsilon)
            {
                tMin = tMinNumerator / direction;
                tMax = tMaxNumerator / direction;
            }
            else
            {
                tMin = tMinNumerator >= 0 ? double.PositiveInfinity : double.NegativeInfinity;
                tMax = tMaxNumerator >= 0 ? double.PositiveInfinity : double.NegativeInfinity;
            }

            if (tMin > tMax) return (tMax, tMin);

            return (tMin, tMax);
        }

        public override Tuple NormalAtLocal(Tuple localPoint, Intersection hit = null)
        {
            var absX = Math.Abs(localPoint.X);
            var absY = Math.Abs(localPoint.Y);
            var absZ = Math.Abs(localPoint.Z);
            var maxC = Math.Max(Math.Max(absX, absY), absZ);

            if (maxC == absX) return Tuple.Vector(localPoint.X, 0, 0);
            if (maxC == absY) return Tuple.Vector(0, localPoint.Y, 0);

            return Tuple.Vector(0, 0, localPoint.Z);
        }
    }
}
