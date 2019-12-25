using System;

namespace SimpleValidation.Validation.Validator
{
    public class NotEmptyGuard : Guard
    {
        public NotEmptyGuard(string id) : base(id)
        {
        }

        protected override bool Validate(object value)
        {
            if (null != value && !value.ToString().Equals(""))
            {
                return true;
            }
            return false;
        }

    }
}
