using RayTracer.Lights;
using RayTracer.Shapes;

namespace RayTracer
{
    public interface IWorld
    {
        void AddShape(IShape s);
        void SetLight(ILight light);
        Color ColorAt(Ray ray, int remaining = 4);
    }
}