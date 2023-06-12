using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;

namespace Benchmark.StringBuilder
{
    // #1 run net 461
    //|                                   Method |      Mean |     Error |    StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
    //|----------------------------------------- |----------:|----------:|----------:|------:|------:|------:|----------:|
    //|             NumberMultiplyForLoopNoCheck |  5.224 ns | 0.0398 ns | 0.0353 ns |     - |     - |     - |         - |
    //|           NumberMultiplyForLoopCheckLoop |  9.450 ns | 0.0455 ns | 0.0404 ns |     - |     - |     - |         - |
    //| NumberMultiplyForLoopCheckExactOperation |  4.785 ns | 0.0289 ns | 0.0226 ns |     - |     - |     - |         - |
    //|                  NumberMultiplyBitMoving |  9.144 ns | 0.0704 ns | 0.0658 ns |     - |     - |     - |         - |
    //|                  NumberPowerWithoutCheck | 30.211 ns | 0.2171 ns | 0.2031 ns |     - |     - |     - |         - |
    //|                     NumberPowerWithCheck | 30.147 ns | 0.2300 ns | 0.2039 ns |     - |     - |     - |         - |
    // #2 run netcore50
    //|                                   Method |       Mean |     Error |    StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
    //|----------------------------------------- |-----------:|----------:|----------:|------:|------:|------:|----------:|
    //|             NumberMultiplyForLoopNoCheck |  7.8799 ns | 0.0666 ns | 0.0590 ns |     - |     - |     - |         - |
    //|           NumberMultiplyForLoopCheckLoop | 12.5660 ns | 0.0276 ns | 0.0258 ns |     - |     - |     - |         - |
    //| NumberMultiplyForLoopCheckExactOperation |  7.8121 ns | 0.0687 ns | 0.0609 ns |     - |     - |     - |         - |
    //|                  NumberMultiplyBitMoving | 12.8108 ns | 0.1026 ns | 0.0960 ns |     - |     - |     - |         - |
    //|                  NumberPowerWithoutCheck |  0.0000 ns | 0.0000 ns | 0.0000 ns |     - |     - |     - |         - |
    //|                     NumberPowerWithCheck |  0.0000 ns | 0.0000 ns | 0.0000 ns |     - |     - |     - |         - |
    
    //[SimpleJob(runtimeMoniker: RuntimeMoniker.Net461)]
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
    [MemoryDiagnoser]
    public class MathPowerOrMultiplyForLoop
    {
        public static int testNumber = 2;
        private static int poweringRate = 10;

        [Benchmark]
        public void NumberMultiplyForLoopNoCheck()
        {
            int result = 0;
            for (int i = 0; i < poweringRate - 1; i++)
            {
                if (i == 0)
                {
                    result = testNumber * testNumber;
                }
                else
                {
                    result *= testNumber;
                }
            }
        }

        [Benchmark]
        public void NumberMultiplyForLoopCheckLoop()
        {
            int result = 0;
            checked
            {
                for (int i = 0; i < poweringRate - 1; i++)
                {
                    if (i == 0)
                    {
                        result = testNumber * testNumber;
                    }
                    else
                    {
                        result *= testNumber;
                    }
                }
            }
        }

        [Benchmark]
        public void NumberMultiplyForLoopCheckExactOperation()
        {
            int result = 0;

            for (int i = 0; i < poweringRate - 1; i++)
            {
                if (i == 0)
                {
                    checked
                    {
                        result = testNumber * testNumber;
                    }
                }
                else
                {
                    checked
                    {
                        result *= testNumber;
                    }
                }
            }
        }

        [Benchmark]
        public void NumberMultiplyBitMoving()
        {
            int result = 0;
            checked
            {
                for (int i = 0; i < poweringRate - 1; i++)
                {
                    if (i == 0)
                    {
                        result = testNumber << testNumber;
                    }
                    else
                    {
                        result = result << testNumber;
                    }
                }
            }
        }

        [Benchmark]
        public void NumberPowerWithoutCheck()
        {
            double result = Math.Pow(testNumber, poweringRate);
        }

        [Benchmark]
        public void NumberPowerWithCheck()
        {
            double result;
            checked
            {
                result = Math.Pow(testNumber, poweringRate);
            }
        }
    }
}
