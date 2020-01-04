using System;
using System.Collections.Generic;
using System.Text;
using DKL_Validation.Validators;

namespace DKL_Validation.Resources
{
    public class EnglishLanguage : Language
    {
        public const string Culture = "en";
        public override string Name => Culture;

        public EnglishLanguage()
        {
            Translate<EmailValidator>("'{PropertyName}' is not a valid email address.");
            //Translate<GreaterThanValidator>("'{PropertyName}' must be greater than '{ComparisonValue}'.");
            Translate<MinimumLengthValidator>("The length of '{PropertyName}' must be at least {MinLength} characters. You entered {TotalLength} characters.");
            //Translate<MaximumLengthValidator>("The length of '{PropertyName}' must be {MaxLength} characters or fewer. You entered {TotalLength} characters.");
            //Translate<LessThanValidator>("'{PropertyName}' must be less than '{ComparisonValue}'.");
            Translate<NotEmptyValidator>("'{PropertyName}' must not be empty.");
            //Translate<NotEqualValidator>("'{PropertyName}' must not be equal to '{ComparisonValue}'.");
            Translate<NotNullValidator>("'{PropertyName}' must not be empty.");
            //Translate<RegularExpressionValidator>("'{PropertyName}' is not in the correct format.");
            //Translate<EqualValidator>("'{PropertyName}' must be equal to '{ComparisonValue}'.");
        }
    }
}
