using System;
using System.Collections.Generic;
using System.Linq;
using TypeScript.Modeller.Definition;

namespace TypeScript.Modeller.Mapping
{
    public class ReferenceTypeMapper : IMapper
    {
        public bool AppliesTo(Type type, ICollection<Type> allTypes) => allTypes.Any(o => o.FullName == type.FullName);

        public TypeScriptDeclaration Map(Type type, ICollection<Type> allTypes)
        {
            var name = allTypes.First(o => o.FullName == type?.FullName).Name;

            var result = new TypeScriptDeclaration
            {
                AlternateTypes = new[] {"undefined"},
                Initialiser = "undefined",
                IsArray = false,
                IsImport = true,
                IsNullable = false,
                Type = name
            };

            return result;
        }
    }
}