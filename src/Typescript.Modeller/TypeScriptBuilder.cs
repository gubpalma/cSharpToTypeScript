using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TypeScript.Modeller.Builder;
using TypeScript.Modeller.Definition;

namespace Typescript.Modeller
{
    public class TypeScriptBuilder
    {
        private static IEnumerable<Type> LoadFromAssembly(Assembly assembly) =>
            assembly
                .GetTypes()
                .Where(t => t.GetCustomAttribute<TypeScriptViewModelAttribute>() != null);

        public async Task<IEnumerable<ConversionResult>> ConvertAsync(string dllFileName, string outputFolder = null)
        {
            var assembly = Assembly.LoadFrom(dllFileName);

            var tsCurrentAssemblyClasses = new List<Type>();
            var tsReferencedAssemblyClasses = new List<Type>();

            tsCurrentAssemblyClasses.AddRange(LoadFromAssembly(assembly));
            tsReferencedAssemblyClasses.AddRange(LoadFromAssembly(assembly));

            foreach (var referencedAssembly in assembly.GetReferencedAssemblies())
            {
                tsReferencedAssemblyClasses
                    .AddRange(LoadFromAssembly(Assembly.Load(referencedAssembly)));
            }

            if (!string.IsNullOrEmpty(outputFolder))
            {
                if (!Directory.Exists(outputFolder))
                    Directory.CreateDirectory(outputFolder);
            }

            var results = new List<ConversionResult>();

            await
                Task.WhenAll(
                    tsCurrentAssemblyClasses.Select(o =>
                    {
                        try
                        {
                            var mappedTypeScriptClass =
                                TypeScriptConverter
                                    .Convert(o, tsReferencedAssemblyClasses);

                            var output =
                                FileBuilder
                                    .BuildTypeScriptClass(mappedTypeScriptClass);

                            var fileName = $"{o.Name}.ts";

                            results.Add(new ConversionResult {FileName = fileName, FileData = output.ToString()});

                            return Task.CompletedTask;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("--------- ERROR ---------");
                            Console.WriteLine($"Could not convert class {o.FullName}");
                            Console.WriteLine(e.Message);
                            Console.WriteLine("-------------------------");
                            return Task.FromException(e);
                        }
                    }));

            if (!string.IsNullOrEmpty(outputFolder))
            {
                await
                    Task.WhenAll(
                        results
                            .Select(o =>
                            {
                                var filePath = $"{outputFolder}\\{o.FileName}";
                                try
                                {
                                    if (File.Exists(filePath))
                                        Console.WriteLine($"File [{filePath}] exists, skipping");
                                    else
                                    {
                                        Console.WriteLine($"Writing file [{filePath}]");
                                        File.WriteAllTextAsync(filePath, o.FileData.ToString());
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("--------- ERROR ---------");
                                    Console.WriteLine($"Could not write file {filePath}");
                                    Console.WriteLine(e.Message);
                                    Console.WriteLine("-------------------------");
                                    return Task.FromException(e);
                                }

                                return Task.CompletedTask;
                            }));
            }

            return results;
        }
    }
}
