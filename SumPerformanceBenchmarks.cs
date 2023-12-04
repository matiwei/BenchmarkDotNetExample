using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;

namespace PerformanceTests;

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
            _ = AddJob(
                Job.ShortRun
                .WithPlatform(Platform.X64)
                .WithJit(Jit.RyuJit)
                .WithRuntime(CoreRuntime.Core80)
                .WithLaunchCount(1)
                .WithWarmupCount(0)
                .WithEvaluateOverhead(false)
                .WithMaxRelativeError(0.1)
                .WithId("X64"));
        }
    }

    // Gives result dependent on passed parameters
    private readonly int[] intArray = [1, 321, 37885543, 321321, 3212412, 645564, 63243234, 32352432, 4324325, 321, 37885543, 321321, 1, 321, 37885543, 321321, 3212412, 645564, 63243234, 32352432, 4324325, 321, 37885543, 321321, 1, 321, 37885543, 321321, 3212412, 645564, 63243234, 32352432, 4324325, 321, 37885543, 321321, 3212412, 645564, 63243234, 32352432, 4324325, 321, 37885543, 321321, 3212412, 645564, 63243234, 32352432, 4324325];
    private readonly List<int> intList = [1, 321, 37885543, 321321, 3212412, 645564, 63243234, 32352432, 4324325, 321, 37885543, 321321, 1, 321, 37885543, 321321, 3212412, 645564, 63243234, 32352432, 4324325, 321, 37885543, 321321, 1, 321, 37885543, 321321, 3212412, 645564, 63243234, 32352432, 4324325, 321, 37885543, 321321, 3212412, 645564, 63243234, 32352432, 4324325, 321, 37885543, 321321, 3212412, 645564, 63243234, 32352432, 4324325];


    //Baseline marks benchmark, from which it will count speedup
    [Benchmark(Baseline = true)]
    public int SumLinqArray()
    {
        return intArray.Sum();
    }

    //Baseline marks benchmark, from which it will count speedup
    [Benchmark]
    public int SumLinqList()
    {
        return intList.Sum();
    }

    [Benchmark]
    public int SumForArray()
    {
        int sum = 0;
        for (int i = 0; i < intArray.Length; i++)
        {
            sum += intArray[i];
        }
        return sum;
    }

    [Benchmark]
    public int SumForList()
    {
        int sum = 0;
        for (int i = 0; i < intList.Count; i++)
        {
            sum += intList[i];
        }
        return sum;
    }

    [Benchmark]
    public int SumForeachArray()
    {
        int sum = 0;
        foreach (int i in intArray)
        {
            sum += i;
        }
        return sum;
    }

    [Benchmark]
    public int SumForeachList()
    {
        int sum = 0;
        foreach (int i in intList)
        {
            sum += i;
        }
        return sum;
    }

    [Benchmark]
    public int SumForeachArraySpan()
    {
        int sum = 0;
        Span<int> span = new(intArray);

        foreach (int i in span)
        {
            sum += i;
        }
        return sum;
    }

    [Benchmark]
    public int SumForArraySpan()
    {
        int sum = 0;
        Span<int> span = new(intArray);

        for (int i = 0; i < span.Length; i++)
        {
            sum += span[i];
        }
        return sum;
    }

    [Benchmark]
    public int SumForeachListSpan()
    {
        int sum = 0;
        Span<int> span = new([.. intList]);

        foreach (int i in span)
        {
            sum += i;
        }
        return sum;
    }

    [Benchmark]
    public int SumForListSpan()
    {
        int sum = 0;
        Span<int> span = new([.. intList]);

        for (int i = 0; i < span.Length; i++)
        {
            sum += span[i];
        }
        return sum;
    }
}
