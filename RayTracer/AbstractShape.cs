using System.Collections.Generic;

namespace RayTracer
{
    public abstract class AbstractShape : IShape
    {
        public Matrix Transform { get; set; } = Matrix.Identity;
        public virtual Material Material { get; set; } = new Material();
        public IShape Parent { get; set; }

        public abstract Bounds Box { get; }

        public abstract void IntersectLocal(ref Tuple origin, ref Tuple direction, Intersections intersections);
        public abstract Tuple NormalAtLocal(Tuple localPoint, Intersection hit = null);

        public void Intersect(ref Tuple origin, ref Tuple direction, Intersections intersections)
        {
            if (ReferenceEquals(Transform, Matrix.Identity))
            {
                IntersectLocal(ref origin, ref direction, intersections);
                return;
            }

            var transformInverse = Transform.Inverse();
            var transformedOrigin = origin * transformInverse;
            var transformedDirection = direction * transformInverse;
            IntersectLocal(ref transformedOrigin, ref transformedDirection, intersections);
        }

        public Tuple NormalAt(Tuple worldPoint, Intersection hit = null)
        {
            var localPoint = WorldToObject(worldPoint);
            var localNormal = NormalAtLocal(localPoint, hit);
            return NormalToWorld(localNormal);

        }

        public override bool Equals(object obj)
        {
            return obj is AbstractShape shape &&
                   EqualityComparer<Matrix>.Default.Equals(Transform, shape.Transform) &&
                   EqualityComparer<Material>.Default.Equals(Material, shape.Material);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Transform, Material);
        }

        public Tuple WorldToObject(Tuple point)
        {
            if (Parent != null)
            {
                point = Parent.WorldToObject(point);
            }
            if (ReferenceEquals(Transform, Matrix.Identity))
            {
                return point;
            }

            return Transform.Inverse() * point;
        }

        public Tuple NormalToWorld(Tuple normal)
        {
            if (!ReferenceEquals(Transform, Matrix.Identity))
            {
                normal = Transform.Inverse().Transpose() * normal;
            }
            normal = Tuple.Vector(normal.X, normal.Y, normal.Z).Normalize();

            if (Parent != null)
            {
                normal = Parent.NormalToWorld(normal);
            }

            return normal;
        }
    }
}