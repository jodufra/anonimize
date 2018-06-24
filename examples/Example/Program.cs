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

            service = new AesCryptoService();
            Console.WriteLine("Using " + service.GetType().Name);
            RunExamples();
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
    }
}
