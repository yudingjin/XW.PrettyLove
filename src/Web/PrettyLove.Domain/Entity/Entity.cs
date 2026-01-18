using FreeSql.DataAnnotations;
using PrettyLove.Domain.Shared;
using Newtonsoft.Json;

namespace PrettyLove.Domain
{
    /// <summary>
    /// 普通实体抽象类
    /// </summary>
    public abstract class Entity : IEntity<long>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column(IsNullable = false, IsPrimary = true, CanUpdate = false, Name = nameof(Id))]
        public long? Id { get; set; }

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
        public DBEnum DBEnum { get; set; }
    }
}
