using FluentAssertionConverterExtension.Rewriters.MethodValidators;

namespace FluentAssertionConverterExtension.Rewriters.MethodRewriters
{
    public class BeAssignableToRewriter : GenericMethodRewriter
    {
        public BeAssignableToRewriter(IMethodValidator methodNameValidator) : base(methodNameValidator) { }

        protected override string NewMethod => "BeAssignableTo";

        protected override string OldMethod => "IsInstanceOfType";
    }
}