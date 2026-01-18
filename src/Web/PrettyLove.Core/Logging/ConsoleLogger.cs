namespace PrettyLove.Core
{
    /// <summary>
    /// 控制台日志
    /// </summary>
    public class ConsoleLogger
    {
        /// <summary>
        /// 写入控制台
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息</param>
        /// <param name="subTitle">副标题</param>
        /// <param name="consoleColor">颜色</param>
        public static void WriteLine(string title, string message, string subTitle, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            string nowTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string content = string.IsNullOrEmpty(subTitle) ? $" {nowTime} {title} : {message}" : $" {nowTime} {title} {subTitle} : {message}";
            Console.ForegroundColor = consoleColor;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine(content);
        }

        /// <summary>
        /// 写入控制台
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="subTitle">副标题</param>
        /// <param name="consoleColor">颜色</param>
        public static void ServerWriteLine(string message, string subTitle = null, ConsoleColor consoleColor = ConsoleColor.White)
        {
            WriteLine("Server", message, subTitle, consoleColor);
        }

        /// <summary>
        /// 输出错误信息
        /// </summary>
        /// <param name="exception"></param>
        public static void Error(Exception exception)
        {
            ServerWriteLine(exception.Message, "错误", ConsoleColor.Red);
        }

        /// <summary>
        /// 控制台输出通知信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="consoleColor"></param>
        public static void Information(string message, ConsoleColor consoleColor = ConsoleColor.Green)
        {
            Console.ForegroundColor = consoleColor;
            string nowTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string content = $" {nowTime} : {message}";
            Console.ForegroundColor = consoleColor;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine(content);
        }

        /// <summary>
        /// 控制台输出错误信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="consoleColor"></param>
        public static void Error(string message, ConsoleColor consoleColor = ConsoleColor.Red)
        {
            Console.ForegroundColor = consoleColor;
            string nowTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string content = $" {nowTime} : {message}";
            Console.ForegroundColor = consoleColor;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine(content);
        }
    }
}
