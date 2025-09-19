namespace LS.ERP.Domain.Shared
{
    /// <summary>
    /// 实体状态
    /// </summary>
    public enum EntityStatus
    {
        /// <summary>
        /// 初始
        /// </summary>
        None = 1,

        /// <summary>
        /// 新增
        /// </summary>
        New = 2,

        /// <summary>
        /// 修改
        /// </summary>
        Updated = 3,

        /// <summary>
        /// 删除
        /// </summary>
        Deleted = 4
    }
}
