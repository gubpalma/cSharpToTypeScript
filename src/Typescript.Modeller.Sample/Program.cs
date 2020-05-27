using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Sample.Assembly.Two;
using TypeScript.Modeller.Builder;

namespace TypeScript.Modeller.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var projectDirectory = $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}\\Results";

            var assemblyFilePath = Assembly.GetAssembly(typeof(AddressViewModel)).Location;

            var builder = new TypeScriptBuilder();

            await builder.ConvertAsync(assemblyFilePath, projectDirectory);

            Console.ReadKey();
        }
    }
}
