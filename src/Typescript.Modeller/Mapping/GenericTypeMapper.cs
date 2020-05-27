using System;
using System.Collections.Generic;
using Typescript.Modeller;
using TypeScript.Modeller.Definition;

namespace TypeScript.Modeller.Mapping
{
    public class GenericTypeMapper : IMapper
    {
        internal static Dictionary<string, Func<TypeScriptDeclaration, TypeScriptDeclaration>> 
            GenericTypesDictionary = new Dictionary<string, Func<TypeScriptDeclaration, TypeScriptDeclaration>>
        {
            {
                "Nullable`1", 
                source => 
                    new TypeScriptDeclaration {
                    Type = source.Type,
                    IsNullable = true,
                    IsImport = source.IsImport,
                    IsArray = false,
                    Initialiser = "undefined",
                    AlternateTypes = new string[0]
                }
            },
            {
                "IEnumerable`1",
                source =>
                    new TypeScriptDeclaration {
                        Type = source.Type,
                        IsNullable = false,
                        IsImport = source.IsImport,
                        IsArray = true,
                        Initialiser = "[]",
                        AlternateTypes = new string[0]
                    }
            },
            {
                "ICollection`1",
                source =>
                    new TypeScriptDeclaration {
                        Type = source.Type,
                        IsNullable = false,
                        IsImport = source.IsImport,
                        IsArray = true,
                        Initialiser = "[]",
                        AlternateTypes = new string[0]
                    }
            },
        };

        public bool AppliesTo(Type type, ICollection<Type> currentTypes, ICollection<Type> referencedTypes)
        {
            return GenericTypesDictionary
                .ContainsKey(type?.Name ?? string.Empty);
        }

        public TypeScriptDeclaration Map(
            Type type, 
            ICollection<Type> currentTypes, 
            ICollection<Type> referencedTypes,
            ICollection<Type> unMappedDependencies)
        {
            var genericType = GenericTypesDictionary[type.Name];

            if (type.GenericTypeArguments.Length == 1)
            {
                var nulledType = type.GenericTypeArguments[0];
                var mapped = TypeMapper.Map(nulledType, currentTypes, referencedTypes, unMappedDependencies);

                var result = genericType(mapped);
                return result;
            }

            throw new Exception($"Unexpected amount of generic type arguments: {type.GenericTypeArguments.Length}");
        }
    }
}