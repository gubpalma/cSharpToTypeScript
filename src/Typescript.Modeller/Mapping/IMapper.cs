using System;
using System.Collections.Generic;
using TypeScript.Modeller.Definition;

namespace TypeScript.Modeller.Mapping
{
    public interface IMapper
    {
        bool AppliesTo(Type type, ICollection<Type> allTypes);

        TypeScriptDeclaration Map(Type type, ICollection<Type> allTypes);
    }
}
