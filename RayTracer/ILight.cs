namespace RayTracer
{
    public interface ILight
    {
        Color Intensity { get; }
        Tuple Position { get; }
    }
}