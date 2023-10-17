// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using WeNerds.Benchmark;



var config = ManualConfig.Create(DefaultConfig.Instance).AddJob(Job.ShortRun);
            
var summaryReport = BenchmarkRunner.Run<StringCompareBenchmark>(config);
