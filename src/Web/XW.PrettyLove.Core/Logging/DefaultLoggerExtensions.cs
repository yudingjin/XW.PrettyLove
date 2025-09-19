using Exceptionless;
using Exceptionless.Logging;

namespace XW.PrettyLove.Core
{
    public static class DefaultLoggerExtensions
    {
        /// <summary>
        /// 输出Trace日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="message"></param>
        /// <param name="tags"></param>
        public static void TraceToExceptionless(this Microsoft.Extensions.Logging.ILogger log, string message, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Trace).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Trace日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="tags"></param>
        public static void TraceToExceptionless(this Microsoft.Extensions.Logging.ILogger log, string message, object obj, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Trace).AddObject(obj).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Trace日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="source"></param>
        /// <param name="identity"></param>
        /// <param name="tags"></param>
        public static void TraceToExceptionless(this Microsoft.Extensions.Logging.ILogger log, string message, object obj, string source, string identity, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Trace).SetSource(source).SetUserIdentity(identity).AddObject(obj).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Info日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="message"></param>
        /// <param name="tags"></param>
        public static void InfoToExceptionless(this Microsoft.Extensions.Logging.ILogger log, string message, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Info).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Info日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="tags"></param>
        public static void InfoToExceptionless(this Microsoft.Extensions.Logging.ILogger log, string message, object obj, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Info).AddObject(obj).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Info日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="source"></param>
        /// <param name="identity"></param>
        /// <param name="tags"></param>
        public static void InfoToExceptionless(this Microsoft.Extensions.Logging.ILogger log, string message, object obj, string source, string identity, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Info).SetSource(source).SetUserIdentity(identity).AddObject(obj).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Warn日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="message"></param>
        /// <param name="tags"></param>
        public static void WarnToExceptionless(this Microsoft.Extensions.Logging.ILogger log, string message, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Warn).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Warn日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="tags"></param>
        public static void WarnToExceptionless(this Microsoft.Extensions.Logging.ILogger log, string message, object obj, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Warn).AddObject(obj).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Warn日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="source"></param>
        /// <param name="identity"></param>
        /// <param name="tags"></param>
        public static void WarnToExceptionless(this Microsoft.Extensions.Logging.ILogger log, string message, object obj, string source, string identity, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Warn).SetSource(source).SetUserIdentity(identity).AddObject(obj).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Error日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="message"></param>
        /// <param name="tags"></param>
        public static void ErrorToExceptionless(this Microsoft.Extensions.Logging.ILogger log, string message, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Error).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Error日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="tags"></param>
        public static void ErrorToExceptionless(this Microsoft.Extensions.Logging.ILogger log, string message, object obj, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Error).AddObject(obj).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Error日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="source"></param>
        /// <param name="identity"></param>
        /// <param name="tags"></param>
        public static void ErrorToExceptionless(this Microsoft.Extensions.Logging.ILogger log, string message, object obj, string source, string identity, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Error).SetSource(source).SetUserIdentity(identity).AddObject(obj).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Error日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="exception"></param>
        /// <param name="tags"></param>
        public static void ErrorToExceptionless(this Microsoft.Extensions.Logging.ILogger log, Exception exception, params string[] tags)
        {
            ExceptionlessClient.Default.CreateException(exception).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Error日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="exception"></param>
        /// <param name="obj"></param>
        /// <param name="tags"></param>
        public static void ErrorToExceptionless(this Microsoft.Extensions.Logging.ILogger log, Exception exception, object obj, params string[] tags)
        {
            ExceptionlessClient.Default.CreateException(exception).AddObject(obj).AddTags(tags).Submit();
        }
    }
}
