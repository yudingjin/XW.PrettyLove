using System.ComponentModel;

namespace PrettyLove.Domain.Shared
{
    /// <summary>
    /// 宗教信仰
    /// </summary>
    public enum Religion
    {
        /// <summary>
        /// 无宗教信仰
        /// </summary>
        [Description("无宗教信仰")]
        None = 0,

        /// <summary>
        /// 佛教
        /// </summary>
        [Description("佛教")]
        Buddhism = 1,

        /// <summary>
        /// 道教
        /// </summary>
        [Description("道教")]
        Taoism = 2,

        /// <summary>
        /// 基督教
        /// </summary>
        [Description("基督教")]
        Christianity = 3,

        /// <summary>
        /// 天主教
        /// </summary>
        [Description("天主教")]
        Catholicism = 4,

        /// <summary>
        ///  伊斯兰教
        /// </summary>
        [Description("伊斯兰教")]
        Islam = 5,

        /// <summary>
        /// 其他教宗
        /// </summary>
        [Description("其他宗教")]
        Other = 98,

        /// <summary>
        /// 不愿透漏
        /// </summary>
        [Description("不愿透露")]
        PreferNotToSay = 99
    }
}
