namespace RayTracer.Lights
{
    public interface ILight
    {
        Color Intensity { get; }
        Tuple Position { get; }
    }
}