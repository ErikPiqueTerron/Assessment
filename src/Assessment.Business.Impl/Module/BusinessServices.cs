using Assessment.Business.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Assessment.Business.Impl.Module
{
    public static class BusinessServices
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new Exception(nameof(services));
            }

            services.AddTransient<ICustomerServices, CustomerServices>()
                .AddTransient<IPolicyServices, PolicyServices>();

            return services;
        }
    }
}
