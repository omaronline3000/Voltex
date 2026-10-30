using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Voltex.Core
{
    public static class CoreRegisteration
    {
        public static IServiceCollection CoreConfiguration(this IServiceCollection service)
        {


            return service;
        }
    }
}
