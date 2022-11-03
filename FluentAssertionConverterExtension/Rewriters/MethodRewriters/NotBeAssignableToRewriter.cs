using FluentAssertionConverterExtension.Rewriters.MethodValidators;

namespace FluentAssertionConverterExtension.Rewriters.MethodRewriters
{
    public class NotBeAssignableToRewriter : GenericMethodRewriter
    {
        public NotBeAssignableToRewriter(IMethodValidator methodNameValidator) : base(methodNameValidator) { }

        protected override string NewMethod => "NotBeAssignableTo";

        protected override string OldMethod => "IsNotInstanceOfType";
    }
}