using BenchmarkDotNet.Attributes;
using WeNerds.Commons.Extensions;

namespace WeNerds.Test
{
    [MemoryDiagnoser]
    public class Worker : BackgroundService
    {
        private const string value1 = "CompareStringTest";
        private const string value2 = "Comparestringtest";
        
        public Worker()
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            

        }

        [Benchmark]
        private void CompareToLower() 
        {
            Console.WriteLine(value1.ToLower() == value2.ToLower());
        }
        [Benchmark]
        private void CompareToUpper()
        {
            Console.WriteLine(value1.ToUpper() == value2.ToUpper());
        }
        [Benchmark]
        private void CompareStringComparison()
        {
            Console.WriteLine(value1.IsEqual(value2));
        }

    }
}
