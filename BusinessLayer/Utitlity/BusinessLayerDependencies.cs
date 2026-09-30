using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDataAccessLayer.SettingClasses;

namespace BusinessLayer.Utitlity
{
    public static class BusinessLayerDependencies
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, string connectionString)
        {
            services.AddDataAccessServices(connectionString);
            return services;
        }
      
    }
}
