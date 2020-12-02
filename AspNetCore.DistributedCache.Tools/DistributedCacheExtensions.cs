using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace AspNetCore.DistributedCache.Tools
{
    public static class DistributedCacheExtensions
    {
        public static IServiceCollection ConfigureDistributedCacheRepository(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<string>("DistributedCache:ConnectionString");
                options.InstanceName = configuration.GetValue<string>("DistributedCache:InstanceName");
            });

            services.Configure<List<TimeCache>>(configuration.GetSection("DistributedCache:TimeCache"));

            services.AddSingleton<IDistributedCacheRepository, DistributedCacheRepository>();

            return services;
        }
    }
}
