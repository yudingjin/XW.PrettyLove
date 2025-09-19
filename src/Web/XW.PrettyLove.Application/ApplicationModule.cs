using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OnceMi.AspNetCore.OSS;
using System.Reflection;
using XW.PrettyLove.Core;

namespace XW.PrettyLove.Application
{
    [DependsOn(typeof(PersistenceModule))]
    public class ApplicationModule : IModule
    {
        public void ConfigureServices(ServiceConfigurationContext context)
        {
            // 泛型服务注入
            context.Services.AddScoped(typeof(IAppService<>), typeof(AppService<>));
            // 注册程序集服务
            context.Services.AddAssemblyServices(Assembly.GetExecutingAssembly());
            // Exceptionless
            context.Services.ConfigureExceptionless(context.Configuration);
            // CAP
            context.Services.AddCap(context.Configuration, "/cap");
            // OSS注入
            context.Services.AddOSSService("Minio", "OSSProvider");
            // Redis
            context.Services.AddRedis(context.Configuration);
            // Mapster
            context.Services.AddMapster();
            // HttpContext 
            context.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            context.Services.AddHttpClient();
            // 用户
            context.Services.AddScoped<IAspNetUser, AspNetUser>();
        }
    }
}
