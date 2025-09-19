using System.ComponentModel;

namespace LS.ERP.Domain.Shared
{
    /// <summary>
    /// 应用类型
    /// </summary>
    public enum AppType
    {
        /// <summary>
        /// 桌面
        /// </summary>
        [Description("桌面")]
        Desktop = 1,

        /// <summary>
        /// Web
        /// </summary>
        [Description("Web")]
        Web = 2,

        /// <summary>
        /// 苹果
        /// </summary>
        [Description("苹果")]
        IOS = 3,

        /// <summary>
        /// 安卓
        /// </summary>
        [Description("安卓")]
        Android = 4
    }
}
