namespace LS.ERP.Domain.Shared
{
    /// <summary>
    /// 审计动作
    /// </summary>
    public enum AuditAction
    {
        /// <summary>
        /// 新增
        /// </summary>
        Add,

        /// <summary>
        /// 修改
        /// </summary>
        Modify,

        /// <summary>
        /// 删除
        /// </summary>
        Delete,

        /// <summary>
        /// 其他
        /// </summary>
        Other,

        /// <summary>
        /// 质控
        /// </summary>
        Qa,

        /// <summary>
        /// 保存报告
        /// </summary>
        Save,

        /// <summary>
        /// 审核报告
        /// </summary>
        Audit,

        /// <summary>
        /// 撤销报告
        /// </summary>
        Revoke,

        /// <summary>
        /// 修改患者信息
        /// </summary>
        ModifyPatient
    }
}
