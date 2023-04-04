using System.Diagnostics;
using Ark.StronglyTypedIds;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Ark.StronglyTypedId.SourceGenerator;

[Generator]
public class StronglyTypedIdGenerator : ISourceGenerator
{
    // Adjust the namespace for your project
    private static readonly string StronglyTypedIdMetadataName = typeof(StronglyTypedId<>).FullName!;

    #region ISourceGenerator Members

    public void Initialize(GeneratorInitializationContext context)
    {
        // Uncomment the next line to debug the source generator
        Debugger.Launch();
        context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        if (context.SyntaxReceiver is not SyntaxReceiver receiver)
            return;

        var stronglyTypedIdBaseType = context.Compilation.GetTypeByMetadataName(StronglyTypedIdMetadataName)
                                      ?? throw new InvalidOperationException(
                                          $"Type '{StronglyTypedIdMetadataName}' could not be found");

        foreach (var declaration in receiver.CandidateDeclarations)
        {
            var model = context.Compilation.GetSemanticModel(declaration.SyntaxTree);
            var type = ModelExtensions.GetDeclaredSymbol(model, declaration);
            if (type is null)
                continue;

            if (!IsStronglyTypedId((INamedTypeSymbol)type))
                continue;

            var fullNamespace = type.ContainingNamespace.ToDisplayString();
            var typeName = type.Name;

            var source = $@"namespace {fullNamespace}
{{
    partial record {typeName}
    {{
        public override string ToString() => Value.ToString();
    }}
}}";
            context.AddSource($"{typeName}.Generated", source);
        }

        bool IsStronglyTypedId(INamedTypeSymbol type)
        {
            return type.BaseType!.IsGenericType
                   && !type.BaseType.IsUnboundGenericType
                   && SymbolEqualityComparer.Default.Equals(type.BaseType.ConstructedFrom, stronglyTypedIdBaseType);
        }
    }

    #endregion

    #region Nested type: SyntaxReceiver

    private class SyntaxReceiver : ISyntaxReceiver
    {
        public List<RecordDeclarationSyntax> CandidateDeclarations { get; } = new();

        #region ISyntaxReceiver Members

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is RecordDeclarationSyntax declaration)
            {
                // Must declare a base type (we don't care which at this point)
                if (declaration.BaseList is null || !declaration.BaseList.Types.Any())
                    return;

                // Must be partial (otherwise we can't add new members)
                if (!declaration.Modifiers.Any(SyntaxKind.PartialKeyword))
                    return;

                // Must have a single parameter, either in the parameter list or in an explicit constructor
                if (declaration.ParameterList is null)
                {
                    var ctors = declaration.Members.OfType<ConstructorDeclarationSyntax>().ToList();
                    // We need at least one constructor with one parameter
                    if (!ctors.Any(c => c.ParameterList.Parameters.Count == 1))
                        return;
                    // And there can't be a constructor with more than one parameter
                    if (ctors.Any(c => c.ParameterList.Parameters.Count > 1))
                        return;
                }
                else if (declaration.ParameterList.Parameters.Count != 1) return;

                CandidateDeclarations.Add(declaration);
            }
        }

        #endregion
    }

    #endregion
}