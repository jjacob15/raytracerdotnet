using RayTracer.Lights;
using RayTracer.Shapes;

namespace RayTracer
{
    public interface IWorld
    {
        void AddShape(IShape s);
        void SetLight(ILight light);
        Color ColorAt(Ray ray, int remaining = 4);
        Color ColorAtNew(ref Tuple origin, ref Tuple direction, int remaining = 4);
    }
}