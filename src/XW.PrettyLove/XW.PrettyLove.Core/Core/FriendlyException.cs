using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.CompilerServices;

namespace XW.PrettyLove.Core
{
    /// <summary>
    /// 自定义异常
    /// </summary>
    public class FriendlyException : Exception
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public object ErrorCode { get; set; }

        /// <summary>
        /// 错误消息（支持 Object 对象）
        /// </summary>
        public object ErrorMessage { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int StatusCode { get; set; } = StatusCodes.Status500InternalServerError;

        /// <summary>
        /// 是否是数据验证异常
        /// </summary>
        public bool ValidationException { get; set; } = false;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FriendlyException()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        public FriendlyException(string message, object errorCode) : base(message)
        {
            ErrorCode = errorCode;
            ErrorMessage = message;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        /// <param name="innerException"></param>
        public FriendlyException(string message, object errorCode, Exception innerException) : base(message, innerException)
        {
            ErrorMessage = message;
            ErrorCode = errorCode;
        }

        /// <summary>
        /// 静态方法 - 抛出异常
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="paramName"></param>
        public static void ThrowIfNull([NotNull] object argument, [CallerArgumentExpression("argument")] string paramName = null)
        {
            if (argument is null)
            {
                throw new FriendlyException($"{argument} is null", HttpStatusCode.InternalServerError);
            }
        }
    }
}
