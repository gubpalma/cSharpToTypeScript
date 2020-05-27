using System;
using System.Collections.Generic;
using Typescript.Modeller;
using TypeScript.Modeller.Definition;
using Xunit;

namespace TypeScript.Modeller.Tests.Unit
{
    public class TypeMapperTests
    {
        private readonly TestContext _context;

        public TypeMapperTests()
        {
            _context = new TestContext();
        }

        [Theory]
        [InlineData("System.String", "string")]
        [InlineData("System.Boolean", "boolean")]
        [InlineData("System.Char", "string")]
        [InlineData("System.Guid", "string")]
        [InlineData("System.SByte", "number")]
        [InlineData("System.Decimal", "number")]
        [InlineData("System.Double", "number")]
        [InlineData("System.Single", "number")]
        [InlineData("System.Int32", "number")]
        [InlineData("System.UInt32", "number")]
        [InlineData("System.Int64", "number")]
        [InlineData("System.UInt64", "number")]
        [InlineData("System.Int16", "number")]
        [InlineData("System.UInt16", "number")]
        public void Test_Mappings(string typeName, string expected)
        {
            var type = Type.GetType(typeName);

            _context.ArrangeType(type);

            _context.ActMapType();

            _context.AssertMappedType(expected);
        }

        private class TestContext
        {
            private Type _type;
            private TypeScriptDeclaration _result;

            public void ArrangeType(Type type)
            {
                _type = type;
            }

            public void ActMapType()
            {
                _result = TypeMapper.Map(_type, new List<Type>(), new List<Type>(), new List<Type>());
            }

            public void AssertMappedType(string expected)
            {
                Assert.Equal(expected, _result.Type);
            }
        }
    }
}