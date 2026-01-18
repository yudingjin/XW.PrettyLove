using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PrettyLove.Core
{
    /// <summary>
    /// 内部只用于初始化使用
    /// </summary>
    public static class InternalApp
    {
        /// <summary>
        /// 内部服务集合
        /// </summary>
        internal static IServiceCollection InternalServices;

        /// <summary>
        /// 根服务
        /// </summary>
        internal static IServiceProvider RootServices;

        /// <summary>
        /// Web主机环境
        /// </summary>
        internal static IWebHostEnvironment WebHostEnvironment;

        /// <summary>
        /// 获取泛型主机环境
        /// </summary>
        internal static IHostEnvironment HostEnvironment;

        /// <summary>
        /// 配置对象
        /// </summary>
        internal static IConfiguration Configuration;

        /// <summary>
        /// 配置应用程序环境和服务集合
        /// </summary>
        /// <param name="web"></param>
        public static void ConfigureApplication(this WebApplicationBuilder web)
        {
            HostEnvironment = web.Environment;
            WebHostEnvironment = web.Environment;
            InternalServices = web.Services;
        }

        public static void ConfigureApplication(this IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static void ConfigureApplication(this IHost app)
        {
            RootServices = app.Services;
        }
    }
}
