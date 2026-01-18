using FreeSql.DataAnnotations;
using XW.PrettyLove.Domain.Wechat;

namespace XW.PrettyLove.Domain
{
    /// <summary>
    /// 会员建议
    /// </summary>
    [Table(Name = "member_suggestion")]
    public class MemberSuggestion : WechatEntity
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        [Column(IsNullable = false)]
        public long? MemberId { get; set; }

        /// <summary>
        /// 建议内容
        /// </summary>
        [Column(IsNullable = false, StringLength = 200)]
        public string Content { get; set; }

        /// <summary>
        /// 状态 0-未处理 1-已处理
        /// </summary>
        [Column(IsNullable = false)]
        public int? Status { get; set; } = 0;
    }
}
