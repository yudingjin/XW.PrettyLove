using System.ComponentModel;

namespace PrettyLove.Domain.Shared
{
    /// <summary>
    /// 体型
    /// </summary>
    public enum BodyType
    {
        /// <summary>
        /// 适中
        /// </summary>
        [Description("适中")]
        Moderate = 0,

        /// <summary>
        /// 偏瘦
        /// </summary>
        [Description("偏瘦")]
        Thin = 1,

        /// <summary>
        /// 健壮
        /// </summary>
        [Description("健壮")]
        Robust = 2,

        /// <summary>
        /// 微胖
        /// </summary>
        [Description("微胖")]
        SlightlyFat = 3
    }
}
