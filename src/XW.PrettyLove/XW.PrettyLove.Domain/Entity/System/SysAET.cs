using FreeSql.DataAnnotations;
using XW.PrettyLove.Domain.Shared;

namespace XW.PrettyLove.Domain.System
{
    /// <summary>
    /// AET表
    /// </summary>
    [Table(Name = "sys_aet")]
    public class SysAET : SysAggregateRootEntity, ITenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        [Column(IsNullable = false)]
        public long? TenantId { get; set; }

        /// <summary>
        /// AET码
        /// </summary>
        [Column(IsNullable = false, StringLength = 20)]
        public string Code { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [Column(IsNullable = true)]
        public DateTime? ExpirationTime { get; set; }
    }
}
