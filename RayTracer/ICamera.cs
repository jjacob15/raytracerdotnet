namespace RayTracer
{
    public interface ICamera
    {
        Ray RayForPixel(int px, int py);
    }
}