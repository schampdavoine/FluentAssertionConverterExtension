using FluentAssertionConverterExtension.Rewriters.MethodValidators;

namespace FluentAssertionConverterExtension.Rewriters.MethodRewriters
{
    public class NotBeSameAsRewriter : ComplexMethodRewriter
    {
        public NotBeSameAsRewriter(IMethodValidator methodNameValidator) : base(methodNameValidator) { }

        protected override string NewMethod => "NotBeSameAs";

        protected override string OldMethod => "AreNotSame";
    }
}