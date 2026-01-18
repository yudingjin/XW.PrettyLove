using System.ComponentModel;

namespace PrettyLove.Domain.Shared
{
    /// <summary>
    /// 收入等级
    /// </summary>
    public enum IncomLevel
    {
        /// <summary>
        /// 5000以下
        /// </summary>
        [Description("5000以下")]
        Below5000 = 0,

        /// <summary>
        /// 5000-8000
        /// </summary>
        [Description("5000-8000")]
        From5000To8000 = 1,

        /// <summary>
        /// 8000-12000
        /// </summary>
        [Description("8000-12000")]
        From8000To12000 = 2,

        /// <summary>
        /// 12000-20000
        /// </summary>
        [Description("12000-20000")]
        From12000To20000 = 3,

        /// <summary>
        /// 20000-50000
        /// </summary>
        [Description("20000-50000")]
        From20000To50000 = 4,

        /// <summary>
        /// 50000以上
        /// </summary>
        [Description("50000以上")]
        Above50000 = 5
    }
}
