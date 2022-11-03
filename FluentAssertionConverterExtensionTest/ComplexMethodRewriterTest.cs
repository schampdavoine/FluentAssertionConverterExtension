using ConvertToFluentAssertionTest.TestFactory;
using FluentAssertionConverterExtensionTest.Stubs;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FluentAssertionConverterExtensionTest
{
    [TestClass]
    public class ComplexMethodRewriterTest
    {
        [TestMethod]
        public void Visit_ShouldReturnFluentNode()
        {
            // Arrange
            var assert = TestExpressionFactory.CreateComplexAssertionExpression("OldMethod");

            var invocationExpression = assert.Expression as InvocationExpressionSyntax;
            var argumentList = invocationExpression.ChildNodes()
                                .OfType<ArgumentListSyntax>().Single();

            // Act
            var fluentNode = new ComplexMethodRewriterStub().VisitExpressionStatement(assert, argumentList);

            // Assert

            Action<InvocationExpressionSyntax, MemberAccessExpressionSyntax> assertions = (invocationExpressionFluent, memberAccessFluent) =>
            {
                invocationExpressionFluent.ArgumentList.Arguments.Should().HaveCount(1);
                invocationExpressionFluent.ArgumentList.Arguments[0].Expression.Should().NotBeNull();
                invocationExpressionFluent.ArgumentList.Arguments[0].Expression.Should().BeAssignableTo<LiteralExpressionSyntax>();
                var literal = invocationExpressionFluent.ArgumentList.Arguments[0].Expression as LiteralExpressionSyntax;
                literal.Token.Text.Should().Be("true");
            };

            NodeAssertion.FluentNodeAssertion(fluentNode, assertions);

        }
    }
}
