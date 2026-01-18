using Microsoft.AspNetCore.Http;

namespace PrettyLove.Core
{
    /// <summary>
    /// 请求拦截
    /// </summary>
    public class UnifyResultStatusCodesMiddleware
    {
        private readonly RequestDelegate _next;

        public UnifyResultStatusCodesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Response.StatusCode == 401)
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("未授权", System.Text.Encoding.UTF8);
            }
            else if (context.Response.StatusCode == 404)
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("请求url不存在", System.Text.Encoding.UTF8);
            }
            else
            {
                await _next(context);
            }
        }
    }
}
