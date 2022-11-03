using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;

namespace ConvertToFluentAssertionTest.TestFactory
{
    public static class TestExpressionFactory
    {
        public static ExpressionStatementSyntax CreateIsTrueAssertionExpression() => CreateAssertionExpression("IsTrue");

        public static ExpressionStatementSyntax CreateAssertionExpression(string methodName) => CreateAssertionExpression(
            methodName,
            new[] { SyntaxFactory.Argument(SyntaxFactory.IdentifierName("actual")) });

        public static ExpressionStatementSyntax CreateComplexAssertionExpression(string methodName) => CreateAssertionExpression(
            methodName,
            new[]
            {
                SyntaxFactory.Argument(SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression)),
                SyntaxFactory.Argument(SyntaxFactory.IdentifierName("actual"))
            });
        public static ExpressionStatementSyntax CreateGenericAssertionExpression<T>(string methodName) => CreateAssertionExpression(
            methodName,
            new[]
            {
                SyntaxFactory.Argument(SyntaxFactory.IdentifierName("actual")),
                SyntaxFactory.Argument(SyntaxFactory.TypeOfExpression(SyntaxFactory.ParseTypeName("string"))),
            });

        public static ExpressionStatementSyntax CreateAssertionExpression(string methodName, IEnumerable<ArgumentSyntax> arguments) => SyntaxFactory.ExpressionStatement(
                SyntaxFactory.InvocationExpression(
                    SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Assert"),
                        SyntaxFactory.IdentifierName(methodName)),
                    SyntaxFactory.ArgumentList(SyntaxFactory.SeparatedList(arguments))));
    }
}
