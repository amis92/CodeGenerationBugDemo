using CodeGenerationBugDemo.Generator;

namespace CodeGenerationBugDemo.Library
{
    [DemoGeneration]
    public partial class DemoClass
    {
        public void Problem()
        {
            // When CodeGenerationBugDemo.Generator targets netstandard1.5/1.6: Succeeds because member was generated:
            // When CodeGenerationBugDemo.Generator targets netstandard2.0: Fails because member was not generated:

            Generated();
        }
    }
}
