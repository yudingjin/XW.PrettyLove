using FreeSql.DataAnnotations;
using XW.PrettyLove.Domain.Shared;
using XW.PrettyLove.Domain.Wechat;

namespace XW.PrettyLove.Domain
{
    /// <summary>
    /// 会员子女择偶条件
    /// </summary>
    [Table(Name = "member_children_condition")]
    public class MemberChildrenCondition : WechatEntity
    {
        /// <summary>
        /// 子女ID
        /// </summary>
        [Column(IsNullable = false)]
        public int? ChildId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Column(IsNullable = false)]
        public Gender? Gender { get; set; }

        /// <summary>
        /// 学历最低要求
        /// </summary>
        [Column(IsNullable = false)]
        public Education? MinEducation { get; set; }

        /// <summary>
        /// 学历最高要求
        /// </summary>
        [Column(IsNullable = false)]
        public Education? MaxEducation { get; set; }

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
