using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.Shapes
{
    public class Group : AbstractShape
    {
        public List<IShape> Shapes { get; } = new List<IShape>();

        private Bounds box;
        public override Bounds Box
        {
            get
            {
                if (box == null)
                {
                    ComputeBounds();
                }
                return box;
            }
        }

        private void ComputeBounds()
        {
            double minX = double.PositiveInfinity;
            double minY = double.PositiveInfinity;
            double minZ = double.PositiveInfinity;
            double maxX = double.NegativeInfinity;
            double maxY = double.NegativeInfinity;
            double maxZ = double.NegativeInfinity;

            foreach (var shape in Shapes)
            {
                var box = shape.Box;
                var p1 = box.Min;
                var p2 = Tuple.Point(box.Min.X, box.Min.Y, box.Max.Z);
                var p3 = Tuple.Point(box.Min.X, box.Max.Y, box.Min.Z);
                var p4 = Tuple.Point(box.Min.X, box.Max.Y, box.Max.Z);
                var p5 = Tuple.Point(box.Max.X, box.Min.Y, box.Min.Z);
                var p6 = Tuple.Point(box.Max.X, box.Min.Y, box.Max.Z);
                var p7 = Tuple.Point(box.Max.X, box.Max.Y, box.Min.Z);
                var p8 = box.Max;

                var transformP1 = shape.Transform * p1;
                var transformP2 = shape.Transform * p2;
                var transformP3 = shape.Transform * p3;
                var transformP4 = shape.Transform * p4;
                var transformP5 = shape.Transform * p5;
                var transformP6 = shape.Transform * p6;
                var transformP7 = shape.Transform * p7;
                var transformP8 = shape.Transform * p8;

                var points = new[] { transformP1, transformP2, transformP3, transformP4, transformP5, transformP6, transformP7, transformP8 };

                minX = Math.Min(minX, points.Select(p => p.X).Min());
                minY = Math.Min(minY, points.Select(p => p.Y).Min());
                minZ = Math.Min(minZ, points.Select(p => p.Z).Min());
                maxX = Math.Max(maxX, points.Select(p => p.X).Max());
                maxY = Math.Max(maxY, points.Select(p => p.Y).Max());
                maxZ = Math.Max(maxZ, points.Select(p => p.Z).Max());
            }
        }

        public IShape this[int i] => Shapes[i];

        public void Add(params IShape[] shapes)
        {
            foreach (var shape in shapes)
            {
                shape.Parent = this;
                Shapes.Add(shape);
            }
        }

        public override bool Contains(IShape other)
        {
            foreach(var shape in Shapes)
            {
                if (shape.Contains(other))
                    return true;
            }

            return false;
        }

        public override void IntersectLocal(ref Tuple origin, ref Tuple direction, Intersections intersections)
        {
            for (var i = 0; i < Shapes.Count; i++)
            {
                Shapes[i].Intersect(ref origin, ref direction, intersections);
            }
        }

        public override Tuple NormalAtLocal(Tuple localPoint, Intersection hit)
        {
            throw new NotImplementedException();
        }
    }
}
