using FreeSql.DataAnnotations;
using XW.PrettyLove.Domain.Shared;

namespace XW.PrettyLove.Domain.System
{
    /// <summary>
    /// 部门
    /// </summary>
    [Table(Name = "sys_department")]
    public class SysDepartment : SysAggregateRootEntity, ITenant, ITreeNode<SysDepartment, long>
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        [Column(IsNullable = false)]
        public long? TenantId { get; set; }

        /// <summary>
        /// 上级部门ID
        /// </summary>
        [Column(IsNullable = true)]
        public long? ParentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [Column(IsNullable = false, StringLength = 20)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 部分负责人
        /// </summary>
        [Column(IsNullable = false, StringLength = 20)]
        public string Contact { get; set; } = string.Empty;

        /// <summary>
        /// 联系电话
        /// </summary>
        [Column(IsNullable = false, StringLength = 20)]
        public string Telephone { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        [Column(IsNullable = false)]
        public int? Sort { get; set; } = 100;

        /// <summary>
        /// 备注
        /// </summary>
        [Column(IsNullable = false, StringLength = 200)]
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 子级
        /// </summary>
        [Column(IsIgnore = true)]
        public List<SysDepartment> Children { get; set; }

        /// <summary>
        /// 是否有子级
        /// </summary>
        [Column(IsIgnore = true)]
        public bool HasChildren => this.Children?.Count > 0;
    }
}
