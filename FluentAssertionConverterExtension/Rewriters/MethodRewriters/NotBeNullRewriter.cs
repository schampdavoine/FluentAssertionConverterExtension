using FluentAssertionConverterExtension.Rewriters.MethodValidators;

namespace FluentAssertionConverterExtension.Rewriters.MethodRewriters
{
    public class NotBeNullRewriter : SimpleMethodRewriter
    {
        public NotBeNullRewriter(IMethodValidator methodNameValidator) : base(methodNameValidator) { }

        protected override string NewMethod => "NotBeNull";

        protected override string OldMethod => "IsNotNull";
    }
}