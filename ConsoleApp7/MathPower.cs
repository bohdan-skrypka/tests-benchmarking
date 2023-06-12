using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.StringBuilder
{
    [SimpleJob(runtimeMoniker: RuntimeMoniker.NetCoreApp50)]
    [KeepBenchmarkFiles]
    [MemoryDiagnoser]
    public class MathPower
    {
        public static int testNumber = 2;
        private static int poweringRate = 10;

        [Benchmark]
        public void NumberPowerWithoutCheck()
        {
            double result = 1 + Math.Pow(testNumber, poweringRate);
        }
    }
}
