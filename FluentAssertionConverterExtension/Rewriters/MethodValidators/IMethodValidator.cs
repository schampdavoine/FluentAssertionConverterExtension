using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FluentAssertionConverterExtension.Rewriters.MethodValidators
{
    public interface IMethodValidator
    {
        bool NodeContainsMethod(ExpressionStatementSyntax node, string expectedMethodName);
    }
}