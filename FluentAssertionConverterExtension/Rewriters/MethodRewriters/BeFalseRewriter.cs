using FluentAssertionConverterExtension.Rewriters.MethodValidators;

namespace FluentAssertionConverterExtension.Rewriters.MethodRewriters
{

    public class BeFalseRewriter : SimpleMethodRewriter
    {
        public BeFalseRewriter(IMethodValidator methodNameValidator) : base(methodNameValidator) { }

        protected override string NewMethod => "BeFalse";

        protected override string OldMethod => "IsFalse";
    }
}