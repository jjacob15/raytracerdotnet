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
            c = new CameraSettings().Build();
        }

        [Benchmark]
        public void CameraRayForPixelTest()
        {
            c.RayForPixel(200, 400);
        }
        
        //[Benchmark]
        //public void CameraRayForPixelTestOld()
        //{
        //    Random r = new Random();
        //    c.RayForPixelOld(r.Next(0, (int)c.HSize), r.Next(0, (int)c.HSize));
        //}
    }
}
