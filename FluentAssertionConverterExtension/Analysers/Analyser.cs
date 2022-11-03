using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace FluentAssertionConverterExtension.Analysers
{
    public class Analyser<T> where T : MemberDeclarationSyntax
    {
        private const string ASSERT_TOKEN = "Assert";
        private readonly string attributeTest;

        public Analyser(string attributeTest)
        {
            this.attributeTest = attributeTest;
        }

        public IEnumerable<ExpressionStatementSyntax> FindNodesToRefactor(SyntaxNode node)
        {
            var memberDeclaration = node.FirstAncestorOrSelf<T>();
            if (memberDeclaration == null)
                return Enumerable.Empty<ExpressionStatementSyntax>();

            if (HasAttributeTest(memberDeclaration.AttributeLists, attributeTest))
            {
                return memberDeclaration
                    .DescendantNodes()
                    .OfType<ExpressionStatementSyntax>()
                    .Where(node => this.IsAssertion(node));
            }

            return Enumerable.Empty<ExpressionStatementSyntax>();
        }

        private bool IsAssertion(ExpressionStatementSyntax expressionStatement)
        {
            if (expressionStatement.Expression is not InvocationExpressionSyntax invocationExpression)
                return false;

            if (invocationExpression.Expression is not MemberAccessExpressionSyntax memberAccessExpression)
                return false;

            if (memberAccessExpression.Expression.GetFirstToken().ValueText == ASSERT_TOKEN)
                return true;

            return false;
        }

        private bool HasAttributeTest(SyntaxList<AttributeListSyntax> attributeLists, string attributeSearched)
        {
            return attributeLists
                .Any(attributeList =>
                    attributeList.Attributes
                        .Any(attribute => attribute.Name.ToFullString() == attributeSearched));
        }
    }
}