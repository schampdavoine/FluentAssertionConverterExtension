using FluentAssertionConverterExtension.Rewriters.MethodRewriters;
using FluentAssertionConverterExtension.Rewriters.MethodValidators;

namespace FluentAssertionConverterExtension.Rewriters
{
    public class RewriterFactory
    {
        public Rewriter CreateRewriter()
        {
            var methodNameValidator = new MethodNameValidator();

            return new Rewriter(new MethodRewriter[]
            {
                new BeTrueRewriter(methodNameValidator),
                new BeFalseRewriter(methodNameValidator),
                new BeNullRewriter(methodNameValidator),
                new NotBeNullRewriter(methodNameValidator),
                new BeRewriter(methodNameValidator),
                new NotBeRewriter(methodNameValidator),
                new BeSameAsRewriter(methodNameValidator),
                new NotBeSameAsRewriter(methodNameValidator),
                new BeAssignableToRewriter(methodNameValidator),
                new NotBeAssignableToRewriter(methodNameValidator),
            });
        }
    }
}