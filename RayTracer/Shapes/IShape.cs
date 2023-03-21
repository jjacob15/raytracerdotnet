namespace RayTracer.Shapes
{
    public interface IShape : ITransformable
    {
        Material Material { get; set; }

        void Intersect(Ray ray,Intersections intersections);
        void Intersect(ref Tuple origin,ref Tuple direction, Intersections intersections);
        Tuple NormalAt(Tuple worldPoint);
    }
}