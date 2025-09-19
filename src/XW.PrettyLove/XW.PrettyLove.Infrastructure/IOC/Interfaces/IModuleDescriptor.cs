using System.Reflection;

namespace LS.ERP.Infrastructure
{
    /// <summary>
    /// 模块描述接口
    /// </summary>
    public interface IModuleDescriptor
    {
        /// <summary>
        /// 模块入口类型
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// 模块所在程序集
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// 模块实例
        /// </summary>
        object Instance { get; }
    }
}
