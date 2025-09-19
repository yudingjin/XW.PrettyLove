using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace LS.ERP.Infrastructure
{
    /// <summary>
    /// 自定义控制器工厂
    /// </summary>
    public class CustomControllerFactory : IControllerFactory
    {
        /// <summary>
        /// 创建Controller
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public object CreateController(ControllerContext context)
        {
            var controllerType = context.ActionDescriptor.ControllerTypeInfo.AsType();
            var instance = ActivatorUtilities.CreateInstance(context.HttpContext.RequestServices, controllerType);
            //实现属性注入
            PropertyInjector.InjectProperties(instance, context.HttpContext.RequestServices);
            return instance;
        }

        /// <summary>
        /// 释放Controller
        /// </summary>
        /// <param name="context"></param>
        /// <param name="controller"></param>
        public void ReleaseController(ControllerContext context, object controller)
        {
            // 控制器释放逻辑
            if (controller is IDisposable disposable)
            {
                disposable?.Dispose();
            }
        }
    }
}
