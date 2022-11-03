using Microsoft.CodeAnalysis.CSharp.Syntax;
using FluentAssertionConverterExtension.Rewriters.MethodValidators;

namespace FluentAssertionConverterExtension.Rewriters.MethodRewriters
{
    public abstract class MethodRewriter
    {
        protected readonly IMethodValidator methodValidator;

        protected abstract string NewMethod { get; }
        protected abstract string OldMethod { get; }

        public MethodRewriter(IMethodValidator methodValidator)
        {
            this.methodValidator = methodValidator;
        }
        public ExpressionStatementSyntax VisitExpressionStatement(ExpressionStatementSyntax node, ArgumentListSyntax arguments)
        {
            if (!methodValidator.NodeContainsMethod(node, OldMethod))
                return node;
            else
                return Visit(node, arguments);
        }

        protected abstract ExpressionStatementSyntax Visit(ExpressionStatementSyntax node, ArgumentListSyntax arguments);
    }
}