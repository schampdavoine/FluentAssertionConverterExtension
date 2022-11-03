using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FluentAssertionConverterExtension.Converters
{
    public interface IConverter
    {
        Task<Document> ConvertAsync(IEnumerable<ExpressionStatementSyntax> expressions, Document document, CancellationToken cancellationToken);
    }
}