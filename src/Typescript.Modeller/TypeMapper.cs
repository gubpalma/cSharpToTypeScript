using System;
using System.Collections.Generic;
using System.Linq;
using TypeScript.Modeller.Definition;
using TypeScript.Modeller.Mapping;

namespace Typescript.Modeller
{
    public static class TypeMapper
    {
        internal static IMapper[] Mappers = new List<IMapper>
        {
            new PrimitiveMapper(),
            new GenericTypeMapper(),
            new ReferenceTypeMapper()
        }.ToArray();

        public static TypeScriptDeclaration Map(Type type, ICollection<Type> allTypes)
        {
            var mapper =
                Mappers
                    .FirstOrDefault(o => o.AppliesTo(type, allTypes));

            if (mapper == null)
                throw new Exception($"Could not map unknown type {type?.Name}; check that the included classes for mapping possess the correct attribute.");

            return 
                mapper
                    .Map(type, allTypes);
        }
    }
}
