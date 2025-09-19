using FreeSql.DataAnnotations;

namespace XW.PrettyLove.Domain.System
{
    /// <summary>
    /// 角色权限关系
    /// </summary>
    [Table(Name = "sys_role_menu")]
    public class SysRoleMenu : SysEntity
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Column(IsNullable = false)]
        public long? RoleId { get; set; }

        /// <summary>
        /// 权限Id
        /// </summary>
        [Column(IsNullable = false)]
        public long? MenuId { get; set; }
    }
}
