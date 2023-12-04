using BenchmarkDotNet.Running;
using PerformanceTests;

BenchmarkRunner.Run<SumPerformanceBenchmarks>();


// dotnet run -c Release --filter "*"