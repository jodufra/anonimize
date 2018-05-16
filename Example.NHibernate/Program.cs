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
            CreateUsers();
            ReadUsers();
            UpdateUsers();
            DeleteUsers();
        }

        static void CreateUsers()
        {
            Console.WriteLine("Create Users");

            var session = NHibernateManager.OpenSession();

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

            Console.ReadKey();
        }

        static void ReadUsers()
        {
            Console.WriteLine("Read Users");

            var session = NHibernateManager.OpenSession();
            var users = session.Query<User>().ToList();
            users.ForEach(u => Console.WriteLine($"{u.Id.ToString("000")} - {u.Name} | {u.Email}"));

            Console.ReadKey();
        }

        static void UpdateUsers()
        {
            Console.WriteLine("Update Users");

            var session = NHibernateManager.OpenSession();
            
            Console.ReadKey();
        }

        static void DeleteUsers()
        {
            Console.WriteLine("Delete Users");

            var session = NHibernateManager.OpenSession();
            
            Console.ReadKey();
        }
    }
}
