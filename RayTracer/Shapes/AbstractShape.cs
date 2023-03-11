namespace RayTracer.Shapes
{
    public abstract class AbstractShape
    {
        public Matrix Transform { get; set; } = Matrix.Identity();
        public Material Material { get; set; } = new Material();

        public abstract Intersections IntersectLocal(Ray ray);

        public Intersections Intersect(Ray ray)
        {
            var transformedRay = ray.Transform(Transform.Inverse());
            return IntersectLocal(transformedRay);
        }

        public Tuple NormalAt(Tuple worldPoint)
        {
            var objectPoint = Transform.Inverse() * worldPoint;
            var objectNormal = objectPoint - Tuple.Point(0, 0, 0);
            var worldNormal = Transform.Inverse().Transpose() * objectNormal;
            worldNormal.W = 0;
            return worldNormal.Normalize();
        }
    }
}