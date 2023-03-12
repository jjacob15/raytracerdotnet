using System.Collections.Generic;

namespace RayTracer.Shapes
{
    public abstract class AbstractShape : IShape
    {
        public Matrix Transform { get; set; } = Matrix.Identity();
        public Material Material { get; set; } = new Material();

        public abstract Intersections IntersectLocal(Ray ray);
        public abstract Tuple NormalAtLocal(Tuple localPoint);

        public Intersections Intersect(Ray ray)
        {
            var transformedRay = ray.Transform(Transform.Inverse());
            return IntersectLocal(transformedRay);
        }

        public Tuple NormalAt(Tuple worldPoint)
        {
            var localPoint = Transform.Inverse() * worldPoint;
            //var localNormal = localPoint - Tuple.Point(0, 0, 0);
            var localNormal = NormalAtLocal(localPoint);
            var worldNormal = Transform.Inverse().Transpose() * localNormal;
            worldNormal.W = 0;
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