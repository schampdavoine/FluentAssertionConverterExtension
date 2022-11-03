using FluentAssertionConverterExtension.Rewriters.MethodValidators;

namespace FluentAssertionConverterExtension.Rewriters.MethodRewriters
{
    public class BeTrueRewriter : SimpleMethodRewriter
    {
        public BeTrueRewriter(IMethodValidator methodNameValidator) : base(methodNameValidator) { }

        protected override string NewMethod => "BeTrue";

        protected override string OldMethod => "IsTrue";
    }
}
