using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace FluentAssertionConverterExtension.Rewriters.MethodValidators
{
    public class MethodNameValidator : IMethodValidator
    {
        private string cachedMethodName;
        private ExpressionStatementSyntax cachedNode = null;

        public bool NodeContainsMethod(ExpressionStatementSyntax node, string expectedMethodName)
        {
            if (cachedNode != node)
            {
                if (node.Expression is not InvocationExpressionSyntax invocationExpression) return false;
                if (invocationExpression.Expression is not MemberAccessExpressionSyntax memberAccessExpression) return false;

                cachedMethodName = memberAccessExpression.Name.Identifier.ValueText;
                cachedNode = node;
            }

            return cachedMethodName == expectedMethodName;
        }
    }
}