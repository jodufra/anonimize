using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Example
{
    class Program
    {
        delegate void CrudTask();

        static void Main()
        {
            Console.WriteLine("##########################");
            Console.WriteLine("Example NHibernate");
            Console.WriteLine("##########################");
            Console.WriteLine();

            while (ChooseTask(out CrudTask task))
            {
                task();
                Console.WriteLine("Done!");
                Console.WriteLine();
            }
        }

        static bool ChooseTask(out CrudTask task)
        {
            var tasks = new List<CrudTask> { Create, Read, Update, Delete };

            var taskNames = tasks.Select(t => t.Method.Name.Insert(0, "(").Insert(2, ")"));

            Console.Write($"Choose task: '{string.Join("', '", taskNames)}'. ");

            var key = Console.ReadKey().Key;
            switch (key)
            {
                case ConsoleKey.C:
                    task = Create;
                    break;
                case ConsoleKey.R:
                    task = Read;
                    break;
                case ConsoleKey.U:
                    task = Update;
                    break;
                case ConsoleKey.D:
                    task = Delete;
                    break;
                default:
                    task = null;
                    break;
            }

            Console.WriteLine();

            return task != null;
        }

        static void Create()
        {
            Console.WriteLine("Creating");

            var session = SessionManager.OpenSession();

            var hasUsers = session.Query<User>().Any();

            if (hasUsers)
            {
                Console.WriteLine("Users table is not empty!");
                return;
            }

            var users = new List<User>();
            for (int i = 0; i < 10; i = i + 2)
            {
                var id = i + 1;
                var user = new User
                {
                    DateCreated = DateTime.Now
                };
                User.PopulateAllProperties(id, user);
                users.Add(user);

                id++;
                user = new User
                {
                    DateCreated = DateTime.Now
                };
                User.PopulateRequiredProperties(id, user);
                users.Add(user);
            }

            using (var transaction = session.BeginTransaction())
            {
                users.ForEach(user => session.Save(user));
                transaction.Commit();
            }
        }

        static void Read()
        {
            Console.WriteLine("Reading");

            var session = SessionManager.OpenSession();

            var hasUsers = session.Query<User>().Any();

            if (!hasUsers)
            {
                Console.WriteLine("No users found!");
                return;
            }

            var users = session.Query<User>().ToList();

            Console.WriteLine($"Total: {users.Count} users");
            Console.WriteLine();

            var json = JsonConvert.SerializeObject(users, Formatting.Indented);
            Console.WriteLine(json);
        }

        static void Update()
        {
            Console.WriteLine("Updating");

            var session = SessionManager.OpenSession();

            var hasUsers = session.Query<User>().Any();

            if (!hasUsers)
            {
                Console.WriteLine("No users found!");
                return;
            }

            var users = session.Query<User>().ToList();
            var seed = users.Max(u => u.Id) + 1;

            foreach (var user in users)
            {
                user.DateUpdated = DateTime.Now;
                User.PopulateAllProperties(seed, user);
                seed++;
            }

            using (var transaction = session.BeginTransaction())
            {
                users.ForEach(session.Update);
                transaction.Commit();
            }
        }

        static void Delete()
        {
            Console.WriteLine("Deleting");

            var session = SessionManager.OpenSession();

            var hasUsers = session.Query<User>().Any();

            if (!hasUsers)
            {
                Console.WriteLine("No users found!");
                return;
            }

            var users = session.Query<User>().ToList();

            using (var transaction = session.BeginTransaction())
            {
                users.ForEach(session.Delete);
                transaction.Commit();
            }
        }
    }
}
