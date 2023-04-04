using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Shapes
{
    public class Triangle : AbstractShape
    {
        public Triangle(Tuple p1, Tuple p2, Tuple p3)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;

            E1 = p2 - p1;
            E2 = p3 - p1;
            N = E2.Cross(E1).Normalize();
        }
        public override Bounds Box => throw new NotImplementedException();

        public Tuple P1 { get; }
        public Tuple P2 { get; }
        public Tuple P3 { get; }
        public Tuple E1 { get; }
        public Tuple E2 { get; }
        public Tuple N { get; }

        public override void IntersectLocal(ref Tuple origin, ref Tuple direction, Intersections intersections)
        {
            var directionCrossE2 = direction.Cross(E2);
            var det = E1.Dot(directionCrossE2);
            if (Math.Abs(det) < Constants.Epsilon) return;

            var f = 1.0 / det;
            var p1ToOrigin = origin - P1;
            var u = f * p1ToOrigin.Dot(directionCrossE2);

            if (u < 0 || u > 1) return;

            var originCrossE1 = p1ToOrigin.Cross(E1);
            var v = f * direction.Dot(originCrossE1);
            if (v < 0 || (u + v) > 1) return;

            var t = f * E2.Dot(originCrossE1);

            intersections.Add(new Intersection(this, t, u, v));
        }

        public override Tuple NormalAtLocal(Tuple localPoint, Intersection hit = null)
        {
            return N;
        }
    }
}
