using FreeSql.DataAnnotations;
using PrettyLove.Domain.Shared;

namespace PrettyLove.Domain.System
{
    /// <summary>
    /// 系统模块 聚合根
    /// </summary>
    public abstract class SysAggregateRootEntity : Entity, ISoftDelete
    {
        /// <summary>
        /// 创建用户ID
        /// </summary>
        [Column(Name = nameof(CreatedUserId), IsNullable = false)]
        public long? CreatedUserId { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        [Column(Name = nameof(CreatedUserName), IsNullable = false, StringLength = 50)]
        public string CreatedUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(Name = nameof(CreatedTime), IsNullable = false)]
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// 更新用户ID
        /// </summary>
        [Column(Name = nameof(UpdatedUserId), IsNullable = false)]
        public long? UpdatedUserId { get; set; }

        /// <summary>
        /// 更新用户
        /// </summary>
        [Column(Name = nameof(UpdatedUserName), IsNullable = false, StringLength = 50)]
        public string UpdatedUserName { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column(Name = nameof(UpdatedTime), IsNullable = false)]
        public DateTime? UpdatedTime { get; set; }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        [Column(Name = nameof(IsDeleted), IsNullable = false)]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SysAggregateRootEntity()
        {
            this.DBEnum = DBEnum.System;
        }
    }
}
