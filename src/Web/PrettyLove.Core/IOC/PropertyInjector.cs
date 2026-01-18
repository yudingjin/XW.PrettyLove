using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PrettyLove.Core
{
    public class PropertyInjector
    {
        /// <summary>
        /// 为控制器注入属性
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="serviceProvider"></param>
        public static void InjectProperties(object instance, IServiceProvider serviceProvider)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));
            if (serviceProvider == null)
                throw new ArgumentNullException(nameof(serviceProvider));

            var type = instance.GetType();
            // 检索出带有[Inject]的属性
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Where(p => p.IsDefined(typeof(InjectionAttribute), true)).ToList();
            foreach (var property in properties)
            {
                // 获取属性的类型
                var serviceType = property.PropertyType;
                var serviceInstance = serviceProvider.GetRequiredService(serviceType);
                if (serviceInstance != null)
                {
                    // 设置属性值
                    property.SetValue(instance, serviceInstance);
                    InjectProperties(serviceInstance, serviceProvider);
                }
                else
                {
                    throw new InvalidOperationException($"无法解析服务 {serviceType.FullName}");
                }
            }
        }
    }
}
