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
    public class GenericMethodRewriterTest
    {
        [TestMethod]
        public void Visit_ShouldReturnFluentNode()
        {
            // Arrange
            var assert = TestExpressionFactory.CreateGenericAssertionExpression<string>("OldMethod");

            var invocationExpression = assert.Expression as InvocationExpressionSyntax;
            var argumentList = invocationExpression.ChildNodes()
                                .OfType<ArgumentListSyntax>().Single();

            // Act
            var fluentNode = new GenericMethodRewriterStub().VisitExpressionStatement(assert, argumentList);

            // Assert


            Action<InvocationExpressionSyntax, MemberAccessExpressionSyntax> assertions = (invocationExpressionFluent, memberAccessFluent) =>
            {
                var genericName = memberAccessFluent.Name as GenericNameSyntax;
                genericName.Should().NotBeNull();
                genericName.TypeArgumentList.Arguments.Should().HaveCount(1);
                genericName.TypeArgumentList.Arguments[0].ToFullString().Should().Be("string");
            };

            NodeAssertion.FluentNodeAssertion(fluentNode, assertions);

        }
    }
}
