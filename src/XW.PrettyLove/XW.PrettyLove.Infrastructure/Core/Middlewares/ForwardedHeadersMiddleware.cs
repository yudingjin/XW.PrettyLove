using Microsoft.AspNetCore.Http;
using System.Net;

namespace LS.ERP.Infrastructure
{
    /// <summary>
    /// 获取真实IP
    /// </summary>
    public class ForwardedHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public ForwardedHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Connection.RemoteIpAddress = IPAddress.Parse(context.GetRequestIPv4());
            await _next(context);
        }
    }
}
