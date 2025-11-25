using System.ComponentModel;

namespace XW.PrettyLove.Domain.Shared
{
    /// <summary>
    /// 婚姻状态
    /// </summary>
    public enum MaritalStatus
    {
        /// <summary>
        /// 未婚
        /// </summary>
        [Description("未婚")]
        Single = 0,

        /// <summary>
        /// 已婚
        /// </summary>
        [Description("已婚")]
        Married = 1,

        /// <summary>
        /// 离异
        /// </summary>
        [Description("离异")]
        Divorced = 2,

        /// <summary>
        /// 丧偶
        /// </summary>
        [Description("丧偶")]
        Widowed = 3,

        /// <summary>
        /// 保密
        /// </summary>
        [Description("保密")]
        Confidential = 99
    }
}
