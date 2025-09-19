using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace LS.ERP.Infrastructure
{
    /// <summary>
    /// MethodInfo扩展
    /// </summary>
    public static class MethodInfoExtensions
    {
        /// <summary>
        /// 判断方法是否为异步方法
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static bool IsAsync([NotNull] this MethodInfo method)
        {
            return method.ReturnType.IsTaskOrTaskOfT();
        }
    }
}
