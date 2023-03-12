namespace RayTracer.Shapes
{
    public interface IShape
    {
        Material Material { get; set; }
        Matrix Transform { get; set; }

        Intersections Intersect(Ray ray);
        Intersections IntersectLocal(Ray ray);
        Tuple NormalAt(Tuple worldPoint);
    }
}