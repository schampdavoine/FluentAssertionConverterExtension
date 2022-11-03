using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertionConverterExtension.Rewriters;

namespace FluentAssertionConverterExtension.Converters
{
    public class ExpressionNodesConverter : IConverter
    {
        private readonly Rewriter rewriter;

        public ExpressionNodesConverter(RewriterFactory rewriterFactory)
        {
            this.rewriter = rewriterFactory.CreateRewriter();
        }

        public async Task<Document> ConvertAsync(IEnumerable<ExpressionStatementSyntax> expressions, Document document, CancellationToken cancellationToken)
        {
            var editor = await DocumentEditor.CreateAsync(document, cancellationToken);

            var nodes = expressions.Select(expression => rewriter.Visit(expression));

            for (var i = 0; i < expressions.Count(); i++)
            {
                if (cancellationToken.IsCancellationRequested) return editor.OriginalDocument;

                editor.ReplaceNode(expressions.ElementAt(i), nodes.ElementAt(i));
            }

            return editor.GetChangedDocument();
        }
    }
}
