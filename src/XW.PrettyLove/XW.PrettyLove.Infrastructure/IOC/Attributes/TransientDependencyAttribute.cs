using Microsoft.Extensions.DependencyInjection;

namespace LS.ERP.Infrastructure
{
    /// <summary>
    /// 以Transient方式注册到容器中
    /// </summary>
    public class TransientDependencyAttribute : DependencyAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TransientDependencyAttribute() : base(ServiceLifetime.Transient)
        {

        }
    }
}
