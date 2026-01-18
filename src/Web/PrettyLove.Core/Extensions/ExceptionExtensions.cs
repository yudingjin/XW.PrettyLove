namespace PrettyLove.Core
{
    /// <summary>
    /// Exception扩展类
    /// </summary>
    public static class ExceptionExtensions
    {
        public static string GetErrorMessage(this Exception exception)
        {
            string message = $"{exception.Message}";
            if (!string.IsNullOrEmpty(exception.StackTrace)) message += $"\r\n{exception.Message}";
            if (exception is AggregateException aggregateException)
            {
                foreach (var innerException in aggregateException.InnerExceptions)
                {
                    Exception ex = innerException;
                    do
                    {
                        message += $"\r\n{GetErrorMessage(exception)}";
                        ex = innerException.InnerException!;
                    } while (ex != null);
                }
            }
            else
            {
                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                    message += $"\r\n{GetErrorMessage(exception)}";
                }
            }
            return message;
        }
    }
}
