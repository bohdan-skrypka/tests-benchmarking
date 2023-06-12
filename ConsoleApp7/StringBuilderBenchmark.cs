using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Collections.Generic;
using System.Text;

namespace Benchmarks
{
    [MemoryDiagnoser]
    [SimpleJob(runtimeMoniker: RuntimeMoniker.Net461)]
    //[SimpleJob(runtimeMoniker: RuntimeMoniker.Net462)]
    //[SimpleJob(runtimeMoniker: RuntimeMoniker.Net47)]
    //[SimpleJob(runtimeMoniker: RuntimeMoniker.Net471)]
    //[SimpleJob(runtimeMoniker: RuntimeMoniker.Net472)]
    //[SimpleJob(runtimeMoniker: RuntimeMoniker.Net48)]
    //[SimpleJob(runtimeMoniker: RuntimeMoniker.NetCoreApp20)]
    //[SimpleJob(runtimeMoniker: RuntimeMoniker.NetCoreApp21)]
    //[SimpleJob(runtimeMoniker: RuntimeMoniker.NetCoreApp22)]
    //[SimpleJob(runtimeMoniker: RuntimeMoniker.NetCoreApp30)]
    //[SimpleJob(runtimeMoniker: RuntimeMoniker.NetCoreApp31)]
    [SimpleJob(runtimeMoniker: RuntimeMoniker.NetCoreApp50)]
    [KeepBenchmarkFiles]
    public class StringBuilderBenchmark
    {
        [Params(10, 100, 1000, 10_000)]
        public static int N;

        public static string str = "jhjjkakjkj asdjasjkasjkkj jajiiieie hjjjjs jjjjjvnnvbb " +
            "kkdsfjsddsfosdofsdjkjlxjlk ll ldsldl lorotiiyi ollgl l" +
            "lliiiyiykfkjgjll;;km;''lkdskfksji49030" +
            "sdflksdkdsk kdsfk lfloo4959 kfdl l;df 599olfdl dfk 9fd9r999  odof fl df9994o fdo 9fd9 o4]" +
            "b hhshsh uuwuui iiahh kkakshh hgh gfgfg gasjj gjjgj";

        [Benchmark(Baseline = true)]
        public void StringConcatToBenchmark()
        {
            var t = ConcatString(str);
        }

        [Benchmark]
        public void StringBuilderToBenchmark()
        {
            var t = StringBuilder(str);
        }

        public static string StringBuilder(string unsortedString)
        {
            List<int> list = new List<int>(N);
            for (int i = 0; i < N; i++)
            {
                list.Add(i);
            }

            list.Sort();

            StringBuilder builder = new StringBuilder(capacity: list.Count);
            foreach (var digit in list)
            {
                builder.Append(digit);
            }

            return builder.ToString();
        }

        public static string ConcatString(string unsortedString)
        {
            List<int> list = new List<int>(N);
            for (int i = 0; i < N; i++)
            {
                list.Add(i);
            }

            list.Sort();

            var str = "";
            foreach (var digit in list)
            {
                str += digit + " ";
            }

            return str;
        }
    }
}
