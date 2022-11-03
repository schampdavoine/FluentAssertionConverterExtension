using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using FluentAssertionConverterExtension.Rewriters.MethodValidators;

namespace FluentAssertionConverterExtension.Rewriters.MethodRewriters
{
    public abstract class GenericMethodRewriter : MethodRewriter
    {
        public GenericMethodRewriter(IMethodValidator methodNameValidator) : base(methodNameValidator) { }

        protected override ExpressionStatementSyntax Visit(ExpressionStatementSyntax node, ArgumentListSyntax arguments)
        {
            var shouldInvocationMethod = SyntaxFactoryExtension.CreateShouldInvocation(arguments.Arguments.FirstOrDefault());

            var lastArgument = arguments.Arguments.LastOrDefault();

            if (lastArgument != null && lastArgument.Expression is TypeOfExpressionSyntax typeOfExpression)
            {
                var genericName = SyntaxFactory.GenericName(
                    SyntaxFactory.ParseToken(NewMethod),
                    SyntaxFactory.TypeArgumentList(SyntaxFactory.SeparatedList(new List<TypeSyntax> { typeOfExpression.Type })));

                var memberAccess = SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    shouldInvocationMethod,
                    SyntaxFactory.Token(SyntaxKind.DotToken),
                    genericName);

                var invocationMethod = SyntaxFactory.InvocationExpression(
                    memberAccess,
                    SyntaxFactory.ArgumentList());

                return SyntaxFactory.ExpressionStatement(invocationMethod);
            }

            return node;
        }
    }
}