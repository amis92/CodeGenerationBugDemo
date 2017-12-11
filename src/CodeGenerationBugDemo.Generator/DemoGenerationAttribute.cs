using CodeGeneration.Roslyn;
using System;
using System.Diagnostics;

namespace CodeGenerationBugDemo.Generator
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    [Conditional("CodeGeneration")]
    [CodeGenerationAttribute(typeof(DemoGenerator))]
    public sealed class DemoGenerationAttribute : Attribute
    {
    }
}
