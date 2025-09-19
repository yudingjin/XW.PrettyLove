using Microsoft.Extensions.DependencyInjection;

namespace XW.PrettyLove.Core
{
    /// <summary>
    /// 将当前类型自动注册到容器中
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public abstract class DependencyAttribute : Attribute
    {
        /// <summary>
        /// 服务的生命周期.
        /// </summary>
        public ServiceLifetime Lifetime { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="lifetime"></param>
        /// <param name="scheme"></param>
        public DependencyAttribute(ServiceLifetime lifetime)
        {
            Lifetime = lifetime;
        }
    }
}
