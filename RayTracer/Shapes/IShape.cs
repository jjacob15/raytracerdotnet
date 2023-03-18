namespace RayTracer.Shapes
{
    public interface IShape : ITransformable
    {
        Material Material { get; set; }

        Intersections Intersect(Ray ray);
        Intersections Intersect(ref Tuple origin,ref Tuple direction);
        Tuple NormalAt(Tuple worldPoint);
    }
}