using System.ComponentModel;

namespace PrettyLove.Domain.Shared
{
    public enum OperateSystemType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        None = 0,

        /// <summary>
        /// Windows
        /// </summary>
        [Description("Windows")]
        Windows = 1,

        /// <summary>
        /// Android
        /// </summary>
        [Description("Android")]
        Android = 2,

        /// <summary>
        /// Web
        /// </summary>
        [Description("Web")]
        Web = 3
    }
}
