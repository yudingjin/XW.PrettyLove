using FreeSql.DataAnnotations;
using XW.PrettyLove.Domain.Shared;

namespace XW.PrettyLove.Domain.System
{
    /// <summary>
    /// 租户
    /// </summary>
    [Table(Name = "sys_tenant")]
    public class SysTenant : SysAggregateRootEntity
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        [Column(IsNullable = false, StringLength = 20)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 编码
        /// </summary>
        [Column(IsNullable = false, StringLength = 20)]
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// 租户地址
        /// </summary>
        [Column(IsNullable = false, StringLength = 200)]
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// 租户电话
        /// </summary>
        [Column(IsNullable = false, StringLength = 20)]
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// 租户状态
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
