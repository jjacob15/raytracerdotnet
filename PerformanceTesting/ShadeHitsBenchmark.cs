using BenchmarkDotNet.Attributes;
using RayTracer;
using RayTracer.Lights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tuple = RayTracer.Tuple;

namespace Profiling
{
    [MemoryDiagnoser]
    public class ShadeHitsBenchmark
    {
        World w;

        [GlobalSetup]
        public void Setup()
        {
            w = ObjectFactory.DefaultWorld();
        }
        [Benchmark]
        public void ShadingAnIntersection()
        {
            var r = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var shape = w.Shapes.First();
            var i = new Intersection(shape, 4);
            var comps = i.PrepareComputations(r);
        }

        [Benchmark]
        public void ShadingAnIntersectionFromInside()
        {
            w.SetLight(new PointLight(Tuple.Point(0, 0.25, 0), new Color(1, 1, 1)));

            var r = new Ray(Tuple.Point(0, 0, 0), Tuple.Vector(0, 0, 1));
            var shape = w.Shapes[1];
            var i = new Intersection(shape, 0.5);
            var comps = i.PrepareComputations(r);
            var c = w.ShadeHits(comps);
        }
    }
}
