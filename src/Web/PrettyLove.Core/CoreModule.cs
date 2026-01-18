using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace PrettyLove.Core
{
    public class CoreModule : IModule
    {
        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="context"></param>
        public void ConfigureServices(ServiceConfigurationContext context)
        {
            // 注册程序集服务
            context.Services.AddAssemblyServices(Assembly.GetExecutingAssembly());

            // ID雪花算法
            context.Services.AddSnowflakeId();

            // HttpContext 
            context.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            context.Services.AddHttpClient();

            // DataProtection
            context.Services.AddDataProtection()
                            .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "secret")))
                            .SetDefaultKeyLifetime(TimeSpan.FromDays(365));

            // 使用自定义ControllerFactory
            context.Services.Replace(ServiceDescriptor.Singleton<IControllerFactory>(new CustomControllerFactory()));

            // 配置项
            context.Services.AddSingleton(new AppSettings(context.Configuration));
            context.Services.AddAllOptionsRegister();
        }
    }
}
