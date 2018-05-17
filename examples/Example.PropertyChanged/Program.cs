using System;

namespace Example
{
    class Program
    {
        static void Main()
        {
            var user = new Person();

            Console.WriteLine("### EXAMPLE ###");

            user.Name = "My Name";
            user.Email = "email@example.com";
            user.Address = "Arround the corner street";

            Console.WriteLine("Name:");
            Console.WriteLine(user.Name);
            Console.WriteLine(user._Name);

            Console.WriteLine();

            Console.WriteLine("Email:");
            Console.WriteLine(user.Email);
            Console.WriteLine(user._Email);

            Console.WriteLine();

            Console.WriteLine("Address:");
            Console.WriteLine(user.Address);
            Console.WriteLine(user._Address);
        }
    }
}
