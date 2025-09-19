using Microsoft.AspNetCore.Builder;
using System.Reflection;

namespace LS.ERP.Infrastructure
{
    public static class ModuleLoader
    {
        // 已经初始化的模块
        static readonly HashSet<Type> initializedModules = new HashSet<Type>();

        // 已经初始化的程序集
        static readonly HashSet<Assembly> initializedAssemblies = new HashSet<Assembly>();

        // 模块描述集合
        static readonly HashSet<ModuleDescriptor> moduleDescriptors = new HashSet<ModuleDescriptor>();

        /// <summary>
        /// 运行模块
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="startupModuleType"></param>
        /// <returns></returns>
        public static void Run(WebApplicationBuilder builder, Type startupModuleType)
        {
            //递归解析模块依赖
            CollectModules(startupModuleType);

            // 创建模块实例并调用 ConfigureServices
            var modules = initializedModules
                .Select(type => Activator.CreateInstance(type))
                .ToList();
            var serviceContext = new ServiceConfigurationContext(builder.Services, builder.Configuration);
            foreach (var module in modules)
            {
                if (module is IConfigureService service)
                {
                    service.ConfigureServices(serviceContext);
                }
            }
            var applicationContenxt = new ApplicationInitializationContext(builder);
            foreach (var module in modules)
            {
                if (module is IOnApplicationInitialization service)
                {
                    service.OnApplicationInitialization(applicationContenxt);
                }
            }
        }

        /// <summary>
        /// 递归解析模块依赖
        /// </summary>
        /// <param name="moduleType"></param>
        private static void CollectModules(Type moduleType)
        {
            if (initializedModules.Contains(moduleType))
                return;
            ModuleDescriptor moduleDescriptor = new ModuleDescriptor
            {
                Type = moduleType,
                Assembly = moduleType.Assembly,
                Instance = Activator.CreateInstance(moduleType)
            };
            initializedModules.Add(moduleType);
            initializedAssemblies.Add(moduleType.Assembly);
            var dependsOnAttributes = moduleType.GetCustomAttributes<DependsOnAttribute>();
            foreach (var attr in dependsOnAttributes)
            {
                foreach (var dependency in attr.Dependencies)
                {
                    CollectModules(dependency);
                }
            }
            initializedModules.Add(moduleType);
            moduleDescriptors.Add(moduleDescriptor);
        }
    }
}
