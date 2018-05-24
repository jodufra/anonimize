using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Anonimize.DataAccess;

namespace Example.DataAccess.AssemblyNames
{
    class Program
    {
        static void Main(string[] args)
        {
            var assembly = Assembly.GetAssembly(typeof(AEncryptedType<>));
            var types = GetTypesInNamespace(assembly, "Anonimize.DataAccess");
            var names = types.Select(t => t.AssemblyQualifiedName);

            var folder = Path.GetFullPath(@"..\..\");
            var filename = "assemblynames.txt";

            File.WriteAllLines(Path.Combine(folder, filename), names);

            foreach (var type in types)
            {
                Console.WriteLine(type.AssemblyQualifiedName);
            }
        }

        static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return
              assembly.GetTypes()
                      .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                      .ToArray();
        }
    }
}
