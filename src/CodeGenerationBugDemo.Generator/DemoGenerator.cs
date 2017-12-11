using CodeGeneration.Roslyn;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace CodeGenerationBugDemo.Generator
{
    public class DemoGenerator : ICodeGenerator
    {
        public DemoGenerator(AttributeData attributeData)
        {
        }

        public const string GeneratedMethodName = "Generated";

        public Task<SyntaxList<MemberDeclarationSyntax>> GenerateAsync(TransformationContext context, IProgress<Diagnostic> progress, CancellationToken cancellationToken)
        {
            var classDeclaration = context.ProcessingMember as ClassDeclarationSyntax;
            var generatedPartial = GeneratePartialWithMethod(classDeclaration);
            var members = SingletonList<MemberDeclarationSyntax>(generatedPartial);
            return Task.FromResult(members);
        }

        public static ClassDeclarationSyntax GeneratePartialWithMethod(ClassDeclarationSyntax classDeclaration)
        {
            var builderPartial =
                ClassDeclaration(classDeclaration.Identifier)
                .WithModifiers(TokenList(Token(SyntaxKind.PartialKeyword)))
                .WithMembers(
                    SingletonList<MemberDeclarationSyntax>(
                        MethodDeclaration(PredefinedType(Token(SyntaxKind.VoidKeyword)), GeneratedMethodName)
                        .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                        .WithBody(Block())));
            return builderPartial;
        }
    }
}
