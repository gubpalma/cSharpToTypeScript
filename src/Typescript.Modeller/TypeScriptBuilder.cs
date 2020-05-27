using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TypeScript.Modeller;
using TypeScript.Modeller.Builder;
using TypeScript.Modeller.Definition;

namespace Typescript.Modeller
{
    public class TypeScriptBuilder
    {
        

        public async Task<IEnumerable<ConversionResult>> ConvertAsync(string dllFileName, string outputFolder = null)
        {
            AssemblyLoader
                .LoadAllAssemblies(
                    dllFileName, 
                    out var tsCurrentAssemblyClasses, 
                    out var tsReferencedAssemblyClasses);

            if (!string.IsNullOrEmpty(outputFolder))
            {
                if (!Directory.Exists(outputFolder))
                    Directory.CreateDirectory(outputFolder);
            }

            var results = new List<ConversionResult>();

            var unMappedDependencies = new List<Type>();

            do
            {
                unMappedDependencies.Clear();

                await
                    Task.WhenAll(
                        tsCurrentAssemblyClasses.Select(o =>
                        {
                            try
                            {
                                var mappedTypeScriptClass =
                                    TypeScriptConverter
                                        .Convert(o, tsCurrentAssemblyClasses, tsReferencedAssemblyClasses, unMappedDependencies);

                                var output =
                                    FileBuilder
                                        .BuildTypeScriptClass(mappedTypeScriptClass);

                                var fileName = $"{o.Name}.ts";

                                results.Add(
                                    new ConversionResult
                                    {
                                        FileName = fileName, 
                                        FileData = output.ToString(),
                                        Hash = o.GUID
                                    });

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

                // Remove duplicates
                unMappedDependencies = 
                    unMappedDependencies
                        .Distinct()
                        .ToList();

                // Remove existing (prevent circular references)
                unMappedDependencies =
                    unMappedDependencies
                        .Where(o => !results.Select(r => r.Hash).Contains(o.GUID))
                        .ToList();

                tsCurrentAssemblyClasses = new List<Type>(unMappedDependencies);

            } while (unMappedDependencies.Any());

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
