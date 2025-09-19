using Exceptionless;
using Exceptionless.Logging;
using XW.PrettyLove.Domain.Shared;

namespace XW.PrettyLove.Core
{
    /// <summary>
    /// 日志实现
    /// </summary>
    public class ExceptionLessLogger : ILogger
    {
        /// <summary>
        /// 输出Trace日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="tags"></param>
        public void Trace(string message, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Trace).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Trace日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="tags"></param>
        public void Trace(string message, object obj, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Trace).AddObject(obj).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Trace日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="source"></param>
        /// <param name="identity"></param>
        /// <param name="tags"></param>
        public void Trace(string message, object obj, string source, string identity, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Trace).SetSource(source).SetUserIdentity(identity).AddObject(obj).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Debug日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="tags"></param>
        public void Debug(string message, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Debug).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Debug日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="tags"></param>
        public void Debug(string message, object obj, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Debug).AddObject(obj).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Debug日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="source"></param>
        /// <param name="identity"></param>
        /// <param name="tags"></param>
        public void Debug(string message, object obj, string source, string identity, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Debug).SetSource(source).SetUserIdentity(identity).AddObject(obj).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Info日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="tags"></param>
        public void Info(string message, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Info).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Info日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="tags"></param>
        public void Info(string message, object obj, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Info).AddObject(obj).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Info日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="source"></param>
        /// <param name="identity"></param>
        /// <param name="tags"></param>
        public void Info(string message, object obj, string source, string identity, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Info).SetSource(source).SetUserIdentity(identity).AddObject(obj).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Warn日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="tags"></param>
        public void Warn(string message, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Warn).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Warn日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="tags"></param>
        public void Warn(string message, object obj, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Warn).AddObject(obj).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Warn日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="source"></param>
        /// <param name="identity"></param>
        /// <param name="tags"></param>
        public void Warn(string message, object obj, string source, string identity, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Warn).AddObject(obj).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Error日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="tags"></param>
        public void Error(string message, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Error).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Error日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="tags"></param>
        public void Error(string message, object obj, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Error).AddObject(obj).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Error日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="source"></param>
        /// <param name="identity"></param>
        /// <param name="tags"></param>
        public void Error(string message, object obj, string source, string identity, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Error).SetSource(source).SetUserIdentity(identity).AddObject(obj).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Error日志
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="source"></param>
        /// <param name="identity"></param>
        /// <param name="tags"></param>
        public void Error(Exception exception, string message, object obj, string source, string identity, params string[] tags)
        {
            ExceptionlessClient.Default.CreateLog(message, LogLevel.Error).SetSource(source).SetUserIdentity(identity).AddObject(exception).AddObject(obj).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Error日志
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="tags"></param>
        public void Error(Exception exception, params string[] tags)
        {
            ExceptionlessClient.Default.CreateException(exception).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Error日志
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="obj"></param>
        /// <param name="tags"></param>
        public void Error(Exception exception, object obj, params string[] tags)
        {
            ExceptionlessClient.Default.CreateException(exception).AddObject(obj).AddTags(tags).Submit();
        }

        /// <summary>
        /// 输出Error日志
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="obj"></param>
        /// <param name="source"></param>
        /// <param name="identity"></param>
        /// <param name="tags"></param>
        public void Error(Exception exception, object obj, string source, string identity, params string[] tags)
        {
            ExceptionlessClient.Default.CreateException(exception).AddObject(obj).AddTags(tags).Submit();
        }
    }
}
