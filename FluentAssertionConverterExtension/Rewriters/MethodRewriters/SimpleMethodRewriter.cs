using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using FluentAssertionConverterExtension.Rewriters.MethodValidators;

namespace FluentAssertionConverterExtension.Rewriters.MethodRewriters
{
    public abstract class SimpleMethodRewriter : MethodRewriter
    {
        public SimpleMethodRewriter(IMethodValidator methodNameValidator) : base(methodNameValidator) { }

        protected override ExpressionStatementSyntax Visit(ExpressionStatementSyntax node, ArgumentListSyntax arguments)
        {
            var shouldInvocationMethod = SyntaxFactoryExtension.CreateShouldInvocation(arguments.Arguments.FirstOrDefault());

            var memberAccess = SyntaxFactory.MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                shouldInvocationMethod,
                SyntaxFactory.Token(SyntaxKind.DotToken),
                SyntaxFactory.IdentifierName(NewMethod));

            var invocationMethod = SyntaxFactory.InvocationExpression(
                memberAccess,
                SyntaxFactory.ArgumentList());

            return SyntaxFactory.ExpressionStatement(invocationMethod);
        }
    }
}
