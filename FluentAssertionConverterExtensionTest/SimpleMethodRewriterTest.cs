using ConvertToFluentAssertionTest.TestFactory;
using FluentAssertionConverterExtensionTest.Stubs;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FluentAssertionConverterExtensionTest
{
    [TestClass]
    public class SimpleMethodRewriterTest
    {
        [TestMethod]
        public void Visit_ShouldReturnFluentNode()
        {
            // Arrange
            var assert = TestExpressionFactory.CreateAssertionExpression("OldMethod");

            var invocationExpression = assert.Expression as InvocationExpressionSyntax;
            var argumentList = invocationExpression.ChildNodes()
                                .OfType<ArgumentListSyntax>().Single();

            // Act
            var fluentNode = new SimpleMethodRewriterStub().VisitExpressionStatement(assert, argumentList);

            // Assert
            NodeAssertion.FluentNodeAssertion(fluentNode);
        }
    }
}
