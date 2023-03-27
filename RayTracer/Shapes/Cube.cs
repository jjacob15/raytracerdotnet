﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Shapes
{
    public class Cube : AbstractShape
    {
        public override void IntersectLocal(ref Tuple origin, ref Tuple direction, Intersections intersections)
        {
            (double xmin, double xmax) = CheckAxis(origin.X, direction.X);
            (double ymin, double ymax) = CheckAxis(origin.Y, direction.Y);
            (double zmin, double zmax) = CheckAxis(origin.Z, direction.Z);

            var tmin = Math.Max(Math.Max(xmin, ymin), zmin);
            var tmax = Math.Min(Math.Min(xmax, ymax), zmax);

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

        public override Tuple NormalAtLocal(Tuple localPoint)
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
