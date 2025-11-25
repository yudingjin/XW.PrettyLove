using System.ComponentModel;

namespace XW.PrettyLove.Domain.Shared
{
    /// <summary>
    /// 体型
    /// </summary>
    public enum BodyType
    {
        /// <summary>
        /// 偏瘦
        /// </summary>
        [Description("偏瘦")]
        Slim = 1,

        /// <summary>
        /// 标准
        /// </summary>
        [Description("标准")]
        Normal = 2,

        /// <summary>
        /// 健壮
        /// </summary>
        [Description("健壮")]
        Athletic = 3,

        /// <summary>
        /// 微胖
        /// </summary>
        [Description("微胖")]
        SlightlyOverweight = 4,

        /// <summary>
        /// 丰满
        /// </summary>
        [Description("丰满")]
        FullFigured = 5,

        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other = 99,

        /// <summary>
        /// 保密
        /// </summary>
        [Description("保密")]
        Confidential = 0
    }
}
