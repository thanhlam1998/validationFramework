using System;
using System.Collections.Generic;
using System.Text;
using ValidationFramework.Results;

namespace ValidationFramework
{

    public interface IValidator<in T> : IValidator
    {
        /// <summary>
        /// Validate the specific intstance
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        ValidationResult Validate(T instance);

        CascadeMode CascadeMode { get; set; }
    }

    public interface IValidator
    {
        ValidationResult Validate(object instance);
        ValidationResult Validate(ValidationContext context);

        bool CanValidateInstancesOfType(Type type);

    }
}
