using System;
using System.Collections.Generic;
using TypeScript.Modeller.Definition;

namespace TypeScript.Modeller.Mapping
{
    public interface IMapper
    {
        bool AppliesTo(
            Type type, 
            ICollection<Type> currentTypes, 
            ICollection<Type> referencedTypes);

        TypeScriptDeclaration Map(
            Type type,
            ICollection<Type> currentTypes,
            ICollection<Type> referencedTypes,
            ICollection<Type> unMappedDependencies);
    }
}
