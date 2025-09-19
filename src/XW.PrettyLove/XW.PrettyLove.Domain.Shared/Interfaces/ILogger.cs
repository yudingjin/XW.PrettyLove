namespace LS.ERP.Domain.Shared
{
    /// <summary>
    /// 自定义日志接口
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 输出Trace日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="tags"></param>
        void Trace(string message, params string[] tags);

        /// <summary>
        /// 输出Trace日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="tags"></param>
        void Trace(string message, object obj, params string[] tags);

        /// <summary>
        /// 输出Trace日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="source"></param>
        /// <param name="identity"></param>
        /// <param name="tags"></param>
        void Trace(string message, object obj, string source, string identity, params string[] tags);

        /// <summary>
        /// 输出Debug日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Debug(string message, params string[] tags);

        /// <summary>
        /// 输出Debug日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="tags"></param>
        void Debug(string message, object obj, params string[] tags);

        /// <summary>
        /// 输出Debug日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="source"></param>
        /// <param name="identity"></param>
        /// <param name="tags"></param>
        void Debug(string message, object obj, string source, string identity, params string[] tags);

        /// <summary>
        /// 输出Info日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Info(string message, params string[] tags);

        /// <summary>
        /// 输出Info日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="tags"></param>
        void Info(string message, object obj, params string[] tags);

        /// <summary>
        /// 输出Info日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="source"></param>
        /// <param name="identity"></param>
        /// <param name="tags"></param>
        void Info(string message, object obj, string source, string identity, params string[] tags);

        /// <summary>
        /// 输出Warn日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="tags"></param>
        void Warn(string message, params string[] tags);

        /// <summary>
        /// 输出Warn日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="tags"></param>
        void Warn(string message, object obj, params string[] tags);

        /// <summary>
        /// 输出Warn日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="source"></param>
        /// <param name="identity"></param>
        /// <param name="tags"></param>
        void Warn(string message, object obj, string source, string identity, params string[] tags);

        /// <summary>
        /// 输出Error日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="tags"></param>
        void Error(string message, params string[] tags);

        /// <summary>
        /// 输出Error日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="tags"></param>
        void Error(string message, object obj, params string[] tags);

        /// <summary>
        /// 输出Error日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="source"></param>
        /// <param name="identity"></param>
        /// <param name="tags"></param>
        void Error(string message, object obj, string source, string identity, params string[] tags);

        /// <summary>
        /// 输出Error日志
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        /// <param name="source"></param>
        /// <param name="identity"></param>
        /// <param name="tags"></param>
        void Error(Exception exception, string message, object obj, string source, string identity, params string[] tags);

        /// <summary>
        /// 输出Error日志
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="tags"></param>
        void Error(Exception ex, params string[] tags);

        /// <summary>
        /// 输出Error日志
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="obj"></param>
        /// <param name="tags"></param>
        void Error(Exception ex, object obj, params string[] tags);

        /// <summary>
        /// 输出Error日志
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="obj"></param>
        /// <param name="source"></param>
        /// <param name="identity"></param>
        /// <param name="tags"></param>
        void Error(Exception ex, object obj, string source, string identity, params string[] tags);
    }
}
