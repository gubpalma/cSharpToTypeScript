using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;

namespace TypeScript.Modeller.AssemblyLoaders
{
    public static class ContextAssemblyLoader
    {
        public static Assembly LoadFromAssemblyPath(string assemblyFullPath)
        {
            Console.WriteLine('1');

            var fileNameWithOutExtension = Path.GetFileNameWithoutExtension(assemblyFullPath);
            var fileName = Path.GetFileName(assemblyFullPath);
            var directory = Path.GetDirectoryName(assemblyFullPath);

            Console.WriteLine('2');

            var inCompileLibraries = DependencyContext.Default.CompileLibraries.Any(l =>
                l.Name.Equals(fileNameWithOutExtension, StringComparison.OrdinalIgnoreCase));
            var inRuntimeLibraries = DependencyContext.Default.RuntimeLibraries.Any(l =>
                l.Name.Equals(fileNameWithOutExtension, StringComparison.OrdinalIgnoreCase));

            Console.WriteLine('3');

            var assembly = (inCompileLibraries || inRuntimeLibraries)
                ? Assembly.Load(new AssemblyName(fileNameWithOutExtension))
                : AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyFullPath);

            Console.WriteLine('4');

            if (assembly != null)
                LoadReferencedAssemblies(assembly, fileName, directory);

            Console.WriteLine('5');

            LoadReferencedAssemblies(assembly, "Infrastructure.Host.WebApp", directory);

            Console.WriteLine('6');

            return assembly;
        }

        private static void LoadReferencedAssemblies(Assembly assembly, string fileName, string directory)
        {
            var filesInDirectory = Directory.GetFiles(directory).Where(x => x != fileName).Select(x => Path.GetFileNameWithoutExtension(x)).ToList();
            var references = assembly.GetReferencedAssemblies();

            foreach (var reference in references)
            {
                if (filesInDirectory.Contains(reference.Name))
                {
                    try
                    {
                        var loadFileName = reference.Name + ".dll";
                        var path = Path.Combine(directory, loadFileName);
                        var loadedAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
                        if (loadedAssembly != null)
                            LoadReferencedAssemblies(loadedAssembly, loadFileName, directory);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("--------- WARNING ---------");
                        Console.WriteLine($"Could not load reference assembly {reference.Name}");
                        Console.WriteLine(e.Message);
                        Console.WriteLine("-------------------------");
                    }
                }
            }
        }
    }
}