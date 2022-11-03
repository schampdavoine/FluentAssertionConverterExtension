using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FluentAssertionConverterExtension.Analysers
{

    public static class AnalyserFactory
    {
        private const string TESTCLASS_ATTRIBUTENAME = "TestClass";
        private const string TESTMETHOD_ATTRIBUTENAME = "TestMethod";

        public static Analyser<MethodDeclarationSyntax> CreateMethodAnalyser() => new Analyser<MethodDeclarationSyntax>(TESTMETHOD_ATTRIBUTENAME);

        public static Analyser<ClassDeclarationSyntax> CreateClassAnalyser() => new Analyser<ClassDeclarationSyntax>(TESTCLASS_ATTRIBUTENAME);
    }
}
