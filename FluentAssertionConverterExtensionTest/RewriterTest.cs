using ConvertToFluentAssertionTest.TestFactory;
using FluentAssertionConverterExtension.Rewriters;
using FluentAssertionConverterExtension.Rewriters.MethodRewriters;
using FluentAssertionConverterExtensionTest.Stubs;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FluentAssertionConverterExtensionTest
{
    [TestClass]
    public class RewriterTest
    {
        [TestMethod]
        public void VisitExpressionStatement_WithNotInvocationNode_ShouldDoNothing()
        {
            var rewriter = new Rewriter(null);

            var node = rewriter.VisitExpressionStatement(SyntaxFactory.ExpressionStatement(SyntaxFactory.DefaultExpression(SyntaxFactory.ParseTypeName(nameof(RewriterTest)))));
            
            node.Should().BeNull();
        }

        [TestMethod]
        public void VisitExpressionStatement_WithNoRewriter_ShouldReturnSameNode()
        {
            var rewriter = new Rewriter(Enumerable.Empty<MethodRewriter>());

            var oldNode = TestExpressionFactory.CreateAssertionExpression("OldMethod");

            var node = rewriter.VisitExpressionStatement(oldNode);

            node.ToFullString().Should().Be(oldNode.ToFullString());
        }

        [TestMethod]
        public void VisitExpressionStatement_WithNullAssert_ShouldReturnNullAssertInFluentAssertion()
        {
            // Arrange
            var rewriter = new Rewriter(new List<MethodRewriter>
            {
                new SimpleMethodRewriterStub()
            });

            var oldNode = TestExpressionFactory.CreateAssertionExpression("OldMethod");

            // Act
            var fluentNode = rewriter.VisitExpressionStatement(oldNode);

            // Assert
            fluentNode.ToFullString().Should().Be("actual.Should().FluentMethod();");
        }
    }
}
