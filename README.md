# Comparison of Sum alternatives in .NET 8

Anecdotally I've been seeing overall 25% performance improvements when using .NET 8 over .NET 7 and was just reading this article about [performance improvements in .NET 8](https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-8).  Specifically, I can see now the LINQ `.Sum()` is faster than an iterating loop method.  Here's the comparison of different sum methods between .NET 7 and .NET 8.

Worth a note though, this is somewhat dependent on whether the data type is vectorizable, like `int` and also when using an enumerable `struct`.  In other words, more advanced enumerable data types and class structures may not benefit as shown here.  See [Linq.sum source code](https://github.com/dotnet/runtime/blob/88b5e3d4b77dd8238331ade1b31ac8ddc62f22f7/src/libraries/System.Linq/src/System/Linq/Sum.cs) for more information.

```bash
BenchmarkDotNet v0.13.10, Ubuntu 22.04.3 LTS (Jammy Jellyfish)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 7.0.14 (7.0.1423.51910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.14 (7.0.1423.51910), X64 RyuJIT AVX2
```

| Method              | Mean      | Error    | StdDev   |  Ratio | RatioSD | Rank |
|-------------------- |----------:|---------:|---------:|----------:|------:|--------:|
| SumForeachArraySpan |  16.49 ns | 0.075 ns | 0.070 ns |    0.90 |    0.01 |    1 |
| SumForArraySpan     |  16.60 ns | 0.092 ns | 0.081 ns |    0.91 |    0.01 |    1 |
| SumForeachArray     |  16.92 ns | 0.055 ns | 0.049 ns |    0.92 |    0.01 |    2 |
| SumLinqArray        |  18.29 ns | 0.153 ns | 0.136 ns |    1.00 |    0.00 |    3 |
| SumLinqList         |  20.14 ns | 0.159 ns | 0.148 ns |    1.10 |    0.01 |    4 |
| SumForArray         |  30.51 ns | 0.276 ns | 0.231 ns |    1.67 |    0.02 |    5 |
| SumForList          |  31.75 ns | 0.026 ns | 0.020 ns |   1.74 |    0.01 |    6 |
| SumForeachList      |  62.22 ns | 1.192 ns | 2.238 ns |    3.40 |    0.13 |    7 |
| SumForeachListSpan  | 121.37 ns | 2.148 ns | 2.009 ns |   6.64 |    0.13 |    8 |
| SumForListSpan      | 121.85 ns | 2.081 ns | 1.845 ns |   6.66 |    0.10 |    8 |

```bash
BenchmarkDotNet v0.13.10, Ubuntu 22.04.3 LTS (Jammy Jellyfish)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
```

| Method              | Mean      | Error     | StdDev    | Ratio | RatioSD | Rank |
|-------------------- |----------:|----------:|----------:|------:|--------:|-----:|
| SumLinqArray        |  6.519 ns | 0.0284 ns | 0.0266 ns |  1.00 |    0.00 |    1 |
| SumLinqList         |  6.714 ns | 0.0134 ns | 0.0119 ns |  1.03 |    0.00 |    2 |
| SumForeachArraySpan | 16.631 ns | 0.0533 ns | 0.0473 ns |  2.55 |    0.01 |  3 |
| SumForArraySpan     | 16.631 ns | 0.0326 ns | 0.0289 ns |  2.55 |    0.01 |    3 |
| SumForeachArray     | 16.684 ns | 0.0691 ns | 0.0647 ns |  2.56 |    0.01 |    3 |
| SumForArray         | 30.398 ns | 0.0807 ns | 0.0630 ns |  4.66 |    0.02 |    4 |
| SumForList          | 30.409 ns | 0.0713 ns | 0.0632 ns |  4.66 |    0.02 |    4 |
| SumForeachList      | 30.551 ns | 0.4511 ns | 0.4220 ns |  4.69 |    0.06 |    4 |
| SumForListSpan      | 76.098 ns | 1.1625 ns | 1.0305 ns | 11.67 |    0.15 |    5 |
| SumForeachListSpan  | 76.649 ns | 0.6666 ns | 0.6236 ns | 11.76 |    0.11 |    5 |


cc: inspired by a fork from @matiwei