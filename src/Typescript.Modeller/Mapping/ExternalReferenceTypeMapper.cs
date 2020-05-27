using System;
using System.Collections.Generic;
using System.Linq;
using TypeScript.Modeller.Definition;

namespace TypeScript.Modeller.Mapping
{
    public class ExternalReferenceTypeMapper : IMapper
    {
        public bool AppliesTo(
            Type type, 
            ICollection<Type> currentTypes,
            ICollection<Type> referencedTypes) => referencedTypes.Any(o => o.FullName == type.FullName);

        public TypeScriptDeclaration Map(
            Type type, 
            ICollection<Type> currentTypes, 
            ICollection<Type> referencedTypes,
            ICollection<Type> unMappedDependencies)
        {
            var name = referencedTypes.First(o => o.FullName == type?.FullName).Name;

            var result = new TypeScriptDeclaration
            {
                AlternateTypes = new[] { "undefined" },
                Initialiser = "undefined",
                IsArray = false,
                IsImport = true,
                IsNullable = false,
                Type = name
            };

            unMappedDependencies
                .Add(type);

            return result;
        }
    }
}