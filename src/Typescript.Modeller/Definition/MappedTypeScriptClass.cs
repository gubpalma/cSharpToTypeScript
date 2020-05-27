using System.Collections.Generic;

namespace TypeScript.Modeller.Definition
{
    public class MappedTypeScriptClass
    {
        public string Name { get; set; }

        public IEnumerable<MappedImport> Imports { get; set; }

        public IEnumerable<MappedTypeScriptMember> Members { get; set; }
    }
}
