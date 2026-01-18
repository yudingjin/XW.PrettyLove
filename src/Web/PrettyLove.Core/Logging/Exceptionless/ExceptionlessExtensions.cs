using Exceptionless;
using Exceptionless.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PrettyLove.Core
{
    /// <summary>
    /// Exceptionless扩展方法
    /// </summary>
    public static class ExceptionlessExtensions
    {
        public static IApplicationBuilder UseCustomExceptionless(this IApplicationBuilder app, ExceptionlessClient client = null)
        {
            app.UseExceptionless();
            app.Use(async (context, next) =>
            {
                if (context.Request.Body.CanSeek)
                {
                    context.Request.Body.Position = 0;
                }
                await next();
            });
            return app;
        }

        /// <summary>
        /// Exceptionless注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureExceptionless(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddExceptionless(client =>
            {
                client.ApiKey = configuration["Exceptionless:ApiKey"];
                client.ServerUrl = configuration["Exceptionless:ServerUrl"];
                client.SetDefaultMinLogLevel(LogLevel.Info);
                // 添加事件过滤规则
                client.AddEventExclusion(e =>
                {
                    //忽略404错误
                    if (e.IsNotFound())
                    {
                        // 返回false表示排除此事件
                        return false;
                    }
                    // 默认情况下，不排除事件
                    return true;
                });
            });
            services.AddSingleton<PrettyLove.Domain.Shared.ILogger, ExceptionLessLogger>();
            return services;
        }

        /// <summary>
        /// Exceptionless注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="apiKey"></param>
        /// <param name="serverUrl"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureExceptionless(this IServiceCollection services, string apiKey, string serverUrl)
        {
            services.AddExceptionless(client =>
            {
                client.ApiKey = apiKey;
                client.ServerUrl = serverUrl;
                client.SetDefaultMinLogLevel(LogLevel.Info);
            });
            services.AddSingleton<PrettyLove.Domain.Shared.ILogger, ExceptionLessLogger>();
            return services;
        }

        /// <summary>
        /// Exceptionless注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="apiKey"></param>
        /// <param name="serverUrl"></param>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureExceptionless(this IServiceCollection services, string apiKey, string serverUrl, LogLevel logLevel)
        {
            services.AddExceptionless(client =>
            {
                client.ApiKey = apiKey;
                client.ServerUrl = serverUrl;
                client.SetDefaultMinLogLevel(logLevel);
            });
            services.AddSingleton<PrettyLove.Domain.Shared.ILogger, ExceptionLessLogger>();
            return services;
        }
    }
}
