using ConvertToFluentAssertionTest.TestFactory;
using FluentAssertionConverterExtension.Rewriters.MethodValidators;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAssertionConverterExtensionTest
{
    [TestClass]
    public class MethodNameValidatorTest
    {
        [TestMethod]
        public void NodeContainsMethod_WithNoInvocationExpression_ShouldReturnFalse()
        {
            // Arrange
            var node = SyntaxFactory.ExpressionStatement(
                SyntaxFactory.DefaultExpression(SyntaxFactory.ParseTypeName("string")));
            var validator = new MethodNameValidator();

            // Act
            var isContains = validator.NodeContainsMethod(node, "name");

            // Assert
            isContains.Should().BeFalse();
        }

        [TestMethod]
        public void NodeContainsMethod_WithNoMemberAccessExpression_ShouldReturnFalse()
        {
            // Arrange
            var node = SyntaxFactory.ExpressionStatement(
                SyntaxFactory.InvocationExpression(
                    SyntaxFactory.DefaultExpression(SyntaxFactory.ParseTypeName("string"))));

            var validator = new MethodNameValidator();

            // Act
            var isContains = validator.NodeContainsMethod(node, "name");

            // Assert
            isContains.Should().BeFalse();
        }

        [TestMethod]
        public void NodeContainsMethod_WithNotSameMethodNameExpected_ShouldReturnFalse()
        {
            // Arrange
            var node = TestExpressionFactory.CreateAssertionExpression("methodExpected");

            var validator = new MethodNameValidator();

            // Act
            var isContains = validator.NodeContainsMethod(node, "notGoodMethod");

            // Assert
            isContains.Should().BeFalse();
        }

        [TestMethod]
        public void NodeContainsMethod_WithSameMethodNameExpected_ShouldReturnTrue()
        {
            // Arrange
            var node = TestExpressionFactory.CreateAssertionExpression("methodExpected");

            var validator = new MethodNameValidator();

            // Act
            var isContains = validator.NodeContainsMethod(node, "methodExpected");

            // Assert
            isContains.Should().BeTrue();
        }
    }
}
