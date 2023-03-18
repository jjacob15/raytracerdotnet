using BenchmarkDotNet.Attributes;
using RayTracer;
using System;

namespace Profiling
{
    [MemoryDiagnoser]
    public class CameraBenchmarks
    {
        Camera c;
        [GlobalSetup]
        public void Setup()
        {
            c = Camera.DefaultCamera();
        }

        [Benchmark]
        public void CameraRayForPixelTest()
        {
            Random r = new Random();
            c.RayForPixel(r.Next(0, (int)c.HSize), r.Next(0, (int)c.HSize));
        }
        
        [Benchmark]
        public void CameraRayForPixelTestOld()
        {
            Random r = new Random();
            c.RayForPixelOld(r.Next(0, (int)c.HSize), r.Next(0, (int)c.HSize));
        }
    }
}
