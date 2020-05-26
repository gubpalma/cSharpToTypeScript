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
        public async Task<IEnumerable<ConversionResult>> ConvertAsync(string dllFileName, string outputFolder = null)
        {
            var assembly = Assembly.LoadFrom(dllFileName);

            var tsViewModels =
                assembly
                    .GetTypes()
                    .Where(t => t.GetCustomAttribute<TypeScriptViewModelAttribute>() != null)
                    .ToList();

            if (!string.IsNullOrEmpty(outputFolder))
            {
                if (!Directory.Exists(outputFolder))
                    Directory.CreateDirectory(outputFolder);
            }

            var results = new List<ConversionResult>();

            foreach (var tsViewModel in tsViewModels)
            {
                var mappedTypeScriptClass = 
                    TypeScriptConverter
                        .Convert(tsViewModel, tsViewModels);
            
                var output = 
                    FileBuilder
                        .BuildTypeScriptClass(mappedTypeScriptClass);
            
                var fileName = $"{tsViewModel.Name}.ts";
                
                results.Add(new ConversionResult {FileName = fileName, FileData = output.ToString()});
            }

            if (!string.IsNullOrEmpty(outputFolder))
            {
                await
                    Task.WhenAll(
                        results
                            .Select(o =>
                            {
                                var filePath = $"{outputFolder}\\{o.FileName}";

                                if (File.Exists(filePath))
                                    Console.WriteLine($"File [{filePath}] exists, skipping");
                                else
                                {
                                    Console.WriteLine($"Writing file [{filePath}]");
                                    File.WriteAllTextAsync(filePath, o.FileData.ToString());
                                }

                                return Task.CompletedTask;
                            }));
            }

            return results;
        }
    }
}
