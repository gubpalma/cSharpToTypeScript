using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Typescript.Modeller.Definition;
using TypeScript.Modeller.Definition;
using Typescript.Modeller.Extensions;

namespace Typescript.Modeller
{
    public static class TypeScriptConverter
    {
        public static MappedTypeScriptClass Convert(
            Type type, 
            ICollection<Type> currentTypes, 
            ICollection<Type> referencedTypes,
            ICollection<Type> unMappedDependencies)
        {
            var output = new MappedTypeScriptClass();

            var properties =
                type
                    .GetProperties(
                        BindingFlags.DeclaredOnly |
                        BindingFlags.Public |
                        BindingFlags.Instance);

            output.Name = type.Name;

            output.Members =
                properties
                    .Select(o => Convert(o, currentTypes, referencedTypes, unMappedDependencies));

            var imports = new List<MappedImport>();
            foreach(var imported in output.Members.Where(o => o.IsImport))
                imports.Add(new MappedImport(imported.Type));

            output.Imports = imports;

            return output;
        }

        public static MappedTypeScriptMember Convert(
            PropertyInfo property, 
            ICollection<Type> currentTypes, 
            ICollection<Type> referencedTypes,
            ICollection<Type> unMappedDependencies)
        {
            var output = new MappedTypeScriptMember();

            var mappedType = 
                TypeMapper
                    .Map(
                        property.PropertyType, 
                        currentTypes, 
                        referencedTypes,
                        unMappedDependencies);

            output.Name = property.Name.ToCamelCase();

            output.Scope = "public";

            output.Type = mappedType.Type;

            output.AlternateTypes = mappedType.AlternateTypes;

            output.Initialiser = mappedType.Initialiser;

            output.IsNullable = mappedType.IsNullable;

            output.IsImport = mappedType.IsImport;

            output.IsArray = mappedType.IsArray;

            return output;
        }
    }
}
