using FluentAssertionConverterExtension.Rewriters.MethodValidators;

namespace FluentAssertionConverterExtension.Rewriters.MethodRewriters
{
    public class NotBeRewriter : ComplexMethodRewriter
    {
        public NotBeRewriter(IMethodValidator methodNameValidator) : base(methodNameValidator) { }

        protected override string NewMethod => "NotBe";

        protected override string OldMethod => "AreNotEqual";
    }
}