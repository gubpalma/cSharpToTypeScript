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
            var assembly = Assembly.LoadFrom(dllFileName);

            currentAssemblies = new List<Type>();
            referenceAssemblies = new List<Type>();

            currentAssemblies.AddRange(LoadFromAssembly(assembly));
            referenceAssemblies.AddRange(LoadFromAssembly(assembly));

            foreach (var referencedAssembly in assembly.GetReferencedAssemblies())
            {
                referenceAssemblies
                    .AddRange(Assembly.Load(referencedAssembly).GetTypes());
            }
        }
    }
}
