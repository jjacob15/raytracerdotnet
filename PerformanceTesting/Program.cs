using BenchmarkDotNet.Running;

namespace PerformanceTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<CameraBenchmarks>();
        }
    }
}
