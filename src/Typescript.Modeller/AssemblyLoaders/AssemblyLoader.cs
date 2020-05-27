using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TypeScript.Modeller.AssemblyLoaders
{
    public class AssemblyLoader
    {
        private static IEnumerable<Type> LoadFromAssembly(Assembly assembly)
        {
            var reg = assembly
                .GetTypes()
                .Where(t => t.GetCustomAttribute<TypeScriptViewModelAttribute>() != null);

            Console.WriteLine(string.Join(", ", reg.Select(o => o.AssemblyQualifiedName)));

            return assembly
                .GetTypes()
                .Where(t => t.GetCustomAttribute<TypeScriptViewModelAttribute>() != null);
        }

        public static void LoadAllAssemblies(
            string dllFileName,
            out List<Type> currentAssemblies,
            out List<Type> referenceAssemblies)
        {
            var assembly = 
                ContextAssemblyLoader
                    .LoadFromAssemblyPath(dllFileName);

            currentAssemblies = new List<Type>();
            referenceAssemblies = new List<Type>();

            Console.WriteLine('7');

            currentAssemblies.AddRange(LoadFromAssembly(assembly));

            Console.WriteLine('8');

            referenceAssemblies.AddRange(LoadFromAssembly(assembly));

            Console.WriteLine('9');

            var references = assembly.GetReferencedAssemblies();

            foreach (var referencedAssembly in references)
            {
                try
                {
                    referenceAssemblies
                        .AddRange(Assembly.Load(referencedAssembly).GetTypes());
                }
                catch (Exception e)
                {
                    Console.WriteLine("--------- WARNING ---------");
                    Console.WriteLine($"Could not load reference assembly {referencedAssembly.FullName}");
                    Console.WriteLine(e.Message);
                    Console.WriteLine("-------------------------");
                }
            }
        }
    }
}
