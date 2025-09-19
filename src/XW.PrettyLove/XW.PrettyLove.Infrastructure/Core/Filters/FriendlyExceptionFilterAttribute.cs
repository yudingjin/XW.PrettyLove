using Exceptionless;
using FreeSql.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace LS.ERP.Infrastructure
{
    /// <summary>
    /// 全局异常过滤器
    /// </summary>
    public class FriendlyExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            if (context.ExceptionHandled == false)
            {
                var result = new ReturnObject<object> { Success = false, Message = string.Empty, Data = null, Code = (int)HttpStatusCode.InternalServerError };
                if (context.Exception is FriendlyException friendlyException)
                {
                    result.Code = (int)HttpStatusCode.InternalServerError;
                    result.Message = context.Exception.Message;
                }
                else if (context.Exception is DbUpdateVersionException dbUpdateException)
                {
                    result.Code = (int)HttpStatusCode.InternalServerError;
                    result.Message = "数据版本已改变，请返回刷新，获取最新数据";
                }
                else
                {
                    result.Message = context.Exception.Message;
                    context.Exception.ToExceptionless().Submit();
                }
                context.ExceptionHandled = true;
                context.Result = new ObjectResult(result);
            }
        }
    }
}
