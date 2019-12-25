using SimpleValidation.Validation.Validator;
using System.Collections.Generic;

namespace SimpleValidation.Validation
{
    public abstract class ChangeManager
    {
        List<Guard> guards;

        public ChangeManager()
        {
            guards = new List<Guard>();
        }

        public bool AddCondition(Guard guard)
        {
            guards.Add(guard);
            return true;
        }

        public Reporter CheckAllCondition(string id, object value)
        {
            Reporter reporter = new Reporter();
            foreach (var guard in guards)
            {
                reporter.AddWarningMessage(guard.Check(id, value));
            }
            return reporter;
        }

    }

}
