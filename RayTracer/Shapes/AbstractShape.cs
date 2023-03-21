﻿using System.Collections.Generic;

namespace RayTracer.Shapes
{
    public abstract class AbstractShape : IShape
    {
        public Matrix Transform { get; set; } = Matrix.Identity;
        public Material Material { get; set; } = new Material();

        public abstract void IntersectLocal(Ray ray, Intersections intersections);
        public abstract void IntersectLocal(ref Tuple origin, ref Tuple direction, Intersections intersections);
        public abstract Tuple NormalAtLocal(Tuple localPoint);

        public void Intersect(Ray ray, Intersections intersections)
        {
            if (ReferenceEquals(Transform, Matrix.Identity))
            {
                IntersectLocal(ray, intersections);
                return;
            }

            var transformedRay = ray.Transform(Transform.Inverse());
            IntersectLocal(transformedRay, intersections);
        }

        public void Intersect(ref Tuple origin, ref Tuple direction, Intersections intersections)
        {
            if (ReferenceEquals(Transform, Matrix.Identity))
            {
                IntersectLocal(ref origin, ref direction, intersections);
                return;
            }

            var transformInverse = Transform.Inverse();
            //var transformedRay = ray.Transform(Transform.Inverse());
            var transformedOrigin = origin * transformInverse;
            var transformedDirection = direction * transformInverse;
            IntersectLocal(ref transformedOrigin, ref transformedDirection, intersections);
        }

        public Tuple NormalAt(Tuple worldPoint)
        {
            var localPoint = Transform.Inverse() * worldPoint;
            //var localNormal = localPoint - Tuple.Point(0, 0, 0);
            var localNormal = NormalAtLocal(localPoint);
            var worldNormal = Transform.Inverse().Transpose() * localNormal;
            worldNormal.SetW(0);
            return worldNormal.Normalize();
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
    }
}