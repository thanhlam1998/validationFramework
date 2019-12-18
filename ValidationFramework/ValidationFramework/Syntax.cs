using System;
using System.Collections.Generic;
using System.Text;
using ValidationFramework.Internal;

namespace ValidationFramework
{
    public interface IRuleBuilderInitial<T, out TProperty>: IRuleBuilder<T, TProperty>, IConfigurable<PropertyRule, IRuleBuilderInitial<T,TProperty>>
    {

    }

    public interface IRuleBuilder<T, out TProperty>
    {
        IRuleBuilderOptions<T, TProperty> SetValidator(IPropertyValidator validator);

        IRuleBuilderOptions<T, TProperty> SetValidator(IValidator<TProperty> validator, params string[] ruleSets);
    }

    public interface IRuleBuilderOptions<T,out TProperty>: IRuleBuilder<T, TProperty>, IConfigurable<PropertyRule, IRuleBuilderOptions<T, TProperty>>
    {

    }
}
