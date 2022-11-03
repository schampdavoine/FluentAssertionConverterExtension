using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeRefactorings;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertionConverterExtension.Analysers;
using FluentAssertionConverterExtension.Converters;
using FluentAssertionConverterExtension.Rewriters;

namespace FluentAssertionConverterExtension
{
    [ExportCodeRefactoringProvider(LanguageNames.CSharp, Name = nameof(FluentAssertionConverterCodeRefactoringProvider)), Shared]
    internal class FluentAssertionConverterCodeRefactoringProvider : CodeRefactoringProvider
    {
        public sealed override async Task ComputeRefactoringsAsync(CodeRefactoringContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
            var node = root.FindNode(context.Span);

            var classAnalyser = AnalyserFactory.CreateClassAnalyser();
            var expressions = classAnalyser.FindNodesToRefactor(node);

            if (expressions.Any())
            {
                var converter = new ExpressionNodesConverter(new RewriterFactory());

                var classAction = CodeAction.Create(
                    "Convert the class to Fluent Assertion",
                    cancellationToken => converter.ConvertAsync(expressions, context.Document, cancellationToken));
                context.RegisterRefactoring(classAction);

                var methodAnalyser = AnalyserFactory.CreateMethodAnalyser();
                var expressionsInMethod = methodAnalyser.FindNodesToRefactor(node);

                var methodAction = CodeAction.Create(
                    "Convert the method to Fluent Assertion",
                    cancellationToken => converter.ConvertAsync(expressionsInMethod, context.Document, cancellationToken));
                context.RegisterRefactoring(methodAction);
            }
        }
    }
}