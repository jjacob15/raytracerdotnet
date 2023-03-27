using BenchmarkDotNet.Attributes;
using RayTracer;
using RayTracer.Patterns;
using RayTracer.Shapes;
using System;
using System.Collections.Generic;
using System.Text;
using Tuple = RayTracer.Tuple;

namespace Profiling
{
    [MemoryDiagnoser]
    public class CubeBenchmark
    {
        [Benchmark]
        public void RayIntersectsCube()
        {
            Cube c = new Cube();
            Ray r = new Ray(Tuple.Point(0.5, 0, 5), Tuple.Vector(0, 0, -1));
            var origin = r.Origin;
            var direction = r.Direction;
            var xs = new Intersections();
            c.Intersect(ref origin, ref direction, xs);
        }
    }
}
