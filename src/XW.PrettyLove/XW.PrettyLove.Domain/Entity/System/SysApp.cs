using FreeSql.DataAnnotations;
using XW.PrettyLove.Domain.Shared;

namespace XW.PrettyLove.Domain.System
{
    /// <summary>
    /// 应用
    /// </summary>
    [Table(Name = "sys_app")]
    public class SysApp : SysAggregateRootEntity, ITenant
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        [Column(IsNullable = true)]
        public long? TenantId { get; set; }

        /// <summary>
        /// 应用类型
        /// </summary>
        [Column(IsNullable = false)]
        public AppType? AppType { get; set; } = Shared.AppType.Web;

        /// <summary>
        /// 应用名称
        /// </summary>
        [Column(IsNullable = false, StringLength = 20)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 应用编码
        /// </summary>
        [Column(IsNullable = false, StringLength = 20)]
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// 状态
        /// </summary>
        [Column(IsNullable = false)]
        public CommonStatus? Status { get; set; } = CommonStatus.Enabled;

        /// <summary>
        /// 序号
        /// </summary>
        [Column(IsNullable = false)]
        public int? Sort { get; set; } = 100;
    }
}
