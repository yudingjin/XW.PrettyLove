using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace XW.PrettyLove.Core
{
    /// <summary>
    /// 自定义控制器特性提供程序，用于将标记了 DynamicWebApiAttribute 特性的类识别为控制器
    /// </summary>
    public class ApplicationServiceControllerFeatureProvider : ControllerFeatureProvider
    {
        /// <summary>
        /// 判断给定的类型是否为控制器
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <returns></returns>
        protected override bool IsController(TypeInfo typeInfo)
        {
            // 如果类型标记了 DynamicWebApiAttribute特性，则认为它是控制器
            if (typeInfo.IsDefined(typeof(DynamicWebApiAttribute), true))
            {
                // 检查类型是否满足以下条件：
                // 必须是Public、不能是抽象类、必须是非泛型的
                if (typeInfo.IsPublic && !typeInfo.IsAbstract && !typeInfo.IsGenericType && !typeInfo.IsInterface)
                {
                    return true;
                }
            }
            // 如果不满足上述条件，则返回 false
            return false;
        }
    }
}
