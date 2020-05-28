using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TypeScript.Modeller.AssemblyLoaders
{
    public class AssemblyLoader
    {
        private static IEnumerable<Type> LoadFromAssembly(Assembly assembly)
        {
            return assembly
                .GetTypes()
                .Where(t => t.GetCustomAttribute<TypeScriptViewModelAttribute>() != null)
                .ToList();
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

            currentAssemblies.AddRange(LoadFromAssembly(assembly));
            referenceAssemblies.AddRange(LoadFromAssembly(assembly));

            var references = assembly.GetReferencedAssemblies();

            foreach (var referencedAssembly in references)
            {
                try
                {
                    Console.WriteLine($"* Attempting to load reference assembly {referencedAssembly}");
                    referenceAssemblies
                        .AddRange(Assembly.Load(referencedAssembly)
                            .GetTypes()
                            .ToList());
                }
                catch (IOException e)
                {
                    Console.WriteLine($"\t - Could not load {referencedAssembly}, trying base folder load");

                    var folder = Path.GetDirectoryName(dllFileName);
                    var path = $"{folder}\\{referencedAssembly.Name}.dll";

                    Console.WriteLine($"* Attempting to load reference assembly {referencedAssembly} at {path}");

                    referenceAssemblies
                        .AddRange(ContextAssemblyLoader.LoadFromAssemblyPath(path)
                            .GetTypes()
                            .ToList());
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
