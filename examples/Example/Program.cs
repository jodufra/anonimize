using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Anonimize;
using Anonimize.Services;

namespace Example
{
    class Program
    {
        static ICryptoService service;

        enum ExampleEnum { Default }

        const int BENCHMARK_ITERATIONS = 1000;

        static void Main()
        {
            Console.WriteLine("##########################");
            Console.WriteLine("Example DataAccess");
            Console.WriteLine("##########################");
            Console.WriteLine();

            service = new TripleDESCryptoService();
            Console.WriteLine("Using " + service.GetType().Name);
            RunExamples();
            RunBenchmarks();

            service = new AesCryptoService();
            Console.WriteLine("Using " + service.GetType().Name);
            RunExamples();
            RunBenchmarks();
        }

        static void RunExamples()
        {
            RunExample(DateTime.Now);
            RunExample("Example string");
            RunExample(ExampleEnum.Default);
        }

        static void RunExample<T>(T input)
        {
            var encrypted = service.Encrypt(input);
            var decrypted = service.Decrypt<T>(encrypted);

            Console.WriteLine(input.GetType().Name);
            Console.WriteLine($"Original: '{input}'");
            Console.WriteLine($"Encrypted: '{encrypted}'");
            Console.WriteLine($"Decrypted: '{decrypted}'");
            Console.WriteLine();
        }

        static void RunBenchmarks()
        {
            var eMarks = new List<long>();
            var dMarks = new List<long>();

            var result = RunBenchmark(DateTime.Now);
            eMarks.AddRange(result.Item1);
            dMarks.AddRange(result.Item2);

            result = RunBenchmark("Example string");
            eMarks.AddRange(result.Item1);
            dMarks.AddRange(result.Item2);

            result = RunBenchmark(ExampleEnum.Default);
            eMarks.AddRange(result.Item1);
            dMarks.AddRange(result.Item2);


            Console.WriteLine("RESULTS");
            Console.WriteLine("Encrypt");
            Console.WriteLine("Average: " + eMarks.Average());
            Console.WriteLine("Max: " + eMarks.Max());
            Console.WriteLine("Min: " + eMarks.Min());
            Console.WriteLine("Decrypt");
            Console.WriteLine("Average: " + dMarks.Average());
            Console.WriteLine("Max: " + dMarks.Max());
            Console.WriteLine("Min: " + dMarks.Min());
        }

        static Tuple<List<long>, List<long>> RunBenchmark<T>(T input)
        {
            Console.WriteLine("Benchmark " + input.GetType().Name);
            Console.WriteLine("Encrypt");

            var eMarks = new List<long>();
            var sw = new Stopwatch();

            for (int i = 0; i < 10; i++)
            {
                GC.Collect();

                sw.Restart();
                for (int j = 0; j < BENCHMARK_ITERATIONS; j++)
                {
                    service.Encrypt(input);
                }
                sw.Stop();
                eMarks.Add(sw.ElapsedMilliseconds);
            }
            Console.WriteLine("Average: " + eMarks.Average());
            Console.WriteLine("Max: " + eMarks.Max());
            Console.WriteLine("Min: " + eMarks.Min());
            Console.WriteLine();


            Console.WriteLine("Decrypt");

            var toDecrypt = service.Encrypt(input);
            var dMarks = new List<long>();
            sw = new Stopwatch();

            for (int i = 0; i < 10; i++)
            {
                GC.Collect();

                sw.Restart();
                for (int j = 0; j < BENCHMARK_ITERATIONS; j++)
                {
                    service.Decrypt<T>(toDecrypt);
                }
                sw.Stop();
                dMarks.Add(sw.ElapsedMilliseconds);
            }
            Console.WriteLine("Average: " + dMarks.Average());
            Console.WriteLine("Max: " + dMarks.Max());
            Console.WriteLine("Min: " + dMarks.Min());
            Console.WriteLine();

            return new Tuple<List<long>, List<long>>(eMarks, dMarks);
        }
    }
}
