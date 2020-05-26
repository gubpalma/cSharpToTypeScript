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
                                File.WriteAllTextAsync(
                                    $"{outputFolder}\\{o.FileName}",
                                    o.FileData.ToString())));
            }

            return results;
        }
    }
}
