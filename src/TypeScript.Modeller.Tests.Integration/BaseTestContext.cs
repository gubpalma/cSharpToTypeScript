using System;
using System.Collections.Generic;
using System.IO;
using TypeScript.Modeller.Definition;

namespace TypeScript.Modeller.Tests.Integration
{
    internal class BaseTestContext
    {
        protected static IEnumerable<ConversionResult> LoadExpectedConversionResults(string folderPath)
        {
            var results = new List<ConversionResult>();

            var projectDirectory = $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}\\{folderPath}";

            foreach (var file in Directory.GetFiles(projectDirectory))
            {
                results.Add(new ConversionResult
                {
                    FileName = Path.GetFileName(file),
                    FileData = File.ReadAllText(file)
                });
            }

            return results.ToArray();
        }
    }
}