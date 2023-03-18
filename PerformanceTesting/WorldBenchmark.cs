using BenchmarkDotNet.Attributes;
using RayTracer;
using RayTracer.Patterns;
using System;
using System.Collections.Generic;
using System.Text;
using Tuple = RayTracer.Tuple;

namespace Profiling
{
    [MemoryDiagnoser]
    public class WorldBenchmark
    {
        World w;
        Ray r;

        [GlobalSetup]
        public void Setup()
        {
            w = ObjectFactory.DefaultWorld();
        }

        //[Benchmark]
        //public void ColorRayTest()
        //{
        //    Ray r = ObjectFactory.IntersectingRay();
        //    w.ColorAt(r, 10);
        //}

        [Benchmark]
        public void ColorRayTest2()
        {
            var a = w.Shapes[0];
            a.Material.Ambient = 1;
            a.Material.Pattern = new TestPattern();

            var b = w.Shapes[1];
            b.Material.Transparency = 1;
            b.Material.RefractiveIndex = 1.5;

            var localRay = new Ray(Tuple.Point(0, 0, 0.1), Tuple.Vector(0, 1, 0));
            w.ColorAt(localRay, 10);
        }

        //[Benchmark]
        //public void ColorRayTestNew()
        //{
        //    Ray r = ObjectFactory.IntersectingRay();
        //    var origin = r.Origin;
        //    var dir = r.Direction;
        //    w.ColorAtNew(ref origin, ref dir, 10);
        //}

        [Benchmark]
        public void ColorRayTest2New()
        {
            var a = w.Shapes[0];
            a.Material.Ambient = 1;
            a.Material.Pattern = new TestPattern();

            var b = w.Shapes[1];
            b.Material.Transparency = 1;
            b.Material.RefractiveIndex = 1.5;

            var localRay = new Ray(Tuple.Point(0, 0, 0.1), Tuple.Vector(0, 1, 0));
            var origin = localRay.Origin;
            var dir = localRay.Direction;
            w.ColorAtNew(ref origin, ref dir, 10);
        }
    }
}
