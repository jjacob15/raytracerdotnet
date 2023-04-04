namespace RayTracer
{
    public interface IShape : ITransformable
    {
        IShape Parent { get; set; }
        Material Material { get; set; }

        Bounds Box { get; }

        Tuple WorldToObject(Tuple point);
        Tuple NormalToWorld(Tuple normal);

        void Intersect(ref Tuple origin,ref Tuple direction, Intersections intersections);
        Tuple NormalAt(Tuple worldPoint, Intersection hit = null);
    }
}