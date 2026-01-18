using System.ComponentModel;

namespace PrettyLove.Domain.Shared
{
    /// <summary>
    /// 希望何时结婚
    /// </summary>
    public enum ExpectedMarriageTime
    {
        /// <summary>
        /// 半年内
        /// </summary>
        [Description("半年内")]
        Within6Months = 0,

        /// <summary>
        /// 1年内
        /// </summary>
        [Description("1年内")]
        Within1Year = 1,

        /// <summary>
        /// 1-2年内
        /// </summary>
        [Description("1-2年内")]
        Within1To2Years = 2,

        /// <summary>
        /// 2-3年内
        /// </summary>
        [Description("2-3年内")]
        Within2To3Years = 3,

        /// <summary>
        /// 3年以上
        /// </summary>
        [Description("3年以上")]
        MoreThan3Years = 4,

        /// <summary>
        /// 顺其自然
        /// </summary>
        [Description("顺其自然")]
        GoWithTheFlow = 5,

        /// <summary>
        /// 暂不考虑结婚
        /// </summary>
        [Description("暂不考虑结婚")]
        NotConsideringMarriage = 6,

        /// <summary>
        /// 不愿透露
        /// </summary>
        [Description("不愿透露")]
        PreferNotToSay = 99
    }
}
