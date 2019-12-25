using System;

namespace SimpleValidation.Validation.Validator
{
    public abstract class Guard
    {
        private string id;
        private string warningMessage;

        public Guard(string id)
        {
            this.id = id;
            this.warningMessage = "Invalid value.";
        }

        internal string Check(string id, object value)
        {
            if (!this.id.Equals(id))
            {
                return null;
            }

            if (Validate(value))
            {
                return null;
            }
            else
            {
                return warningMessage;
            }
        }

        protected abstract bool Validate(object value);

        public Guard SetWarningMessage(string message)
        {
            this.warningMessage = message;
            return this;
        }
    }

}
