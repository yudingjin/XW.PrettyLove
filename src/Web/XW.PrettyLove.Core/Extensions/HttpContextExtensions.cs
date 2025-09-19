using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Text;

namespace XW.PrettyLove.Core
{
    /// <summary>
    /// HttpContext扩展类
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// 获取 Action 特性
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static TAttribute GetMetadata<TAttribute>(this HttpContext httpContext) where TAttribute : class
        {
            Endpoint endpoint = httpContext.GetEndpoint();
            if (endpoint == null)
            {
                return null;
            }

            EndpointMetadataCollection metadata = endpoint!.Metadata;
            if (metadata == null)
            {
                return null;
            }

            return metadata.GetMetadata<TAttribute>();
        }

        /// <summary>
        /// 获取 控制器/Action 描述器
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static ControllerActionDescriptor GetControllerActionDescriptor(this HttpContext httpContext)
        {
            return httpContext.GetEndpoint()?.Metadata?.FirstOrDefault((object u) => u is ControllerActionDescriptor) as ControllerActionDescriptor;
        }

        /// <summary>
        /// 设置规范化文档自动登录
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="accessToken"></param>
        public static void SigninToSwagger(this HttpContext httpContext, string accessToken)
        {
            httpContext.Response.Headers["access-token"] = accessToken;
        }

        /// <summary>
        /// 设置规范化文档退出登录
        /// </summary>
        /// <param name="httpContext"></param>
        public static void SignoutToSwagger(this HttpContext httpContext)
        {
            httpContext.Response.Headers["access-token"] = "invalid_token";
        }

        /// <summary>
        /// 获取本机 IPv4地址
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetLocalIpAddressToIPv4(this HttpContext context)
        {
            return context.Connection.LocalIpAddress?.MapToIPv4()?.ToString();
        }

        /// <summary>
        /// 获取本机 IPv6地址
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetLocalIpAddressToIPv6(this HttpContext context)
        {
            return context.Connection.LocalIpAddress?.MapToIPv6()?.ToString();
        }

        /// <summary>
        /// 获取远程 IPv4地址
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetRemoteIpAddressToIPv4(this HttpContext context)
        {
            return context.Connection.RemoteIpAddress?.MapToIPv4()?.ToString();
        }

        /// <summary>
        /// 获取远程 IPv6地址
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetRemoteIpAddressToIPv6(this HttpContext context)
        {
            return context.Connection.RemoteIpAddress?.MapToIPv6()?.ToString();
        }

        /// <summary>
        /// 获取完整请求地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetRequestUrlAddress(this HttpRequest request)
        {
            return new StringBuilder().Append(request.Scheme).Append("://").Append(request.Host)
                .Append((string)request.PathBase)
                .Append((string)request.Path)
                .Append(request.QueryString)
                .ToString();
        }

        /// <summary>
        /// 获取来源地址
        /// </summary>
        /// <param name="request"></param>
        /// <param name="refererHeaderKey"></param>
        /// <returns></returns>
        public static string GetRefererUrlAddress(this HttpRequest request, string refererHeaderKey = "Referer")
        {
            return request.Headers[refererHeaderKey].ToString();
        }

        /// <summary>
        /// 获取请求的ip4
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetRequestIPv4(this HttpContext context)
        {
            string ip = string.Empty;
            if (context.Connection.RemoteIpAddress != null)
            {
                if (context.Request.Headers.ContainsKey("X-Real-IP"))
                {
                    ip = context.Request.Headers["X-Real-IP"].FirstOrDefault();
                }
                if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
                {
                    ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                }
                if (string.IsNullOrEmpty(ip))
                {
                    ip = context.Connection.RemoteIpAddress?.MapToIPv4()?.ToString();
                }
            }
            return ip;
        }
    }
}
