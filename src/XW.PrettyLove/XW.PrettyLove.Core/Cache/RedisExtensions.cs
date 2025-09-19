using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace XW.PrettyLove.Core
{
    /// <summary>
    /// Redis扩展
    /// </summary>
    public static class RedisExtensions
    {
        /// <summary>
        /// Redis注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RedisOptions>(configuration.GetSection("RedisConfig"));
            services.AddSingleton<RedisInstance>();
            return services;
        }
    }
}
