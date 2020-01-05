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
            Translate<NotEmptyValidator>("'{PropertyName}' must not be empty.");
            Translate<NotNullValidator>("'{PropertyName}' must not be empty.");
            Translate<NullValidator>("'{PropertyName} must be null'");
            Translate<EqualValidator>("'{PropertyName}' must be equal to '{ComparisonValue}'.");
            Translate<NotEqualValidator>("'{PropertyName}' must not be equal to '{ComparisonValue}'.");
        }
    }
}
