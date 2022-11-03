using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FluentAssertionConverterExtension.Rewriters
{
    public static class SyntaxFactoryExtension
    {
        public static InvocationExpressionSyntax CreateShouldInvocation(ArgumentSyntax argument)
        {
            var variableMemberAccess = SyntaxFactory.MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                argument.Expression,
                SyntaxFactory.Token(SyntaxKind.DotToken),
                SyntaxFactory.IdentifierName("Should")
                );

            return SyntaxFactory.InvocationExpression(
                variableMemberAccess,
                SyntaxFactory.ArgumentList());
        }
    }
}
