using FreeSql.DataAnnotations;
using XW.PrettyLove.Domain.Shared;

namespace XW.PrettyLove.Domain.System
{
    /// <summary>
    /// 用户
    /// </summary>
    [Table(Name = "sys_user")]
    public class SysUser : SysAggregateRootEntity, ITenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        [Column(IsNullable = true)]
        public long? TenantId { get; set; }

        /// <summary>
        /// 部分ID
        /// </summary>
        [Column(IsNullable = true)]
        public long? DepartmentId { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        [Column(IsNullable = true)]
        public long? CustomerId { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [Column(IsNullable = false, StringLength = 20)]
        public string Account { get; set; } = string.Empty;

        /// <summary>
        /// 密码（默认MD5加密）
        /// </summary>
        [Column(IsNullable = false, StringLength = 50)]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 姓名
        /// </summary>
        [Column(IsNullable = false, StringLength = 20)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 生日
        /// </summary>
        [Column(IsNullable = true)]
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Column(IsNullable = false)]
        public Gender? Sex { get; set; } = Gender.None;

        /// 邮箱
        /// </summary>
        [Column(IsNullable = false, StringLength = 50)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 手机
        /// </summary>
        [Column(IsNullable = false, StringLength = 20)]
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// 用户类型
        /// </summary>
        [Column(IsNullable = false, StringLength = 20)]
        public UserType? UserType { get; set; } = Shared.UserType.Normal;

        /// <summary>
        /// 角色ID
        /// </summary>
        [Column(IsNullable = false)]
        public long? RoleId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Column(IsNullable = false)]
        public CommonStatus? Status { get; set; } = CommonStatus.Enabled;

        /// <summary>
        /// 不允许删除标记
        /// </summary>
        [Column(IsNullable = false)]
        public bool? AllowDelete { get; set; } = true;

        /// <summary>
        /// 不允许删除密码标记
        /// </summary>
        [Column(IsNullable = false)]
        public bool? AllowUpdate { get; set; } = true;
    }
}
