using System.ComponentModel;

namespace XW.PrettyLove.Domain.Shared
{
    /// <summary>
    /// 受教育程度
    /// </summary>
    public enum Education
    {
        /// <summary>
        /// 高中以下
        /// </summary>
        [Description("高中以下")]
        BelowHighSchool = 0,

        /// <summary>
        /// 中专
        /// </summary>
        [Description("中专")]
        TechnicalSecondarySchool = 1,

        /// <summary>
        /// 大专
        /// </summary>
        [Description("大专")]
        College = 2,

        /// <summary>
        /// 本科
        /// </summary>
        [Description("本科")]
        Bachelor = 3,

        /// <summary>
        /// 研究生
        /// </summary>
        [Description("研究生")]
        Postgraduate = 4,

        /// <summary>
        /// 博士
        /// </summary>
        [Description("博士")]
        Doctor = 5
    }
}
