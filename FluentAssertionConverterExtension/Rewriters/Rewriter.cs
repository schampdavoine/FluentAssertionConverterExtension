using FluentAssertionConverterExtension.Rewriters.MethodRewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace FluentAssertionConverterExtension.Rewriters
{
    public class Rewriter : CSharpSyntaxRewriter
    {
        private readonly IEnumerable<MethodRewriter> rewriters;

        public Rewriter(IEnumerable<MethodRewriter> rewriters) => this.rewriters = rewriters;

        public override SyntaxNode VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            if (node.Expression is not InvocationExpressionSyntax invocationExpression)
                return null;

            var argumentList = invocationExpression.ChildNodes()
                                .OfType<ArgumentListSyntax>().Single();

            var expressionStatement = node;

            foreach (var rewriter in this.rewriters)
            {
                expressionStatement = rewriter.VisitExpressionStatement(expressionStatement, argumentList);
            }

            return base.VisitExpressionStatement(expressionStatement)
                .WithLeadingTrivia(node.GetLeadingTrivia())
                .WithTrailingTrivia(node.GetTrailingTrivia());
        }
    }
}
