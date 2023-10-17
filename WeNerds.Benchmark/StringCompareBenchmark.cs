using BenchmarkDotNet.Attributes;
using WeNerds.Commons.Extensions;

namespace WeNerds.Benchmark;

[MemoryDiagnoser]
public class StringCompareBenchmark
{
    private readonly string _value1 = "CompareStringTestReplace";
    private readonly string _value2 = "ComparestringtestReplacE";
            
    [Benchmark]
    public void CompareToLower()
    {
        _ = _value1.ToLower() == _value2.ToLower();
    }
    [Benchmark]
    public void CompareToUpper()
    {
        _ = _value1.ToUpper() == _value2.ToUpper();
    }
    [Benchmark]
    public void CompareStringEquals()
    {
        string.Equals(_value1, _value2, StringComparison.OrdinalIgnoreCase);
    }
    [Benchmark]
    public void WeNerdsExtensionCompareStringEquals()
    {
        _value1.IsEqual(_value2);
    }
}
