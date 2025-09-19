﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace LS.ERP.Infrastructure
{
    /// <summary>
    /// 自定义应用程序模型约定，用于配置实现了 IApplicationService 接口的控制器。
    /// </summary>
    public class ApplicationServiceConvention : IApplicationModelConvention
    {
        private IConfiguration _configuration;
        private List<HttpMethodConfigure> httpMethods = new List<HttpMethodConfigure>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public ApplicationServiceConvention(IConfiguration configuration)
        {
            _configuration = configuration;
            httpMethods = _configuration.GetSection("HttpMethodInfo").Get<List<HttpMethodConfigure>>();
        }

        /// <summary>
        /// 应用约定
        /// </summary>
        /// <param name="application"></param>
        public void Apply(ApplicationModel application)
        {
            //循环每一个控制器信息
            foreach (var controller in application.Controllers)
            {
                var controllerType = controller.ControllerType.AsType();
                //如果类型标记了 DynamicWebApiAttribute 特性，则认为它是控制器
                if (controllerType.IsDefined(typeof(DynamicWebApiAttribute), true))
                {
                    // 检查控制器是否标记了[AllowAnonymous]（允许匿名访问）
                    //var allowAnonymous = controllerType.GetCustomAttribute<AllowAnonymousAttribute>() != null;
                    //if (!allowAnonymous)
                    //{
                    //    // 为控制器添加[Authorize]特性
                    //    controller.Filters.Add(new AuthorizeFilter());
                    //}

                    //Actions就是接口的方法
                    foreach (var item in controller.Actions)
                    {
                        ConfigureSelector(controller.ControllerName, item);
                    }
                }
            }
        }

        /// <summary>
        /// 配置选择器
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="action"></param>
        private void ConfigureSelector(string controllerName, ActionModel action)
        {
            //如果属性路由模型为空，则移除
            for (int i = 0; i < action.Selectors.Count; i++)
            {
                if (action.Selectors[i].AttributeRouteModel is null)
                {
                    action.Selectors.Remove(action.Selectors[i]);
                }
            }
            //去除路径中的AppService后缀
            if (controllerName.EndsWith("AppService"))
            {
                controllerName = controllerName.Substring(0, controllerName.Length - 10);
            }
            //如果有选择器，则遍历选择器，添加默认路由
            if (action.Selectors.Any())
            {
                foreach (var item in action.Selectors)
                {
                    var routePath = string.Concat("api/", controllerName + "/", action.ActionName).Replace("//", "/");
                    var routeModel = new AttributeRouteModel(new RouteAttribute(routePath));
                    //如果没有设置路由，则添加路由
                    if (item.AttributeRouteModel == null)
                    {
                        item.AttributeRouteModel = routeModel;
                    }
                }
            }
            //如果没有选择器，则创建一个选择器并添加。
            else
            {
                action.Selectors.Add(CreateActionSelector(controllerName, action));
            }
        }

        /// <summary>
        /// 创建Action选择器
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private SelectorModel CreateActionSelector(string controllerName, ActionModel action)
        {
            var selectorModel = new SelectorModel();
            string httpMethod = string.Empty;
            //是否有HttpMethodAttribute
            var routeAttributes = action.ActionMethod.GetCustomAttributes(typeof(HttpMethodAttribute), false);
            //如果标记了HttpMethodAttribute
            if (routeAttributes != null && routeAttributes.Any())
            {
                httpMethod = routeAttributes.SelectMany(m => (m as HttpMethodAttribute).HttpMethods).ToList().Distinct().FirstOrDefault();
            }
            else
            {
                //大写方法名
                var methodName = action.ActionMethod.Name.ToUpper();
                //遍历HttpMethodInfo配置，匹配方法名
                foreach (var item in httpMethods)
                {
                    foreach (var method in item.MethodVal)
                    {
                        if (methodName.StartsWith(method))
                        {
                            httpMethod = item.MethodKey;
                            break;
                        }
                    }
                }
                //如果没有找到对应的HttpMethod，默认使用POST
                if (httpMethod == string.Empty)
                {
                    httpMethod = "POST";
                }
            }
            return ConfigureSelectorModel(selectorModel, action, controllerName, httpMethod);
        }

        /// <summary>
        /// 配置选择器模型
        /// </summary>
        /// <param name="selectorModel"></param>
        /// <param name="action"></param>
        /// <param name="controllerName"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        public SelectorModel ConfigureSelectorModel(SelectorModel selectorModel, ActionModel action, string controllerName, string httpMethod)
        {
            //route路径
            var routePath = string.Concat("api/", controllerName + "/", action.ActionName).Replace("//", "/").ToLower();
            //给此选择器添加路由
            selectorModel.AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(routePath));
            //添加HttpMethod
            selectorModel.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { httpMethod }));
            return selectorModel;
        }
    }
}
