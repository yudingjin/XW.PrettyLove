using FreeSql.DataAnnotations;
using PrettyLove.Domain.Shared;
using PrettyLove.Domain.Wechat;

namespace PrettyLove.Domain
{
    /// <summary>
    /// 会员子女择偶条件
    /// </summary>
    [Table(Name = "member_children_condition")]
    public class MemberChildrenCondition : WechatEntity
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        [Column(IsNullable = false)]
        public long? MemberId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Column(IsNullable = false)]
        public Gender? Gender { get; set; }

        /// <summary>
        /// 学历最低要求
        /// </summary>
        [Column(IsNullable = false)]
        public EducationRequirement? MinEducation { get; set; }

        /// <summary>
        /// 最小年龄
        /// </summary>
        [Column(IsNullable = false)]
        public int? MinAge { get; set; }

        /// <summary>
        /// 最大年龄
        /// </summary>
        [Column(IsNullable = false)]
        public int? MaxAge { get; set; }

        /// <summary>
        /// 最小身高
        /// </summary>
        [Column(IsNullable = false)]
        public int? MinHeight { get; set; }

        /// <summary>
        /// 最大身高
        /// </summary>
        [Column(IsNullable = false)]
        public int? MaxHeight { get; set; }
    }
}
