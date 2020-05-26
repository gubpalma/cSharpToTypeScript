using System.Collections.Generic;
using TypeScript.Modeller.Definition;

namespace Typescript.Modeller.Definition
{
    public class MappedTypeScriptClass
    {
        public string Name { get; set; }

        public IEnumerable<MappedImport> Imports { get; set; }

        public IEnumerable<MappedTypeScriptMember> Members { get; set; }
    }
}
