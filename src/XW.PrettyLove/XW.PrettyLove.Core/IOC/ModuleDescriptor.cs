using System.Reflection;

namespace XW.PrettyLove.Core
{
    /// <summary>
    /// 模块描述类
    /// </summary>
    public class ModuleDescriptor : IModuleDescriptor
    {
        /// <summary>
        /// 模块入口类型
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// 模块所在程序集
        /// </summary>
        public Assembly Assembly { get; set; }

        /// <summary>
        /// 模块实例
        /// </summary>
        public object Instance { get; set; }
    }
}
