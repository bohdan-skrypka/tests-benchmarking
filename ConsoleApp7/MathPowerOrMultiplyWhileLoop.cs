using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.StringBuilder
{
    // #1 run .net 461
    //|                                     Method |      Mean |     Error |    StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
    //|------------------------------------------- |----------:|----------:|----------:|------:|------:|------:|----------:|
    //|             NumberMultiplyWhileLoopNoCheck |  5.242 ns | 0.0379 ns | 0.0316 ns |     - |     - |     - |         - |
    //|           NumberMultiplyWhileLoopCheckLoop |  8.425 ns | 0.0562 ns | 0.0498 ns |     - |     - |     - |         - |
    //| NumberMultiplyWhileLoopCheckExactOperation |  5.769 ns | 0.1430 ns | 0.1530 ns |     - |     - |     - |         - |
    //|                    NumberMultiplyBitMoving |  9.193 ns | 0.0886 ns | 0.0786 ns |     - |     - |     - |         - |
    //|                    NumberPowerWithoutCheck | 30.913 ns | 0.5180 ns | 0.6362 ns |     - |     - |     - |         - |
    //|                       NumberPowerWithCheck | 30.313 ns | 0.1992 ns | 0.1766 ns |     - |     - |     - |         - |
    // #2 run .net 461
    //|                                     Method |      Mean |     Error |    StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
    //|------------------------------------------- |----------:|----------:|----------:|------:|------:|------:|----------:|
    //|             NumberMultiplyWhileLoopNoCheck |  5.254 ns | 0.0290 ns | 0.0242 ns |     - |     - |     - |         - |
    //|           NumberMultiplyWhileLoopCheckLoop |  8.537 ns | 0.1516 ns | 0.1746 ns |     - |     - |     - |         - |
    //| NumberMultiplyWhileLoopCheckExactOperation |  5.733 ns | 0.1048 ns | 0.0980 ns |     - |     - |     - |         - |
    //|                    NumberMultiplyBitMoving |  9.117 ns | 0.0771 ns | 0.0684 ns |     - |     - |     - |         - |
    //|                    NumberPowerWithoutCheck | 30.264 ns | 0.1431 ns | 0.1339 ns |     - |     - |     - |         - |
    //|                       NumberPowerWithCheck | 30.158 ns | 0.2129 ns | 0.1992 ns |     - |     - |     - |         - |

    // #3 run .netcore50
    //|                                     Method |       Mean |     Error |    StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
    //|------------------------------------------- |-----------:|----------:|----------:|------:|------:|------:|----------:|
    //|             NumberMultiplyWhileLoopNoCheck |  8.4222 ns | 0.1299 ns | 0.1085 ns |     - |     - |     - |         - |
    //|           NumberMultiplyWhileLoopCheckLoop |  9.3060 ns | 0.0518 ns | 0.0484 ns |     - |     - |     - |         - |
    //| NumberMultiplyWhileLoopCheckExactOperation | 11.4951 ns | 0.0355 ns | 0.0315 ns |     - |     - |     - |         - |
    //|                    NumberMultiplyBitMoving | 12.8189 ns | 0.1063 ns | 0.0994 ns |     - |     - |     - |         - |
    //|                    NumberPowerWithoutCheck |  0.0264 ns | 0.0112 ns | 0.0099 ns |     - |     - |     - |         - |
    //|                       NumberPowerWithCheck |  0.0000 ns | 0.0000 ns | 0.0000 ns |     - |     - |     - |         - |

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
    public class MathPowerOrMultiplyWhileLoop
    {
        public static int testNumber = 2;
        private static int poweringRate = 10;

        [Benchmark]
        public void NumberMultiplyWhileLoopNoCheck()
        {
            int result = 0;
            uint i = 0;
            while (i < poweringRate - 1)
            {
                if (i == 0)
                {
                    result = testNumber * testNumber;
                }
                else
                {
                    result *= testNumber;
                }
                i++;
            }
        }

        [Benchmark]
        public void NumberMultiplyWhileLoopCheckLoop()
        {
            int result = 0;
            checked
            {
                uint i = 0;
                while (i < poweringRate - 1)
                {
                    if (i == 0)
                    {
                        result = testNumber * testNumber;
                    }
                    else
                    {
                        result *= testNumber;
                    }
                    i++;
                }
            }
        }

        [Benchmark]
        public void NumberMultiplyWhileLoopCheckExactOperation()
        {
            int result = 0;

            uint i = 0;
            while (i < poweringRate - 1)
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
                i++;
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

            int i = 0;

            result = result + i;

        }

        [Benchmark]
        public void NumberPowerWithCheck()
        {
            double result;
            checked
            {
                result = Math.Pow(testNumber, poweringRate);
            }
            int i = 0;
            result = result + i;
        }
    }
}
