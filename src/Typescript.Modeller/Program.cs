using System;
using TypeScript.Modeller.Builder;

namespace TypeScript.Modeller
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("--------- Initialising TypeScript converter ---------");

                if (args.Length != 2)
                    throw new Exception("Expected two arguments <DLL File Path>, <Output Folder>");

                Console.WriteLine($"Assembly Path: {args[0]}");
                Console.WriteLine($"Output Path: {args[1]}");

                new TypeScriptBuilder()
                    .ConvertAsync(args[0], args[1])
                    .Wait();

                Console.WriteLine("--------- Completed TypeScript conversion ---------");
            }
            catch (Exception e)
            {
                Console.WriteLine("--------- ERROR ---------");
                Console.WriteLine($"Could not complete TypeScript conversion for arguments - {string.Join(',', args)}");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("-------------------------");

                Environment.Exit(1);
            }
        }
    }
}
