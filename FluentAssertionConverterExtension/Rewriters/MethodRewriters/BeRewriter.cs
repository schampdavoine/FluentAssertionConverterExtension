using FluentAssertionConverterExtension.Rewriters.MethodValidators;

namespace FluentAssertionConverterExtension.Rewriters.MethodRewriters
{
    public class BeRewriter : ComplexMethodRewriter
    {
        public BeRewriter(IMethodValidator methodNameValidator) : base(methodNameValidator) { }

        protected override string NewMethod => "Be";

        protected override string OldMethod => "AreEqual";
    }
}