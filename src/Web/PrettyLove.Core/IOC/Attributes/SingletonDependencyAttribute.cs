using Microsoft.Extensions.DependencyInjection;

namespace PrettyLove.Core
{
    /// <summary>
    /// 以Singleton方式注册到容器中
    /// </summary>
    public class SingletonDependencyAttribute : DependencyAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SingletonDependencyAttribute() : base(ServiceLifetime.Singleton)
        {

        }
    }
}
