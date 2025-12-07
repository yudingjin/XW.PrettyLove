using FreeSql.DataAnnotations;
using XW.PrettyLove.Domain.Wechat;

namespace XW.PrettyLove.Domain
{
    /// <summary>
    /// 会员子女图片
    /// </summary>
    [Table(Name = "member_children_image")]
    public class MemberChildrenImage : WechatEntity
    {
        /// <summary>
        /// 子女ID
        /// </summary>
        [Column(IsNullable = false)]
        public long? ChildId { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [Column(IsNullable = false)]
        public string ImageUrl { get; set; }
    }
}
