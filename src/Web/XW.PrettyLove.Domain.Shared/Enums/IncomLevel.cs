namespace XW.PrettyLove.Domain.Shared
{
    /// <summary>
    /// 收入等级
    /// </summary>
    public enum IncomLevel
    {
        /// <summary>
        /// 无收入
        /// </summary>
        None = 0,

        /// <summary>
        /// 3000元以下
        /// </summary>
        Below3000 = 1,

        /// <summary>
        /// 3000-5000元
        /// </summary>
        Level3kTo5k = 2,

        /// <summary>
        /// 5000-8000元
        /// </summary>
        Level5kTo8k = 3,

        /// <summary>
        /// 8000-12000元
        /// </summary>
        Level8kTo12k = 4,

        /// <summary>
        /// 12000-20000
        /// </summary>
        Level12kTo20k = 5,

        /// <summary>
        /// 20000元以上
        /// </summary>
        Above20000 = 6,

        /// <summary>
        /// 保密
        /// </summary>
        Confidential = 99
    }
}
