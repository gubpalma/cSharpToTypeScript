using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Typescript.Modeller;
using TypeScript.Modeller.Definition;
using Xunit;

namespace TypeScript.Modeller.Tests.Integration
{
    public class FileConversionTests
    {
        private readonly TestContext _testContext;

        public FileConversionTests()
        {
            _testContext = new TestContext();
        }

        [Theory]
        [InlineData("Sample.Assembly.One", "SampleOneExpected")]
        [InlineData("Sample.Assembly.Two", "SampleTwoExpected")]
        public async Task TestParseSteps(string featureFile, string expectedResultsFolder)
        {
            _testContext.ArrangeAssembly(featureFile);
            await _testContext.ActConvertAssembly();
            _testContext.AssertConversion(expectedResultsFolder);
        }

        private class TestContext : BaseTestContext
        {
            private readonly TypeScriptBuilder _sut;
            private string _assemblyPath;
            private IEnumerable<ConversionResult> _results;

            public TestContext()
            {
                _sut = new TypeScriptBuilder();
            }

            public void ArrangeAssembly(string assemblyName)
            {
                var assembly = Assembly.Load(assemblyName);
                _assemblyPath = assembly.Location;
            }

            public async Task ActConvertAssembly() => _results = await _sut.ConvertAsync(_assemblyPath);

            public void AssertConversion(string expectedFolderPath)
            {
                var expectedData = 
                    LoadExpectedConversionResults(expectedFolderPath)
                        .ToList();

                foreach (var result in _results)
                {
                    var expectedResult = expectedData.FirstOrDefault(o => o.FileName == result.FileName);
                    Assert.NotNull(expectedResult);
                    Assert.Equal(expectedResult.FileData, result.FileData);
                }
            }
        }
    }
}