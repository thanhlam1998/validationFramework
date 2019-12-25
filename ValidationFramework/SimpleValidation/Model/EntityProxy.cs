using System;
using SimpleValidation.Validation;

namespace SimpleValidation.Model
{
    public abstract class EntityProxy : Entity
    {
        protected ChangeManager GuardManager { get; set; }

        public void SetChangeManager(ChangeManager changeManager)
        {
            GuardManager = changeManager;
        }

        protected abstract object GetAttribute(string attribute);
        protected abstract void SetAttribute(string attribute, object value);

        protected Reporter CheckThenSetAttribute(string attribute, object value)
        {
            if (null == GuardManager) return null;
            Reporter reporter = GuardManager.CheckAllCondition(attribute, value);

            if (reporter.GetResult())
            {
                SetAttribute(attribute, value);
            }
            
            return reporter;
        }


    }

}
