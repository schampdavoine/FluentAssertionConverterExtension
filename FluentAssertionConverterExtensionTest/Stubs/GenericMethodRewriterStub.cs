using FluentAssertionConverterExtension.Rewriters.MethodRewriters;
using FluentAssertionConverterExtension.Rewriters.MethodValidators;

namespace FluentAssertionConverterExtensionTest.Stubs
{
    public class GenericMethodRewriterStub : GenericMethodRewriter
    {
        public GenericMethodRewriterStub() : base(new MethodNameValidator()) { }

        protected override string NewMethod => "FluentMethod";

        protected override string OldMethod => "OldMethod";
    }
}
