using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Shapes
{
    public enum CsgOperation
    {
        Union,
        Intersect,
        Difference
    }
    public class Csg : Group
    {
        public Csg(CsgOperation operation, IShape left, IShape right)
        {
            Operation = operation;
            Left = left;
            Right = right;

            Left.Parent = this;
            Right.Parent = this;

            Add(Left, Right);
        }

        public CsgOperation Operation { get; }
        public IShape Left { get; }
        public IShape Right { get; }

        public bool IntersectionAllowed(bool leftHit, bool insideLeft, bool insideRight)
        {
            switch (Operation)
            {
                case CsgOperation.Union:
                    return (leftHit && !insideRight) || (!leftHit && !insideLeft);
                case CsgOperation.Intersect:
                    return (leftHit && insideRight) || (!leftHit && insideLeft);
                case CsgOperation.Difference:
                    return (leftHit && !insideRight) || (!leftHit && insideLeft);
                default:
                    return false;
            }
        }

        public override bool Contains(IShape shape)
        {
            return ReferenceEquals(Left, shape) || ReferenceEquals(Right, shape) ||
                Left.Contains(shape) || Right.Contains(shape);
        }

        public Intersections Filter(Intersections intersections)
        {
            Intersections filtered = new Intersections();
            bool insideLeft = false, insideRight = false;

            foreach (var i in intersections)
            {
                bool leftHit = Left.Contains(i.Object);
                if (IntersectionAllowed(leftHit, insideLeft, insideRight))
                {
                    filtered.Add(i);
                }

                if (leftHit)
                    insideLeft = !insideLeft;
                else
                    insideRight = !insideRight;
            }
            return filtered;
        }

        public override void IntersectLocal(ref Tuple origin, ref Tuple direction, Intersections intersections)
        {
            var leftIntersections = new Intersections();
            Left.Intersect(ref origin, ref direction, leftIntersections);
            var rightIntersections = new Intersections();
            Right.Intersect(ref origin, ref direction, rightIntersections);

            leftIntersections.AddRange(rightIntersections);
            leftIntersections.Sort();

            intersections.AddRange(Filter(leftIntersections));
        }
    }
}
