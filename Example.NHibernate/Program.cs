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

            //var anonimize = AnonimizeProvider.GetInstance();
            //var service = anonimize.GetCryptoService();

            //var original = "Guinea Pig 1";
            //var encrypted = service.Encrypt(original);
            //var decrypted = service.Decrypt<string>(encrypted);

            //Console.WriteLine(original);
            //Console.WriteLine(encrypted);
            //Console.WriteLine(decrypted);
        }

        static void CreateUsers()
        {
            var session = NHibernateManager.OpenSession();

            var users = new List<User>();
            for (int i = 0; i < 10; i++)
            {
                var id = i + 1;
                var user = new User
                {
                    Email = $"{id}@example.com",
                    Name = $"Guinea Pig {id}"
                };

                users.Add(user);
            }

            using (var transaction = session.BeginTransaction())
            {
                users.ForEach(user => session.Save(user));
                transaction.Commit();
            }
        }

        static void ReadUsers()
        {
            var session = NHibernateManager.OpenSession();
            var users = session.Query<User>().Where(u => u.Name == "Guinea Pig 5").ToList();
            users.ForEach(u => Console.WriteLine($"{u.Id.ToString("000")} {u.Name} {u.Email}"));
        }

        static void UpdateUsers()
        {
            var session = NHibernateManager.OpenSession();

        }

        static void DeleteUsers()
        {
            var session = NHibernateManager.OpenSession();

        }
    }
}
