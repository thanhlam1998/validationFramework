using System;
using System.Collections.Generic;
using System.Text;
using DKL_Validation.Internal;

namespace DKL_Validation
{
    public interface IRuleBuilderInitial<T, out TProperty>: IRuleBuilder<T, TProperty>, IConfigurable<PropertyRule, IRuleBuilderInitial<T,TProperty>>
    {

    }

    public interface IRuleBuilder<T, out TProperty>
    {
        IRuleBuilderOptions<T, TProperty> SetValidator(IPropertyValidator validator);
    }

    public interface IRuleBuilderOptions<T,out TProperty>: IRuleBuilder<T, TProperty>, IConfigurable<PropertyRule, IRuleBuilderOptions<T, TProperty>>
    {

    }
}
