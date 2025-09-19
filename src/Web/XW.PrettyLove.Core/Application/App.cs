using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Reflection;
using XW.PrettyLove.Domain.Shared;

namespace XW.PrettyLove.Core
{
    /// <summary>
    /// 全局应用
    /// </summary>
    public class App
    {
        /// <summary>
        /// 静态构造
        /// </summary>
        static App()
        {
            EffectiveTypes = Assemblies.SelectMany(GetTypes);
        }

        private static bool _isRun;
        /// <summary>
        /// 是否正在运行
        /// </summary>
        public static bool IsBuild { get; set; }

        public static bool IsRun
        {
            get => _isRun;
            set => _isRun = IsBuild = value;
        }

        /// <summary>
        /// 应用有效程序集
        /// </summary>
        public static readonly IEnumerable<Assembly> Assemblies = RuntimeExtensions.GetAllAssemblies();

        /// <summary>
        /// 有效程序集类型
        /// </summary>
        public static readonly IEnumerable<Type> EffectiveTypes;

        /// <summary>
        /// 优先使用App.GetService()手动获取服务
        /// </summary>
        public static IServiceProvider RootServices => IsRun || IsBuild ? InternalApp.RootServices : null;

        /// <summary>
        /// 获取Web主机环境，如，是否是开发环境，生产环境等
        /// </summary>
        public static IWebHostEnvironment WebHostEnvironment => InternalApp.WebHostEnvironment;

        /// <summary>
        /// 获取泛型主机环境，如，是否是开发环境，生产环境等
        /// </summary>
        public static IHostEnvironment HostEnvironment => InternalApp.HostEnvironment;

        /// <summary>
        /// 全局配置选项
        /// </summary>
        public static IConfiguration Configuration => InternalApp.Configuration;

        /// <summary>
        /// 获取请求上下文
        /// </summary>
        public static HttpContext HttpContext => RootServices?.GetService<IHttpContextAccessor>()?.HttpContext;

        public static IAspNetUser User => GetService<IAspNetUser>();

        #region Services

        /// <summary>解析服务提供器</summary>
        /// <param name="serviceType"></param>
        /// <param name="mustBuild"></param>
        /// <param name="throwException"></param>
        /// <returns></returns>
        public static IServiceProvider GetServiceProvider(Type serviceType, bool mustBuild = false, bool throwException = true)
        {
            var flag = InternalApp.InternalServices.Where(u => u.ServiceType == (serviceType.IsGenericType ? serviceType.GetGenericTypeDefinition() : serviceType))
                                                   .Any(u => u.Lifetime == ServiceLifetime.Singleton);
            if ((HostEnvironment == null || RootServices != null) && flag)
                return RootServices;

            //获取请求生存周期的服务
            if (HttpContext?.RequestServices != null)
                return HttpContext.RequestServices;

            if (RootServices != null)
            {
                IServiceScope scope = RootServices.CreateScope();
                return scope.ServiceProvider;
            }

            if (mustBuild)
            {
                if (throwException)
                {
                    throw new ApplicationException("当前不可用，必须要等到 WebApplication Build后");
                }
                return default;
            }

            ServiceProvider serviceProvider = InternalApp.InternalServices.BuildServiceProvider();
            return serviceProvider;
        }

        /// <summary>
        /// 获取请求生存周期的服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="mustBuild"></param>
        /// <returns></returns>
        public static TService GetService<TService>(bool mustBuild = true) where TService : class 
            => GetService(typeof(TService), null, mustBuild) as TService;

        /// <summary>
        /// 获取请求生存周期的服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <param name="mustBuild"></param>
        /// <returns></returns>
        public static TService GetService<TService>(IServiceProvider serviceProvider, bool mustBuild = true) where TService : class 
            => (serviceProvider ?? GetServiceProvider(typeof(TService), mustBuild, false))?.GetService<TService>();

        /// <summary>
        /// 获取请求生存周期的服务
        /// </summary>
        /// <param name="type"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="mustBuild"></param>
        /// <returns></returns>
        public static object GetService(Type type, IServiceProvider serviceProvider = null, bool mustBuild = true) 
            => (serviceProvider ?? GetServiceProvider(type, mustBuild, false))?.GetService(type);

        #endregion

        #region Options

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <typeparam name="TOptions">强类型选项类</typeparam>
        /// <returns></returns>
        public static TOptions GetConfig<TOptions>() where TOptions : class, IConfigurableOptions
        {
            TOptions instance = Configuration.GetSection(ConfigurableOptionsExtensions.GetConfigurationPath(typeof(TOptions))).Get<TOptions>();
            return instance;
        }

        /// <summary>
        /// 获取选项
        /// </summary>
        /// <typeparam name="TOptions">强类型选项类</typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static TOptions GetOptions<TOptions>(IServiceProvider serviceProvider = null) where TOptions : class, new()
        {
            IOptions<TOptions> service = GetService<IOptions<TOptions>>(serviceProvider ?? RootServices, false);
            return service?.Value;
        }

        /// <summary>
        /// 获取选项
        /// </summary>
        /// <typeparam name="TOptions">强类型选项类</typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static TOptions GetOptionsMonitor<TOptions>(IServiceProvider serviceProvider = null) where TOptions : class, new()
        {
            IOptionsMonitor<TOptions> service = GetService<IOptionsMonitor<TOptions>>(serviceProvider ?? RootServices, false);
            return service?.CurrentValue;
        }

        /// <summary>
        /// 获取选项
        /// </summary>
        /// <typeparam name="TOptions">强类型选项类</typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static TOptions GetOptionsSnapshot<TOptions>(IServiceProvider serviceProvider = null) where TOptions : class, new()
        {
            IOptionsSnapshot<TOptions> service = GetService<IOptionsSnapshot<TOptions>>(serviceProvider, false);
            return service?.Value;
        }

        #endregion

        #region Private

        /// <summary>
        /// 加载程序集中的所有类型
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        private static IEnumerable<Type> GetTypes(Assembly assembly) => assembly.GetTypes().Where(u => u.IsPublic);

        #endregion
    }
}
