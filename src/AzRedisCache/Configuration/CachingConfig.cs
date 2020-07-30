using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AzRedisCache.Configuration
{
    public static class CachingConfig
    {
        public static IServiceCollection AddCachingConfig(this IServiceCollection services,
                                                          IConfiguration configuration)
        {
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("AzureRedisCache");
                options.InstanceName = "AzureRedisCache";
            });

            return services;
        }
    }
}
