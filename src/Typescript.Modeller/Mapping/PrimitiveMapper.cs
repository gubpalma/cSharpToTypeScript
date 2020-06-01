using System;
using System.Collections.Generic;
using TypeScript.Modeller.Definition;

namespace TypeScript.Modeller.Mapping
{
    public class PrimitiveMapper: IMapper
    {
        internal static Dictionary<string, TypeScriptDeclaration> DefinedTypesDictionary = new Dictionary<string, TypeScriptDeclaration>
        {
            {"System.Boolean", new TypeScriptDeclaration("boolean", "false")},

            {"System.String", new TypeScriptDeclaration("string", "''")},
            {"System.Char", new TypeScriptDeclaration("string", "''")},
            {"System.Guid", new TypeScriptDeclaration("string", "''")},

            {"System.SByte", new TypeScriptDeclaration("number", "0")},
            {"System.Decimal", new TypeScriptDeclaration("number", "0")},
            {"System.Double", new TypeScriptDeclaration("number", "0")},
            {"System.Single", new TypeScriptDeclaration("number", "0")},
            {"System.Int32", new TypeScriptDeclaration("number", "0")},
            {"System.UInt32", new TypeScriptDeclaration("number", "0")},
            {"System.Int64", new TypeScriptDeclaration("number", "0")},
            {"System.UInt64", new TypeScriptDeclaration("number", "0")},
            {"System.Int16", new TypeScriptDeclaration("number", "0")},
            {"System.UInt16", new TypeScriptDeclaration("number", "0")},

            {"System.DateTime", new TypeScriptDeclaration("Date", "new Date(0)")},
        };

        public bool AppliesTo(
            Type type, 
            ICollection<Type> currentTypes, 
            ICollection<Type> referencedTypes)
        {
            return DefinedTypesDictionary
                .ContainsKey(type?.FullName ?? string.Empty);
        }

        public TypeScriptDeclaration Map(
            Type type, 
            ICollection<Type> currentTypes, 
            ICollection<Type> referencedTypes,
            ICollection<Type> unMappedDependencies) => DefinedTypesDictionary[type?.FullName];
    }
}
