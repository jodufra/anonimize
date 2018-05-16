using System;
using Anonimize;
using Anonimize.Services;

namespace Example
{
    class Program
    {
        static ICryptoService service; 

        static void Main()
        {
            service = AnonimizeProvider.GetInstance().GetCryptoService();
            EncryptString("My example string");
        }

        static void EncryptString(string original)
        {
            WriteHeader("String");
            var encrypted = service.Encrypt(original);
            var decrypted = service.Decrypt<string>(encrypted);
            WriteResult(original, encrypted, decrypted);
        }

        static void WriteHeader(string title)
        {
            Console.WriteLine();
            Console.WriteLine("#########################");
            Console.WriteLine(title);
            Console.WriteLine("#########################");
        }

        static void WriteResult(string original, string encrypted, string decrypted)
        {
            Console.WriteLine($"Original: '{original}'");
            Console.WriteLine($"Encrypted: '{encrypted}'");
            Console.WriteLine($"Decrypted: '{decrypted}'");
        }
    }
}
