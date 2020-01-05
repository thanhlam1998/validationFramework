using DKL_Validation.Resources;

namespace DKL_Validation.Validators
{
    public class NullValidator : PropertyValidator, INullValidator
    {
        public NullValidator() : base(new LanguageStringSource(nameof(NullValidator)))
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue != null)
            {
                return false;
            }

            return true;
        }

    }

    public interface INullValidator : IPropertyValidator{}
}