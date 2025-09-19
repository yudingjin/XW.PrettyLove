using FreeSql.DataAnnotations;
using XW.PrettyLove.Domain.Shared;

namespace XW.PrettyLove.Domain.System
{
    /// <summary>
    /// 角色
    /// </summary>
    [Table(Name = "sys_role")]
    public class SysRole : SysAggregateRootEntity, ITenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        [Column(IsNullable = false)]
        public long? TenantId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Column(IsNullable = false, StringLength = 50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 序号
        /// </summary>
        [Column(IsNullable = false)]
        public int? Sort { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Column(IsNullable = false)]
        public CommonStatus? Status { get; set; } = CommonStatus.Enabled;

        /// <summary>
        /// 备注
        /// </summary>
        [Column(IsNullable = false, StringLength = 200)]
        public string Remark { get; set; } = string.Empty;
    }
}
