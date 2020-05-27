using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Typescript.Modeller;

namespace TypeScript.Modeller
{
    public class AssemblyLoader
    {
        private static IEnumerable<Type> LoadFromAssembly(Assembly assembly) =>
            assembly
                .GetTypes()
                .Where(t => t.GetCustomAttribute<TypeScriptViewModelAttribute>() != null);

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

            currentAssemblies.AddRange(LoadFromAssembly(assembly));
            referenceAssemblies.AddRange(LoadFromAssembly(assembly));

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
