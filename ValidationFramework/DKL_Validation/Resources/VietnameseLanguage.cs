using System;
using System.Collections.Generic;
using System.Text;
using DKL_Validation.Validators;

namespace DKL_Validation.Resources
{
    class VietnameseLanguage : Language
    {
        public const string Culture = "vi";
        public override string Name => Culture;
        public VietnameseLanguage()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Translate<NotEmptyValidator>("'{PropertyName}' không được để trống.");
            Translate<NotNullValidator>("'{PropertyName}' không được để null.");
            Translate<EmailValidator>("'{PropertyName}' không phải là email đúng.");
            Translate<EqualValidator>("'{PropertyName}' phải bằng với '{ComparisonValue}'.");
            Translate<NotEqualValidator>("'{PropertyName}' không được bằng với '{ComparisonValue}'.");
        }
    }
}
