using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace XW.PrettyLove.Core
{
    /// <summary>
    /// 全局Action过滤器 
    /// </summary>
    public class FriendlyActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            // 如果是文件流结果，直接返回不处理（保持原始流）
            if (context.Result is FileStreamResult || context.Result is FileResult)
            {
                return; // 不修改流类型结果，保持原样返回
            }
            if (context.Result is ObjectResult dataResult)
            {
                if (dataResult.Value is Stream stream)
                {
                    context.Result = new FileStreamResult(stream, "application/octet-stream");
                }
                else if (dataResult.Value is byte[] bytes)
                {
                    context.Result = new FileStreamResult(new MemoryStream(bytes), "application/octet-stream");
                }
                else
                {
                    var result = new ReturnObject<Object>
                    {
                        Success = true,
                        Code = (int)HttpStatusCode.OK,
                        Data = dataResult.Value,
                        Message = "success"
                    };
                    context.Result = new JsonResult(result);
                }
            }
        }
    }
}
