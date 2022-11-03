using ConvertToFluentAssertionTest.TestFactory;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertionConverterExtension.Analysers;

namespace ConvertToFluentAssertionTest
{
    [TestClass]
    public class AnalyserTest
    {
        [TestMethod]
        public void FindNodesToRefactor_WithNoAncestor_ShouldReturnEmptyArray()
        {
            // Arrange
            var currentNode = SyntaxFactory.MethodDeclaration(SyntaxFactory.IdentifierName("myReturnType"), "myMethodName");

            var analyser = new Analyser<ClassDeclarationSyntax>("TestClass");

            // Act
            var nodes = analyser.FindNodesToRefactor(currentNode);

            // Assert
            nodes.Should().BeEmpty();
        }

        [TestMethod]
        public void FindNodesToRefactor_WithNoAttributeTest_ShouldReturnEmptyArray()
        {
            // Arrange
            var currentNode = SyntaxFactory.MethodDeclaration(
                SyntaxFactory.SingletonList(SyntaxFactory.AttributeList(SyntaxFactory.SeparatedList(new[] { SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("TestMethod")) }))),
                SyntaxFactory.TokenList(),
                SyntaxFactory.IdentifierName("myReturnType"),
                null,
                SyntaxFactory.Identifier("myIdentifier"),
                null,
                SyntaxFactory.ParseParameterList("myParameter"),
                SyntaxFactory.List<TypeParameterConstraintClauseSyntax>(),
                null,
                null);

            var analyser = new Analyser<MethodDeclarationSyntax>("BadTestMethodToFind");

            // Act
            var nodes = analyser.FindNodesToRefactor(currentNode);

            // Assert
            nodes.Should().BeEmpty();
        }

        [TestMethod]
        public void FindNodesToRefactor_WithNoAssertion_ShouldReturnEmptyArray()
        {
            // Arrange
            var currentNode = SyntaxFactory.MethodDeclaration(
                SyntaxFactory.SingletonList(SyntaxFactory.AttributeList(SyntaxFactory.SeparatedList(new[] { SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("TestMethod")) }))),
                SyntaxFactory.TokenList(),
                SyntaxFactory.IdentifierName("myReturnType"),
                null,
                SyntaxFactory.Identifier("myIdentifier"),
                null,
                SyntaxFactory.ParseParameterList("myParameter"),
                SyntaxFactory.List<TypeParameterConstraintClauseSyntax>(),
                SyntaxFactory.Block(SyntaxFactory.SeparatedList(new[] { SyntaxFactory.EmptyStatement() })),
                null);

            var analyser = new Analyser<MethodDeclarationSyntax>("TestMethod");

            // Act
            var nodes = analyser.FindNodesToRefactor(currentNode);

            // Assert
            nodes.Should().BeEmpty();
        }

        [TestMethod]
        public void FindNodesToRefactor_WithAssertions_ShouldReturnArrayFulfilled()
        {
            // Arrange
            var blockSyntax = SyntaxFactory.Block(SyntaxFactory.SeparatedList(new StatementSyntax[] {
                SyntaxFactory.EmptyStatement(),
                TestExpressionFactory.CreateIsTrueAssertionExpression()
            }));

            var currentNode = SyntaxFactory.MethodDeclaration(
                SyntaxFactory.SingletonList(SyntaxFactory.AttributeList(SyntaxFactory.SeparatedList(new[] { SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("TestMethod")) }))),
                SyntaxFactory.TokenList(),
                SyntaxFactory.IdentifierName("myReturnType"),
                null,
                SyntaxFactory.Identifier("myIdentifier"),
                null,
                SyntaxFactory.ParseParameterList("myParameter"),
                SyntaxFactory.List<TypeParameterConstraintClauseSyntax>(),
                blockSyntax,
                null);

            var analyser = new Analyser<MethodDeclarationSyntax>("TestMethod");

            // Act
            var nodes = analyser.FindNodesToRefactor(currentNode);

            // Assert
            nodes.Should().HaveCount(1);
        }
    }
}