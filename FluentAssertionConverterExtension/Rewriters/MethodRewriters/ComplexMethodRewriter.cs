using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using FluentAssertionConverterExtension.Rewriters.MethodValidators;

namespace FluentAssertionConverterExtension.Rewriters.MethodRewriters
{
    public abstract class ComplexMethodRewriter : MethodRewriter
    {
        public ComplexMethodRewriter(IMethodValidator methodNameValidator) : base(methodNameValidator) { }

        protected override ExpressionStatementSyntax Visit(ExpressionStatementSyntax node, ArgumentListSyntax arguments)
        {
            var shouldInvocationMethod = SyntaxFactoryExtension.CreateShouldInvocation(arguments.Arguments[1]);

            var memberAccess = SyntaxFactory.MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                shouldInvocationMethod,
                SyntaxFactory.Token(SyntaxKind.DotToken),
                SyntaxFactory.IdentifierName(NewMethod));

            var separatedList = SyntaxFactory.SeparatedList(new List<ArgumentSyntax> { arguments.Arguments.FirstOrDefault() });

            var invocationMethod = SyntaxFactory.InvocationExpression(
                memberAccess,
                SyntaxFactory.ArgumentList(
                    separatedList));

            return SyntaxFactory.ExpressionStatement(invocationMethod);
        }
    }
}