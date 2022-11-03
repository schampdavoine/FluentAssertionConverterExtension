using FluentAssertionConverterExtension.Rewriters.MethodValidators;

namespace FluentAssertionConverterExtension.Rewriters.MethodRewriters
{
    public class BeNullRewriter : SimpleMethodRewriter
    {
        public BeNullRewriter(IMethodValidator methodNameValidator) : base(methodNameValidator) { }

        protected override string NewMethod => "BeNull";

        protected override string OldMethod => "IsNull";
    }
}