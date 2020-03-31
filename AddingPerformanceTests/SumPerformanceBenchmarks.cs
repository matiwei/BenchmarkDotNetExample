using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Horology;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using System.Collections.Generic;

namespace AddingPerformanceTests
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    //Uncomment to run faster, but less accurate measurments
    //[Config(typeof(Config))]

    public class SumPerformanceBenchmarks
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                Add(
                    Job.ShortRun
                    .With(Platform.X64)
                    .With(Jit.RyuJit)
                    .With(CoreRuntime.Core31)
                    .WithLaunchCount(1)
                    .WithWarmupCount(0)
                    .WithEvaluateOverhead(false)
                    .WithIterationTime(TimeInterval.Millisecond * 200)
                    .WithMaxRelativeError(0.1)
                    .WithId("X64"));
            }
        }

        // Gives result dependent on passed parameters
        [Params(5, 15)]
        public int startIndex { get; set; }
        [Params(33)]
        public int endIndex { get; set; }
        private readonly int[] intArray = { 1, 321, 37885543, 321321, 3212412, 645564, 63243234, 32352432, 4324325, 321, 37885543, 321321, 1, 321, 37885543, 321321, 3212412, 645564, 63243234, 32352432, 4324325, 321, 37885543, 321321, 1, 321, 37885543, 321321, 3212412, 645564, 63243234, 32352432, 4324325, 321, 37885543, 321321, 3212412, 645564, 63243234, 32352432, 4324325, 321, 37885543, 321321, 3212412, 645564, 63243234, 32352432, 4324325 };
        private readonly List<int> intList = new List<int>{ 1, 321, 37885543, 321321, 3212412, 645564, 63243234, 32352432, 4324325, 321, 37885543, 321321, 1, 321, 37885543, 321321, 3212412, 645564, 63243234, 32352432, 4324325, 321, 37885543, 321321, 1, 321, 37885543, 321321, 3212412, 645564, 63243234, 32352432, 4324325, 321, 37885543, 321321, 3212412, 645564, 63243234, 32352432, 4324325, 321, 37885543, 321321, 3212412, 645564, 63243234, 32352432, 4324325 };
        
        
        [Benchmark]
        public void ArraySumAEqualsAPlusB()
        {
            var a = SumPerformanceTestFunctionsArray.ArraySumAEqualsAPlusB(intArray, startIndex, endIndex);
        }
        [Benchmark]
        public void ListSumAEqualsAPlusB()
        {
            var a = SumPerformanceTestFunctionsList.ListSumEqualsAPlusB(intList, startIndex, endIndex);
        }
        [Benchmark]
        public void ArraySumAPlusEqualsB()
        {
            var a = SumPerformanceTestFunctionsArray.ArraySumAPlusEqualsB(intArray, startIndex, endIndex);
        }
        [Benchmark]
        public void ListSumPlusEqualsB()
        {
            var a = SumPerformanceTestFunctionsList.ListSumPlusEquals(intList, startIndex, endIndex);
        }
        [Benchmark]
        public void ArraySumWithForeach()
        {
            var a = SumPerformanceTestFunctionsArray.ArraySumForeach(intArray, startIndex, endIndex);
        }
        [Benchmark]
        public void ListSumWithForeach()
        {
            var a = SumPerformanceTestFunctionsList.ListSumForeach(intList, startIndex, endIndex);
        }
        //Baseline marks benchmark, from which it will count speedup
        [Benchmark(Baseline = true)]
        public void ArraySumForeachSpan()
        {
            var a = SumPerformanceTestFunctionsArray.ArraySumForeachSpan(intArray, startIndex, endIndex);
        }
        [Benchmark]
        public void ArraySumSpan()
        {
            var a = SumPerformanceTestFunctionsArray.ArraySumSpan(intArray, startIndex, endIndex);
        }
    }
}
