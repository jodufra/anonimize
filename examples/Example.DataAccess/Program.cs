using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLib.Entities;

namespace Example
{
    class Program
    {
        static void Main()
        {
            CreateUsers();
            ReadUsers();
            //UpdateUsers();
            //DeleteUsers();
        }

        static void CreateUsers()
        {
            Console.WriteLine("Create Users");

            var session = SessionManager.OpenSession();

            var hasUsers = session.Users.Any();

            if (hasUsers)
            {
                Console.WriteLine("Table contains users!");
                Console.WriteLine("Continue?");
                Console.ReadKey();
                return;
            }

            var users = new List<User>();
            for (int i = 0; i < 10; i = i + 2)
            {
                var id = i + 1;
                var user = new User
                {
                    AccountBalance = i * 12.345m,
                    AccountDebt = i * 1.234m,
                    Address = "Street nº" + i,
                    CivilId = i * 111111,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    Email = $"{id}@example.com",
                    FiscalId = i * 222222,
                    IsActive = true,
                    IsFemale = i % 2 == 0,
                    Name = $"User {id}"
                };
                users.Add(user);

                id++;
                user = new User
                {
                    AccountBalance = i * 12.345m,
                    AccountDebt = null,
                    Address = null,
                    CivilId = i * 333333,
                    DateCreated = DateTime.Now,
                    DateUpdated = null,
                    Email = $"{id}@example.com",
                    FiscalId = null,
                    IsActive = false,
                    IsFemale = null,
                    Name = $"User {id}"
                };
                users.Add(user);
            }

            users.ForEach(session.Add);
            session.SaveChanges();

            Console.WriteLine("Continue?");
            Console.ReadKey();
        }

        static void ReadUsers()
        {
            Console.WriteLine("Read Users");

            var session = SessionManager.OpenSession();

            var users = session.Users.ToList();
            Console.WriteLine($"Count {users.Count}");
            Console.WriteLine();

            foreach (var user in users)
            {
                Console.WriteLine("#" + user.Id);
                Console.WriteLine(user.AccountBalance);
                Console.WriteLine(user.AccountDebt);
                Console.WriteLine(user.Address);
                Console.WriteLine(user.CivilId);
                Console.WriteLine(user.DateCreated);
                Console.WriteLine(user.DateUpdated);
                Console.WriteLine(user.Email);
                Console.WriteLine(user.FiscalId);
                Console.WriteLine(user.IsActive);
                Console.WriteLine(user.IsFemale);
                Console.WriteLine(user.Name);
                Console.WriteLine();
            }

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
