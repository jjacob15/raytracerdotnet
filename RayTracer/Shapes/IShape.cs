namespace RayTracer.Shapes
{
    public interface IShape : ITransformable
    {
        Material Material { get; set; }

        Intersections Intersect(Ray ray);
        Intersections IntersectLocal(Ray ray);
        Tuple NormalAt(Tuple worldPoint);
    }
}