using System.Collections.Generic;

namespace TypeScript.Modeller.Definition
{
    public class TypeScriptDeclaration
    {
        public string Type { get; set; }

        public IEnumerable<string> AlternateTypes { get; set; }

        public string Initialiser { get; set; }

        public bool IsNullable { get; set; }

        public bool IsImport { get; set; }

        public bool IsArray { get; set; }

        public TypeScriptDeclaration()
        {
        }

        public TypeScriptDeclaration(
            string type,
            string initialiser)
        {
            Type = type;
            Initialiser = initialiser;
            IsNullable = false;
            IsImport = false;
            IsArray = false;
            AlternateTypes = new string[0];
        }
    }
}
