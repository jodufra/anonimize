using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new User();

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
