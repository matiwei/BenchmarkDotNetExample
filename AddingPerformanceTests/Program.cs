using System.Threading.Tasks;
using BenchmarkDotNet.Running;

namespace AddingPerformanceTests
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<SumPerformanceBenchmarks>();
        }
    }
}
