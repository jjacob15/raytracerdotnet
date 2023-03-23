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
    public class TupleBenchmark
    {
        [Benchmark]
        public void TupleMagnitude()
        {
            Tuple v = Tuple.Vector(1, 2, 3);
            var m = v.Magnitude;
        }

        [Benchmark]
        public void CrossProduct()
        {
            Tuple v1 = Tuple.Vector(1, 2, 3);
            Tuple v2 = Tuple.Vector(2, 3, 4);
            var r1 = v1.Cross(v2);
            var r2 = v2.Cross(v1);
        }

        [Benchmark]
        public void ReflectingAVectorAt45()
        {
            Tuple v = Tuple.Vector(1, -1, 0);
            Tuple n = Tuple.Vector(0, 1, 0);
            var r1 = v.Reflect(n);
        }

        [Benchmark]
        public void ReflectingAVectorAtSlanted()
        {
            Tuple v = Tuple.Vector(0, -1, 0);
            Tuple n = Tuple.Vector(Math.Sqrt(2) / 2, Math.Sqrt(2) / 2, 0);
            var r1 =  v.Reflect(n);
        }
    }
}
