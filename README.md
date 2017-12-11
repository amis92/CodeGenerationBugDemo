# CodeGeneration.Roslyn bug demo

This project demonstrates a bug in [CodeGeneration.Roslyn project](https://github.com/AArnott/CodeGeneration.Roslyn)
where the generated files do not contain any members (but they *do* contain using directives meaning the generation runs).

Package version used is **0.4.11**. Bug occurs in Visual Studio 2017 **v15.5** (checked, may happen in earlier builds supporting netstandard2.0).

### Steps to reproduce:

1. Change `CodeGenerationBugDemo.Generator` target to `netstandard2.0` (as opposed to `netstandard1.5` or `netstandard1.6`).
2. Observe that:
    a. `CodeGenerationBugDemo.Library.DemoClass` doesn't compile since the invoked method was not generated.
    b. `CodeGenerationBugDemo.GeneratorTests.DemoGeneratorTests.GivenTestClass_CanInvokeGeneratedMethod` test fails because the method was not generated.

### Additional remarks

The task still runs (as msbuild logs show and because there are generated files in `/obj`) but the generated files
only contain using directives. That indicates there are no exceptions thrown. Looking through source code (from tag v0.4.11) of
`CodeGeneration.Roslyn.DocumentTransform.TransformAsync` it seems:

* either there are no `memberNodes` found in `inputSyntaxTree` (which seems unlikely as that only calls Roslyn APIs
  and because the call to `inputSyntaxTree.GetRoot()` definitely returns at least the using directives inserted later)

* or there are no `generators` found for given `memberNode`
