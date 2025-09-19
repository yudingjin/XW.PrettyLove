using FreeSql.DataAnnotations;
using XW.PrettyLove.Domain.Shared;

namespace XW.PrettyLove.Domain.System
{
    /// <summary>
    /// 权限
    /// </summary>
    [Table(Name = "sys_menu")]
    public class SysMenu : SysAggregateRootEntity, ITenant, ITreeNode<SysMenu, long>
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        [Column(IsNullable = false)]
        public long? TenantId { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        [Column(IsNullable = true)]
        public long? ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Column(IsNullable = false, StringLength = 20)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 权限标识
        /// </summary>
        [Column(IsNullable = false, StringLength = 50)]
        public string Permission { get; set; } = string.Empty;

        /// <summary>
        /// 权限类型
        /// </summary>
        [Column(IsNullable = false)]
        public MenuType? MenuType { get; set; } = Shared.MenuType.Menu;

        /// <summary>
        /// 图标
        /// </summary>
        [Column(IsNullable = false, StringLength = 50)]
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        /// 路由地址
        /// </summary>
        [Column(IsNullable = false, StringLength = 100)]
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        [Column(IsNullable = false)]
        public int? Sort { get; set; } = 100;

        /// <summary>
        /// 状态（字典 0正常 1停用）
        /// </summary>
        [Column(IsNullable = false)]
        public CommonStatus? Status { get; set; } = CommonStatus.Enabled;

        /// <summary>
        /// 子级
        /// </summary>
        [Column(IsIgnore = true)]
        public List<SysMenu> Children { get; set; } = new List<SysMenu>();

        /// <summary>
        /// 是否有子级
        /// </summary>
        [Column(IsIgnore = true)]
        public bool HasChildren => this.Children?.Count > 0;
    }
}
