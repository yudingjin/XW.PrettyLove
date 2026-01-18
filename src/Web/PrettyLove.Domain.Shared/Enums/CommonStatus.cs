using System.ComponentModel;

namespace PrettyLove.Domain.Shared
{
    /// <summary>
    /// 公共状态
    /// </summary>
    public enum CommonStatus
    {
        /// <summary>
        /// 停用
        /// </summary>
        [Description("停用")]
        Disabled = 0,

        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Enabled = 1,
    }
}
