using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationFramework
{
    /// <summary>
    /// Specified how rule should cascaded when one fails
    /// </summary>
    public enum CascadeMode
    {
        Continue,
        StopOnFirstFailure
    }

    public enum ApplyConditionTo
    {
        AllValidators,
        CurrentValidators
    }

    public enum Severity
    {
        Error,
        Warning,
        Info
    }
}
