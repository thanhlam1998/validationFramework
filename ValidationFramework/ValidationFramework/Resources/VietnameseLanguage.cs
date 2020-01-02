using System;
using System.Collections.Generic;
using System.Text;
using ValidationFramework.Validators;

namespace ValidationFramework.Resources
{
    class VietnameseLanguage : Language
    {
        public const string Culture = "vi";
        public override string Name => Culture;
        public VietnameseLanguage()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Translate<NotEmptyValidator>("'{PropertyName}' không được để kí tự trống.");
            Translate<NotNullValidator>("'{PropertyName}' không được bỏ trống.");
        }
    }
}
