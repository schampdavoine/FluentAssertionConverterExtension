using FluentAssertionConverterExtension.Rewriters.MethodValidators;

namespace FluentAssertionConverterExtension.Rewriters.MethodRewriters
{
    public class BeSameAsRewriter : ComplexMethodRewriter
    {
        public BeSameAsRewriter(IMethodValidator methodNameValidator) : base(methodNameValidator) { }

        protected override string NewMethod => "BeSameAs";

        protected override string OldMethod => "AreSame";
    }
}