using System.Collections.Generic;

namespace TypeScript.Modeller.Definition
{
    public class MappedTypeScriptMember
    {
        public string Scope { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Initialiser { get; set; }

        public bool IsNullable { get; set; }

        public bool IsImport { get; set; }

        public bool IsArray { get; set; }

        public IEnumerable<string> AlternateTypes { get; set; }
    }
}
