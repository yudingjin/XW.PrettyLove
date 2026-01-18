using System.ComponentModel;

namespace PrettyLove.Domain.Shared
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
        Unmarried = 0,

        /// <summary>
        /// 离异未育
        /// </summary>
        [Description("离异未育")]
        DivorcedWithoutChildren = 1,

        /// <summary>
        /// 离异已育
        /// </summary>
        [Description("离异已育")]
        DivorcedWithChildren = 2,

        /// <summary>
        /// 暂不填写
        /// </summary>
        [Description("暂不填写")]
        NotFilled = 3
    }
}
