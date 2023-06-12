using Benchmark.StringBuilder;
using BenchmarkDotNet.Running;
using Benchmarks;

namespace ConsoleApp9
{
    public class Program
    {

        static void Main(string[] args)
        {
            //MathPowerOrMultiply.NumberMultiplyWhileLoopNoCheck();
            //BenchmarkRunner.Run<StringBuilderBenchmark>();
            //BenchmarkRunner.Run<MathPowerOrMultiplyForLoop>();
            //BenchmarkRunner.Run<MathPowerOrMultiplyForLoop>();
            BenchmarkRunner.Run<MathPower>();
        }
    }
}
