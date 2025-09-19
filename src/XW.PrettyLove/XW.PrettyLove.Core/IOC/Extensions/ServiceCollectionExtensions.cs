using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace XW.PrettyLove.Core
{
    /// <summary>
    /// IServiceCollection扩展类
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 扫描程序集实现接口自动注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        public static IServiceCollection AddAssemblyServices(this IServiceCollection services, Assembly assembly)
        {
            // 找到所有带有InjectOnAttribute特性的类
            var typeList = assembly.GetTypes().Where(p => p.IsDefined(typeof(DependencyAttribute), true));
            foreach (var type in typeList)
            {
                var attbiute = type.GetCustomAttribute<DependencyAttribute>();
                // 找到所有实现的接口
                var interfaces = type.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    // 只注册接口
                    if (@interface.IsInterface)
                    {
                        services.AddService(attbiute.Lifetime, @interface, type);
                    }
                }
            }
            return services;
        }

        /// <summary>
        /// 实现不同ServiceLifetime的服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="lifetime"></param>
        /// <param name="serviceType"></param>
        /// <param name="implementationType"></param>
        /// <returns></returns>
        public static IServiceCollection AddService(this IServiceCollection services, ServiceLifetime lifetime, Type serviceType, Type implementationType)
        {
            switch (lifetime)
            {
                case ServiceLifetime.Transient:
                    services.AddTransient(serviceType, implementationType);
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped(serviceType, implementationType);
                    break;
                case ServiceLifetime.Singleton:
                    services.AddSingleton(serviceType, implementationType);
                    break;
            }
            return services;
        }
    }
}
