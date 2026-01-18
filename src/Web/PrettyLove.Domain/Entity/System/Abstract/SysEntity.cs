using FreeSql.DataAnnotations;

namespace PrettyLove.Domain.System
{
    /// <summary>
    /// 系统模块 实体基类
    /// </summary>
    public abstract class SysEntity : Entity
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
        /// 构造函数
        /// </summary>
        public SysEntity()
        {
            this.DBEnum = Shared.DBEnum.System;
        }
    }
}
