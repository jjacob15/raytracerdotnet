using BenchmarkDotNet.Running;
using Profiling;

namespace Profiling
{
    class Program
    {
        static void Main(string[] args)
        {
            //BenchmarkRunner.Run<CameraBenchmarks>();
            //BenchmarkRunner.Run<WorldBenchmark>();
            BenchmarkRunner.Run<CubeBenchmark>();
            //BenchmarkRunner.Run<TupleBenchmark>();
            //BenchmarkRunner.Run<CameraBenchmarks>();
            //BenchmarkRunner.Run<ShadeHitsBenchmark>();
        }
    }
}
