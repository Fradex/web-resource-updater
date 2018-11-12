using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WebPackUpdater.Context;

namespace WebPackUpdater
{
    public static class DbInitializer
    {
        public static void Seed(IServiceProvider applicationBuilder)
        {
            var context =
                applicationBuilder.GetRequiredService<WebResourceContext>();
        }
    }
}
