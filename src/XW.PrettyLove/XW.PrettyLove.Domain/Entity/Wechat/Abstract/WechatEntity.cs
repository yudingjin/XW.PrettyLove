using FreeSql.DataAnnotations;

namespace XW.PrettyLove.Domain.Wechat
{
    public abstract class WechatEntity : Entity
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(Name = nameof(CreatedTime), IsNullable = false)]
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public WechatEntity()
        {
            this.DBEnum = Shared.DBEnum.System;
        }
    }
}
