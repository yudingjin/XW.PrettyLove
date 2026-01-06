namespace XW.PrettyLove.Domain.Shared
{
    /// <summary>
    /// 学历要求枚举
    /// </summary>
    public enum EducationRequirement
    {
        /// <summary>
        /// 不限学历
        /// </summary>
        NoLimit = 0,

        /// <summary>
        /// 高中以上
        /// </summary>
        HighSchoolAndAbove = 1,

        /// <summary>
        /// 中专以上
        /// </summary>
        TechnicalSecondarySchoolAndAbove = 2,

        /// <summary>
        /// 大专以上
        /// </summary>
        CollegeAndAbove = 3,

        /// <summary>
        /// 本科以上
        /// </summary>
        BachelorAndAbove = 4,

        /// <summary>
        /// 研究生以上
        /// </summary>
        PostgraduateAndAbove = 5
    }
}
