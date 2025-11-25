namespace XW.PrettyLove.Domain.Shared
{
    /// <summary>
    /// 受教育程度
    /// </summary>
    public enum Education
    {
        /// <summary>
        /// 初中及以下
        /// </summary>
        JuniorHighOrBelow = 0,

        /// <summary>
        /// 高中/中专
        /// </summary>
        HighSchoolOrTechnical = 1,

        /// <summary>
        /// 大专
        /// </summary>
        AssociateDegree = 2,

        /// <summary>
        /// 本科
        /// </summary>
        Bachelor = 3,

        /// <summary>
        /// 硕士
        /// </summary>
        Master = 4,

        /// <summary>
        /// 博士
        /// </summary>
        Doctor = 5,

        /// <summary>
        /// 其他
        /// </summary>
        Other = 99
    }
}
