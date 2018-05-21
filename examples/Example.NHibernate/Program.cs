using Anonimize;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Example
{
    class Program
    {
        static void Main()
        {
            //CreateUsers();
            ReadUsers();
            //UpdateUsers();
            //DeleteUsers();
        }

        static void CreateUsers()
        {
            Console.WriteLine("Create Users");

            var session = SessionManager.OpenSession();

            var users = new List<User>();
            for (int i = 0; i < 10; i++)
            {
                var id = i + 1;
                var user = new User
                {
                    Email = $"{id}@example.com",
                    Name = $"User Name {id}"
                };
                users.Add(user);
            }

            using (var transaction = session.BeginTransaction())
            {
                users.ForEach(user => session.Save(user));
                transaction.Commit();
            }

            Console.WriteLine("Continue?");
            Console.ReadKey();
        }

        static void ReadUsers()
        {
            Console.WriteLine("Read Users");

            var session = SessionManager.OpenSession();

            Console.WriteLine("Email == 5@example.com");
            var users = session.Query<User>().Where(u => u.Email == "5@example.com").ToList();
            Console.WriteLine($"Count {users.Count}");
            users.ForEach(u => Console.WriteLine($"{u.Id.ToString("000")} - {u.Name} | {u.Email}"));

            Console.WriteLine("Email == sRmLdtiG9tZ6QintJH18yXX9pi8CkX6fWJSvsQHJdmhdDx1rL1Cf6g==");
            users = session.Query<User>().Where(u => u.Email == "sRmLdtiG9tZ6QintJH18yXX9pi8CkX6fWJSvsQHJdmhdDx1rL1Cf6g==").ToList();
            Console.WriteLine($"Count {users.Count}");
            users.ForEach(u => Console.WriteLine($"{u.Id.ToString("000")} - {u.Name} | {u.Email}"));

            Console.WriteLine("Continue?");
            Console.ReadKey();
        }

        static void UpdateUsers()
        {
            Console.WriteLine("Update Users");

            var session = SessionManager.OpenSession();

            Console.WriteLine("Continue?");
            Console.ReadKey();
        }

        static void DeleteUsers()
        {
            Console.WriteLine("Delete Users");

            var session = SessionManager.OpenSession();

            Console.WriteLine("Continue?");
            Console.ReadKey();
        }
    }
}
