using CodeGenerationBugDemo.Generator;
using Microsoft.CodeAnalysis;
using Xunit;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace CodeGenerationBugDemo.GeneratorTests
{
    public class DemoGeneratorTests
    {
        [Theory]
        [InlineData("MyClass")]
        [InlineData("ClassA")]
        [InlineData("ClassB")]
        [InlineData("OtherName")]
        public void GivenAnyClass_GeneratesPartialWithEmptyMethod(string className)
        {
            var classDeclaration = ClassDeclaration(className);
            var partial = DemoGenerator.GeneratePartialWithMethod(classDeclaration);
            var text = partial.NormalizeWhitespace().ToString();
            var expected = $@"partial class {className}
{{
    public void {DemoGenerator.GeneratedMethodName}()
    {{
    }}
}}";
            Assert.Equal(expected, text);
        }

        [Fact]
        public void GivenTestClass_CanInvokeGeneratedMethod()
        {
            // When CodeGenerationBugDemo.Generator targets netstandard1.5/1.6: Succeeds because member was generated:
            // When CodeGenerationBugDemo.Generator targets netstandard2.0: Fails because member was not generated:
            var method = typeof(TestClass).GetMethod(DemoGenerator.GeneratedMethodName);
            Assert.NotNull(method);
        }
    }

    [DemoGeneration]
    internal partial class TestClass
    {

    }
}
