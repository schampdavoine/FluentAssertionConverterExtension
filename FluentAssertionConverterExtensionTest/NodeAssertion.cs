using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace FluentAssertionConverterExtensionTest
{
    public static class NodeAssertion
    {
        public static void FluentNodeAssertion(ExpressionStatementSyntax fluentNode, Action<InvocationExpressionSyntax, MemberAccessExpressionSyntax>? assertions = null)
        {
            fluentNode.Should().NotBeNull();
            fluentNode.Expression.Should().NotBeNull();
            fluentNode.Expression.Should().BeAssignableTo<InvocationExpressionSyntax>();

            var invocationFluent = fluentNode.Expression as InvocationExpressionSyntax;
            invocationFluent.Should().NotBeNull();
            invocationFluent.Expression.Should().NotBeNull();
            invocationFluent.Expression.Should().BeAssignableTo<MemberAccessExpressionSyntax>();

            var memberAccessFluent = invocationFluent.Expression as MemberAccessExpressionSyntax;
            memberAccessFluent.Should().NotBeNull();
            memberAccessFluent.Expression.Should().NotBeNull();
            memberAccessFluent.Expression.Should().BeAssignableTo<InvocationExpressionSyntax>();
            memberAccessFluent.Name.Identifier.Text.Should().Be("FluentMethod");

            var invocationShould = memberAccessFluent.Expression as InvocationExpressionSyntax;
            invocationShould.Should().NotBeNull();
            invocationShould.Expression.Should().BeAssignableTo<MemberAccessExpressionSyntax>();

            var memberShould = invocationShould.Expression as MemberAccessExpressionSyntax;
            memberShould.Should().NotBeNull();
            memberShould.Expression.Should().NotBeNull();
            memberShould.Name.Identifier.Text.Should().Be("Should");

            var identifierName = memberShould.Expression as IdentifierNameSyntax;
            identifierName.Should().NotBeNull();
            identifierName.Identifier.Text.Should().Be("actual");

            if (assertions != null)
                assertions(invocationFluent, memberAccessFluent);
        }
    }
}
