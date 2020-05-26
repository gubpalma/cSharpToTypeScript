using System.Linq;
using System.Text;
using Typescript.Modeller.Definition;

namespace TypeScript.Modeller.Builder
{
    internal static class FileBuilder
    {
        public static StringBuilder BuildTypeScriptClass(MappedTypeScriptClass source)
        {
            var output = new StringBuilder();

            foreach(var import in source.Imports)
                output.AppendLine($"import {{ {import.Name} }} from './{import.Name}';");

            if (source.Imports.Any()) output.AppendLine();

            output.AppendLine($"export class {source.Name} {{");

            foreach (var property in source.Members)
            {
                var alternates = string.Join(" | ", property.AlternateTypes);

                output.Append($"\t");
                output.Append($"{property.Scope} ");
                output.Append($"{property.Name}");
                output.Append($"{(property.IsNullable ? "?" : "")}: ");
                output.Append($"{property.Type}");
                output.Append($"{(alternates.Length > 0 ? " | " + alternates : null)}");
                output.Append($"{(property.IsArray ? "[]" : "")}");
                output.Append($" = ");
                output.Append($"{property.Initialiser};");
                output.AppendLine();
            }

            output.AppendLine($"}}");

            return output;
        }
    }
}