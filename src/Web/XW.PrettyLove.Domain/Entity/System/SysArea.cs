using FreeSql.DataAnnotations;
using XW.PrettyLove.Domain.Shared;
using Newtonsoft.Json;

namespace XW.PrettyLove.Domain.System
{
    [Table(Name = "sys_area")]
    public class SysArea : IEntity<long>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column(IsNullable = false, IsPrimary = true, CanUpdate = false, Name = nameof(Id))]
        public long? Id { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        [Column(IsNullable = false, StringLength = 10)]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Column(IsNullable = false, StringLength = 30)]
        public string Name { get; set; }

        /// <summary>
        /// 父级Code
        /// </summary>
        [Column(IsNullable = false, StringLength = 10)]
        public string ParentCode { get; set; }

        /// <summary>
        /// 实体状态 忽略
        /// </summary>
        [Column(IsIgnore = true)]
        [JsonIgnore]
        public EntityStatus? EntityStatus { get; set; }

        /// <summary>
        /// 所属数据库
        /// </summary>
        [Column(IsIgnore = true)]
        [JsonIgnore]
        public DBEnum DBEnum { get; set; } = DBEnum.System;
    }
}
