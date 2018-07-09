using Assessment.Infrastructure.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Assessment.Infrastructure.Impl.Module
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new Exception(nameof(services));
            }

            services.AddTransient<IHttpProxy, HttpProxy>();

            return services;
        }
    }
}
