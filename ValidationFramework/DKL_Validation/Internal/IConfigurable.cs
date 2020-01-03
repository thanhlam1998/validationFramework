using System;
using System.Collections.Generic;
using System.Text;

namespace DKL_Validation.Internal
{
    public interface IConfigurable<TConfiguration, out TNext>
    {
        /// <summary>
        /// Configures the current object
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        TNext Configure(Action<TConfiguration> configuration);
    }
}
